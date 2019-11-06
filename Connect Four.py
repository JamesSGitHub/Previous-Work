# print the current grid
def draw(grid,width,height):
    print("")
    for i in range(1,width+1):
        print(i, end=" ") #Print column headers on one row
        i = i + 1
    print("")
    for n in range(1,width+1): 
        print("|", end=" ") 
        n = n + 1
    print("")
    
    for x in range(0, height):
        for p in range (0,width):
            print(grid[x][p], end = " ")
            p = p + 1
        print("- row ", x+1)
        x = x + 1

# Adds a piece to the board
def add_piece(grid, column, row, player):
    valid_placement = False
    while not valid_placement == True:
        if row == -1:
            print("That column is full, please input in another column")
            column = int(input()) - 1
            row = height
        else:
            if grid[row][column] == 0:
                if grid[row][column] == 0:      #Checks to see if there is no piece on the board then will place
                    grid[row][column] = piece
                    valid_placement = True
                else:                           #Otherwise if there is a player piece will forfiet turn
                    print("There is already a piece there, you have forfieted your turn")
            else:
                row = row - 1
    return grid, row

# Checks whether the game has been drawn
def Check_Draw(board, width):
    for x in range(0, width):
        if board[0][x] == 0:
            return False
        x = x + 1
    return True

# Checks whether this was a winning move
def check_winner(board,height,width,piece,current_column,current_row):
    won = False
    # count contains the consecutive number of "pieces" counted
    count = 0
    # Determine whether the player has won in a vertical direction
    for y in range(height - 1, -1, -1):          #Count goes backwards
        if board[y][current_column] == piece:
            count = count + 1
            if count >= 4:
                won = True
                break
        else:
            count = 0 
        y = y + 1

    # don't check if the player has already won    
    if won == False:
        count = 0
        # Determine whether the player has won in a horizontal direction
        for x in range(0, width):
            if board[current_row][x] == piece:
                count = count + 1
                if count >= 4:
                    won = True
                    break
            else:
                count = 0 
            x = x + 1
            
    # don't check if the player has already won    
    if won == False:
        #Check for win diagonally from top left to bottom right

        #start_col and start_row are the start position for the diagonal check
        start_col = 0
        start_row = 0
        # max_checks contains the maximum number of checks required
        max_checks = 0
        count = 0
        if current_row > current_column:
            start_col = 0
            start_row = current_row - current_column
            max_checks = height
        else:
            start_row = 0
            start_col = current_column - current_row
            max_checks = width
            
        # check diagonally from start position            
        for i in range(0, max_checks-1):
            if board[start_row][start_col] == piece:
                count = count + 1
                # check whether we have 4 in a row (if it is, set won to true and exit)
                if count >= 4:
                    won = True
                    break
            else:
                count = 0
                    

            # increment to the next diagonal position 
            start_col = start_col + 1
            start_row = start_row + 1
            i = i + 1
            # Check whether there are any more diagonal moves
            if start_col == width or start_row == height:
                break
                
    # don't check if the player has already won    
    if won == False:
        #Check for win diagonally from bottom left to top right

        #start_col and start_row are the start position for the diagonal check
        start_col = 0
        start_row = 0
        # max_checks contains the maximum number of checks required
        max_checks = 0
        count = 0
        # row is inverted value of current_row (ie if current_row = height - 1, row = 0)
        # it is used to determine whether start_row or start_col should be set to 0
        row = (height - current_row - 1)
        
        if current_column > row:
            # if this is the first row then no need to change values
            if current_row == height - 1:          
               start_col = current_column
               start_row = current_row
               max_checks = height
            else:
               start_col = current_column - (height - current_row)
               start_row = current_row
               max_checks = height
        else:
            start_col = 0
            start_row = height - (row - current_column) - 1
            max_checks = width

        # check diagonally from start position            
        for i in range(0, max_checks-1):
            if board[start_row][start_col] == piece:
                count = count + 1
                # check whether we have 4 in a row (if it is, set won to true and exit)
                if count >= 4:
                    won = True
                    break
            else:
                # reset count as non "piece" detected
                count = 0

            # increment to the next diagonal position 
            start_col = start_col + 1
            start_row = start_row - 1
            i = i + 1
            # Check whether there are any more diagonal moves
            if start_col == width or start_row == 0:
                break
                
    return won


#Main Program
won = False
drawn_game = False
valid_width = False
valid_height = False
while not valid_width == True:
    print("How many columns do you want?")
    width = int(input())
    if width > 0 and width < 10:
        valid_width = True
    else:
        print("The amount of columns has to be within 0 and 9")
        
while not valid_height == True:
    print("How many row do you want?")
    height = int(input())
    if height > 0 and height < 10:
        valid_height = True
    else:
        print("The amount of rows has to be within 0 and 9")

board = [[0 for x in range(width)] for y in range(height)] 
draw(board,width,height)


player = 1
while not won == True:
    print("It is player " + str(player) + "'s go.")
    valid_rinput = False
    valid_cinput = False
    
    #Subtracts 1 to the column and row choice so that it fits the array index correctly

    while not valid_cinput == True:   #Loops until there is a valid input for the column
        print("Enter the column number. ")
        c_choice = int(input())
        if c_choice > 0 and c_choice <= width:
            valid_cinput = True
            c_choice = c_choice - 1
        else:
           print("The number for the column is out of bounds. Please input another number")

    r_choice = height - 1
    
    if player == 1:
        piece = "B"
    else:
        piece = "R"
        
    board, r_choice = add_piece(board, c_choice, r_choice, player)
    won = check_winner(board,height,width,piece,c_choice,r_choice)

    draw(board,width,height)
    if won == False:
        drawn_game = Check_Draw(board, width)
        if drawn_game == True:
            print("It's a draw!")
            won = True
        else:    
            if player == 1:            
                player = 2
            else:
                player = 1
    else:
         print("Player " + str(player) + " has won!")
