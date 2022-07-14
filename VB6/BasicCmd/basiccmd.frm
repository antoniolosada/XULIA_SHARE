VERSION 5.00
Object = "{60462311-3373-11D1-8C43-0060081841DE}#1.0#0"; "Xcommand.dll"
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "MSWINSCK.OCX"
Begin VB.Form Form1 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "XULIA SAPI4"
   ClientHeight    =   7380
   ClientLeft      =   -15
   ClientTop       =   210
   ClientWidth     =   11985
   Icon            =   "basiccmd.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   7380
   ScaleWidth      =   11985
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdEnviar 
      Caption         =   "Enviar"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   11.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   5160
      TabIndex        =   16
      Top             =   3720
      Width           =   1095
   End
   Begin VB.TextBox tbComando 
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   11.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   2040
      TabIndex        =   15
      Top             =   3720
      Width           =   3015
   End
   Begin VB.Timer tmrEspera 
      Enabled         =   0   'False
      Interval        =   1000
      Left            =   360
      Top             =   4560
   End
   Begin VB.PictureBox picIconTray 
      Height          =   495
      Left            =   6120
      ScaleHeight     =   435
      ScaleWidth      =   915
      TabIndex        =   14
      Top             =   5400
      Width           =   975
   End
   Begin VB.CommandButton Command5 
      Caption         =   "Command5"
      Height          =   315
      Left            =   1920
      TabIndex        =   13
      Top             =   7800
      Width           =   1815
   End
   Begin VB.CommandButton Command4 
      Caption         =   "Command4"
      Height          =   675
      Left            =   3540
      TabIndex        =   12
      Top             =   5580
      Width           =   1515
   End
   Begin VB.CommandButton cmdPrueba 
      Caption         =   "Command4"
      Height          =   795
      Left            =   2880
      TabIndex        =   11
      Top             =   4380
      Width           =   1815
   End
   Begin VB.Frame Frame2 
      Caption         =   "Último comando"
      Height          =   675
      Left            =   60
      TabIndex        =   8
      Top             =   60
      Width           =   4515
      Begin VB.Label Label3 
         Caption         =   "<nothing>"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   9
         Top             =   240
         Width           =   4215
      End
   End
   Begin VB.Frame Frame1 
      Height          =   3075
      Left            =   60
      TabIndex        =   4
      Top             =   840
      Width           =   1875
      Begin VB.TextBox Text2 
         Height          =   375
         Left            =   180
         TabIndex        =   10
         Text            =   "yellow"
         Top             =   1680
         Width           =   1575
      End
      Begin VB.CommandButton Command1 
         Caption         =   "Add Command"
         Height          =   375
         Left            =   120
         TabIndex        =   7
         Top             =   2520
         Width           =   1575
      End
      Begin VB.CommandButton Command2 
         Caption         =   "Remove Command"
         Height          =   375
         Left            =   120
         TabIndex        =   6
         Top             =   2100
         Width           =   1575
      End
      Begin VB.ListBox List1 
         Height          =   1425
         Left            =   180
         TabIndex        =   5
         Top             =   240
         Width           =   1575
      End
   End
   Begin VB.ListBox lstComandos 
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   11.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2865
      Left            =   2040
      Sorted          =   -1  'True
      TabIndex        =   2
      Top             =   840
      Width           =   4395
   End
   Begin MSWinsockLib.Winsock sock 
      Left            =   6840
      Top             =   1080
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
      LocalPort       =   10600
   End
   Begin VB.CommandButton Command3 
      Caption         =   "Stop Listening"
      Height          =   315
      Left            =   4680
      TabIndex        =   1
      Top             =   420
      Width           =   1695
   End
   Begin HSRLibCtl.Vcommand Vcommand1 
      Height          =   615
      Left            =   6840
      OleObjectBlob   =   "basiccmd.frx":044A
      TabIndex        =   0
      Top             =   1680
      Visible         =   0   'False
      Width           =   615
   End
   Begin VB.Label lblConectado 
      Alignment       =   2  'Center
      BackColor       =   &H0000FFFF&
      Caption         =   "Espera"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   4680
      TabIndex        =   3
      Top             =   60
      Width           =   1695
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim gMymenu As Long

Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)

