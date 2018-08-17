Public Class General
    Private Sub Settings_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Form1.Enabled = True
    End Sub
    Private Sub Settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.homepage
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Form1.TabControl1.TabCount = 1 Then
            Me.Close()
            Dim t As New TabPage
            Dim newtab As New Tab
            newtab.Show()
            newtab.TopLevel = False
            newtab.Dock = DockStyle.Fill
            t.Controls.Add(newtab)
            Form1.TabControl1.TabPages.Add(t)
            Form1.TabControl1.SelectedTab.Dispose()
        Else
            Form1.TabControl1.SelectedTab.Dispose()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        My.Settings.homepage = TextBox1.Text
    End Sub

End Class