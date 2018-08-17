Public Class Tab
    Private Sub Tab_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim newtab As New Tab
        Panel2.hide()
        My.Settings.Panel2 = "Hidden"
        newtab.Dock = DockStyle.Fill
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            AxWebBrowser1.GoBack()
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            AxWebBrowser1.GoForward()
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            AxWebBrowser1.Navigate(My.Settings.homepage)
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            AxWebBrowser1.Refresh()
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            AxWebBrowser1.Stop()
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            AxWebBrowser1.Navigate(TextBox1.Text)
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            Dim t As New TabPage
            Dim newtab As New Tab
            Settings.Show()
        Catch ex As Exception : End Try
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        AxWebBrowser1.Navigate("https://www.google.dk/search?q=" + Me.TextBox2.Text)
    End Sub
    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            AxWebBrowser1.Navigate("https://www.google.dk/search?q=" + Me.TextBox2.Text)
        End If
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
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button6.PerformClick()
        End If
    End Sub
    Private Sub AxWebBrowser1_NavigateComplete2(sender As Object, e As AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event)
        Parent.Text = AxWebBrowser1.LocationName
        Form1.Text = AxWebBrowser1.LocationName
        TextBox1.Text = AxWebBrowser1.LocationURL
        If Not My.Settings.history.Contains(AxWebBrowser1.LocationURL.ToString) Then
            My.Settings.history.Add(AxWebBrowser1.LocationURL.ToString)
        End If
    End Sub
    Private Sub AxWebBrowser1_NewWindow2(sender As Object, e As AxSHDocVw.DWebBrowserEvents2_NewWindow2Event)
        Dim t As New TabPage
        Dim newtab As New Tab
        newtab.Show()
        newtab.Dock = DockStyle.Fill
        newtab.AxWebBrowser1.RegisterAsBrowser = True
        e.ppDisp = newtab.AxWebBrowser1.Application
        newtab.Visible = True
        newtab.TopLevel = False
        t.Controls.Add(newtab)
        Form1.TabControl1.TabPages.Add(t)
        Form1.TabControl1.SelectedTab = t
    End Sub
End Class