Option Explicit On
Imports System.Speech.Recognition
Imports System.Speech.Recognition.SrgsGrammar
Imports System.Speech
Imports System.Data.OleDb
Imports System.Runtime.InteropServices
Imports REcVoz






Public Class Form1

    Private Declare Function GetForegroundWindow Lib "user32" () As Long
    Private Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Long, ByVal lpString As String, ByVal cch As Long) As Long

    <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Sub mouse_event(dwFlags As Integer, dx As Integer, dy As Integer, cButtons As Integer, dwExtraInfo As Integer)
    End Sub
    <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function SetForegroundWindow(hWnd As IntPtr) As Boolean
    End Function
    Declare Function ShowWindow Lib "user32" (ByVal hWnd As System.IntPtr, ByVal nCmdShow As Integer) As Boolean

    Dim Handle2 As Long

    Const MOUSEEVENTF_LEFTDOWN As Integer = &H2
    Const MOUSEEVENTF_LEFTUP As Integer = &H4
    Const MOUSEEVENTF_RIGHTDOWN As Integer = &H8
    Const MOUSEEVENTF_RIGHTUP As Integer = &H10

    Dim PUNTOX As Integer
    Dim PUNTOY As Integer
    Dim PALABRA As String
    Dim VELOCIDAD As Integer = 10

    Dim rv As Program = New Program

    ''VARIABLE PUBLICA OBJETO SPEECHRECOGNIZER
    Dim recognizer As SpeechRecognizer
    ''VARIABLE PARA REPRODUCIR TEXTO
    Dim voz As New Speech.Synthesis.SpeechSynthesizer
    Dim dbConnection As OleDbConnection
    Dim texto1 As String
    Dim texto2 As String
    Dim texto3 As String
    Dim texto4 As String
    Dim gNombre As String
    Dim gsCad As String

    Dim handle1 As IntPtr
    '

    Public Shared Sub TextoReconocido()

    End Sub

    Public Sub COPIARTEXTO()
        'handle1 = FindWindow("Chrome_WidgetWin_1", Nothing)

        'If (handle1 = IntPtr.Zero) Then
        '    MessageBox.Show("Ventana no encontrada.")
        '    Exit Sub
        'End If

        'SetForegroundWindow(handle1)
        '"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" 

        Shell("C:\Program Files (x86)\Google\Chrome\Application\chrome.exe http://www.xunta.es/dxfp/RCP/api.html", )

        System.Threading.Thread.Sleep(3000)
        AppActivate("La API de Reconocimiento")
        SendKeys.SendWait("Hola, mundo")
        SendKeys.SendWait("AA")
        SendKeys.SendWait("^a")
        SendKeys.SendWait("^x")



    End Sub
    Public Sub CLICKIZDO()
        mouse_event(MOUSEEVENTF_LEFTDOWN, PUNTOX, PUNTOY, 0, 0)
        mouse_event(MOUSEEVENTF_LEFTUP, PUNTOX, PUNTOY, 0, 0)
    End Sub
    Public Sub CLICKDCHO()
        mouse_event(MOUSEEVENTF_RIGHTDOWN, PUNTOX, PUNTOY, 0, 0)
        mouse_event(MOUSEEVENTF_RIGHTUP, PUNTOX, PUNTOY, 0, 0)
    End Sub

    Private Sub MOVERMOUSE()
        Cursor = New Cursor(Cursor.Current.Handle)
        Cursor.Position = New Point(PUNTOX, PUNTOY)
    End Sub

    Public Sub Conectar(Optional ByVal nombreBaseDatos As String = "")
        Dim CadenaConexion As String

        CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & nombreBaseDatos
        '
        Try
            dbConnection = New OleDbConnection(CadenaConexion)
        Catch e As Exception
            MessageBox.Show("Error al crear la conexión:" & vbCrLf & e.Message)
            Exit Sub
        End Try
        '
        dbConnection.Open()
        '
        Dim Comando As New OleDbCommand
        Dim rs As OleDbDataReader

        Comando.CommandText = "SELECT * FROM cfg"
        Comando.Connection = dbConnection

        rs = Comando.ExecuteReader()

        If rs.Read Then
            texto1 = ReadField(rs, "texto_loc1")
            texto2 = ReadField(rs, "texto_loc2")
            texto3 = ReadField(rs, "texto_loc3")
            texto4 = ReadField(rs, "texto_loc4")
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        rv.IniciarRecVoz()
        Exit Sub


        ''INICIAMOS LA VARIABLE QUE UTILIZAMOS PARA EL RECONOCIMIENTO
        recognizer = New SpeechRecognizer()

        ''ANADIMOS EVENTOS PARA EL RECONOCIMIENTO
        AddHandler recognizer.SpeechDetected, AddressOf detectado
        AddHandler recognizer.SpeechRecognitionRejected, AddressOf noreco
        AddHandler recognizer.SpeechRecognized, AddressOf RECONOCE
        ''INDICAMOS QUE EL OBJETO ESTE A TRUE=ACTIVADO
        recognizer.Enabled = True
        ''DECLARAMOS OBJETO GRAMMAR
        Dim grammar As New GrammarBuilder
        ''AÑADIMOS FRASES A RECONOCER
        grammar.Append(New Choices("abajo", "parar", "arriba", "izquierda", "derecha", "seleccionar", "doble clic", "rapido", "lento", "normal", "copiartexto"))
        ''CARGAMOS OBJETO GRAMMAR EN OBJETO SPEECHRECOGNIZER
        recognizer.LoadGrammar(New Grammar(grammar))
        System.Windows.Forms.Application.DoEvents()

        'Me.TopMost = True
        'Conectar("C:\Users\antonio\Dropbox\TONI\PROY\PruebasSystemSpeech\calc\CalcSpeech\CalcSpecch\Productos.mdb")
        'Conectar("C:\DROPBOX\Dropbox\TONI\PROY\PruebasSystemSpeech\calc\CalcSpeech\CalcSpecch\Productos.mdb")

        voz.Speak("El sistema se encuentra activado")

    End Sub
    ''EVENTOS DE SYSTEM.SPEECH.RECOGNITION
    ''SE DETECTA EL HABLA POR MICROFONO
    Private Sub detectado(ByVal sender As Object, ByVal e As SpeechDetectedEventArgs)
        ''MsgBox(e.AudioPosition.Duration.ToString())
    End Sub
    Private Sub noreco(ByVal sender As Object, ByVal e As SpeechRecognitionRejectedEventArgs)
        ''MsgBox("No se reconoce el comando de VOZ")
        'voz.Speak("Comando no encontrado")
    End Sub

    Public Sub RECONOCE(ByVal sender As Object, ByVal e As SpeechRecognizedEventArgs)
        Dim RESULTADO As RecognitionResult
        RESULTADO = e.Result
        PALABRA = RESULTADO.Text
        label2.Text = PALABRA.ToUpper

        Timer1.Enabled = True
        precision.Text = e.Result.Confidence
    End Sub
    Private Sub EjecutaComando()


        label2.Text = PALABRA

        Select Case PALABRA
            Case "parar"
                Timer1.Enabled = False

            Case "arriba"
                PUNTOX = Cursor.Position.X
                PUNTOY = Cursor.Position.Y - VELOCIDAD
                MOVERMOUSE()

            Case "abajo"
                PUNTOX = Cursor.Position.X
                PUNTOY = Cursor.Position.Y + VELOCIDAD
                MOVERMOUSE()

            Case "izquierda"
                PUNTOX = Cursor.Position.X - VELOCIDAD
                PUNTOY = Cursor.Position.Y
                MOVERMOUSE()

            Case "derecha"
                PUNTOX = Cursor.Position.X + VELOCIDAD
                PUNTOY = Cursor.Position.Y
                MOVERMOUSE()

            Case "seleccionar"
                CLICKIZDO()
                Timer1.Enabled = False

            Case "doble clic"
                CLICKIZDO()
                CLICKIZDO()
                Timer1.Enabled = False

            Case "rapido"
                VELOCIDAD = 30
                label2.Text = VELOCIDAD
                Timer1.Enabled = False

            Case "lento"
                VELOCIDAD = 2
                label2.Text = VELOCIDAD
                Timer1.Enabled = False

            Case "normal"
                VELOCIDAD = 10
                label2.Text = VELOCIDAD
                Timer1.Enabled = False

            Case "copiartexto"
                PALABRA = ""
                COPIARTEXTO()
        End Select

    End Sub
    Sub BuscarProducto(lCodigo As Long)
        Dim Comando As New OleDbCommand
        Dim rs As OleDbDataReader
        Dim sCad As String
        '


        Comando.CommandText = "SELECT COUNT(*) FROM productos WHERE codigo_reves LIKE '" & lCodigo & "%'"
        Comando.Connection = dbConnection

        rs = Comando.ExecuteReader()

        If rs.Read Then
            If rs.GetInt32(0) = 1 Then
                rs.Close()
                Comando.CommandText = "SELECT * FROM productos WHERE codigo_reves LIKE '" & lCodigo & "%'"
                Comando.Connection = dbConnection

                rs = Comando.ExecuteReader()
                If rs.Read Then
                    'Loc.Text = rs.GetString(rs.GetOrdinal("nombre")) & vbCrLf &
                    '            texto1 & " " & rs.GetString(rs.GetOrdinal("loc1")) & vbCrLf &
                    '            texto2 & " " & rs.GetString(rs.GetOrdinal("loc2")) & vbCrLf &
                    '            texto3 & " " & rs.GetString(rs.GetOrdinal("loc3")) & vbCrLf &
                    '            texto4 & " " & rs.GetString(rs.GetOrdinal("loc4"))
                    label2.Text = ""
                    gNombre = rs.GetString(rs.GetOrdinal("nombre"))
                    gsCad = texto1 & " " & rs.GetString(rs.GetOrdinal("loc1")) & texto2 & " " & rs.GetString(rs.GetOrdinal("loc2")) &
                               texto3 & " " & rs.GetString(rs.GetOrdinal("loc3")) & texto4 & " " & rs.GetString(rs.GetOrdinal("loc4"))
                    voz.Speak(gNombre)
                    voz.Speak(gsCad)
                End If
            End If
        End If
        rs.Close()
    End Sub

    Function ReadField(rs As OleDbDataReader, Tabla As String) As String
        If rs.IsDBNull(rs.GetOrdinal(Tabla)) Then
            ReadField = ""
        Else
            ReadField = rs.GetString(rs.GetOrdinal(Tabla))
        End If
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim s As String
        Dim handle As Long = GetForegroundWindow()

        precision.Text = handle
        EjecutaComando()
        s = rv.LeerSalida()
        If s <> "" Then
            label2.Text = s
        End If
    End Sub

    Private Const SW_MINIMIZE As Integer = 6 ' Minimizar la ventana. 
    Private Const SW_MAXIMIZE As Integer = 3 ' Maximizar la ventana
    Private Const SW_RESTORE As Integer = 9 ' Restaura la ventana a su tamaño y posición original
    Private Const SW_HIDE As Integer = 0 ' Ocultar la ventana
    Private Const SW_SHOWNORMAL As Integer = 1 ' Visualiza la ventana y la activa.
    Private Const SW_SHOWMINIMIZED As Integer = 2 ' Miniminiza la ventana
    Private Const SW_SHOWNOACTIVATE As Integer = 4 'Visualiza la ventana en su tamaño y posición actual sin activarla
    Private Const SW_SHOW As Integer = 5 'Visualiza la ventana
    Private Const SW_SHOWMINNOACTIVE As Integer = 7 'Miniminiza la ventana pero no la activa
    Private Const SW_SHOWNA As Integer = 8 'Visualiza la ventana en su estado actual pero no la activa. 


    Private Sub MostrarVentana()
        Dim p As Process() = System.Diagnostics.Process.GetProcessesByName("!!Nombre Del Proceso!!")
        Try
            ShowWindow(p(0).MainWindowHandle, SW_MAXIMIZE)
            ShowWindow(p(0).MainWindowHandle, SW_RESTORE)
            ShowWindow(p(0).MainWindowHandle, SW_SHOWNORMAL)
        Catch ex As Exception
            MessageBox.Show("Error")
            Me.Close()
        End Try
    End Sub

    Private Sub ActivarVentana_Click(sender As Object, e As EventArgs) Handles ActivarVentana.Click
        Dim p As Process() = System.Diagnostics.Process.GetProcesses
        Dim handle As Long = GetForegroundWindow()

        Dim proceso As Process
        Procesos.Items.Clear()
        For Each proceso In p
            If proceso.MainWindowTitle <> "" Then
                Procesos.Items.Add(proceso.MainWindowHandle.ToString() + " - " + proceso.MainWindowTitle + "  -  " + proceso.ProcessName)
                'If proceso.MainWindowTitle.Substring(0, 3) = "Sin" Then
                '    ShowWindow(proceso.MainWindowHandle, SW_MAXIMIZE)
                'End If
            End If

        Next

        MessageBox.Show(handle) ' Lo colocamos en el Caption del Form1 para ver lo que nos devolvió.

        'VentanaActiva()

    End Sub

    Dim WindowText As String 'Declaramos una variable tipo String para recibir el titulo de la ventana actual

    Private Sub VentanaActiva()
        WindowText = Space(255) 'Asignamos a la variable una cadena con 255 espacios vacíos (buffer)
        GetWindowText(GetForegroundWindow, WindowText, 255) 'Hacemos la llamada a las API's que nos devolveran la cadena
        MessageBox.Show(Trim(WindowText)) ' Lo colocamos en el Caption del Form1 para ver lo que nos devolvió.
    End Sub
    Private Sub Transparencia_Click(sender As Object, e As EventArgs) Handles Transparencia.Click

        'Me.Opacity = 0.5

        Me.TransparencyKey = System.Drawing.Color.Black

    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Dim myBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Red)
        Dim formGraphics As System.Drawing.Graphics
        formGraphics = Me.CreateGraphics()
        formGraphics.FillRectangle(myBrush, New Rectangle(0, 0, 200, 300))
        myBrush.Dispose()
        formGraphics.Dispose()

    End Sub

    Private Sub Procesos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Procesos.SelectedIndexChanged

    End Sub
End Class

