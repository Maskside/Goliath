Public Class RestartDLG
    Private Sub RestartDLG_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RestartDLG.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Genstarter Programmet
        Process.Start(Application.StartupPath & "\" & Application.ProductName & ".exe")
        End


        'Fordi så vi ender med at putte "form1.show" til det sidste i applikitionen så der ikke vil ske noget.
    End Sub
End Class