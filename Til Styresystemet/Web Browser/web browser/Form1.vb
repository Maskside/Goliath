Imports Skybound.Gecko
Public Class Form1
    Sub New()
        InitializeComponent()
        Xpcom.Initialize(Environment.CurrentDirectory + "\xulrunner")
    End Sub
    Private Sub Goliath_Browser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Goliath Browser " + Application.ProductVersion + " ALPHA"
        Dim t As New TabPage
        Dim newtab As New Tab
        newtab.Show()
        newtab.TopLevel = False
        newtab.Dock = DockStyle.Fill
        t.Controls.Add(newtab)
        TabControl1.TabPages.Add(t)
        MenuStrip1.Hide()
    End Sub
    Private Sub NewTabToolStripDownMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewTabToolStripMenuItem.Click
        Dim t As New TabPage
        Dim newtab As New Tab
        newtab.Show()
        newtab.TopLevel = False
        newtab.Dock = DockStyle.Fill
        t.Controls.Add(newtab)
        TabControl1.TabPages.Add(t)
    End Sub
    Private Sub RemoveToolStripDownMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveTabToolStripMenuItem.Click
        If TabControl1.TabCount = 1 Then
            Dim t As New TabPage
            Dim newtab As New Tab
            newtab.Show()
            newtab.TopLevel = False
            newtab.Dock = DockStyle.Fill
            t.Controls.Add(newtab)
            TabControl1.TabPages.Add(t)
            TabControl1.SelectedTab.Dispose()
        Else
            TabControl1.SelectedTab.Dispose()
        End If
    End Sub
    Private Sub PrintStripDownMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Tab.AxWebBrowser1.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINT, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
    End Sub
    Private Sub PrintPreviewStripDownMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        Tab.AxWebBrowser1.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINTPREVIEW, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
    End Sub
    Private Sub PageSetupStripDownMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageSetupToolStripMenuItem.Click
        Tab.AxWebBrowser1.ExecWB(SHDocVw.OLECMDID.OLECMDID_PAGESETUP, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
    End Sub
    Private Sub FullscreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullscreenToolStripMenuItem.Click
        Try
            If FullscreenToolStripMenuItem.Text = "Fullscreen" Then
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                Me.TopMost = TopMost
                Me.WindowState = FormWindowState.Maximized
                FullscreenToolStripMenuItem.Visible = True
                FullscreenToolStripMenuItem.Text = "Exit Fullscreen"
            Else
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                Me.TopMost = False
                Me.WindowState = FormWindowState.Normal
                FullscreenToolStripMenuItem.Visible = True
                FullscreenToolStripMenuItem.Text = "Fullscreen"
            End If
        Catch ex As Exception : End Try
    End Sub
    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Settings.Show()
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub FileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileToolStripMenuItem.Click
        Me.Show()
    End Sub
End Class
