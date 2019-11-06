
Public Class Options
    Private Declare Function waveOutSetVolume Lib "Winmm" (ByVal wDeviceID As Integer, ByVal dwVolume As Long) As Integer

    Private Declare Function waveOutGetVolume Lib "Winmm" (ByVal wDeviceID As Integer, dwVolume As Long) As Integer
    Private filehandler As Settingshandler

    Dim currentSettings As Settingshandler.SettingsData

    Dim IniFileLoaded As Boolean = False

    Public Sub Initialise()
        currentSettings.ballcolour = Color.Red
        currentSettings.blockcolor = Color.Blue
        currentSettings.specialblockcolor = Color.Green
        currentSettings.soundOn = False

        If Not IniFileLoaded Then
            filehandler = New Settingshandler()
            filehandler.Initialise(currentSettings)
            IniFileLoaded = True
        End If
        ' read in the user defined options files
        Dim setups As Settingshandler.filenameData = filehandler.SelectableFilenames()
        ComboBox1.Items.Clear()

        For i As Integer = 0 To setups.num_names - 1
            ComboBox1.Items.Add(setups.names(i))
        Next i

        ComboBox1.Text = filehandler.SelectedFilenames()
        currentSettings = filehandler.GetSettings(ComboBox1.Text)
        TextBox1.Text = ComboBox1.Text
        SetLoadedSettings(currentSettings)

    End Sub

    Private Sub Options_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Initialise()
    End Sub

    Private Sub SetLoadedSettings(currentSettings As Settingshandler.SettingsData)
        ' set the Sound On state from the settings
        CheckBox1.Checked = currentSettings.soundOn

        SetButtonColour(Button3, currentSettings.blockcolor)
        SetButtonColour(Button4, currentSettings.ballcolour)
        SetButtonColour(Button6, currentSettings.specialblockcolor)
    End Sub
    'Default colours
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text <> "" Then
            ' check whether the combo box already contains the current name
            ' if it does don't add it again
            If Not ComboBox1.Items.Contains(TextBox1.Text) Then
                ComboBox1.Items.Add(TextBox1.Text)
            End If

            ' set the name to save the file the same as the loaded one
            ComboBox1.Text = TextBox1.Text
            filehandler.SaveSettings(currentSettings, TextBox1.Text)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ColorDialog1.Color = currentSettings.blockcolor
        If (ColorDialog1.ShowDialog() = DialogResult.OK) Then
            currentSettings.blockcolor = ColorDialog1.Color
            SetButtonColour(sender, ColorDialog1.Color)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub SetButtonColour(sender As Object, colour As Color)
        ' change the colour of the button to the selected colour
        DirectCast(sender, Button).BackColor = colour
        ' chnages the text to the complimentory colour
        DirectCast(sender, Button).ForeColor = Color.FromArgb(colour.ToArgb() Xor &HFFFFFF)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ColorDialog1.Color = currentSettings.ballcolour
        If (ColorDialog1.ShowDialog() = DialogResult.OK) Then
            currentSettings.ballcolour = ColorDialog1.Color
            SetButtonColour(sender, ColorDialog1.Color)
        End If
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        currentSettings.soundOn = CheckBox1.Checked
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        currentSettings = filehandler.GetSettings(ComboBox1.Text)
        SetLoadedSettings(currentSettings)
        TextBox1.Text = ComboBox1.Text
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ColorDialog1.Color = currentSettings.ballcolour
        If (ColorDialog1.ShowDialog() = DialogResult.OK) Then
            currentSettings.specialblockcolor = ColorDialog1.Color
            SetButtonColour(sender, ColorDialog1.Color)
        End If
    End Sub

End Class