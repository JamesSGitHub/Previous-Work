import random
import time

                    
used = ([[False,False,False,False,False,False,False,False,False,False,False,False,False],
         [False,False,False,False,False,False,False,False,False,False,False,False,False],
         [False,False,False,False,False,False,False,False,False,False,False,False,False],
         [False,False,False,False,False,False,False,False,False,False,False,False,False]])

computerlist = [0]
userlist = [0]
playerscore = [0]
computerscore = [0]

overallPlayerScore = 0
overallComputerScore = 0

def GetCard ():
    
    found = False
    while found == False:
        num1 = random.randint(1,13)
        num2 = random.randint(1,4)
        
        newcard = num1
        
        cardnum1 = ""
        cardnum2 = ""
        
        if num1 == 12:                  
            cardnum1 = "Queen"
            newcard = 10
        elif num1 == 1:
            cardnum1 = "Ace"
        elif num1 == 11:
            cardnum1 = "Jack"
            newcard = 10
        elif num1 == 13:
            cardnum1 = "King"
            newcard = 10
        else:
            cardnum1 = num1
        
        if num2 == 1:                   
            cardnum2 = "Spades"
        elif num2 == 2:
            cardnum2 = "Hearts"
        elif num2 == 3:
            cardnum2 = "Diamonds"
        elif num2 == 4:
            cardnum2 = "Clubs"

        if used[num2-1][num1-1] == False:
            print(cardnum1,'of', cardnum2)
            found = True                                        
            used[num2-1][num1-1] = True
    return newcard

def GetNextCard (currentScore, name):
    currentScore = currentScore + int(GetCard())
    print (name, "now at",currentScore, "!");
    return currentScore

def Play ():
    userscore = 0
    computerscore = 0

    time.sleep(1)
    userscore = GetNextCard (userscore, "You are")
    time.sleep(1)
    userscore = GetNextCard (userscore, "You are")
    
    
    complete = False
    while complete==False:
        time.sleep(1)
        print("Do you want to stick or twist? (s or t)")
        playermove = input()
        if playermove == 's':
            print("You stick at", userscore,"!")
            time.sleep(1)
            complete = True
            
        elif playermove == 't':
            print("You twist!")
            userscore = GetNextCard (userscore, "You are")
            if userscore > 21:
                print("You Bust!")
                complete = True
                
    print()
    computerscore = GetNextCard (computerscore, "Computer is")
    time.sleep(1)
    computerscore = GetNextCard (computerscore, "Computer is")
    time.sleep(1)
    
    complete = False
    while complete==False:
        
        if computerscore < 16:
            computerscore = GetNextCard (computerscore, "Computer is")
            time.sleep(1)
           
        elif computerscore > 21:
            print("Computer Busts!")
            time.sleep(1)
            complete = True

        elif computerscore >= 16 :
            print("Computer Sticks on ",computerscore, "!")
            time.sleep(1)
            complete = True
                        
    if userscore > computerscore:
        if userscore<22:
            print("You win!")
            result = 1
        elif computerscore <22:
            print("The computer wins!")
            result =-1
        else:
            print("Draw!")
            result =0
    elif computerscore > userscore:
        if computerscore<22:
            print("The computer wins!")
            result = -1
        elif userscore <22:
            print("You win!")
            result = 1
        else:
            print("Draw!")
            result = 0
    else:     # same scores
        print("Draw!")
        result = 0
    
    return result


Finished = False

while not Finished:
    print("Are you ready to play blackjack? (y/n)")
    playAgain = input()
    if playAgain == 'y':
        print("You will go first!")
        time.sleep(1.5)

        result = int(Play())
        if result == 1:
            overallPlayerScore += 1 
        elif result == -1:
            overallComputerScore += 1
            
        if overallPlayerScore > overallComputerScore:
            print ("You are winning", overallPlayerScore, "to",overallComputerScore)
        elif overallComputerScore > overallPlayerScore:
            print ("The Computer is winning", overallPlayerScore, "to",overallComputerScore)
        else:
            print ("It's a draw", overallPlayerScore, "all")
    else:
        Finished = True
        if overallPlayerScore > overallComputerScore:
            print ("You won", overallPlayerScore, "to",overallComputerScore)
        elif overallComputerScore > overallPlayerScore:
            print ("The Computer won", overallPlayerScore, "to",overallComputerScore)
        else:
            print ("It was a draw", overallPlayerScore, "all")
    
                



