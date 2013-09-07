Imports System.Math
Imports System.IO

Module sharedStarmapFunctions
    Public starNameList As New List(Of String)
    Public defaultSystemSupply As New List(Of String)
    Public systemSupply As New List(Of String)
    Public systemXYDictionary As New Dictionary(Of Integer, Point)
    Public systemPlanetXYDictionary As New Dictionary(Of Integer, Point)
    Public systemPlanetLocationList As New List(Of Integer)
    Public systemXY As New List(Of Integer)
    Public planetSizeDictionary As New Dictionary(Of Integer, String)
    Public planetSuffixDictionary As New Dictionary(Of Integer, String)
    Public planetPrefixDictionary As New Dictionary(Of String, String)
    Public planetGovernmentDictionary As New Dictionary(Of String, Integer)
    Public romanNumeralDictionary As New Dictionary(Of Integer, String)
    Public planetTypeShortformDictionary As New Dictionary(Of String, String)

    Public Sub initialiseStarmapFunctions()
        If planetSuffixDictionary.Count = 0 Then repopPlanetSuffixDictionary()
        If planetPrefixDictionary.Count = 0 Then repopPlanetPrefixDictionary()
        If planetGovernmentDictionary.Count = 0 Then repopPlanetGovernmentDictionary()
        If romanNumeralDictionary.Count = 0 Then repopRomanNumeralDictionary()
        If planetTypeShortformDictionary.Count = 0 Then repopPlanetTypeShortformDictionary()
        If starNameList Is Nothing Then starNameList = New List(Of String)
        If starNameList.Count = 0 Then repopStarNameList()
        If defaultSystemSupply Is Nothing Then defaultSystemSupply = New List(Of String)
        If defaultSystemSupply.Count = 0 Then repopDefaultSystemSupply()
        If planetSizeDictionary.Count = 0 Then repopPlanetSizeDictionary()
        If systemPlanetXYDictionary.Count = 0 Then repopSystemPlanetXYDictionary()
        If systemPlanetLocationList Is Nothing Then systemPlanetLocationList = New List(Of Integer)
        If systemPlanetLocationList.Count = 0 Then repopSystemPlanetLocationList()
        If systemXYDictionary Is Nothing Then systemXYDictionary = New Dictionary(Of Integer, Point)
        If systemXYDictionary.Count = 0 Then repopSystemXYDictionary()
        If systemXY Is Nothing Then repopSystemXY()
        If systemSupply Is Nothing Then systemSupply = New List(Of String)
        If systemSupply.Count = 0 Then repopSystemSupply()
    End Sub

    'repops repopulate certain lists
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
    Public Sub repopSystemXY()
        'systemXY holds a list of integers from 0 to 35, each representing a location on the starmap
        'entries from this list will be removed as each planet is added
        'in the XML entry only the location integer will be stored; cross-reference with systemXYDictionary to get actual point

        systemXY.Clear()
        For i = 0 To systemXYDictionary.Count - 1 Step 1
            systemXY.Add(i)
        Next
    End Sub
    Private Function repopDefaultStarnameList() As List(Of String)
        Dim defaultNameList As New List(Of String)

        defaultNameList.Add("Nyx")  ' empty entry to escape index 0

        defaultNameList.Add("Wotan")
        defaultNameList.Add("Froh")
        defaultNameList.Add("Sleipnir")
        defaultNameList.Add("Sleipnir")
        defaultNameList.Add("Erda")
        defaultNameList.Add("Nibelung")
        defaultNameList.Add("Fricka")
        defaultNameList.Add("Freia")
        defaultNameList.Add("Donner")
        defaultNameList.Add("Loge")

        defaultNameList.Add("Themis")
        defaultNameList.Add("Hyperion")
        defaultNameList.Add("Coeus")
        defaultNameList.Add("Cronus")
        defaultNameList.Add("Crius")
        defaultNameList.Add("Iapetus")
        defaultNameList.Add("Mnemosyne")
        defaultNameList.Add("Tethys")
        defaultNameList.Add("Theia")
        defaultNameList.Add("Phoebe")

        defaultNameList.Add("Enlil")
        defaultNameList.Add("Ninlil")
        defaultNameList.Add("Dilmun")
        defaultNameList.Add("Nanna")
        defaultNameList.Add("Ningal")
        defaultNameList.Add("Inanna")
        defaultNameList.Add("Utu")
        defaultNameList.Add("Anu")
        defaultNameList.Add("Ninhursag")
        defaultNameList.Add("Enki")

        Return defaultNameList
    End Function
    Public Sub repopStarNameList()
        'names are removed from starNameList as they are assigned to stars
        'must have at least 30 entries; if not repop with own stuff
        If system.IO.file.exists("starnames.txt") = False Then file.create("starnames.txt").dispose()

        Using txtr As streamreader = New StreamReader("starnames.txt")
            If txtr.ReadLine <> Nothing Then starNameList.Add(txtr.ReadLine)
        End Using

        If starnamelist.count < 30 Then
            Dim defaultStarnameList As List(Of String) = repopDefaultStarnameList()
            For i = 1 To (30 - starnamelist.count)
                starNameList.Add(defaultStarnameList(i))
            Next i
        End If
    End Sub
    Private Sub repopPlanetSizeDictionary()
        'translates size into description

        planetSizeDictionary.Add(1, "Tiny")
        planetSizeDictionary.Add(2, "Small")
        planetSizeDictionary.Add(3, "Medium")
        planetSizeDictionary.Add(4, "Large")
        planetSizeDictionary.Add(5, "Massive")
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
    Public Sub repopSystemPlanetLocationList()
        'systemPlanetLocationList holds the list of unused planet locations
        'repop copies systemplanetxydictionary numbers over

        systemPlanetLocationList.Clear()

        For i = 0 To (systemPlanetXYDictionary.Count - 1) Step 1
            systemPlanetLocationList.Add(i)
        Next
    End Sub
    Private Sub repopPlanetSuffixDictionary()
        planetSuffixDictionary.Add(1, "Sprawl")
        planetSuffixDictionary.Add(2, "Wasteland")
        planetSuffixDictionary.Add(3, "Eden")
        planetSuffixDictionary.Add(4, "Barren")
        planetSuffixDictionary.Add(5, "Ocean")
        planetSuffixDictionary.Add(6, "Desert")
        planetSuffixDictionary.Add(7, "Volcanic")
        planetSuffixDictionary.Add(8, "Gaseous")
    End Sub
    Private Sub repopPlanetPrefixDictionary()
        'converts supply into prefix

        planetPrefixDictionary.Add("Metal", "Mining")
        planetPrefixDictionary.Add("Chemicals", "Mining")
        planetPrefixDictionary.Add("Weapons", "Industrial")
        planetPrefixDictionary.Add("Electronics", "Industrial")
        planetPrefixDictionary.Add("Blueprints", "Research")
        planetPrefixDictionary.Add("Savants", "Research")
        planetPrefixDictionary.Add("Azoth", "Prison")
        planetPrefixDictionary.Add("Slaves", "Prison")
        planetPrefixDictionary.Add("Food", "Agrarian")
        planetPrefixDictionary.Add("Plants", "Agrarian")
        planetPrefixDictionary.Add("Media", "Cultural")
        planetPrefixDictionary.Add("Art", "Cultural")
    End Sub
    Private Sub repopDefaultSystemSupply()
        'defaultSystemSupply holds the entire list of supplies
        'must always be called AFTER repopPlanetPrefixDictionary as supplylist is dependent on that

        defaultSystemSupply.Clear()
        For Each entry As KeyValuePair(Of String, String) In planetPrefixDictionary
            If defaultSystemSupply.Contains(entry.Key) = False Then defaultSystemSupply.Add(entry.Key)
        Next
    End Sub
    Public Sub repopSystemSupply()
        'systemSupply holds a running list of supplies that is removed every time a planet is added with that supply
        'repopping it will cause it to take on everything in defaultSystemSupply again

        systemSupply.Clear()
        For x = 0 To (defaultSystemSupply.Count - 1)
            systemSupply.Add(defaultSystemSupply.Item(x))
        Next x
        systemSupply.Add("Tourist")         'Supply nothing, import +1
        systemSupply.Add("Commercial")      'Supply random item
    End Sub
    Private Sub repopPlanetGovernmentDictionary()
        ' holds types of governments (key) and probabilities (value)
        ' if they do not add up to 100 then the roll will be based on total; they should add up to 100 though

        planetGovernmentDictionary.Add("Autocracy", 30)
        planetGovernmentDictionary.Add("Oligarchy", 30)
        planetGovernmentDictionary.Add("Democracy", 23)
        planetGovernmentDictionary.Add("Anarchy", 17)
    End Sub
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


    'crawlers search for certain data
End Module
