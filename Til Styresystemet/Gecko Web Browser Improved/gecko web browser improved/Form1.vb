Imports Gecko
Public Class Form1
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
End Class