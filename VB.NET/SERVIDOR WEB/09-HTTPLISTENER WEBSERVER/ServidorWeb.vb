' https://www.youtube.com/watch?v=qXMJSeZEq_8
' Reservar dominio para no necesitar ser administrador: netsh http add urlacl url=http://*:8080/ user=Xunta
' Para acceder desde un usuario normal netsh http add urlacl url=http://localhost:8080/ user="Antonio Losada"
' Comprobar que no hay mas reservas con similares rutas, p.e. la anterior y http://+:8080, ya que da un error 503 al acceder

Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Configuration

Public Class ServidorWeb
    Public Texto As String = ""

    Dim ColaLOG As Queue = New Queue()
    Dim SERVIDOR As HttpListener
    Dim HEBRA As Threading.Thread
    Dim RESPUESTA As String = "CONECTADO"
    Dim Timer As Long
    Dim trayMenu As ContextMenu
    Dim trayIcon As NotifyIcon
    Dim ServidorActivo As Boolean = False

    Dim varIDIOMA As String = "ESPANOL"
    Dim varCODIDIOMA As String = "es-ES"
    Dim varAUTO As String = "S"
    Dim RESPCOMANDO As String = ""

    Public CONFIG_INIT As String = "IDIOMA = '" + varIDIOMA + "';" + Environment.NewLine + "CODIGO_IDIOMA = '" + varCODIDIOMA + "';" + Environment.NewLine + "ARRANQUE_AUTOMATICO = '" + varAUTO + "';" + Environment.NewLine + "document.title = 'RECVOZ.GOOGLE.' + IDIOMA + '.ACTIVO';"

    Public Sub ConfigVar(idioma As String, codidioma As String, auto As String)
        varIDIOMA = idioma
        varCODIDIOMA = codidioma
        varAUTO = auto
        CONFIG_INIT = "IDIOMA = '" + varIDIOMA + "';" + Environment.NewLine + "CODIGO_IDIOMA = '" + varCODIDIOMA + "';" + Environment.NewLine + "ARRANQUE_AUTOMATICO = '" + varAUTO + "';" + Environment.NewLine + "document.title = 'RECVOZ.GOOGLE.' + IDIOMA + '.ACTIVO';"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Iniciar()

    End Sub
    Public Sub Parar()
        ServidorActivo = False
    End Sub
    Private Function GetEntradaConfig(Key As String, Optional DefaultValue As String = "") As String
        Dim s As String = ConfigurationManager.AppSettings("URL")
        If (String.IsNullOrEmpty(s)) Then
            GetEntradaConfig = DefaultValue
        Else
            GetEntradaConfig = s
        End If
    End Function
    Private Sub Iniciar()
        Try

            SERVIDOR = New HttpListener
            If chkPrefijo.Checked Then
                SERVIDOR.Prefixes.Add(TextBoxSERVER.Text) 'Las aplicaciones que no se ejecutan con privilegios de administrador necesitan reserva de dominio
            Else
                SERVIDOR.Prefixes.Add("http://*:8080/") 'Para este modo hay que ser administrador
            End If
            ' Reservar dominio para no necesitar ser administrador: netsh http add urlacl url=http://*:8080/ user=Xunta
            ' Para acceder desde un usuario normal netsh http add urlacl url=http://localhost:8080/ user="Antonio Losada"
            ' Comprobar que no hay mas reservas con similares rutas, p.e. la anterior y http://+:8080, ya que da un error 503 al acceder
            ServidorActivo = True
            SERVIDOR.Start()
            HEBRA = New Threading.Thread(AddressOf RESPONDE)
            HEBRA.Start()
            Button1.Enabled = False

            'Cargamos el fichero de respuesta
            Dim ruta As String = Application.StartupPath + "\voz.htm"
            RESPUESTA = File.ReadAllText(ruta)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub EnviarComando(comando As String)
        RESPCOMANDO = comando
        TextBoxTEXTO.Text = comando
    End Sub
    Public Sub RESPONDE()
        While ServidorActivo
            Try
                Dim CONTEXTO As HttpListenerContext = SERVIDOR.GetContext
                Dim MISTREAM As Stream = CONTEXTO.Response.OutputStream

                Try
                    If Not CONTEXTO.Request.QueryString.Item("texto") Is Nothing Then
                        Texto = CONTEXTO.Request.QueryString.Item("texto").ToString()
                    End If
                    If chkLOG.Checked Then ColaLOG.Enqueue(Texto)
                    TextoRecuperado(Texto)
                Catch ex As Exception
                End Try

                Using (MISTREAM)
                    Using ESCRITOR As StreamWriter = New StreamWriter(MISTREAM)
                        'Para la página principal devolvemos el script de reconocimiento continuo
                        If CONTEXTO.Request.Url.LocalPath.ToString().IndexOf("voz.htm") > 0 Then
                            RESPUESTA = RESPUESTA.Replace("//___CONFIG___", CONFIG_INIT)

                            CONTEXTO.Response.ContentLength64 = Encoding.UTF8.GetByteCount(RESPUESTA)
                            CONTEXTO.Response.StatusCode = HttpStatusCode.OK
                            ESCRITOR.Write(RESPUESTA)
                        Else
                            CONTEXTO.Response.ContentLength64 = Encoding.UTF8.GetByteCount(RESPCOMANDO)
                            CONTEXTO.Response.StatusCode = HttpStatusCode.OK
                            ESCRITOR.Write(RESPCOMANDO)
                        End If
                    End Using
                End Using
                Application.DoEvents()

            Catch ex As Exception
                If ServidorActivo Then
                    MsgBox(ex.Message)
                End If
            End Try

        End While
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If (ServidorActivo) Then
                ServidorActivo = False
                HEBRA.Join()
                SERVIDOR.Stop()
                SERVIDOR.Close()
                Application.Exit()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        RESPUESTA = TextBoxTEXTO.Text
        RESPCOMANDO = TextBoxTEXTO.Text
        TextBoxTEXTO.Text = ""
    End Sub

    Public Sub CerrarServidor()
        Try
            ServidorActivo = False
            trayIcon.Dispose()
            HEBRA.Abort()
            SERVIDOR.Stop()
            SERVIDOR.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkLOG_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub chkVR_Cabeza_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub tmrLOG_Tick(sender As Object, e As EventArgs) Handles tmrLOG.Tick
        If chkLOG.Checked Then
            If (ColaLOG.Count > 0) Then
                tbLOG.Text = ColaLOG.Dequeue() + Environment.NewLine + tbLOG.Text
                Texto = ""
            End If
        End If
    End Sub

    Private Sub OnActivate(ByVal sender As Object, ByVal e As EventArgs)
        Me.Show()
    End Sub

    Private Sub OnDisable(ByVal sender As Object, ByVal e As EventArgs)
        Me.Hide()
    End Sub
    Private Sub ServidorWeb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MessageBox.Show(GetEntradaConfig("URL"))

    End Sub

    Public Function RecuperarSR() As String
        RecuperarSR = "HOLA"
    End Function

    Public Delegate Sub delegadoTextoRecuperadoGoogleChrome(texto As String)

    Dim TextoRecuperado As delegadoTextoRecuperadoGoogleChrome

    Public Sub ArrancarSR(delTextoRecuperado As delegadoTextoRecuperadoGoogleChrome)

        TextoRecuperado = delTextoRecuperado

        trayMenu = New ContextMenu()
        trayMenu.MenuItems.Add("Ver", AddressOf OnActivate)
        trayMenu.MenuItems.Add("Ocultar", AddressOf OnDisable)

        trayIcon = New NotifyIcon()
        trayIcon.Text = "XULIA_SR"
        trayIcon.Icon = Me.Icon
        ' Add menu to tray icon And show it.
        trayIcon.ContextMenu = trayMenu
        trayIcon.Visible = True

        Me.Hide()
        Iniciar()
    End Sub

End Class
