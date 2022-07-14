<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class Frm_Voice_Command
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents Timer1 As System.Windows.Forms.Timer
	Public WithEvents VU_Meter As System.Windows.Forms.ProgressBar
	Public WithEvents Cmd_Exit As System.Windows.Forms.Button
	Public CD1Open As System.Windows.Forms.OpenFileDialog
	Public CD1Save As System.Windows.Forms.SaveFileDialog
	Public WithEvents Cmd_Save As System.Windows.Forms.Button
	Public WithEvents Cmd_Open As System.Windows.Forms.Button
	Public WithEvents Cmd_Start As System.Windows.Forms.Button
	Public WithEvents Lst_Commands As System.Windows.Forms.ListBox
	Public WithEvents VoiceCmd As AxHSRLib.AxVcommand
	Public WithEvents Detect As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Voice_Command))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.VU_Meter = New System.Windows.Forms.ProgressBar
        Me.Cmd_Exit = New System.Windows.Forms.Button
        Me.CD1Open = New System.Windows.Forms.OpenFileDialog
        Me.CD1Save = New System.Windows.Forms.SaveFileDialog
        Me.Cmd_Save = New System.Windows.Forms.Button
        Me.Cmd_Open = New System.Windows.Forms.Button
        Me.Cmd_Start = New System.Windows.Forms.Button
        Me.Lst_Commands = New System.Windows.Forms.ListBox
        Me.VoiceCmd = New AxHSRLib.AxVcommand
        Me.Detect = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.VoiceCmd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 2000
        '
        'VU_Meter
        '
        Me.VU_Meter.Location = New System.Drawing.Point(8, 32)
        Me.VU_Meter.Maximum = 60000
        Me.VU_Meter.Name = "VU_Meter"
        Me.VU_Meter.Size = New System.Drawing.Size(17, 129)
        Me.VU_Meter.TabIndex = 8
        '
        'Cmd_Exit
        '
        Me.Cmd_Exit.BackColor = System.Drawing.SystemColors.Control
        Me.Cmd_Exit.Cursor = System.Windows.Forms.Cursors.Default
        Me.Cmd_Exit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmd_Exit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Cmd_Exit.Location = New System.Drawing.Point(144, 168)
        Me.Cmd_Exit.Name = "Cmd_Exit"
        Me.Cmd_Exit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Cmd_Exit.Size = New System.Drawing.Size(65, 25)
        Me.Cmd_Exit.TabIndex = 7
        Me.Cmd_Exit.Text = "Exit"
        Me.Cmd_Exit.UseVisualStyleBackColor = False
        '
        'Cmd_Save
        '
        Me.Cmd_Save.BackColor = System.Drawing.SystemColors.Control
        Me.Cmd_Save.Cursor = System.Windows.Forms.Cursors.Default
        Me.Cmd_Save.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmd_Save.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Cmd_Save.Location = New System.Drawing.Point(72, 168)
        Me.Cmd_Save.Name = "Cmd_Save"
        Me.Cmd_Save.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Cmd_Save.Size = New System.Drawing.Size(65, 25)
        Me.Cmd_Save.TabIndex = 6
        Me.Cmd_Save.Text = "Save"
        Me.Cmd_Save.UseVisualStyleBackColor = False
        '
        'Cmd_Open
        '
        Me.Cmd_Open.BackColor = System.Drawing.SystemColors.Control
        Me.Cmd_Open.Cursor = System.Windows.Forms.Cursors.Default
        Me.Cmd_Open.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmd_Open.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Cmd_Open.Location = New System.Drawing.Point(0, 168)
        Me.Cmd_Open.Name = "Cmd_Open"
        Me.Cmd_Open.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Cmd_Open.Size = New System.Drawing.Size(65, 25)
        Me.Cmd_Open.TabIndex = 5
        Me.Cmd_Open.Text = "Open"
        Me.Cmd_Open.UseVisualStyleBackColor = False
        '
        'Cmd_Start
        '
        Me.Cmd_Start.BackColor = System.Drawing.SystemColors.Control
        Me.Cmd_Start.Cursor = System.Windows.Forms.Cursors.Default
        Me.Cmd_Start.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmd_Start.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Cmd_Start.Location = New System.Drawing.Point(32, 32)
        Me.Cmd_Start.Name = "Cmd_Start"
        Me.Cmd_Start.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Cmd_Start.Size = New System.Drawing.Size(105, 25)
        Me.Cmd_Start.TabIndex = 4
        Me.Cmd_Start.Text = "Listen"
        Me.Cmd_Start.UseVisualStyleBackColor = False
        '
        'Lst_Commands
        '
        Me.Lst_Commands.BackColor = System.Drawing.Color.White
        Me.Lst_Commands.Cursor = System.Windows.Forms.Cursors.Default
        Me.Lst_Commands.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lst_Commands.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Lst_Commands.ItemHeight = 14
        Me.Lst_Commands.Location = New System.Drawing.Point(32, 64)
        Me.Lst_Commands.Name = "Lst_Commands"
        Me.Lst_Commands.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Lst_Commands.Size = New System.Drawing.Size(177, 88)
        Me.Lst_Commands.TabIndex = 1
        '
        'VoiceCmd
        '
        Me.VoiceCmd.Enabled = True
        Me.VoiceCmd.Location = New System.Drawing.Point(144, 24)
        Me.VoiceCmd.Name = "VoiceCmd"
        Me.VoiceCmd.OcxState = CType(resources.GetObject("VoiceCmd.OcxState"), System.Windows.Forms.AxHost.State)
        Me.VoiceCmd.Size = New System.Drawing.Size(33, 33)
        Me.VoiceCmd.TabIndex = 0
        Me.VoiceCmd.Visible = False
        '
        'Detect
        '
        Me.Detect.BackColor = System.Drawing.SystemColors.Control
        Me.Detect.Cursor = System.Windows.Forms.Cursors.Default
        Me.Detect.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Detect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Detect.Location = New System.Drawing.Point(128, 8)
        Me.Detect.Name = "Detect"
        Me.Detect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Detect.Size = New System.Drawing.Size(81, 17)
        Me.Detect.TabIndex = 3
        Me.Detect.Text = "<Nothing>"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(105, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Last Heard Command"
        '
        'Frm_Voice_Command
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Menu
        Me.ClientSize = New System.Drawing.Size(214, 199)
        Me.Controls.Add(Me.VU_Meter)
        Me.Controls.Add(Me.Cmd_Exit)
        Me.Controls.Add(Me.Cmd_Save)
        Me.Controls.Add(Me.Cmd_Open)
        Me.Controls.Add(Me.Cmd_Start)
        Me.Controls.Add(Me.Lst_Commands)
        Me.Controls.Add(Me.VoiceCmd)
        Me.Controls.Add(Me.Detect)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Voice_Command"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Voice Commands"
        CType(Me.VoiceCmd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class