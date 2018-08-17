Public Class Settings
    Private Sub Settings_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Form1.Enabled = True
    End Sub
    Private Sub Settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.homepage

        If Not My.Settings.history.Count = 0 Then
            Dim ihist As Integer
            Do Until ListBox1.Items.Count = My.Settings.history.Count
                ListBox1.Items.Add(My.Settings.history(ihist))
                ihist += 1
            Loop
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        My.Settings.homepage = TextBox1.Text
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        My.Settings.history.Clear()
        ListBox1.Items.Clear()
        MsgBox("History is now cleared")
    End Sub
End Class