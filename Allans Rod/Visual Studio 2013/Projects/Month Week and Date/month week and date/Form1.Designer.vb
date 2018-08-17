<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.WeekOfMonthEdit1 = New DevExpress.XtraScheduler.UI.WeekOfMonthEdit()
        Me.WeekOfMonthEdit2 = New DevExpress.XtraScheduler.UI.WeekOfMonthEdit()
        CType(Me.WeekOfMonthEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WeekOfMonthEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'WeekOfMonthEdit1
        '
        Me.WeekOfMonthEdit1.Location = New System.Drawing.Point(301, 177)
        Me.WeekOfMonthEdit1.Name = "WeekOfMonthEdit1"
        Me.WeekOfMonthEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.WeekOfMonthEdit1.Size = New System.Drawing.Size(100, 20)
        Me.WeekOfMonthEdit1.TabIndex = 0
        '
        'WeekOfMonthEdit2
        '
        Me.WeekOfMonthEdit2.Location = New System.Drawing.Point(338, 231)
        Me.WeekOfMonthEdit2.Name = "WeekOfMonthEdit2"
        Me.WeekOfMonthEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.WeekOfMonthEdit2.Size = New System.Drawing.Size(100, 20)
        Me.WeekOfMonthEdit2.TabIndex = 1
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(661, 430)
        Me.ControlBox = False
        Me.Controls.Add(Me.WeekOfMonthEdit2)
        Me.Controls.Add(Me.WeekOfMonthEdit1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form1"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        CType(Me.WeekOfMonthEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WeekOfMonthEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WeekOfMonthEdit1 As DevExpress.XtraScheduler.UI.WeekOfMonthEdit
    Friend WithEvents WeekOfMonthEdit2 As DevExpress.XtraScheduler.UI.WeekOfMonthEdit

End Class
