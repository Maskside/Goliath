Public Class Form1
    Private Sub ShutDownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShutDownToolStripMenuItem.Click
        'Button "Sluk" om personen virkelig vil slukke for computeren
        ShutdownDLG.ShowDialog()
    End Sub
    Private Sub RestartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartToolStripMenuItem.Click
        'Button "Genstart" om personen virkelig vil genstarte computeren
        RestartDLG.ShowDialog()
    End Sub
    Private Sub SleepToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SleepToolStripMenuItem.Click
        'Button "Sov" om personen virkelig vil få computeren til at sove
        Sleep_Mode.ShowDialog()
    End Sub
    Private Sub PersonalizationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PersonalizationToolStripMenuItem.Click
        'Button "Personalization", når man klikker skifter den til Personalization vinduet
        Personalization.ShowDialog()
    End Sub
    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Settings.ShowDialog()
    End Sub
    Private Sub AppsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AppsToolStripMenuItem.Click
        Apps.ShowDialog()
    End Sub
    Private Sub SettingsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem1.Click
        Settings.ShowDialog()
    End Sub
    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Help.ShowDialog()
    End Sub
    Private Sub AboutUsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutUsToolStripMenuItem.Click
        AboutUs.ShowDialog()
    End Sub
    Private Sub ShutdownToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ShutdownToolStripMenuItem1.Click
        Me.Opacity = 100
        Me.WindowState = FormWindowState.Maximized
        SleepModeNotifyIcon.Visible = False
        ShutdownDLG.ShowDialog()
    End Sub

    Private Sub RestartToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RestartToolStripMenuItem1.Click
        Me.Opacity = 100
        Me.WindowState = FormWindowState.Maximized
        SleepModeNotifyIcon.Visible = False
        RestartDLG.ShowDialog()
    End Sub

    Private Sub SleepToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SleepToolStripMenuItem.Click
        Me.Opacity = 100
        Me.WindowState = FormWindowState.Maximized
        SleepModeNotifyIcon.Visible = False
        Sleep_Mode.ShowDialog()
    End Sub
End Class



