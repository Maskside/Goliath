Imports Gecko
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Tab
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Window
Imports System.Windows.Forms.TabDrawMode
Public Class Form1
    Public Sub New()
        InitializeComponent()
        Gecko.Xpcom.Initialize(Environment.CurrentDirectory + "\xulrunner")
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
    Private Sub FullscreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullscreenToolStripMenuItem.Click
        Try
            If FullscreenToolStripMenuItem.Text = "Fullscreen" Then
                Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
                Me.TopMost = TopMost
                Me.WindowState = FormWindowState.Maximized
                FullscreenToolStripMenuItem.Visible = True
                FullscreenToolStripMenuItem.Text = "Exit Fullscreen"
            Else
                Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
                Me.TopMost = False
                Me.WindowState = FormWindowState.Normal
                FullscreenToolStripMenuItem.Visible = True
                FullscreenToolStripMenuItem.Text = "Fullscreen"
            End If
        Catch ex As Exception : End Try
    End Sub
    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Dim t As New TabPage
        Dim newtab As New General
        newtab.Show()
        newtab.TopLevel = False
        newtab.Dock = DockStyle.Fill
        t.Controls.Add(newtab)
        TabControl1.TabPages.Add(t)
        TabControl1.SelectedTab = t

        TabControl1.SelectedTab.Text = "Settings"
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub FileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileToolStripMenuItem.Click
        Me.Show()
    End Sub
End Class
