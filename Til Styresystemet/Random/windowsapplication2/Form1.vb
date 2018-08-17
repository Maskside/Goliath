Public Class Form1

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Button "sluk" om personen virkelig vil slukke for computeren

        ShutdownDLG.ShowDialog()
    End Sub

    Private Sub RestartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartToolStripMenuItem.Click, RestartToolStripMenuItem1.Click
        RestartDLG.ShowDialog()
    End Sub

    Private Sub ShutDownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShutDownToolStripMenuItem.Click
        ShutdownDLG.ShowDialog()
    End Sub

    Private Sub SleepToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SleepToolStripMenuItem.Click
        Sleep-Mode.ShowDialog()
    End Sub

    Private Sub ShutdownToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ShutdownToolStripMenuItem1.Click

        Me.Opacity = 40
        Me.WindowState = FormWindowState.Maximized
        SleepModeNotifyIcon.Visible = False
        ShutdownDLG.ShowDialog()

    End Sub

    Private Sub RestartToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Me.Opacity = 100
        Me.WindowState = FormWindowState.Maximized
        SleepModeNotifyIcon.Visible = False
        RestartDLG.ShowDialog()
    End Sub

    Private Sub GoBackToSystemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoBackToSystemToolStripMenuItem.Click

        Me.Opacity = 100
        Me.WindowState = FormWindowState.Maximized
        SleepModeNotifyIcon.Visible = False


    End Sub

    Private Sub PersonalizationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PersonalizationToolStripMenuItem.Click
        ControlPanel.Show()
    End Sub
End Class
