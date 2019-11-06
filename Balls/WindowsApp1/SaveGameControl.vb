Imports System.IO

Public Class SaveGameControl

    'Game is saved in the order:
    'current score
    'total number of rows survived
    'number of balls in play
    'game difficulty
    'for each block, starting from the top left corner and moving across the row before each column
    '   saves the hitpoints remaining for the block
    '   saves whether the block is special or not


    Public Sub SaveGame(name As String)
        Dim gamefile As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(name, False)

        gamefile.WriteLine(GameScreen.score)
        gamefile.WriteLine(GameScreen.TotalRows)
        gamefile.WriteLine(GameScreen.NumBalls)
        gamefile.WriteLine(GameScreen.gameDifficulty)

        For i As Integer = 0 To GameScreen.BlockRows - 1
            For j As Integer = 0 To GameScreen.BlockColumns - 1
                gamefile.WriteLine(GameScreen.currentBlocks(i, j).Hitpoints)
                gamefile.WriteLine(GameScreen.currentBlocks(i, j).Special)
            Next j
        Next i

        gamefile.Close()
    End Sub

    Public Sub LoadGame(name As String)
        Dim blocks(GameScreen.BlockRows, GameScreen.BlockColumns) As Block

        Dim gamefile As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(name)

        GameScreen.score = gamefile.ReadLine()
        GameScreen.TotalRows = gamefile.ReadLine()
        GameScreen.NumBalls = gamefile.ReadLine()
        GameScreen.gameDifficulty = gamefile.ReadLine()

        GameScreen.Create_Block_Positions(blocks)

        For i As Integer = 0 To GameScreen.BlockRows - 1
            For j As Integer = 0 To GameScreen.BlockColumns - 1
                blocks(i, j).BlockHitpoints = gamefile.ReadLine()
                blocks(i, j).Special = gamefile.ReadLine()
            Next j
        Next i
        GameScreen.currentBlocks = blocks
        gamefile.Close()

    End Sub


End Class
