VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "SAPI4"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Dim Vcommand1 As Vcommand
Dim comando As String



Public Function Prueba(c As String) As String
    Prueba = c
    
    Vcommand1.Initialized = 1
Vcommand1.Enabled = 1
gMymenu = Vcommand1.MenuCreate(App.EXEName, "state1", 4)
tcount = Vcommand1.CountCommands(gMymenu)

If (tcount = 0) Then
    Vcommand1.AddCommand gMymenu, 1, "red", "when you say" + "red", "listen list", 0, ""
    Vcommand1.AddCommand gMymenu, 1, "blue", "when you say" + "blue", "listen list", 0, ""
    Vcommand1.AddCommand gMymenu, 1, "green", "when you say" + "green", "listen list", 0, ""
End If


Vcommand1.Activate gMymenu

End Function

Private Sub Vcommand1_CommandRecognize(ByVal ID As Long, ByVal CmdName As String, ByVal flags As Long, ByVal action As String, ByVal NumLists As Long, ByVal ListValues As String, ByVal command As String)

comando = command
End Sub

Public Function Leer() As String
    Leer = comando
End Function

