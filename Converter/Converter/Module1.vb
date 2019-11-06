Module Module1
    Function Bin_to_Den(binarystring, denaryvalue) As String
        'Binary conversion to denary
        Dim bit As String
        Dim bitvalue As Integer
        Dim lenstring As Integer = Len(binarystring)

        For i = 1 To Len(binarystring)
            bit = Mid(binarystring, i, 1)
            bitvalue = Int(bit)
            denaryvalue = 2 ^ (lenstring - i) * bitvalue + denaryvalue
        Next i
        Return denaryvalue
    End Function

    Function Den_to_Bin(denaryvalue)
        'Denary conversion to binary
        Dim binarystring As String = ""
        Dim power As Integer = 0
        Dim power_value As Integer
        While power_value < denaryvalue
            power_value = 2 ^ power
            power = power + 1
        End While

        'Check that power does not equal 0
        If power < 1 Then
            binarystring = "0"
        Else
            power = power - 1
            power_value = 2 ^ power

            While power >= 0
                If denaryvalue >= power_value Then
                    binarystring = binarystring + "1"
                    denaryvalue = denaryvalue - power_value
                Else
                    binarystring = binarystring + "0"
                End If
                power = power - 1
                power_value = 2 ^ power
            End While
        End If
        Return binarystring
    End Function

    Function Bin_to_Hex(binarystring)
        Dim hex_lookup(15) As String
        Dim hex_stringvalue(15) As String
        Dim hex_index As Integer
        Dim four_character_iterations As Integer
        Dim start_pos As Integer
        Dim overall_hex_string As String
        hex_lookup = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F"}
        hex_stringvalue = {"0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111"}
        four_character_iterations = Math.Ceiling(Len(binarystring) / 4)
        start_pos = Len(binarystring) - 3
        overall_hex_string = ""

        For i As Integer = 0 To four_character_iterations - 1
            Dim four_char_string As String
            If start_pos < 1 Then
                Dim number_chars As Integer
                ' Number of characters left when total length is not divisible by 4
                number_chars = Len(binarystring) Mod 4

                four_char_string = Mid(binarystring, 1, number_chars)
                ' Fills remaining string of four with zeros
                four_char_string = four_char_string.PadLeft(4, "0")

            Else
                four_char_string = Mid(binarystring, start_pos, 4)
                start_pos = start_pos - 4
            End If

            hex_index = (Array.IndexOf(hex_stringvalue, four_char_string))
            If Len(overall_hex_string) > 0 Then
                overall_hex_string = overall_hex_string.Insert(0, hex_lookup(hex_index))
            Else
                overall_hex_string = hex_lookup(hex_index)
            End If
        Next i

        Return overall_hex_string
    End Function

    Function Den_To_Hex(denaryvalue)
        Dim hex As String
        hex = Bin_to_Hex(Den_to_Bin(denaryvalue))
        Return hex
    End Function

    Sub Main()
        Dim binarystring As String
        Dim denaryvalue As Integer = 0
        Dim valid_input As Boolean = False
        Dim input As String

        Console.WriteLine("Would you like to Convert a Denary to Binary (1)")
        Console.WriteLine("Would you like to Convert a Binary to Denary (2)")
        Console.WriteLine("Would you like to Convert a Denary to Hexadecimal (3)")
        Console.WriteLine("Would you like to Convert a Binary to Hexadecimal (4)")


        While valid_input = False

            input = Console.ReadLine()

            If input = "1" Then
                Console.WriteLine("Enter the denary value")
                denaryvalue = Console.ReadLine
                valid_input = True
                Console.WriteLine(Den_to_Bin(denaryvalue))
                Console.ReadLine()

            ElseIf input = "2" Then
                Console.WriteLine("Enter the binary string")
                binarystring = Console.ReadLine
                valid_input = True
                Console.WriteLine(Bin_to_Den(binarystring, denaryvalue))
                Console.ReadLine()

            ElseIf input = "3" Then
                Console.WriteLine("Enter the denary value")
                denaryvalue = Console.ReadLine
                valid_input = True
                Console.WriteLine(Den_To_Hex(denaryvalue))
                Console.ReadLine()

            ElseIf input = "4" Then
                Console.WriteLine("Enter the binary value")
                binarystring = Console.ReadLine
                valid_input = True
                Console.WriteLine(Bin_to_Hex(binarystring))
                Console.ReadLine()

            Else
                Console.WriteLine("Please enter a valid input")
                valid_input = False

            End If
        End While

    End Sub

End Module
