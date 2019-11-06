Public Class Escape
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        GameScreen.Close()
        Difficulty.Close()
        Form1.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Application.Exit()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Options.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Save file
        SaveFileDialog1.Filter = "Save Files|*.sav"
        SaveFileDialog1.Title = "Select a Save File"

        ' Show the Dialog.  
        If SaveFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim savegame As SaveGameControl = New SaveGameControl()
            savegame.SaveGame(SaveFileDialog1.FileName)
        End If
    End Sub
End Class