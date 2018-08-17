Imports Gecko
Public Class Tab
    Public Sub New()
        InitializeComponent()
        Gecko.Xpcom.Initialize(Environment.CurrentDirectory + "\xulrunner")

        Skybound.Gecko.GeckoPreferences.Default("extensions.blocklist.enabled") = False
    End Sub
End Class