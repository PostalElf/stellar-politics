Imports System.Math

Module universalFunctions
    Public romanNumeralDictionary As New Dictionary(Of Integer, String)
    Public systemXYDictionary As New Dictionary(Of Integer, Point)
    Public systemPlanetXYDictionary As New Dictionary(Of Integer, Point)
    Public planetTypeShortformDictionary As New Dictionary(Of String, String)


    Public Sub initialiseUniversalFunctions()
        If romanNumeralDictionary.Count = 0 Then repopRomanNumeralDictionary()
        If systemXYDictionary Is Nothing Then systemXYDictionary = New Dictionary(Of Integer, Point)
        If systemXYDictionary.Count = 0 Then repopSystemXYDictionary()
        If systemPlanetXYDictionary.Count = 0 Then repopSystemPlanetXYDictionary()
    End Sub


    'repop
    Private Sub repopRomanNumeralDictionary()
        romanNumeralDictionary.Add(1, "I")
        romanNumeralDictionary.Add(2, "II")
        romanNumeralDictionary.Add(3, "III")
        romanNumeralDictionary.Add(4, "IV")
        romanNumeralDictionary.Add(5, "V")
        romanNumeralDictionary.Add(6, "VI")
        romanNumeralDictionary.Add(7, "VII")
        romanNumeralDictionary.Add(8, "VIII")
        romanNumeralDictionary.Add(9, "IX")
        romanNumeralDictionary.Add(10, "X")
    End Sub
    Private Sub repopSystemPlanetXYDictionary()
        'holds the X and Y coordinates of the planet in the star screen
        'are not removed when added to a star! Remove entry from systemplanetlocationlist instead

        systemPlanetXYDictionary.Add(0, New Point(60, 176))
        systemPlanetXYDictionary.Add(1, New Point(154, 87))
        systemPlanetXYDictionary.Add(2, New Point(41, 58))
        systemPlanetXYDictionary.Add(3, New Point(214, 24))
        systemPlanetXYDictionary.Add(4, New Point(326, 46))
        systemPlanetXYDictionary.Add(5, New Point(314, 221))
        systemPlanetXYDictionary.Add(6, New Point(392, 132))
        systemPlanetXYDictionary.Add(7, New Point(455, 266))
        systemPlanetXYDictionary.Add(8, New Point(356, 313))
        systemPlanetXYDictionary.Add(9, New Point(455, 24))
    End Sub
    Private Sub repopSystemXYDictionary()
        'each star size should be about 15x15px
        'systemXY holds X,Y coords of each star on the starmap display

        systemXYDictionary.Add(0, New Point(359, 239))
        systemXYDictionary.Add(1, New Point(25, 24))
        systemXYDictionary.Add(2, New Point(6, 91))
        systemXYDictionary.Add(3, New Point(19, 161))
        systemXYDictionary.Add(4, New Point(34, 255))
        systemXYDictionary.Add(5, New Point(55, 305))
        systemXYDictionary.Add(6, New Point(75, 57))
        systemXYDictionary.Add(7, New Point(77, 161))
        systemXYDictionary.Add(8, New Point(94, 210))
        systemXYDictionary.Add(9, New Point(104, 284))
        systemXYDictionary.Add(10, New Point(90, 340))
        systemXYDictionary.Add(11, New Point(132, 6))
        systemXYDictionary.Add(12, New Point(132, 111))
        systemXYDictionary.Add(13, New Point(164, 271))
        systemXYDictionary.Add(14, New Point(173, 179))
        systemXYDictionary.Add(15, New Point(197, 40))
        systemXYDictionary.Add(16, New Point(226, 113))
        systemXYDictionary.Add(17, New Point(241, 210))
        systemXYDictionary.Add(18, New Point(213, 326))
        systemXYDictionary.Add(19, New Point(279, 293))
        systemXYDictionary.Add(20, New Point(260, 24))
        systemXYDictionary.Add(21, New Point(303, 210))
        systemXYDictionary.Add(22, New Point(482, 340))
        systemXYDictionary.Add(23, New Point(482, 195))
        systemXYDictionary.Add(24, New Point(371, 121))
        systemXYDictionary.Add(25, New Point(464, 102))
        systemXYDictionary.Add(26, New Point(444, 24))
        systemXYDictionary.Add(27, New Point(410, 326))
        systemXYDictionary.Add(28, New Point(444, 255))
        systemXYDictionary.Add(29, New Point(337, 336))
        systemXYDictionary.Add(30, New Point(410, 161))
        systemXYDictionary.Add(31, New Point(312, 141))
        systemXYDictionary.Add(32, New Point(387, 57))
        systemXYDictionary.Add(33, New Point(25, 24))
        systemXYDictionary.Add(34, New Point(359, 6))
        systemXYDictionary.Add(35, New Point(296, 78))
    End Sub


    'processors convert data into an appropriate format
    Public Function processAgentID(ByVal id As Integer) As String
        Dim processedID As String

        Select Case id
            Case 1 To 9 : processedID = "00" & id.ToString
            Case 10 To 99 : processedID = "0" & id.ToString
            Case Else : processedID = id
        End Select

        Return processedID
    End Function
    Public Function processRomanNumber(ByVal romanNumber As String) As Integer
        For Each entry As KeyValuePair(Of Integer, String) In romanNumeralDictionary
            If romanNumber = entry.Value Then Return entry.Key
        Next

        Return Nothing
    End Function
    Private Sub repopPlanetTypeShortformDictionary()
        'translates planetPrefix and planetSuffix into shortform

        planetTypeShortformDictionary.Add("Mining", "MNG")
        planetTypeShortformDictionary.Add("Industrial", "IND")
        planetTypeShortformDictionary.Add("Research", "RSH")
        planetTypeShortformDictionary.Add("Prison", "PRN")
        planetTypeShortformDictionary.Add("Agrarian", "AGR")
        planetTypeShortformDictionary.Add("Cultural", "CLT")
        planetTypeShortformDictionary.Add("Tourist", "TST")
        planetTypeShortformDictionary.Add("Commercial", "CMC")

        planetTypeShortformDictionary.Add("Sprawl", "SPL")
        planetTypeShortformDictionary.Add("Wasteland", "WST")
        planetTypeShortformDictionary.Add("Eden", "EDN")
        planetTypeShortformDictionary.Add("Barren", "BRN")
        planetTypeShortformDictionary.Add("Ocean", "OCN")
        planetTypeShortformDictionary.Add("Desert", "DST")
        planetTypeShortformDictionary.Add("Volcanic", "VLC")
        planetTypeShortformDictionary.Add("Gaseous", "GAS")
    End Sub


    'calculators
    Public Function calculateDistanceBetween(ByVal Point1 As Point, ByVal Point2 As Point) As Integer
        'use pythogaras theorem to get distance between two points

        Dim x1 As Integer = Point1.X
        Dim x2 As Integer = Point2.X
        Dim y1 As Integer = Point1.Y
        Dim y2 As Integer = Point2.Y

        Return Sqrt((Abs(x2 - x1) ^ 2) + (Abs(y2 - y1) ^ 2))
    End Function
End Module
