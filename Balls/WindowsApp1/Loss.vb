Public Class Loss
    Dim currentDifficulty As GameScreen.Difficulty

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            Scoreboard.Addscore(Label4.Text, "Anon", Label5.Text, currentDifficulty)
        Else
            Scoreboard.Addscore(Label4.Text, TextBox1.Text, Label5.Text, currentDifficulty)
        End If
        Me.Hide()
        Form1.Show()
    End Sub

    Public Sub AddScore(score As Integer, rows As Integer, difficulty As GameScreen.Difficulty)
        If Scoreboard.IsaHighScore(score, difficulty) Then
            Label6.Show()
            Label7.Show()
            TextBox1.Show()
        Else
            Label6.Hide()
            Label7.Hide()
            TextBox1.Hide()
        End If
        currentDifficulty = difficulty
        Label4.Text = score
        Label5.Text = rows
    End Sub
End Class