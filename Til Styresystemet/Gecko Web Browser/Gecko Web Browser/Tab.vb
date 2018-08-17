Imports Gecko
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Tab
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Window
Imports System.Windows.Forms.TabDrawMode
Public Class Tab
    Private Sub Tab_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim newtab As New Tab
        Panel2.Hide()
        My.Settings.Panel2 = "Hidden"
        newtab.Dock = DockStyle.Fill

        GeckoWebBrowser1.Navigate(My.Settings.homepage)
    End Sub
    Public Sub New()
        InitializeComponent()
        Gecko.Xpcom.Initialize(Environment.CurrentDirectory + "\xulrunner")

        Gecko.GeckoPreferences.Default("extensions.blocklist.enabled") = False
    End Sub
    Public Sub shimscustombuttonfunction(ByVal aControl As Control, ByVal Color1 As Color, ByVal Color2 As Color, _
        Optional ByVal mode As System.Drawing.Drawing2D.LinearGradientMode = Drawing2D.LinearGradientMode.Vertical)

        Dim bmp As New Bitmap(aControl.Width, aControl.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        Dim Rect1 As New RectangleF(0, 0, aControl.Width, aControl.Height)

        Dim lineGBrush As New System.Drawing.Drawing2D.LinearGradientBrush(Rect1, Color1, Color2, mode)
        g.FillRectangle(lineGBrush, Rect1)

        aControl.BackgroundImage = bmp
        g.Dispose()
    End Sub
    Private Sub Button1_Click(sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Try
            GeckoWebBrowser1.GoBack()
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            GeckoWebBrowser1.GoForward()
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            GeckoWebBrowser1.Navigate(My.Settings.homepage)
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            GeckoWebBrowser1.Refresh()
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            GeckoWebBrowser1.Stop()
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            GeckoWebBrowser1.Navigate(TextBox1.Text)
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        GeckoWebBrowser1.Navigate("https://www.google.dk/search?q=" + Me.TextBox2.Text)
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            Panel2Status()
        Catch ex As Exception : End Try
    End Sub
    Private Sub Panel2Status()
        Try
            If My.Settings.Panel2 = "Hidden" Then
                Panel2.Show()
                My.Settings.Panel2 = "Showing"
            Else
                If My.Settings.Panel2 = "Showing" Then
                    Panel2.Hide()
                    My.Settings.Panel2 = "Hidden"
                End If
            End If
        Catch ex As Exception : End Try
    End Sub
    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            GeckoWebBrowser1.Navigate("https://www.google.com/search?q=" + Me.TextBox2.Text)
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button6.PerformClick()
        End If
    End Sub
    Private Sub GeckoWebBrowser1_Navigated(ByVal sender As Object, ByVal e As GeckoNavigatedEventArgs) Handles GeckoWebBrowser1.Navigated
        Form1.TabControl1.SelectedTab.Text = GeckoWebBrowser1.DocumentTitle
        Dim web_request As System.Net.HttpWebRequest = System.Net.WebRequest.Create("http://" & GeckoWebBrowser1.Document.Url.Host.ToString & "/favicon.ico")
        Dim web_respone As System.Net.HttpWebResponse = web_request.GetResponse
        Dim web_stream As System.IO.Stream = web_respone.GetResponseStream
        Dim image As Image = image.FromStream(web_stream)
        Form1.ImageList1.Images.Add(image)
        Form1.TabControl1.SelectedTab.ImageIndex = Form1.ImageList1.Images.Count - 1
    End Sub
    Private Sub GeckoWebBrowser1_DocumentCompleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GeckoWebBrowser1.DocumentCompleted
        Parent.Text = GeckoWebBrowser1.DocumentTitle
        Form1.Text = GeckoWebBrowser1.DocumentTitle + " - Goliath Browser " + Application.ProductVersion + " ALPHA"
        TextBox1.Text = GeckoWebBrowser1.Url.ToString
        If Not My.Settings.history.Contains(GeckoWebBrowser1.Url.ToString) Then
            My.Settings.history.Add(GeckoWebBrowser1.Url.ToString)
        End If
    End Sub
    Private Sub GeckoWebBrowser1_CreateWindow(sender As Object, e As GeckoCreateWindowEventArgs) Handles GeckoWebBrowser1.CreateWindow
        Dim t As New TabPage
        Dim newtab As New Tab
        newtab.Show()
        newtab.Dock = DockStyle.Fill
        newtab.Visible = True
        newtab.TopLevel = False
        t.Controls.Add(newtab)
        Form1.TabControl1.TabPages.Add(t)
        Form1.TabControl1.SelectedTab = t
    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim t As New TabPage
        Dim newtab As New Tab
        newtab.Show()
        newtab.Dock = DockStyle.Fill
        newtab.Visible = True
        newtab.TopLevel = False
        t.Controls.Add(newtab)
        Form1.TabControl1.TabPages.Add(t)
        Form1.TabControl1.SelectedTab = t
    End Sub
    Private Sub Label1_MouseEnter(sender As Object, e As EventArgs) Handles Label1.MouseEnter
        shimscustombuttonfunction(Label1, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label1.MouseLeave
        Label1.BackgroundImage = Nothing
    End Sub
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Process.Start(Application.StartupPath & "\" & Application.ProductName & ".exe")
    End Sub
    Private Sub Label2_MouseEnter(sender As Object, e As EventArgs) Handles Label2.MouseEnter
        shimscustombuttonfunction(Label2, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label2_MouseLeave(sender As Object, e As EventArgs) Handles Label2.MouseLeave
        Label2.BackgroundImage = Nothing
    End Sub
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Process.Start(Application.StartupPath & "\Ninja\" & Application.ProductName & ".exe")
    End Sub
    Private Sub Label3_MouseEnter(sender As Object, e As EventArgs) Handles Label3.MouseEnter
        shimscustombuttonfunction(Label3, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label3_MouseLeave(sender As Object, e As EventArgs) Handles Label3.MouseLeave
        Label3.BackgroundImage = Nothing
    End Sub
    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim t As New TabPage
        Dim newtab As New History
        newtab.Show()
        newtab.TopLevel = False
        newtab.Dock = DockStyle.Fill
        t.Controls.Add(newtab)
        Form1.TabControl1.TabPages.Add(t)
        Form1.TabControl1.SelectedTab = t
        Form1.TabControl1.SelectedTab.Text = "History"
    End Sub
    Private Sub Label4_MouseEnter(sender As Object, e As EventArgs) Handles Label4.MouseEnter
        shimscustombuttonfunction(Label4, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label4_MouseLeave(sender As Object, e As EventArgs) Handles Label4.MouseLeave
        Label4.BackgroundImage = Nothing
    End Sub
    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Dim t As New TabPage
        Dim newtab As New Downloads
        newtab.Show()
        newtab.TopLevel = False
        newtab.Dock = DockStyle.Fill
        t.Controls.Add(newtab)
        Form1.TabControl1.TabPages.Add(t)
        Form1.TabControl1.SelectedTab = t
        Form1.TabControl1.SelectedTab.Text = "Downloads"
    End Sub
    Private Sub Label5_MouseEnter(sender As Object, e As EventArgs) Handles Label5.MouseEnter
        shimscustombuttonfunction(Label5, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label5_MouseLeave(sender As Object, e As EventArgs) Handles Label5.MouseLeave
        Label5.BackgroundImage = Nothing
    End Sub
    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Dim t As New TabPage
        Dim newtab As New Bookmarks
        newtab.Show()
        newtab.TopLevel = False
        newtab.Dock = DockStyle.Fill
        t.Controls.Add(newtab)
        Form1.TabControl1.TabPages.Add(t)
        Form1.TabControl1.SelectedTab = t
        Form1.TabControl1.SelectedTab.Text = "Bookmarks"
    End Sub
    Private Sub Label6_MouseEnter(sender As Object, e As EventArgs) Handles Label6.MouseEnter
        shimscustombuttonfunction(Label6, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label6_MouseLeave(sender As Object, e As EventArgs) Handles Label6.MouseLeave
        Label6.BackgroundImage = Nothing
    End Sub
    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub
    Private Sub Label7_MouseEnter(sender As Object, e As EventArgs) Handles Label7.MouseEnter
        shimscustombuttonfunction(Label7, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label7_MouseLeave(sender As Object, e As EventArgs) Handles Label7.MouseLeave
        Label7.BackgroundImage = Nothing
    End Sub
    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub
    Private Sub Label8_MouseEnter(sender As Object, e As EventArgs) Handles Label8.MouseEnter
        shimscustombuttonfunction(Label8, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label8_MouseLeave(sender As Object, e As EventArgs) Handles Label8.MouseLeave
        Label8.BackgroundImage = Nothing
    End Sub
    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub
    Private Sub Label9_MouseEnter(sender As Object, e As EventArgs) Handles Label9.MouseEnter
        shimscustombuttonfunction(Label9, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label9_MouseLeave(sender As Object, e As EventArgs) Handles Label9.MouseLeave
        Label9.BackgroundImage = Nothing
    End Sub
    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub
    Private Sub Label10_MouseEnter(sender As Object, e As EventArgs) Handles Label10.MouseEnter
        shimscustombuttonfunction(Label10, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label10_MouseLeave(sender As Object, e As EventArgs) Handles Label10.MouseLeave
        Label10.BackgroundImage = Nothing
    End Sub
    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub
    Private Sub Label11_MouseEnter(sender As Object, e As EventArgs) Handles Label11.MouseEnter
        shimscustombuttonfunction(Label11, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label11_MouseLeave(sender As Object, e As EventArgs) Handles Label11.MouseLeave
        Label11.BackgroundImage = Nothing
    End Sub
    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim t As New TabPage
        Dim newtab As New General
        newtab.Show()
        newtab.TopLevel = False
        newtab.Dock = DockStyle.Fill
        t.Controls.Add(newtab)
        Form1.TabControl1.TabPages.Add(t)
        Form1.TabControl1.SelectedTab = t
        Form1.TabControl1.SelectedTab.Text = "General"
    End Sub
    Private Sub Label12_MouseEnter(sender As Object, e As EventArgs) Handles Label12.MouseEnter
        shimscustombuttonfunction(Label12, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label12_MouseLeave(sender As Object, e As EventArgs) Handles Label12.MouseLeave
        Label12.BackgroundImage = Nothing
    End Sub
    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click

    End Sub
    Private Sub Label13_MouseEnter(sender As Object, e As EventArgs) Handles Label13.MouseEnter
        shimscustombuttonfunction(Label13, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label13_MouseLeave(sender As Object, e As EventArgs) Handles Label13.MouseLeave
        Label13.BackgroundImage = Nothing
    End Sub
    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click
        End
    End Sub
    Private Sub Label14_MouseEnter(sender As Object, e As EventArgs) Handles Label14.MouseEnter
        shimscustombuttonfunction(Label14, Color.LightBlue, Color.LightBlue)
    End Sub
    Private Sub Label14_MouseLeave(sender As Object, e As EventArgs) Handles Label14.MouseLeave
        Label14.BackgroundImage = Nothing
    End Sub
End Class