Private Sub UpdateList()
Dim i As Long
Dim command As String
Dim Description As String
Dim category As String
Dim flags As Long
Dim action As String

List1.Clear
tcount = Vcommand1.CountCommands(gMymenu)
For i = 1 To tcount
   Vcommand1.GetCommand gMymenu, i, command, Description, category, flags, action
   List1.AddItem command
Next i
End Sub

Private Sub cmdEnviar_Click()
    If sock.State = sckConnected Then
        sock.SendData "%%" & tbComando.Text & "$"
    End If

End Sub

Private Sub cmdPrueba_Click()
UpdateList
End Sub

Private Sub Command1_Click()
If (Text2.Text <> "") Then
    Vcommand1.AddCommand gMymenu, 1, Text2.Text, "when you say " + Text2.Text, "listen list", 0, ""
End If
UpdateList

End Sub

Private Sub Command2_Click()
If (List1.ListIndex >= 0) Then
    Vcommand1.Remove gMymenu, List1.ListIndex + 1
End If
UpdateList
End Sub

Private Sub Command3_Click()
    If sock.State <> sckClosed Then sock.Close
    lblConectado.Caption = "Espera"
    lblConectado.BackColor = vbYellow
   
    sock.Listen

End Sub

Private Sub Command4_Click()
    Vcommand1.ReleaseMenu gMymenu
End Sub

Private Sub Command5_Click()
gMymenu = Vcommand1.MenuCreate(App.EXEName, "state1", 4)

End Sub

Private Sub Form_Load()
Vcommand1.Initialized = 1
Vcommand1.Enabled = 1
gMymenu = Vcommand1.MenuCreate(App.EXEName, "state1", 4)
tcount = Vcommand1.CountCommands(gMymenu)

If (tcount = 0) Then
    Vcommand1.AddCommand gMymenu, 1, "red", "when you say" + "red", "listen list", 0, ""
    Vcommand1.AddCommand gMymenu, 1, "blue", "when you say" + "blue", "listen list", 0, ""
    Vcommand1.AddCommand gMymenu, 1, "green", "when you say" + "green", "listen list", 0, ""
End If
UpdateList

Vcommand1.Activate gMymenu

    t.cbSize = Len(t)
    t.hWnd = picIconTray.hWnd
    t.uId = 1&
    t.uFlags = NIF_ICON Or NIF_TIP Or NIF_MESSAGE
    t.ucallbackMessage = WM_MOUSEMOVE
    t.hIcon = Me.Icon
'--------------------------------
    t.szTip = "Servidor de reconocimiento de voz SAPI4"
    Shell_NotifyIcon NIM_ADD, t
    Me.Hide
'Nos activamos en modo espera de conexión
    sock.Listen

End Sub


Private Sub Form_Unload(Cancel As Integer)
Dim i As Integer

    Vcommand1.ReleaseMenu gMymenu
    For i = 0 To lstComandos.ListCount - 1
        Vcommand1.ReleaseMenu Val(lstComandos.ItemData(i))
    Next
End Sub



Private Sub picIconTray_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
    If Button > 0 Then
        If tmrEspera.Enabled = False Then
            tmrEspera.Enabled = True
            If Button = 1 Then
                If Me.Visible Then
                    Me.Hide
                    Me.Refresh
                Else
                    Me.Show
                    Me.Refresh
                End If
                Sleep 1000
            End If
        End If
    End If

End Sub

Private Sub sock_Close()
    lblConectado.Caption = "Espera"
    lblConectado.BackColor = vbYellow
    
    sock.Close
    sock.Listen
End Sub

Private Sub sock_ConnectionRequest(ByVal requestID As Long)
    'Si el winsock está abierto lo cerramos.
    If sock.State <> sckClosed Then sock.Close
    sock.Accept requestID 'Aceptamos la conexión
    lblConectado.Caption = "Conectado"
    lblConectado.BackColor = vbGreen
End Sub

