Public Class Form1
    Private Sub AxWindowsMediaPlayer1_Enter(sender As Object, e As EventArgs) Handles AxWindowsMediaPlayer1.Enter
        AxWindowsMediaPlayer1.URL = "http://onair.100fmlive.dk/100fm_live.mp3"
    End Sub
    Private Sub AxWindowsMediaPlayer1_Enter(sender As Object, e As EventArgs) Handles AxWindowsMediaPlayer1.Enter
        If AxWindowsMediaPlayer1.newMedia = 10 Then
            AxWindowsMediaPlayer1 = WMPLib.WMPPlayState.wmppsPlaying
        End If
    End Sub
End Class
