using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using OllamaSharp.Models.Chat;

namespace OllamaSharp;

/// <summary>
/// A chat helper that handles the chat logic internally and
/// automatically extends the message history.
/// </summary>
public class Chat
{
	private List<Message> _messages = new();

	public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

	/// <summary>
	/// Gets the Ollama API client
	/// </summary>
	public IOllamaApiClient Client { get; }

	/// <summary>
	/// Gets or sets the AI model to chat with
	/// </summary>
	public string Model { get; set; }

	/// <summary>
	/// Creates a new chat instance
	/// </summary>
	/// <param name="client">The Ollama client to use for the chat</param>
	/// <param name="systemPrompt">An optional system prompt to define the behavior of the chat assistant</param>
	/// <exception cref="ArgumentNullException"></exception>
	public Chat(IOllamaApiClient client, string systemPrompt = "")
	{
		Client = client ?? throw new ArgumentNullException(nameof(client));
		Model = Client.SelectedModel;

		if (!string.IsNullOrEmpty(systemPrompt))
			_messages.Add(new Message(ChatRole.System, systemPrompt));
	}

	/// <summary>
	/// Sets the message history
	/// </summary>
	/// <param name="messages">The message history</param>
	public void SetMessages(List<Message> messages)
	{
		_messages = messages;
	}

	/// <summary>
	/// Sends a message to the currently selected model and streams its response
	/// </summary>
	/// <param name="message">The message to send</param>
	/// <param name="cancellationToken">The token to cancel the operation with</param>
	public IAsyncEnumerable<string> Send(string message, CancellationToken cancellationToken = default)
		=> Send(message, tools: null, imagesAsBase64: null, cancellationToken);

	/// <summary>
	/// Sends a message to the currently selected model and streams its response
	/// </summary>
	/// <param name="message">The message to send</param>
	/// <param name="tools">Tools that the model can make use of, see https://ollama.com/blog/tool-support. By using tools, response streaming is automatically turned off</param>
	/// <param name="imagesAsBase64">Base64 encoded images to send to the model</param>
	/// <param name="cancellationToken">The token to cancel the operation with</param>
	public IAsyncEnumerable<string> Send(string message, IEnumerable<Tool>? tools, IEnumerable<string>? imagesAsBase64 = default, CancellationToken cancellationToken = default)
		=> SendAs(ChatRole.User, message, tools, imagesAsBase64, cancellationToken);

	/// <summary>
	/// Sends a message in a given role to the currently selected model and streams its response
	/// </summary>
	/// <param name="role">The role in which the message should be sent</param>
	/// <param name="message">The message to send</param>
	/// <param name="cancellationToken">The token to cancel the operation with</param>
	public IAsyncEnumerable<string> SendAs(ChatRole role, string message, CancellationToken cancellationToken = default)
		=> SendAs(role, message, tools: null, imagesAsBase64: null, cancellationToken);

	/// <summary>
	/// Sends a message in a given role to the currently selected model and streams its response
	/// </summary>
	/// <param name="role">The role in which the message should be sent</param>
	/// <param name="message">The message to send</param>
	/// <param name="tools">Tools that the model can make use of, see https://ollama.com/blog/tool-support. By using tools, response streaming is automatically turned off</param>
	/// <param name="imagesAsBase64">Base64 encoded images to send to the model</param>
	/// <param name="cancellationToken">The token to cancel the operation with</param>
	public async Task<List<string>> SendAsEnumerable(string message, IEnumerable<Tool>? tools, IEnumerable<string>? imagesAsBase64 = default, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		List<string> lista = new List<string>();

		IAsyncEnumerable<string> asyncEnumerable = this.SendAs(ChatRole.User, message, tools, imagesAsBase64, cancellationToken);

        await foreach (var item in asyncEnumerable)
        {
            lista.Add(item);
        }
        return lista;
    }
    public delegate Task<string> RespuestaGPT(string texto);
    
	// TODO: SendAsEnumerableDelegado
    public async Task<List<string>> SendAsEnumerableDelegado(string message, RespuestaGPT Respuesta, IEnumerable<Tool>? tools, IEnumerable<string>? imagesAsBase64 = default, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
		string salida = "";
		string separador = ",.:;";
        List<string> lista = new List<string>();

        IAsyncEnumerable<string> asyncEnumerable = this.SendAs(ChatRole.User, message, tools, imagesAsBase64, cancellationToken);

        await foreach (var item in asyncEnumerable)
        {
			salida += item;
			int p = 0;
			foreach (char c in separador.ToCharArray())
			{
				p = salida.IndexOf(c);
				if (p >= 0)
				{ 
					string texto = salida.Substring(0, p);
                    salida = salida.Substring(p + 1);
                    await Respuesta(texto);
					break;
                }
            }
        }
        if (salida.Length > 0) await Respuesta(salida);
        return lista;
    }

    public async IAsyncEnumerable<string> SendAs(ChatRole role, string message, IEnumerable<Tool>? tools, IEnumerable<string>? imagesAsBase64 = default, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		_messages.Add(new Message(role, message, imagesAsBase64?.ToArray()));

		var hasTools = tools?.Any() ?? false;

		var request = new ChatRequest
		{
			Messages = Messages,
			Model = Model,
			Stream = !hasTools, // cannot stream if tools should be used
			Tools = tools
		};

		var messageBuilder = new MessageBuilder();

#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
		await foreach (var answer in Client.Chat(request, cancellationToken))
		{
			if (answer is not null)
			{
				messageBuilder.Append(answer);
				yield return answer.Message?.Content ?? string.Empty;
			}
		}
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions

		if (messageBuilder.HasValue)
			_messages.Add(messageBuilder.ToMessage());
	}
}
