<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServidorWeb
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBoxTEXTO = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBoxSERVER = New System.Windows.Forms.TextBox()
        Me.tbLOG = New System.Windows.Forms.TextBox()
        Me.chkPrefijo = New System.Windows.Forms.CheckBox()
        Me.chkLOG = New System.Windows.Forms.CheckBox()
        Me.tmrLOG = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Black
        Me.Button1.ForeColor = System.Drawing.Color.Aqua
        Me.Button1.Location = New System.Drawing.Point(277, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(140, 40)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "INICIAR"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TextBoxTEXTO
        '
        Me.TextBoxTEXTO.Location = New System.Drawing.Point(13, 76)
        Me.TextBoxTEXTO.Name = "TextBoxTEXTO"
        Me.TextBoxTEXTO.Size = New System.Drawing.Size(259, 22)
        Me.TextBoxTEXTO.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Black
        Me.Button2.ForeColor = System.Drawing.Color.Aqua
        Me.Button2.Location = New System.Drawing.Point(277, 58)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(140, 40)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "ENVIAR"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'TextBoxSERVER
        '
        Me.TextBoxSERVER.BackColor = System.Drawing.Color.Black
        Me.TextBoxSERVER.ForeColor = System.Drawing.Color.Aqua
        Me.TextBoxSERVER.Location = New System.Drawing.Point(12, 12)
        Me.TextBoxSERVER.Name = "TextBoxSERVER"
        Me.TextBoxSERVER.Size = New System.Drawing.Size(259, 22)
        Me.TextBoxSERVER.TabIndex = 3
        Me.TextBoxSERVER.Text = "http://localhost:8081/SERVER/"
        '
        'tbLOG
        '
        Me.tbLOG.Location = New System.Drawing.Point(12, 113)
        Me.tbLOG.Multiline = True
        Me.tbLOG.Name = "tbLOG"
        Me.tbLOG.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbLOG.Size = New System.Drawing.Size(405, 139)
        Me.tbLOG.TabIndex = 5
        '
        'chkPrefijo
        '
        Me.chkPrefijo.AutoSize = True
        Me.chkPrefijo.Checked = True
        Me.chkPrefijo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPrefijo.Location = New System.Drawing.Point(13, 40)
        Me.chkPrefijo.Name = "chkPrefijo"
        Me.chkPrefijo.Size = New System.Drawing.Size(128, 20)
        Me.chkPrefijo.TabIndex = 4
        Me.chkPrefijo.Text = "Asignar prefijo"
        Me.chkPrefijo.UseVisualStyleBackColor = True
        '
        'chkLOG
        '
        Me.chkLOG.AutoSize = True
        Me.chkLOG.Location = New System.Drawing.Point(143, 40)
        Me.chkLOG.Name = "chkLOG"
        Me.chkLOG.Size = New System.Drawing.Size(57, 20)
        Me.chkLOG.TabIndex = 6
        Me.chkLOG.Text = "LOG"
        Me.chkLOG.UseVisualStyleBackColor = True
        '
        'tmrLOG
        '
        Me.tmrLOG.Enabled = True
        Me.tmrLOG.Interval = 200
        '
        'ServidorWeb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(431, 258)
        Me.Controls.Add(Me.chkLOG)
        Me.Controls.Add(Me.tbLOG)
        Me.Controls.Add(Me.chkPrefijo)
        Me.Controls.Add(Me.TextBoxSERVER)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TextBoxTEXTO)
        Me.Controls.Add(Me.Button1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Aqua
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ServidorWeb"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SERVIDOR WEB"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBoxTEXTO As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBoxSERVER As System.Windows.Forms.TextBox
    Friend WithEvents tbLOG As System.Windows.Forms.TextBox
    Friend WithEvents chkPrefijo As CheckBox
    Friend WithEvents chkLOG As CheckBox
    Friend WithEvents tmrLOG As Timer
End Class
