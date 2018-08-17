Imports System.Drawing.Drawing2D
Public Class Form1
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.SetStyle(ControlStyles.DoubleBuffer, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
    End Sub
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Static i As Integer
        i += 1
        If i = 360 Then
            i = 0
        End If
        Me.BackgroundImage = DrawProgressBar(e.Graphics, i)
    End Sub
    Private Function DrawProgressBar(ByVal g As Graphics, ByVal angle As Integer) As Bitmap
        Dim bmp As New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
        g = Graphics.FromImage(bmp)
        Dim percent As Double
        percent = Math.Round(angle * 100 / 360, 1)
        Dim path As New GraphicsPath
        Dim rec As Rectangle = New Rectangle(0, 0, bmp.Width, bmp.Height)
        Dim stringformat As New StringFormat
        stringformat.Alignment = StringAlignment.Center
        stringformat.LineAlignment = StringAlignment.Center
        g.DrawString(percent & " %", New Font("Segoe UI", 32, FontStyle.Bold), Brushes.Green, New Point(rec.Width / 2, rec.Height / 2), stringformat)
        g.DrawEllipse(Pens.Black, rec)
        path.AddPie(rec, 180, angle)
        Dim holeRect As Rectangle = New Rectangle(rec.X + 15, rec.Y + 15, rec.Width - 30, rec.Height - 30)
        g.DrawEllipse(Pens.Black, holeRect)
        g.Clip = (New Region(path))
        path.AddPie(holeRect, 180, angle)
        Dim gradiant As New LinearGradientBrush(rec, Color.Red, Color.Blue, 45)
        g.FillPath(gradiant, path)
        Invalidate(rec)
        Return bmp
    End Function
End Class