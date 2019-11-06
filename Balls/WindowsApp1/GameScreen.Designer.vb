<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GameScreen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MainTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LabelDifficulty = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelScore = New System.Windows.Forms.Label()
        Me.LabelRows = New System.Windows.Forms.Label()
        Me.LabelBalls = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainTimer
        '
        Me.MainTimer.Interval = 25
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.LabelDifficulty)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.LabelScore)
        Me.Panel1.Controls.Add(Me.LabelRows)
        Me.Panel1.Controls.Add(Me.LabelBalls)
        Me.Panel1.Location = New System.Drawing.Point(-7, -7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(400, 70)
        Me.Panel1.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Impact", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(209, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 20)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Difficulty:"
        '
        'LabelDifficulty
        '
        Me.LabelDifficulty.AutoSize = True
        Me.LabelDifficulty.Font = New System.Drawing.Font("Impact", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDifficulty.Location = New System.Drawing.Point(241, 36)
        Me.LabelDifficulty.Name = "LabelDifficulty"
        Me.LabelDifficulty.Size = New System.Drawing.Size(39, 20)
        Me.LabelDifficulty.TabIndex = 7
        Me.LabelDifficulty.Text = "Easy"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Impact", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(126, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Rows:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Impact", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(19, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 20)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Ball Count:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Impact", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(317, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 20)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Score:"
        '
        'LabelScore
        '
        Me.LabelScore.AutoSize = True
        Me.LabelScore.Font = New System.Drawing.Font("Impact", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelScore.Location = New System.Drawing.Point(349, 36)
        Me.LabelScore.Name = "LabelScore"
        Me.LabelScore.Size = New System.Drawing.Size(18, 20)
        Me.LabelScore.TabIndex = 3
        Me.LabelScore.Text = "0"
        '
        'LabelRows
        '
        Me.LabelRows.AutoSize = True
        Me.LabelRows.Font = New System.Drawing.Font("Impact", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelRows.Location = New System.Drawing.Point(155, 36)
        Me.LabelRows.Name = "LabelRows"
        Me.LabelRows.Size = New System.Drawing.Size(18, 20)
        Me.LabelRows.TabIndex = 2
        Me.LabelRows.Text = "0"
        '
        'LabelBalls
        '
        Me.LabelBalls.AutoSize = True
        Me.LabelBalls.Font = New System.Drawing.Font("Impact", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelBalls.Location = New System.Drawing.Point(79, 36)
        Me.LabelBalls.Name = "LabelBalls"
        Me.LabelBalls.Size = New System.Drawing.Size(18, 20)
        Me.LabelBalls.TabIndex = 1
        Me.LabelBalls.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Impact", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(250, 508)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 15)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Press Esc for more options"
        '
        'GameScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(380, 530)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GameScreen"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MainTimer As Timer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents LabelRows As Label
    Friend WithEvents LabelBalls As Label
    Friend WithEvents LabelScore As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents LabelDifficulty As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label5 As Label
End Class
