Public Class Settingshandler
    Dim namefile As System.IO.StreamReader
    Dim currentFileName As String

    Public Structure FilenameData
        Dim num_names As Integer

        ' Forced to use this assignment because you couldn't have a fixed length array inside a structure
        <VBFixedArray(20)> Dim names() As String
        Public Sub Initialize()
            ReDim names(20)
            For im As Integer = 0 To 6
                names(im) = ""
            Next im
        End Sub
    End Structure

    Private Filenames As filenameData

    Public Structure SettingsData
        Dim ballcolour As Color
        Dim blockcolor As Color
        Dim specialblockcolor As Color
        Dim soundOn As Boolean
    End Structure

    Public Shared CurrentSettings As SettingsData

    Private Sub WriteSettingsFile(settings As SettingsData, name As String)
        Dim settingsfile As System.IO.StreamWriter
        If name = "" Then
            settingsfile = My.Computer.FileSystem.OpenTextFileWriter("Default.txt", False)
        Else
            settingsfile = My.Computer.FileSystem.OpenTextFileWriter(name + ".txt", False)
        End If
        settingsfile.WriteLine(settings.ballcolour.ToArgb())
        settingsfile.WriteLine(settings.blockcolor.ToArgb())
        settingsfile.WriteLine(settings.specialblockcolor.ToArgb())
        settingsfile.WriteLine(settings.soundOn)
        settingsfile.Close()
        CurrentSettings = settings
    End Sub

    Private Sub ReadSettingsFile(name As String)
        ' should probably have some checkinh to ensure the values read in are correct
        Dim settingsfile As System.IO.StreamReader
        If name = "" Then
            settingsfile = My.Computer.FileSystem.OpenTextFileReader("Default.txt")
        Else
            settingsfile = My.Computer.FileSystem.OpenTextFileReader(name + ".txt")
        End If
        Dim tmp As Integer
        tmp = settingsfile.ReadLine()
        CurrentSettings.ballcolour = Color.FromArgb(tmp)

        tmp = settingsfile.ReadLine()
        CurrentSettings.blockcolor = Color.FromArgb(tmp)

        tmp = settingsfile.ReadLine()
        CurrentSettings.specialblockcolor = Color.FromArgb(tmp)

        CurrentSettings.soundOn = settingsfile.ReadLine()

        settingsfile.Close()
    End Sub

    Public Sub Initialise(settings As SettingsData)
        Filenames = New filenameData
        Filenames.initialize()

        If System.IO.File.Exists("Coursework.ini") Then
            Dim inifile As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader("Coursework.ini")
            ' first line contans the current filename
            currentFileName = inifile.ReadLine()

            'read remaining filename (user selectable one) Filenames
            Filenames.num_names = 0
            While Not inifile.EndOfStream
                Filenames.names(Filenames.num_names) = inifile.ReadLine()
                Filenames.num_names = Filenames.num_names + 1
            End While

            inifile.Close()

            ' read the setting data for the current selected filename
            ReadSettingsFile(currentFileName)
        Else
            Dim inifile As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter("Coursework.ini", False)
            ' write the current filename
            inifile.WriteLine("Default")
            ' write the list of selectable files (happens to be the same as the current one)
            inifile.WriteLine("Default")

            Filenames.num_names = 0
            Filenames.names(Filenames.num_names) = "Default"
            Filenames.num_names = Filenames.num_names + 1

            inifile.Close()

            ' write the default values to the default file
            WriteSettingsFile(settings, "")

        End If

    End Sub

    Public Function SelectableFilenames() As filenameData
        Return Filenames
    End Function

    Public Function SelectedFilenames() As String
        Return currentFileName
    End Function

    Public Sub SaveSettings(settings As SettingsData, name As String)

        Dim containsName As Boolean = False
        For i As Integer = 0 To Filenames.num_names - 1
            If Filenames.names(i) = name Then
                containsName = True
                Exit For
            End If
        Next i

        ' if the name is not already in the file then save the name into the file
        If Not containsName Then
            Dim inifile As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter("Coursework.ini", False)
            'Add the filename to the list
            If Filenames.num_names < 20 Then
                Filenames.names(Filenames.num_names) = name
                Filenames.num_names = Filenames.num_names + 1
            End If

            ' write the current filename first and then a list of all the filenames stored in the file
            inifile.WriteLine(name)
            For i As Integer = 0 To Filenames.num_names - 1
                inifile.WriteLine(Filenames.names(i))
            Next

            inifile.Close()
        End If

        WriteSettingsFile(settings, name)
    End Sub

    Public Function GetSettings(filename As String) As SettingsData
        currentFileName = filename
        ReadSettingsFile(filename)

        Return CurrentSettings
    End Function
End Class
