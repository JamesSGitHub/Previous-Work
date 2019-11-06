Public Class Difficulty
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Easy difficulty
        Me.Hide()
        GameScreen.Set_Difficulty(GameScreen.Difficulty.Easy)
        GameScreen.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Hard difficulty
        Me.Hide()
        GameScreen.Set_Difficulty(GameScreen.Difficulty.Hard)
        GameScreen.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        Form1.Show()
    End Sub
End Class