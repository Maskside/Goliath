Public Class History
    Private Sub History_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not My.Settings.history.Count = 0 Then
            Dim ihist As Integer
            Do Until ListBox1.Items.Count = My.Settings.history.Count
                ListBox1.Items.Add(My.Settings.history(ihist))
                ihist += 1
            Loop
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.history.Clear()
        ListBox1.Items.Clear()
        MsgBox("History is now cleared")
    End Sub
End Class