Private Sub sock_DataArrival(ByVal bytesTotal As Long)
    Dim data1 As String
    Dim datos As String
    Dim pos As Integer
    Dim comando As String
    Dim Gramatica As String
    Dim NombreGramatica As String
    Dim i As Integer
    'Tomamos los datos que nos envían con GetData
    sock.GetData data1
    DoEvents
    If Left(data1, 2) = "&&" Then
        datos = Mid(data1, 3)
        'Localizado un comando
        pos = InStr(datos, ":")
        If (pos > 0) Then
            comando = Left(datos, pos - 1)
            datos = Mid(datos, pos + 2)
            pos = InStr(datos, "&")
            NombreGramatica = Left(datos, pos - 1)
            datos = Mid(datos, pos + 1)
            pos_fin_comando = InStr(datos, "$")
            Select Case comando
                Case "CARGAR_GRAMATICA"
                    Dim Mymenu  As Long
                    If Not GramaticaCargada(NombreGramatica) Then
                        Mymenu = Vcommand1.MenuCreate(NombreGramatica, NombreGramatica, 4)
                        tcount = Vcommand1.CountCommands(Mymenu)
                        If tcount = 0 Then
                            Dim MaxList As Integer
                            MaxList = lstComandos.ListCount
                            lstComandos.AddItem NombreGramatica
                            lstComandos.ItemData(MaxList) = Mymenu
                            Gramatica = Mid(datos, 1, pos_fin_comando - 1)
                            Dim Comandos() As String
                            Comandos = Split(Gramatica, ",")
                            For i = 1 To UBound(Comandos)
                                Vcommand1.AddCommand Mymenu, 1, Comandos(i), Comandos(i), Comandos(i), 0, ""
                            Next
                            Vcommand1.Activate Mymenu
                            sock.SendData "&&" & Str(Mymenu) & "$"
                            Exit Sub
                        Else
                            'Si está generado recargamos
                            lstComandos.AddItem NombreGramatica
                            lstComandos.ItemData(MaxList) = Mymenu
                            sock.SendData "&&" & Str(Mymenu) & "$"
                        End If
                    Else
                        sock.SendData "&&" & BuscarMenu(NombreGramatica) & "$"
                    End If
                Case "ACTIVAR"
                    Vcommand1.Activate Val(NombreGramatica)
                    sock.SendData "&&" & 1 & "$"
                Case "DESACTIVAR"
                    Vcommand1.Deactivate Val(NombreGramatica)
                    sock.SendData "&&" & 1 & "$"
                Case "RESET"
                    For i = 0 To lstComandos.ListCount
                        Vcommand1.ReleaseMenu Val(lstComandos.ItemData(i))
                    Next
                    sock.SendData "&&" & 1 & "$"
                Case Else
                    sock.SendData "&&" & -1 & "$"
            End Select
        End If
        
    End If
End Sub

Function GramaticaCargada(nombre As String) As Boolean
    Dim i As Integer
    GramaticaCargada = False
    For i = 0 To lstComandos.ListCount - 1
        If lstComandos.List(i) = nombre Then
            GramaticaCargada = True
            Exit For
        End If
    Next
End Function
Function BuscarMenu(nombre As String) As Long
    Dim i As Integer
    BuscarMenu = 0
    For i = 0 To lstComandos.ListCount - 1
        If lstComandos.List(i) = nombre Then
            BuscarMenu = Val(lstComandos.ItemData(i))
            Exit For
        End If
    Next
End Function

Private Sub sock_Error(ByVal Number As Integer, Description As String, ByVal Scode As Long, ByVal Source As String, ByVal HelpFile As String, ByVal HelpContext As Long, CancelDisplay As Boolean)
    sock.Close
End Sub

Private Sub tmrEspera_Timer()
    tmrEspera.Enabled = False
End Sub

Private Sub Vcommand1_CommandRecognize(ByVal ID As Long, ByVal CmdName As String, ByVal flags As Long, ByVal action As String, ByVal NumLists As Long, ByVal ListValues As String, ByVal command As String)
Label3.Caption = command

    If sock.State = sckConnected Then
        sock.SendData "%%" + command + "$"
    End If
End Sub
