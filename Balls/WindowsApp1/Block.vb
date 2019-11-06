Public Class Block
    Private BlockPosition As Rectangle
    Property BlockHitpoints As Integer
    Private NeedsRedraw As Boolean = False

    Public Enum SpecialItem
        None
        ExtraBall
    End Enum

    Public Enum HitType
        NotHit
        HitTop
        HitBottom
        HitLeft
        HitRight
    End Enum

    Property Special As SpecialItem = SpecialItem.None

    Public Function Get_Special_Item()
        Return special
    End Function

    Public Sub New(ByVal Position As Rectangle, ByVal hitpoints As Integer)
        BlockPosition = Position
        BlockHitpoints = hitpoints
    End Sub

    ' return the position of the block
    Public Function Position() As Rectangle
        Return BlockPosition
    End Function

    'Set the Position of the Block
    Public Sub SetPosition(Position As Rectangle)
        BlockPosition = Position
    End Sub

    ' function that returns whether the object is visible
    Public Function Visible() As Boolean
        Return BlockHitpoints > 0
    End Function


    ' function that returns the remaining hitpoints for the object
    Public Function Hitpoints() As Integer
        Return BlockHitpoints
    End Function

    ' Procedure to set the initial Hitpoints for the object
    Public Sub SetInitialHitpoints(Hitpoints As Integer, difficulty As GameScreen.Difficulty)
        BlockHitpoints = Hitpoints

        Dim specialBlockChance As Single

        If difficulty = GameScreen.Difficulty.Easy Then
            specialBlockChance = 0.2
        Else
            specialBlockChance = 0.1
        End If

        If Rnd() < specialBlockChance Then
            special = SpecialItem.ExtraBall
        Else
            special = SpecialItem.None
        End If

    End Sub

    ' Procedure to set the initial Hitpoints for the object
    Public Sub DecrementHitpoints()
        BlockHitpoints = BlockHitpoints - 1
        NeedsRedraw = True
    End Sub

    ' has the ball hit this object
    Public Function HitByBall(ByRef ball As GameScreen.Ball) As HitType
        Dim result As HitType = HitType.NotHit

        If BlockPosition.IntersectsWith(ball.ball) Then
            'Working out the difference between the right hand of the block and the left side of the ball
            Dim RightError As Integer = Math.Abs((BlockPosition.X + BlockPosition.Width) - ball.ball.X)
            'Working out the difference between the left hand of the block and the right side of the ball
            Dim LeftError As Integer = Math.Abs(BlockPosition.X - (ball.ball.X + ball.ball.Width))
            'Working out the difference between the top of the block and the bottom of the ball
            Dim TopError As Integer = Math.Abs(BlockPosition.Y - (ball.ball.Y + ball.ball.Height))
            'Working out the difference between the bottom of the block and the top of the ball
            Dim BottomError As Integer = Math.Abs((BlockPosition.Y + BlockPosition.Height) - ball.ball.Y)
            Dim errors = New Integer() {RightError, LeftError, TopError, BottomError}
            Dim smallesterror As Integer = errors(0)
            Dim index As Integer = 0
            For i As Integer = 1 To 3
                If errors(i) < smallesterror Then
                    index = i
                    smallesterror = errors(i)
                End If
            Next
            If index = 0 Then
                'If (ball.old_position.X + ball.ball.Width > BlockPosition.X) Then
                '    '               If Math.Abs(Math.Round(ball.x_increment)) < RightError Then
                '    If ball.y_increment > 0 Then
                '        ball.ball.Y = ball.ball.Y - (2 * TopError)
                '        result = HitType.HitTop
                '    Else
                '        ball.ball.Y = ball.ball.Y + (2 * BottomError)
                '        result = HitType.HitBottom
                '    End If
                'Else
                ball.ball.X = ball.ball.X + (2 * RightError)
                    result = HitType.HitRight
                '               End If
            ElseIf index = 1 Then
                'If (ball.old_position.X < BlockPosition.X + BlockPosition.Width) Then
                '    'If Math.Abs(Math.Round(ball.x_increment)) < LeftError Then
                '    If ball.y_increment > 0 Then
                '        ball.ball.Y = ball.ball.Y - (2 * TopError)
                '        result = HitType.HitTop
                '    Else
                '        ball.ball.Y = ball.ball.Y + (2 * BottomError)
                '        result = HitType.HitBottom
                '    End If
                'Else
                ball.ball.X = ball.ball.X - (2 * LeftError)
                    result = HitType.HitLeft
                '              End If
            ElseIf index = 2 Then
                'If (ball.old_position.Y + ball.ball.Height < BlockPosition.Y) Then
                '    ' If Math.Abs(Math.Round(ball.y_increment)) < TopError Then
                '    If ball.x_increment > 0 Then
                '        ball.ball.X = ball.ball.X + (2 * RightError)
                '        result = HitType.HitRight
                '    Else
                '        ball.ball.X = ball.ball.X - (2 * LeftError)
                '        result = HitType.HitLeft
                '    End If
                'Else
                ball.ball.Y = ball.ball.Y - (2 * TopError)
                    result = HitType.HitTop
                '           End If
            ElseIf index = 3 Then
                'If (ball.old_position.Y > BlockPosition.Y + BlockPosition.Height) Then
                '    'If Math.Abs(Math.Round(ball.y_increment)) < BottomError Then
                '    If ball.x_increment > 0 Then
                '        ball.ball.X = ball.ball.X + (2 * RightError)
                '        result = HitType.HitRight
                '    Else
                '        ball.ball.X = ball.ball.X - (2 * LeftError)
                '        result = HitType.HitLeft
                '    End If
                'Else
                ball.ball.Y = ball.ball.Y + (2 * BottomError)
                    result = HitType.HitBottom
                '                End If
            End If
        End If
        Return result
    End Function
    Public Function NeedsRefresh() As Boolean
        Return NeedsRedraw
    End Function

    Public Sub redrawn()
        NeedsRedraw = False
    End Sub

    Public Sub CopyData(newBlock As Block)
        BlockHitpoints = newBlock.BlockHitpoints
        NeedsRedraw = True
        special = newBlock.special
    End Sub
End Class
