using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace XULIA
{
    public class CorreoOffice
    {
        SalidaGPT fSalidaGPT;

        #region funciones_correo_outlook
        // EDIT: funciones de correo
        public void LeerCalendario(DateTime Inicio, DateTime Fin)
        {
            // Crear una instancia de la aplicación de Outlook
            Outlook.Application outlookApp = new Outlook.Application();

            // Obtener el namespace MAPI
            Outlook.NameSpace mapiNamespace = outlookApp.GetNamespace("MAPI");

            // Obtener la carpeta del calendario
            Outlook.MAPIFolder calendarFolder = mapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderCalendar);

            // Establecer la fecha de inicio y fin para buscar las citas
            DateTime startDate = new DateTime(2023, 4, 7);
            DateTime endDate = startDate.AddDays(1);

            // Crear el filtro para buscar las citas
            string filter = $"[Start] >= '{startDate:yyyy/MM/dd}' AND [End] < '{endDate:yyyy/MM/dd}'";

            // Obtener las citas usando el filtro
            Outlook.Items calendarItems = calendarFolder.Items;
            calendarItems.IncludeRecurrences = true;
            calendarItems.Sort("[Start]", Type.Missing);
            Outlook.Items restrictedItems = calendarItems.Restrict(filter);

            // Mostrar las citas
            foreach (Outlook.AppointmentItem item in restrictedItems)
            {
                Console.WriteLine($"Asunto: {item.Subject}, Inicio: {item.Start}, Fin: {item.End}");
            }

            // Liberar los objetos COM
            if (calendarFolder != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(calendarFolder);
            if (mapiNamespace != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(mapiNamespace);
            if (outlookApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(outlookApp);
        }

        public void AbrirEntradaCalendario(string Asunto = "", DateTime Fecha = default(DateTime))
        {
            // Crear una instancia de la aplicación de Outlook
            Outlook.Application outlookApp = new Outlook.Application();

            // Obtener el namespace MAPI
            Outlook.NameSpace mapiNamespace = outlookApp.GetNamespace("MAPI");

            // Obtener la carpeta del calendario
            Outlook.MAPIFolder calendarFolder = mapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderCalendar);

            // Buscar una cita específica por su Asunto
            string subjectToFind = "Reunión importante";
            Outlook.Items calendarItems = calendarFolder.Items;
            calendarItems.IncludeRecurrences = true;
            calendarItems.Sort("[Start]", Type.Missing);

            Outlook.AppointmentItem foundAppointment = null;
            foreach (Outlook.AppointmentItem item in calendarItems)
            {
                if (item.Subject == subjectToFind)
                {
                    foundAppointment = item;
                    break;
                }
            }

            if (foundAppointment != null)
            {
                // Abrir la cita en Outlook
                foundAppointment.Display(true);
                Console.WriteLine($"Abriendo la cita: {foundAppointment.Subject}");
            }
            else
            {
                Console.WriteLine($"No se encontró ninguna cita con el asunto: {subjectToFind}");
            }

            // Liberar los objetos COM
            if (calendarFolder != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(calendarFolder);
            if (mapiNamespace != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(mapiNamespace);
            if (outlookApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(outlookApp);
        }

        public Boolean EnviarCorreoOutlook(string mailSubject, string mailContent, List<string> Anexos, string mailDirection)
        {
            try
            {
                var oApp = new Microsoft.Office.Interop.Outlook.Application();

                Microsoft.Office.Interop.Outlook.NameSpace ns = oApp.GetNamespace("MAPI");
                var f = ns.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);

                System.Threading.Thread.Sleep(1000);

                var mailItem = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                mailItem.Subject = mailSubject;
                mailItem.HTMLBody = mailContent;
                mailItem.To = mailDirection;
                foreach (string s in Anexos)
                    mailItem.Attachments.Add(s);
                //mailItem.Send();
                mailItem.Display(false);

            }
            catch (System.Exception ex)
            {
                return false;
            }
            finally
            {
            }
            return true;
        }
        public List<string> BuscarContactosOutLook(string findLastName)
        {
            List<string> contactos = new List<string>();
            Microsoft.Office.Interop.Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.NameSpace outlookNS = null;

            outlookNS = outlookApp.GetNamespace("MAPI");

            Microsoft.Office.Interop.Outlook.MAPIFolder folderContacts = outlookNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderContacts);
            Microsoft.Office.Interop.Outlook.Items searchFolder = folderContacts.Items;
            foreach (Object item in searchFolder)
            {
                try
                {
                    Microsoft.Office.Interop.Outlook.ContactItem foundContact = (Microsoft.Office.Interop.Outlook.ContactItem)item;
                    string Nombre = foundContact.FirstName + " " + foundContact.LastName;
                    if (Nombre.ToUpper().IndexOf(findLastName.ToUpper()) >= 0)
                        contactos.Add(Nombre + "<" + foundContact.Email1Address + ">");
                }
                catch (Exception ex)
                {
                }
            }
            return contactos;
        }



        public string LeerBandejaEntada(int DiasAntiguedad = 0, string emisor = "", bool Contenido = false)
        {
            bool inicio = true;
            string Mensajes = "";
            string filter = "";
            fSalidaGPT = new SalidaGPT();
            fSalidaGPT.AddColumn("numero", "número", 50);
            fSalidaGPT.AddColumn("EntryID", "EntryID", 1);
            fSalidaGPT.AddColumn("Fecha", "Fecha", 90);
            fSalidaGPT.AddColumn("Emisor", "Emisor", 250);
            fSalidaGPT.AddColumn("Asunto", "Asunto", 1000);

            // Inicializa la aplicación de Outlook
            Outlook.Application outlookApp = new Outlook.Application();
            Outlook.NameSpace outlookNamespace = outlookApp.GetNamespace("MAPI");
            Outlook.MAPIFolder inboxFolder = outlookNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            Outlook.Items inboxItems = inboxFolder.Items;

            DateTime startDate = DateTime.Now.AddDays(-15);
            if (DiasAntiguedad > 0) startDate = DateTime.Now.AddDays(-DiasAntiguedad);
            if (emisor != "")
                filter = $"[ReceivedTime] >= '{startDate:g}' AND [SenderEmailAddress] ='{emisor}'";
            else
                filter = $"[ReceivedTime] >= '{startDate:g}'";

            Outlook.Items filteredItems = inboxItems.Restrict(filter);

            Mensajes += "{\"output\":\"";
            int num_mensaje = 1;
            string Asunto;
            string Emisor;
            foreach (object item in filteredItems)
            {
                if (item is Outlook.MailItem)
                {
                    if (!inicio)
                    {
                        Mensajes += "," + Environment.NewLine;
                        inicio = false;
                    }
                    Mensajes += "{\"Mensaje de correo\":{" + Environment.NewLine;
                    Outlook.MailItem mail = (Outlook.MailItem)item;
                    Mensajes += "\"numero\":" + num_mensaje + "," + Environment.NewLine;
                    Mensajes += "\"s_EntryID\":\"" + mail.EntryID + "\"," + Environment.NewLine;
                    Mensajes += "\"Fecha\":\"" + mail.ReceivedTime + "\"";
                    Emisor = mail.SenderEmailAddress;
                    int pos;
                    while ((pos = Emisor.IndexOf("/CN=")) >= 0)
                        Emisor = Emisor.Substring(pos + 4);
                    Mensajes += "\"Emisor\":\"" + Emisor + "\"," + Environment.NewLine;
                    Asunto = mail.Subject;
                    if (Asunto == null) Asunto = "";
                    Mensajes += "\"Asunto\":\"" + Asunto.Replace("\"", "'") + "\"";
                    if (Contenido)
                        Mensajes += "," + Environment.NewLine + "\"Cuerpo\",\"" + mail.Body.Replace("\"", "'") + "\"" + Environment.NewLine;
                    else
                        Mensajes += Environment.NewLine;
                    Mensajes += "}}" + Environment.NewLine;

                    fSalidaGPT.AddRows(num_mensaje, mail.EntryID, mail.ReceivedTime, Emisor, Asunto);

                    Console.WriteLine(Mensajes);
                    num_mensaje++;
                }
            }
            Mensajes += Environment.NewLine + "\"}" + Environment.NewLine;
            if (num_mensaje > 1) fSalidaGPT.MostrarSalidaGPT(this);
            Console.WriteLine(Mensajes);
            return Mensajes;
        }
        public void AbrirMensajeNumero(int numero)
        {
        }
        public void AbrirMensajeCorreo(string messageId)
        {
            // Reemplaza con el identificador del mensaje que deseas abrir
            Outlook.Application outlookApp = new Outlook.Application();
            Outlook.NameSpace outlookNamespace = outlookApp.GetNamespace("MAPI");
            Outlook.MAPIFolder inboxFolder = outlookNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            Outlook.Items mailItems = inboxFolder.Items;

            Outlook.MailItem targetMessage = null;

            foreach (object item in mailItems)
            {
                if (item is Outlook.MailItem mailItem && mailItem.EntryID == messageId)
                {
                    targetMessage = mailItem;
                    break;
                }
            }

            if (targetMessage != null)
            {
                // Abre el mensaje en una nueva ventana
                targetMessage.Display();
                Console.WriteLine("Mensaje abierto exitosamente.");
            }
            else
            {
                Console.WriteLine("No se encontró el mensaje con el identificador proporcionado.");
            }
        }
        public void FiltrarCorreosAsunto(String asunto)
        {
            string subjectFilter = "entrega"; // Reemplaza con el asunto que deseas filtrar

            Outlook.Application outlookApp = new Outlook.Application();
            Outlook.NameSpace outlookNamespace = outlookApp.GetNamespace("MAPI");
            Outlook.MAPIFolder inboxFolder = outlookNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            Outlook.Items mailItems = inboxFolder.Items;

            // Aplicar filtro por asunto
            string filtro = $"@SQL=\"urn:schemas:httpmail:subject\" LIKE '%{subjectFilter}%'";
            //mailItems = mailItems.Restrict($"[Subject] = '{subjectFilter}'");
            mailItems = mailItems.Restrict(filtro);

            foreach (Outlook.MailItem item in mailItems)
            {
                item.Display();
            }
        }
        public void EnviarCorreoIMAP()
        {
            using (var client = new ImapClient())
            {
                // Conectar al servidor IMAP
                client.Connect("correoweb.xunta.es", 443, true);

                // Autenticar usando tus credenciales
                client.Authenticate("XUNTA\alosgon", "DaniXulia1081.");

                // Seleccionar la bandeja de entrada
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                // Buscar y listar los mensajes
                foreach (var uid in inbox.Search(SearchQuery.All))
                {
                    var message = inbox.GetMessage(uid);
                    Console.WriteLine($"Asunto: {message.Subject}");
                    Console.WriteLine($"De: {message.From}");
                    Console.WriteLine($"Fecha: {message.Date}");
                    Console.WriteLine();
                }

                // Desconectar del servidor IMAP
                client.Disconnect(true);
            }
        }

        #endregion funciones_correo_outlook    
    }
    }
