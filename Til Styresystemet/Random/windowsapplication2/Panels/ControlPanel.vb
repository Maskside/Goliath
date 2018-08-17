Public Class ControlPanel

    Imports System
    Imports System.IO
    Imports System.Collections

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer


    Private Sub MoveBar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MoveBar.MouseDown
            drag = True
            mousex = Windows.Forms.Cursor.Position.X - Me.Left
            mousey = Windows.Forms.Cursor.Position.Y - Me.Top
        End Sub

        Private Sub MoveBar_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MoveBar.MouseMove

            If drag Then
                Me.Top = Windows.Forms.Cursor.Position.Y - mousey
                Me.Left = Windows.Forms.Cursor.Position.X - mousex
            End If
        End Sub

        Private Sub MoveBar_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MoveBar.MouseUp
            drag = False
        End Sub

        ' Starts fadein timer


        Private Sub ControllPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            FadeInTimer.Start()

        End Sub

        ' Walls Menu

        Private Sub WallsPanel_MouseEnter(sender As Object, e As EventArgs) Handles WallsPanel.MouseEnter, PictureBox1.MouseEnter, PictureBox2.MouseEnter, PictureBox3.MouseEnter, PictureBox4.MouseEnter, PictureBox5.MouseEnter, PictureBox6.MouseEnter, WallsLbl.MouseEnter

            WallsPanel.BackColor = SystemColors.ActiveBorder
            WallsLbl.ForeColor = Color.White

        End Sub

        Private Sub WallsPanel_MouseLeave(sender As Object, e As EventArgs) Handles WallsPanel.MouseLeave
            WallsPanel.BackColor = Color.White
            WallsLbl.ForeColor = SystemColors.AppWorkspace
        End Sub

        ' Solid Colors menu 

        Private Sub SolidBGPanel_MouseEnter(sender As Object, e As EventArgs) Handles SolidBGPanel.MouseEnter, ColorsPanelOp.MouseEnter, SolidBGLbl.MouseEnter

            SolidBGPanel.BackColor = SystemColors.ActiveBorder
            SolidBGLbl.ForeColor = Color.White


        End Sub

        Private Sub SolidBGPanel_MouseLeave(sender As Object, e As EventArgs) Handles SolidBGPanel.MouseLeave

            SolidBGPanel.BackColor = Color.White
            SolidBGLbl.ForeColor = SystemColors.AppWorkspace

        End Sub

        ' Close Button

        Private Sub CloseBtn_Click(sender As Object, e As EventArgs) Handles CloseBtn.Click
            FadeoutTimer.Start()
        End Sub

        ' Fadeout

        Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles FadeoutTimer.Tick
            ' this is to fade out & close.

            Opacity -= 0.2
            If Opacity = 0 Then
                FadeoutTimer.Stop()
                Close()
            End If


        End Sub

        ' Fadein

        Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles FadeInTimer.Tick
            Opacity += 0.2
            If Opacity = 1 Then
                FadeInTimer.Stop()
            End If
        End Sub

        ' Navigation Menu Stuff

        Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

            PersonalizationGB.BringToFront()

        End Sub

        Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

            ThemeGB.BringToFront()

        End Sub

        Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

            ConfigurationGB.BringToFront()

        End Sub


        Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

            UserSettingsGB.BringToFront()

        End Sub

        Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
            StartupGB.BringToFront()
        End Sub

        ' We will work with this now.

        ' For Wallpapers 

        Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
            ' Here goes the picture address.

            ' Here you have to put the picture address so be careful.

            My.Settings.bgWall = My.Computer.FileSystem.SpecialDirectories.MyPictures & "\" & "8491748484_abcea40c6c_h.jpg"
            My.Settings.Save()
            My.Settings.Reload()
            Try
                Form4.BackgroundImage = Image.FromFile(My.Settings.bgWall)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


        End Sub

        Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

            ' Here goes the picture address.

            ' Here you have to put the picture address so be careful.

            My.Settings.bgWall = My.Computer.FileSystem.SpecialDirectories.MyPictures & "\" & "death_valley_national_park_2-wallpaper-1920x1080.jpg"
            My.Settings.Save()
            My.Settings.Reload()
            Try
                Form4.BackgroundImage = Image.FromFile(My.Settings.bgWall)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub

        Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click

            ' Here goes the picture address.

            ' Here you have to put the picture address so be careful.

            My.Settings.bgWall = My.Computer.FileSystem.SpecialDirectories.MyPictures & "\" & "nyc_photowalk-wallpaper-2560x2048.jpg"
            My.Settings.Save()
            My.Settings.Reload()
            Try
                Form4.BackgroundImage = Image.FromFile(My.Settings.bgWall)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub

        Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click



            ' Here goes the picture address.

            ' Here you have to put the picture address so be careful.

            My.Settings.bgWall = My.Computer.FileSystem.SpecialDirectories.MyPictures & "\" & "nyc-city-lights.jpg"
            My.Settings.Save()
            My.Settings.Reload()
            Try
                Form4.BackgroundImage = Image.FromFile(My.Settings.bgWall)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub

        Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click


            ' Here goes the picture address.

            ' Here you have to put the picture address so be careful.

            My.Settings.bgWall = My.Computer.FileSystem.SpecialDirectories.MyPictures & "\" & "wallhaven-149041.jpg"
            My.Settings.Save()
            My.Settings.Reload()
            Try
                Form4.BackgroundImage = Image.FromFile(My.Settings.bgWall)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub

        ' for manual background image pick

        Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click

            Dim asgard As New OpenFileDialog
            If asgard.ShowDialog = Windows.Forms.DialogResult.OK Then

                My.Settings.bgWall = asgard.FileName
                My.Settings.Save()
                My.Settings.Reload()

                Try
                    Form4.BackgroundImage = Image.FromFile(asgard.FileName)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End If

        End Sub

        ' For Solid Color Background

        Private Sub ColorsPanelOp_Click(sender As Object, e As EventArgs) Handles ColorsPanelOp.Click

        End Sub


    End Class
    End Sub
End Class