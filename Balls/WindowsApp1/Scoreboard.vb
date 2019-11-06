Imports System.IO
Public Class Scoreboard

    Dim numberofhighscores As Integer = 10

    Structure Score
        Dim name As String
        Dim score As Integer
        Dim rows As Integer
    End Structure

    Dim easyTopScores(numberofhighscores - 1) As Score
    Dim hardTopScores(numberofhighscores - 1) As Score
    Const easyFile As String = "easyscore.txt"
    Const hardFile As String = "hardscore.txt"

    Private Sub readfile(filename As String, ByRef topscores() As Score)
        If File.Exists(filename) Then
            'the file exists so read the previous scores and add to the scoreboard
            Dim readfile As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(filename)
            For i As Integer = 0 To numberofhighscores - 1
                topscores(i).name = readfile.ReadLine()
                topscores(i).score = readfile.ReadLine()
                topscores(i).rows = readfile.ReadLine()
            Next i
            readfile.Close()
        Else
            'creates the score file
            File.Create(filename)
            For i As Integer = 0 To numberofhighscores - 1
                topscores(i).name = "..."
                topscores(i).score = 0
                topscores(i).rows = 0
            Next i
        End If

    End Sub

    Public Sub Initialise()

        With ListView1
            .View = View.Details
            .FullRowSelect = True
        End With

        ' read the easy top scores
        readfile(easyFile, easyTopScores)

        ' read the hard top scores
        readfile(hardFile, hardTopScores)
    End Sub

    Public Sub Addscore(score As Integer, name As String, rows As Integer, difficulty As GameScreen.Difficulty)
        If difficulty = GameScreen.Difficulty.Easy Then
            For i As Integer = 0 To numberofhighscores - 1
                If score > easyTopScores(i).score Then
                    MoveScores(i, easyTopScores)
                    easyTopScores(i).score = score
                    easyTopScores(i).name = name
                    easyTopScores(i).rows = rows
                    RefreshScoreDisplay(difficulty)

                    Exit For
                End If
            Next i
        Else
            For i As Integer = 0 To numberofhighscores - 1
                If score > hardTopScores(i).score Then
                    MoveScores(i, hardTopScores)
                    hardTopScores(i).score = score
                    hardTopScores(i).name = name
                    hardTopScores(i).rows = rows
                    RefreshScoreDisplay(difficulty)
                    Exit For
                End If
            Next i
        End If
    End Sub

    Public Function IsaHighScore(score As Integer, difficulty As GameScreen.Difficulty) As Boolean
        If difficulty = GameScreen.Difficulty.Easy Then
            For i As Integer = 0 To numberofhighscores - 1
                If score > easyTopScores(i).score Then
                    Return True
                End If
            Next i
        Else
            For i As Integer = 0 To numberofhighscores - 1
                If score > hardTopScores(i).score Then
                    Return True
                End If
            Next i
        End If
        'If the score is lower than the lowest score in the table then is not a high score and return false
        Return False
    End Function

    Private Sub writeFile(filename As String, ByRef topscores() As Score)
        Dim scorefile As System.IO.StreamWriter

        scorefile = My.Computer.FileSystem.OpenTextFileWriter(filename, False)
        For i As Integer = 0 To numberofhighscores - 1
            scorefile.WriteLine(topscores(i).name)
            scorefile.WriteLine(topscores(i).score)
            scorefile.WriteLine(topscores(i).rows)
        Next i

        scorefile.Close()
    End Sub

    Private Sub updateScoreBoard(ByRef topscores() As Score)
        ListView1.Items.Clear()

        For i As Integer = 0 To numberofhighscores - 1
            ListView1.Items.Add(New ListViewItem({topscores(i).name, topscores(i).score, topscores(i).rows}))
        Next i

    End Sub

    Private Sub RefreshScoreDisplay(difficulty As GameScreen.Difficulty)
        If difficulty = GameScreen.Difficulty.Easy Then
            writeFile(easyFile, easyTopScores)
            RadioButton1.Checked = True
        Else
            writeFile(hardFile, hardTopScores)
            RadioButton2.Checked = True
        End If

    End Sub

    Private Sub MoveScores(row As Integer, ByRef topscores() As Score)
        For Number As Integer = numberofhighscores - 1 To row + 1 Step -1
            topscores(Number) = topscores(Number - 1)
        Next Number
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        updateScoreBoard(easyTopScores)
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        updateScoreBoard(hardTopScores)
    End Sub

    Private Sub Scoreboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'When the form is loaded check whether easy is selected
        If RadioButton1.Checked Then
            updateScoreBoard(easyTopScores)
        Else
            updateScoreBoard(hardTopScores)
        End If

    End Sub
End Class