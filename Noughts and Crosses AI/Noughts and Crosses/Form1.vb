Public Class Form1
    Private the_board(9) As String
    Dim the_board_position() As String = {"top left", "top centre", "top right", "mid left", "centre", "mid right", "bottom left", "bottom centre", "bottom right"}

    Private Const PLAYER_SYMBOL As String = "X"
    Private Const COMPUTER_SYMBOL As String = "O"
    Private Const BLANK_SYMBOL As String = ""
    Private player_turn As Boolean
    Private game_over As Boolean = False

    Function check_win(Symbol As String) As Boolean
        Dim win As Boolean = False
        If the_board(0) = Symbol And the_board(1) = Symbol And the_board(2) = Symbol Then
            win = True
        ElseIf the_board(3) = Symbol And the_board(4) = Symbol And the_board(5) = Symbol Then
            win = True
        ElseIf the_board(6) = Symbol And the_board(7) = Symbol And the_board(8) = Symbol Then
            win = True
        ElseIf the_board(0) = Symbol And the_board(3) = Symbol And the_board(6) = Symbol Then
            win = True
        ElseIf the_board(1) = Symbol And the_board(4) = Symbol And the_board(7) = Symbol Then
            win = True
        ElseIf the_board(2) = Symbol And the_board(5) = Symbol And the_board(8) = Symbol Then
            win = True
        ElseIf the_board(0) = Symbol And the_board(4) = Symbol And the_board(8) = Symbol Then
            win = True
        ElseIf the_board(2) = Symbol And the_board(4) = Symbol And the_board(6) = Symbol Then
            win = True
        End If
        Return win
    End Function

    Private Function check_draw() As Boolean
        Dim complete_board As Boolean = True
        For i As Integer = 0 To 8
            If the_board(i) = BLANK_SYMBOL Then
                complete_board = False
            End If
        Next i
        Return complete_board
    End Function

    Private Sub player_move(n)
        If Me.the_board(n) = BLANK_SYMBOL Then
            If Not game_over Then
                Me.the_board(n) = PLAYER_SYMBOL
                ListBox1.Items.Add("Player moves " & the_board_position(n))
                If Not check_win(PLAYER_SYMBOL) Then
                    game_over = check_draw()
                    If Not game_over Then
                        computer_move()
                    Else
                        display_message("It's a draw!")
                    End If
                Else
                    display_message(PLAYER_SYMBOL & " win")
                End If
                update_board()
            End If
        Else
            display_message("That move is invalid!")
        End If
    End Sub

    Private Sub computer_move()
        Dim move_made As Boolean = False
        'Check whether computer has a winning move
        move_made = winning_move(COMPUTER_SYMBOL)

        'Check whether computer can block a winning move
        If Not move_made Then
            move_made = winning_move(PLAYER_SYMBOL)
        End If

        'Take centre if avaliable
        If Not move_made Then
            If the_board(4) = BLANK_SYMBOL Then
                the_board(4) = COMPUTER_SYMBOL
                move_made = True
                ListBox1.Items.Add("Computer moves " & the_board_position(4))
            End If
        End If

        'Take corner if avaliable
        If Not move_made Then
            move_made = take_corner()
        End If

        'Take non corner (other)
        If Not move_made Then
            move_made = take_other()
        End If

        If check_win(COMPUTER_SYMBOL) Then
            game_over = True
            display_message(COMPUTER_SYMBOL & " wins!")
        End If
        If check_draw() Then
            game_over = True
            display_message("It's a draw!")
        End If

    End Sub
    Private Function take_corner() As Boolean
        Dim corner_found As Boolean = False
        If the_board(0) = PLAYER_SYMBOL And the_board(8) = PLAYER_SYMBOL Or the_board(2) = PLAYER_SYMBOL And the_board(6) = PLAYER_SYMBOL Then
            For i As Integer = 0 To 8
                Select Case i
                    Case 1, 3, 5, 7
                        If the_board(i) = BLANK_SYMBOL Then
                            the_board(i) = COMPUTER_SYMBOL
                            corner_found = True
                            ListBox1.Items.Add("Computer moves " & the_board_position(i))
                            Exit For
                        End If
                    Case Else
                End Select
            Next i
        ElseIf the_board(5) = PLAYER_SYMBOL And the_board(7) = PLAYER_SYMBOL Then
            For i As Integer = 0 To 8
                Select Case i
                    Case 8
                        If the_board(i) = BLANK_SYMBOL Then
                            the_board(i) = COMPUTER_SYMBOL
                            corner_found = True
                            ListBox1.Items.Add("Computer moves " & the_board_position(i))
                            Exit For
                        End If
                    Case Else
                End Select
            Next i
        Else
            For i As Integer = 0 To 8
                Select Case i
                    Case 0, 2, 6, 8
                        If the_board(i) = BLANK_SYMBOL Then
                            the_board(i) = COMPUTER_SYMBOL
                            corner_found = True
                            ListBox1.Items.Add("Computer moves " & the_board_position(i))
                            Exit For
                        End If
                    Case Else
                End Select
            Next i
        End If
        Return corner_found
    End Function
    Private Function take_other() As Boolean
        Dim free_space_found As Boolean = False
        For i As Integer = 0 To 8
            Select Case i
                Case 1, 3, 5, 7
                    If the_board(i) = BLANK_SYMBOL Then
                        the_board(i) = COMPUTER_SYMBOL
                        free_space_found = True
                        ListBox1.Items.Add("Computer moves " & the_board_position(i))
                        Exit For
                    End If
                Case Else
            End Select
        Next i
        Return free_space_found
    End Function
    Private Function winning_move(SYMBOL) As Boolean
        Dim result As Boolean = False
        For i As Integer = 0 To 8
            If the_board(i) = BLANK_SYMBOL Then
                the_board(i) = SYMBOL
                If check_win(SYMBOL) = True Then
                    the_board(i) = COMPUTER_SYMBOL
                    result = True
                    ListBox1.Items.Add("Computer moves " & the_board_position(i))
                    Exit For
                Else
                    the_board(i) = BLANK_SYMBOL
                End If
            End If
        Next i
        Return result
    End Function

    Private Sub display_message(message As String)
        Me.Label2.Text = message
    End Sub

    Private Sub initialise_game()
        ListBox1.Items.Clear()
        Me.game_over = False
        For i As Integer = 0 To 8
            Me.the_board(i) = BLANK_SYMBOL
        Next i
        Dim rand As Single
        Randomize()
        rand = Rnd()
        If rand >= 0.5 Then
            Me.player_turn = True
        Else
            Me.player_turn = False
            computer_move()
            Me.player_turn = True
        End If
        display_message("Players turn")
    End Sub

    Private Sub update_board()
        Me.Button1.Text = the_board(0)
        Me.Button2.Text = the_board(1)
        Me.Button3.Text = the_board(2)
        Me.Button4.Text = the_board(3)
        Me.Button5.Text = the_board(4)
        Me.Button6.Text = the_board(5)
        Me.Button7.Text = the_board(6)
        Me.Button8.Text = the_board(7)
        Me.Button9.Text = the_board(8)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        player_move(0)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        player_move(1)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        player_move(2)
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        player_move(3)
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        player_move(4)
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        player_move(5)
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        player_move(6)
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        player_move(7)
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        player_move(8)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        initialise_game()
        update_board()
    End Sub
End Class

