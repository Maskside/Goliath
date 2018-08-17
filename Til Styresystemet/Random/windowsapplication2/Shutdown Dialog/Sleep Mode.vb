Public Class Sleep_Mode
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Opacity = 0
        Form1.WindowState = FormWindowState.Minimized
        Form1.SleepModeNotifyIcon.Visible = True
        Form1.SleepModeNotifyIcon.ShowBalloonTip(3000) ' det tager 3 sekunder at dukke op på skærmen
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Opacity -= 0.2
        If Me.Opacity = 1 Then
            Timer1.Stop()
        End If
    End Sub

    Private Sub Sleep_Mode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub
End Class