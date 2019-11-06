Public Class GameScreen

    Private easyBlockChance = 0.2
    Private difficultBlockChance = 0.4
    Private blockChance = easyBlockChance

    Public Const BlockRows As Integer = 9
    Public Const BlockColumns As Integer = 8

    'Left hand edge of screen
    Private Const ScreenLeft As Integer = 3
    'Right hand edge of screen
    Private Const ScreenRight As Integer = 380
    'Top edge of screen
    Private Const ScreenTop As Integer = 66
    'Bottom edge of screen
    Private Const ScreenBottom As Integer = 530

    'Angle that ball is pointing (default to straight up)
    Private Angle As Single = -90 * Math.PI / 180

    'Centre of screen
    Private Centre As Integer = (ScreenRight - ScreenLeft) / 2

    'Determines the width and height of the blocks
    Private Const BlockWidth As Integer = 41
    Private Const BlockHeight As Integer = 41
    Private Const BlockSpacing As Integer = 5
    Private Const BallWidth As Integer = 10

    ' list of the blocks and their position and status
    Private blocks(BlockRows, BlockColumns) As Block

    Dim backcolour As Color = Color.FromArgb(&H200020)

    'Increment between ball refreshes
    Dim ballTravel As Single = 10

    Dim BallFired = False
    Private currentNumBalls As Integer = 1
    Private currentRows As Integer = 0
    Private currentDifficulty As Difficulty
    Private currentScore = 0
    Private NumBallsToAdd As Integer = 1

    ' The list of balls positions and states
    Dim balls(1000) As Ball

    'Private startSoundPlayer
    Dim filename As String = "BlockBreakSound.wav"
    Dim sp As System.Media.SoundPlayer = New System.Media.SoundPlayer(filename)

    ' type to define the difficulty
    Enum Difficulty
        Easy = 0
        Hard = 1
    End Enum

    ' type to define the ball state
    Enum BallState
        Not_Fired = 0
        InPlay = 1
        Finished = 2
    End Enum

    ' type to define the ball position (and state)
    Structure Ball
        ' number of x positions to move each refresh
        Dim x_increment As Single
        ' number of y positions to move each refresh
        Dim y_increment As Single
        Dim ball As Rectangle
        Dim old_position As Point
        Dim state As BallState
    End Structure

    ' the curremt score
    Property score As Integer
        Set(ByVal Value As Integer)
            currentScore = Value
            LabelScore.Text = currentScore
        End Set
        Get
            Return currentScore
        End Get
    End Property

    ' the number of current Balls in the stack 
    Property NumBalls As Integer
        Set(ByVal Value As Integer)
            currentNumBalls = Value
            LabelBalls.Text = currentNumBalls
        End Set
        Get
            Return currentNumBalls
        End Get
    End Property

    ' the number of Rows survived
    Property TotalRows As Integer
        Set(ByVal Value As Integer)
            currentRows = Value
            LabelRows.Text = currentRows
        End Set
        Get
            Return currentRows
        End Get
    End Property

    ' the Difficulty of the current game
    Property gameDifficulty As Difficulty
        Set(ByVal Value As Difficulty)
            LoadedGame = False
            currentDifficulty = Value
            LabelDifficulty.Text = [Enum].GetName(GetType(Difficulty), Value)
        End Set
        Get
            Return currentDifficulty
        End Get
    End Property

    ' get/set the blocks in the current game
    Property currentBlocks As Block(,)
        Set(ByVal Value As Block(,))
            blocks = Value
        End Set
        Get
            Return blocks
        End Get
    End Property

    ' defines whether the game has been loaded from a file (true) or is a new game (false)
    Property LoadedGame As Boolean = False

    ' Form called when screen displayed
    Private Sub BallsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Angle = 90.0
        Centre = (ScreenRight - ScreenLeft) / 2
        Me.Width = 390
        Me.Height = 540
        Randomize()

        ' check whether this is a new game(if it is reset the game blocks)
        ' else blocks/ score etc have been loaded from the file
        If Not LoadedGame Then
            Create_Block_Positions(blocks)
            Create_Top_Row()

            BallFired = False
            NumBalls = 1
            NumBallsToAdd = 0
            score = 0
        End If
        'startSoundPlayer = New System.Media.SoundPlayer("BlockBreakSound.wav")

        ' set the back colour of the form 
        Me.BackColor = System.Drawing.Color.FromArgb(8, 0, 10)

        balls(0).ball = New Rectangle(Centre - (BallWidth / 2), ScreenBottom - (BallWidth / 2) - ballTravel, BallWidth, BallWidth)
        ' start the refresh timer
        MainTimer.Enabled = True

    End Sub

    ' Adds a row of blocks to the top of the screen
    Sub Create_Top_Row()
        TotalRows = TotalRows + 1
        Dim Block_Count = 0
        For i As Integer = 0 To BlockColumns - 1
            Dim tmp = Rnd()
            If tmp < blockChance Then
                blocks(0, i).SetInitialHitpoints(TotalRows, gameDifficulty)

            Else
                blocks(0, i).SetInitialHitpoints(0, gameDifficulty)
            End If
            ' If the block is enabled then add one to the block count
            If blocks(0, i).Visible Then
                Block_Count = Block_Count + 1
            End If
        Next

        ' Ensures that there is at least one block in the row
        If Block_Count = 0 Then
            ' No block in the row so add one at a random position
            Dim col As Integer = Rnd() * Block_Count
            blocks(0, col).SetInitialHitpoints(TotalRows, gameDifficulty)
        End If

    End Sub

    ' Move the rows down one row
    Sub Move_Rows_Down()
        ' copy the rows starting from the last row
        For row As Integer = BlockRows - 1 To 1 Step -1
            For column As Integer = 0 To BlockColumns - 1
                ' Move the row down one
                blocks(row, column).CopyData(blocks(row - 1, column))
            Next
        Next
    End Sub

    ' sets the difficulty for the game
    Public Sub Set_Difficulty(difficulty As Difficulty)
        If difficulty = Difficulty.Easy Then
            blockChance = easyBlockChance
        Else
            blockChance = difficultBlockChance
        End If
        gameDifficulty = difficulty
    End Sub

    ' has the game been lost (ie have the blocks reached the bottom row of the screen)
    Function IsGameLost() As Boolean
        Dim Lost As Boolean = False
        ' Check whether there are any blocks left in th lowest row
        For column As Integer = 0 To BlockColumns - 1
            If blocks(BlockRows - 1, column).Visible Then
                Lost = True
                Exit For
            End If
        Next
        Return Lost
    End Function

    ' Draws the positions of where the blocks could be on the screen (grid)
    Public Sub Create_Block_Positions(ByRef blocks As Block(,))

        Dim yOffset As Integer = 80

        For row As Integer = 0 To BlockRows
            Dim xOffset As Integer = BlockSpacing + ScreenLeft
            'width between first block and edge of form
            For col As Integer = 0 To BlockColumns - 1
                blocks(row, col) = New Block(New Rectangle(xOffset, yOffset, BlockWidth, BlockHeight), 0)
                xOffset += BlockWidth + BlockSpacing
            Next
            yOffset += BlockHeight + BlockSpacing
        Next

    End Sub

    ' Mouse Click
    Private Sub BallsForm_MouseClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseClick

        ' only update the ball if the the balls have not been fired
        If Not BallFired Then
            balls(0).x_increment = Math.Cos(Angle) * ballTravel
            balls(0).y_increment = Math.Sin(Angle) * ballTravel

            balls(0).state = BallState.InPlay
            ' initialise all other balls to the same starting position and launch angle as the first ball
            For i As Integer = 1 To balls.Length() - 1
                balls(i).ball = balls(0).ball
                balls(i).x_increment = balls(0).x_increment
                balls(i).y_increment = balls(0).y_increment
                balls(i).state = BallState.Not_Fired
            Next i
            BallFired = True
            NumBallsToAdd = 0
        End If
    End Sub

    Private Sub BallsForm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        ' Create solid brush.
        Dim ballBrush As New SolidBrush(Settingshandler.CurrentSettings.ballcolour)
        Dim specialBrush As New SolidBrush(Settingshandler.CurrentSettings.specialblockcolor)
        Dim blockBrush As New SolidBrush(Settingshandler.CurrentSettings.blockcolor)
        ' Dim blackPen As New Pen(Color.Black)
        Dim blockFont As Font
        Dim blockTextBrush As Brush

        Dim ScreenRectangle As Rectangle = New Rectangle(ScreenLeft, ScreenTop, (ScreenRight - ScreenLeft), (ScreenBottom - ScreenTop))
        e.Graphics.DrawRectangle(New Pen(Settingshandler.CurrentSettings.ballcolour), ScreenRectangle)

        ' Create location and size of ellipse.
        Dim x As Integer = Centre - 15
        Dim y As Integer = (ScreenBottom - 15)

        ' Draw semi circle.
        e.Graphics.FillPie(ballBrush, x, y, 30, 30, 180, 180)

        'draw enabled blocks
        For row As Integer = 0 To BlockRows
            For column As Integer = 0 To BlockColumns - 1
                If blocks(row, column).Visible Then
                    If (blocks(row, column).Get_Special_Item = Block.SpecialItem.ExtraBall) Then
                        e.Graphics.FillRectangle(specialBrush, blocks(row, column).Position)
                        ' set the text for the block to the inverse colour of the block
                        blockTextBrush = New Drawing.SolidBrush(Color.FromArgb(Settingshandler.CurrentSettings.specialblockcolor.ToArgb() Xor &HFFFFFF))
                    Else
                        e.Graphics.FillRectangle(blockBrush, blocks(row, column).Position)
                        ' set the text for the block to the inverse colour of the block
                        blockTextBrush = New Drawing.SolidBrush(Color.FromArgb(Settingshandler.CurrentSettings.blockcolor.ToArgb() Xor &HFFFFFF))
                    End If
                    e.Graphics.DrawRectangle(New Pen(Color.White), blocks(row, column).Position)

                    If blocks(row, column).Hitpoints > 999 Then
                        blockFont = New System.Drawing.Font("Verdana", 8)
                    ElseIf blocks(row, column).Hitpoints > 99 Then
                        blockFont = New System.Drawing.Font("Verdana", 10)
                    Else
                        blockFont = New System.Drawing.Font("Verdana", 14)
                    End If
                    Dim StringFormat As StringFormat = New StringFormat()
                    StringFormat.Alignment = StringAlignment.Center
                    StringFormat.LineAlignment = StringAlignment.Center
                    e.Graphics.DrawString(blocks(row, column).Hitpoints, blockFont, blockTextBrush, blocks(row, column).Position, StringFormat)
                    blocks(row, column).redrawn()
                End If
            Next
        Next

        'draw ball
        For i As Integer = 0 To NumBalls - 1
            If balls(i).state = BallState.InPlay Or (i = 0 And balls(0).state = BallState.Not_Fired) Then
                e.Graphics.FillEllipse(ballBrush, balls(i).ball)
            End If
        Next i
    End Sub

    Private Sub BallsForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            'Checks if the escape key is pressed
            Escape.Show()
            'If the escape key is pressed shows the escape options form
        Else
            balls(0).state = BallState.InPlay
            ' initialise all other balls to the same starting position and launch angle as the first ball
            For i As Integer = 1 To balls.Length() - 1
                balls(i).ball = balls(0).ball
                balls(i).x_increment = balls(0).x_increment
                balls(i).y_increment = balls(0).y_increment
                balls(i).state = BallState.Not_Fired
            Next i

            BallFired = True
            NumBallsToAdd = 0
        End If
    End Sub

    Private Sub BallsForm_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If e.X > 0 And e.X < Me.Width Then
            Dim X_Distance As Integer = e.X - Centre
            Dim Y_Distance As Integer = e.Y - ScreenBottom

            If balls(0).state = BallState.Not_Fired Then
                Angle = Math.Atan(X_Distance / Y_Distance)
                Angle = -Angle

                'Dont allow the ball to be fired 15 degrees of the horizontal
                Const AngleStop As Single = 15 * Math.PI / 180

                If Angle > (Math.PI / 2) - AngleStop Then
                    Angle = (Math.PI / 2) - AngleStop
                End If

                If Angle < -(Math.PI / 2) + AngleStop Then
                    Angle = -(Math.PI / 2) + AngleStop
                End If

                'Changing angle so 0 radians points upward
                Angle = Angle - Math.PI / 2

                balls(0).x_increment = Math.Cos(Angle) * 20
                balls(0).y_increment = Math.Sin(Angle) * 20

                balls(0).ball.X = Centre - (BallWidth / 2) + balls(0).x_increment
                balls(0).ball.Y = ScreenBottom - (BallWidth / 2) + balls(0).y_increment
            End If
        End If
    End Sub

    Private Sub MainTimer_Tick(sender As Object, e As EventArgs) Handles MainTimer.Tick

        If BallFired Then
            Dim Started_Next_Ball As Boolean = False
            For i As Integer = 0 To NumBalls - 1

                ' create the new position in the current direction of travel
                If balls(i).state = BallState.InPlay Then
                    balls(i).old_position = balls(i).ball.Location
                    balls(i).ball.Location = New Point(balls(i).ball.X + balls(i).x_increment, balls(i).ball.Y + balls(i).y_increment)
                ElseIf Not Started_Next_Ball And Not i = 0 And balls(i).state = BallState.Not_Fired Then
                    balls(i).state = BallState.InPlay
                    Started_Next_Ball = True
                End If

                ' check that the ball hasn't gone off the left hand edge of the screen
                If balls(i).ball.X < ScreenLeft Then
                    ' the ball has gone off the left hand side of the screen so change the 
                    ' direction to move towards the right and limit the position
                    balls(i).ball.X = ScreenLeft
                    balls(i).x_increment = -balls(i).x_increment
                End If

                ' check that the ball hasn't gone off the right hand edge of the screen
                If balls(i).ball.X + balls(i).ball.Width > ScreenRight Then
                    ' the ball has gone off the right hand side of the screen so change the 
                    ' direction to move towards the left and limit the position
                    balls(i).ball.X = ScreenRight - balls(i).ball.Width
                    balls(i).x_increment = -balls(i).x_increment
                End If

                ' check that the ball hasn't gone off the top of the screen
                If balls(i).ball.Y < ScreenTop Then
                    ' the ball has gone off the top of the screen so change the 
                    ' direction to move downwards
                    balls(i).ball.Y = ScreenTop
                    balls(i).y_increment = -balls(i).y_increment
                End If

                ' check that the ball hasn't gone off the bottom of the screen
                '       If ballFired And balls(0).ball.Y > ScreenBottom - balls(0).ball.Height Then
                If balls(i).state = BallState.InPlay And balls(i).ball.Y > ScreenBottom - balls(i).ball.Height Then
                    ' the ball has gone off the bottom of the screen so change the 
                    ' direction to move back to the centre

                    balls(i).ball.X = Centre - (BallWidth / 2)

                    balls(i).ball.Y = ScreenBottom - (BallWidth / 2) - 20
                    balls(i).y_increment = -balls(i).y_increment

                    balls(i).state = BallState.Finished
                End If

                If balls(i).state = BallState.InPlay Then
                    For rows As Integer = 0 To BlockRows
                        For columns As Integer = 0 To BlockColumns - 1
                            If blocks(rows, columns).Visible Then
                                Dim hit As Block.HitType = blocks(rows, columns).HitByBall(balls(i))
                                If hit = Block.HitType.NotHit Then
                                    ' nothing to do
                                Else
                                    'There is a hit on the block
                                    blocks(rows, columns).DecrementHitpoints()

                                    If blocks(rows, columns).Hitpoints = 0 Then
                                        If Settingshandler.CurrentSettings.soundOn Then
                                            sp.Play()
                                        End If
                                        Select Case blocks(rows, columns).Get_Special_Item
                                            Case Block.SpecialItem.None
                                            Case Block.SpecialItem.ExtraBall
                                                NumBallsToAdd = NumBallsToAdd + 1
                                        End Select

                                    End If
                                    'Adds one to the score each time a block is hit
                                    score = score + 1

                                    Select Case hit
                                        Case Block.HitType.HitBottom
                                            balls(i).y_increment = -balls(i).y_increment
                                        Case Block.HitType.HitTop
                                            balls(i).y_increment = -balls(i).y_increment
                                        Case Block.HitType.HitLeft
                                            balls(i).x_increment = -balls(i).x_increment
                                        Case Block.HitType.HitRight
                                            balls(i).x_increment = -balls(i).x_increment
                                    End Select
                                    Exit For
                                End If
                            End If
                        Next columns
                    Next rows
                End If
            Next i

            Dim Ball_Still_in_Play As Boolean = False
            For ball As Integer = 0 To NumBalls - 1
                If balls(ball).state = BallState.InPlay Then
                    Ball_Still_in_Play = True
                    Exit For
                End If
            Next

            If Not Ball_Still_in_Play Then
                BallFired = False
                ' Add any extra ball received during play
                NumBalls = NumBalls + NumBallsToAdd
                If NumBalls > balls.Length() Then
                    NumBalls = balls.Length()
                End If
                NumBallsToAdd = 0
                balls(0).state = BallState.Not_Fired
                If IsGameLost() Then
                    Loss.AddScore(score, TotalRows, currentDifficulty)
                    Loss.Show()
                    Me.Close()
                Else
                    Move_Rows_Down()
                    Create_Top_Row()
                End If
            End If

        End If
        Me.Refresh()
    End Sub

End Class