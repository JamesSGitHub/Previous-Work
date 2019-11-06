
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Scoreboard.Initialise()
        Options.Initialise()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Difficulty.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Application.Exit()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Options.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Scoreboard.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Load file
        OpenFileDialog1.Filter = "Save Files|*.sav"
        OpenFileDialog1.Title = "Select a Save File"

        ' Show the Dialog.  
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim savegame As SaveGameControl = New SaveGameControl()
            savegame.LoadGame(OpenFileDialog1.FileName)
            GameScreen.LoadedGame = True
            GameScreen.Show()
        End If
    End Sub
End Class
