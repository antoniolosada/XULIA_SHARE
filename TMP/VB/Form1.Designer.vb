<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Transparencia = New System.Windows.Forms.Button()
        Me.ActivarVentana = New System.Windows.Forms.Button()
        Me.precision = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Procesos = New System.Windows.Forms.ListBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Transparencia)
        Me.Panel1.Controls.Add(Me.ActivarVentana)
        Me.Panel1.Controls.Add(Me.precision)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.label2)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(472, 408)
        Me.Panel1.TabIndex = 5
        '
        'Transparencia
        '
        Me.Transparencia.Location = New System.Drawing.Point(141, 252)
        Me.Transparencia.Name = "Transparencia"
        Me.Transparencia.Size = New System.Drawing.Size(109, 34)
        Me.Transparencia.TabIndex = 6
        Me.Transparencia.Text = "Transparencia"
        Me.Transparencia.UseVisualStyleBackColor = True
        '
        'ActivarVentana
        '
        Me.ActivarVentana.Location = New System.Drawing.Point(23, 252)
        Me.ActivarVentana.Name = "ActivarVentana"
        Me.ActivarVentana.Size = New System.Drawing.Size(102, 34)
        Me.ActivarVentana.TabIndex = 5
        Me.ActivarVentana.Text = "ActivarVentana"
        Me.ActivarVentana.UseVisualStyleBackColor = True
        '
        'precision
        '
        Me.precision.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.precision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.precision.Font = New System.Drawing.Font("Arial", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.precision.Location = New System.Drawing.Point(23, 173)
        Me.precision.MaxLength = 9
        Me.precision.Name = "precision"
        Me.precision.Size = New System.Drawing.Size(426, 63)
        Me.precision.TabIndex = 4
        Me.precision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(137, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(195, 22)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Código de producto"
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.label2.Font = New System.Drawing.Font("Arial", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(23, 37)
        Me.label2.MaxLength = 9
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(426, 63)
        Me.label2.TabIndex = 1
        Me.label2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Procesos
        '
        Me.Procesos.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Procesos.FormattingEnabled = True
        Me.Procesos.ItemHeight = 20
        Me.Procesos.Location = New System.Drawing.Point(573, 13)
        Me.Procesos.Name = "Procesos"
        Me.Procesos.Size = New System.Drawing.Size(407, 384)
        Me.Procesos.TabIndex = 6
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1009, 497)
        Me.Controls.Add(Me.Procesos)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MATRIS - Módulo de Accesibilidad a Través de Reconocimiento Interactivo de Senten" & _
    "cias"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents precision As System.Windows.Forms.TextBox
    Friend WithEvents ActivarVentana As System.Windows.Forms.Button
    Friend WithEvents Procesos As System.Windows.Forms.ListBox
    Friend WithEvents Transparencia As System.Windows.Forms.Button

End Class
