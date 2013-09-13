Public Class starmapGenerator
    Public starNameList As New List(Of String)
    Public defaultSystemSupply As New List(Of String)
    Public systemSupply As New List(Of String)
    Public systemPlanetLocationList As New List(Of Integer)
    Public systemXY As New List(Of Integer)
    Public planetSizeDictionary As New Dictionary(Of Integer, String)
    Public planetSuffixDictionary As New Dictionary(Of Integer, String)
    Public planetPrefixDictionary As New Dictionary(Of String, String)
    Public planetGovernmentDictionary As New Dictionary(Of String, Integer)
    Public agentFirstnameList As New List(Of String)
    Public agentLastnameList As New List(Of String)

    Sub New()
        If planetSuffixDictionary.Count = 0 Then repopPlanetSuffixDictionary()
        If planetPrefixDictionary.Count = 0 Then repopPlanetPrefixDictionary()
        If planetGovernmentDictionary.Count = 0 Then repopPlanetGovernmentDictionary()
        If starNameList Is Nothing Then starNameList = New List(Of String)
        If starNameList.Count = 0 Then repopStarNameList()
        If defaultSystemSupply Is Nothing Then defaultSystemSupply = New List(Of String)
        If defaultSystemSupply.Count = 0 Then repopDefaultSystemSupply()
        If planetSizeDictionary.Count = 0 Then repopPlanetSizeDictionary()
        If systemPlanetLocationList Is Nothing Then systemPlanetLocationList = New List(Of Integer)
        If systemPlanetLocationList.Count = 0 Then repopSystemPlanetLocationList()
        If systemXY Is Nothing Then repopSystemXY()
        If systemSupply Is Nothing Then systemSupply = New List(Of String)
        If systemSupply.Count = 0 Then repopSystemSupply()
    End Sub

    Public Function generatePlayerInfo(ByRef starmapoptions As starmapOptions) As playerinfo
        Dim playerinfo As New playerinfo

        playerinfo.faction = starmapoptions.faction
        Dim investment As New investment
        With investment
            .name = "Oubliette"
            .starName = "Oubliette"
            .planetNumber = 0
            .wealthPerTurn = 3
        End With
        playerinfo.investments.Add(investment)

        Return playerinfo
    End Function
    Public Function generateAgents(ByRef starmapOptions As starmapOptions, ByRef playerinfo As playerinfo) As agentList
        'populate home with default agents

        Dim agentList As New agentList

        For i As Integer = 1 To 5
            Dim agent As New agent
            With agent
                .id = processAgentID(i)
                .name = randomAgentName()
                .type = randomAgentType(playerinfo)
                .starName = "Oubliette"
                .planetNumber = 0
            End With
            agentList.agents.Add(agent)
        Next

        Return agentList
    End Function
    Public Function generateStarmap(ByRef starmapOptions As starmapOptions) As starmap
        ' create a new starmap and write it to starmap.xml
        ' also writes the hashdata for starmap.xml into hashStarmap.txt
        ' galaxySize = min number of times systemSupply will be repopped

        Dim starmap As New starmap
        While starmapOptions.galaxySize > 0
            starmap.stars.Add(generateStar(starmap, starmapOptions))
        End While

        Return starmap
    End Function
    Private Function generateStar(ByRef starmap As starmap, ByRef starmapOptions As starmapOptions) As star
        Dim blackholes As Integer = Int(starmapOptions.galaxySize / 2)
        Dim star As New star

        star.name = randomStarName()
        star.location = randomStarXY()

        If blackholes > 0 AndAlso starmapOptions.blackholes = True AndAlso Int(Rnd() * 100 + 1) > 86 Then
            ' Generate blackhole (15% chance) if menu is ticked
            ' Can generate a maximum number of blackholes = galaxySize/2 (rounded down)
            star.type = "Blackhole"
            blackholes -= 1
        Else
            ' Generate normal star
            Dim planetNumber As Integer = 1
            Dim starSize As Integer = Int(Rnd() * 4) + 6    'measures total planetSize in star

            star.type = randomStarType(starSize)

            While starSize > 0
                star.planets.Add(generatePlanet(star, starmapOptions, starSize, planetNumber, star.name))
            End While

            repopSystemPlanetLocationList()     'reset systemplanetlocationlist
        End If

        Return star
    End Function
    Private Function generatePlanet(ByRef star As star, _
                                    ByRef starmapoptions As starmapOptions, _
                                    ByRef starSize As Integer, _
                                    ByRef planetNumber As Integer, _
                                    ByVal starName As String) As planet

        Dim planet As New planet

        planet.starName = starName
        planet.number = planetNumber
        planet.location = randomPlanetLocation()
        planet.size = randomPlanetSize(starSize)
        'planet.prefix is not determined here
        planet.suffix = randomPlanetSuffix()
        planet.habitation = randomPlanetHabitation(planet.suffix)
        planet.government = randomPlanetGovernment()

        'Generate prefix and supply
        Dim tempPlanetSupply As String = randomPlanetSupply(starmapoptions)       ' pull random supply from remaining systemSupply list
        If tempPlanetSupply = "Tourist" Then
            planet.supply.Add("None")
            planet.prefix = "Tourist"
        ElseIf tempPlanetSupply = "Commercial" Then
            planet.supply.Add(defaultSystemSupply.Item(Int(Rnd() * (defaultSystemSupply.Count - 1))))    ' adds a pure random roll supply
            planet.prefix = "Commercial"
        Else
            planet.supply.Add(tempPlanetSupply)
            planet.prefix = determinePlanetPrefix(tempPlanetSupply)
        End If

        'Generate demand
        For Each demand In randomPlanetDemand(planet.prefix, planet.supply)
            planet.demand.Add(demand)
        Next

        'alter starsize and planetNumber
        starSize -= planet.size
        planetNumber += 1

        Return planet
    End Function

    Private Function randomStarName() As String
        Dim x As Integer = Int(Rnd() * (starNameList.Count - 1))
        randomStarName = starNameList.Item(x)
        starNameList.RemoveAt(x)
        If starNameList.Count <= 0 Then repopStarNameList()
    End Function
    Private Function randomStarXY() As Integer
        Dim x As Integer = Int(Rnd() * (systemXYDictionary.Count - 1))
        systemXY.Remove(x)
        If systemXY.Count = 0 Then repopSystemXY()
        Return x
    End Function
    Private Function randomStarType(ByVal starsize As Integer) As String
        Dim x As Integer = Int(Rnd() * 3) + 1
        Select Case starsize
            Case 1 To 6 : If x = 3 Then Return "Red" Else Return "OrangeRed"
            Case 7 : If x = 3 Then Return "OrangeRed" Else Return "WhiteYellow"
            Case 8 : If x = 3 Then Return "WhiteYellow" Else Return "BlueWhite"
            Case 9 : If x = 3 Then Return "BlueWhite" Else Return "Blue"
            Case Else : Return Nothing
        End Select
    End Function
    Private Function randomPlanetLocation() As Integer
        Dim x As Integer = Int(Rnd() * (systemPlanetLocationList.Count - 1))
        randomPlanetLocation = systemPlanetLocationList(x)
        systemPlanetLocationList.RemoveAt(x)
    End Function
    Private Function randomPlanetSize(ByRef starSize As Integer) As Integer
        Dim x As Integer = Int(Rnd() * 100) + 1

        Select Case starSize
            Case 1 : randomPlanetSize = 1
            Case 2 : If x < 55 Then randomPlanetSize = 2 Else randomPlanetSize = 1
            Case 3
                Select Case x
                    Case 1 To 30 : randomPlanetSize = 1
                    Case 31 To 60 : randomPlanetSize = 2
                    Case Else : randomPlanetSize = 3
                End Select
            Case 4
                Select Case x
                    Case 1 To 15 : randomPlanetSize = 1
                    Case 16 To 45 : randomPlanetSize = 2
                    Case 46 To 85 : randomPlanetSize = 3
                    Case Else : randomPlanetSize = 4
                End Select
            Case Else
                Select Case x
                    Case 1 To 5 : randomPlanetSize = 1
                    Case 6 To 25 : randomPlanetSize = 2
                    Case 26 To 55 : randomPlanetSize = 3
                    Case 55 To 85 : randomPlanetSize = 4
                    Case Else : randomPlanetSize = 5
                End Select
        End Select
    End Function
    Private Function randomPlanetSuffix() As String
        Dim x As Integer = Int(Rnd() * (planetSuffixDictionary.Count)) + 1
        Return planetSuffixDictionary.Item(x)
    End Function
    Private Function randomPlanetGovernment() As String
        Dim totalPercentage As Integer = 0
        For Each government As KeyValuePair(Of String, Integer) In planetGovernmentDictionary
            totalPercentage += government.Value
        Next

        Dim x As Integer = Int(Rnd() * totalPercentage) + 1
        Dim currentPercentage As Integer = 0
        For Each government As KeyValuePair(Of String, Integer) In planetGovernmentDictionary
            currentPercentage += government.Value
            If x <= currentPercentage Then Return government.Key
        Next

        ' If nothing gets caught then just return last key
        Return planetGovernmentDictionary.Last.Key
    End Function
    Private Function randomPlanetHabitation(ByVal planetSuffix As String) As String
        Dim x As Integer = Int(Rnd() * 100 + 1)

        Select Case planetSuffix
            Case "Sprawl"
                Select Case x
                    Case 1 To 50 : Return "Hivecities"
                    Case Else : Return "Cubecities"
                End Select
            Case "Wasteland"
                Select Case x
                    Case 1 To 33 : Return "Sealed Arcologies"
                    Case 34 To 66 : Return "Underground Complex"
                    Case Else : Return "Orbital Ring"
                End Select
            Case "Eden"
                Select Case x
                    Case 1 To 33 : Return "Hivecities"
                    Case 34 To 66 : Return "Undersea Domes"
                    Case Else : Return "Cubecities"
                End Select
            Case "Barren"
                Select Case x
                    Case 1 To 33 : Return "Sealed Arcologies"
                    Case 34 To 66 : Return "Hivecities"
                    Case Else : Return "Underground Complex"
                End Select
            Case "Ocean"
                Select Case x
                    Case 1 To 50 : Return "Undersea Domes"
                    Case Else : Return "Floating Platforms"
                End Select
            Case "Desert"
                Select Case x
                    Case 1 To 33 : Return "Cubecities"
                    Case 34 To 66 : Return "Sealed Arcologies"
                    Case Else : Return "Hivecities"
                End Select
            Case "Volcanic"
                Select Case x
                    Case 1 To 50 : Return "Orbital Ring"
                    Case Else : Return "Floating Platforms"
                End Select
            Case "Gaseous"
                Select Case x
                    Case 1 To 50 : Return "Floating Platforms"
                    Case Else : Return "Orbital Ring"
                End Select
            Case Else
                MsgBox("Bugcatch!")
                Return Nothing
        End Select
    End Function
    Private Function randomPlanetSupply(ByRef starmapoptions As starmapOptions) As String
        Dim x As Integer = Int(Rnd() * (systemSupply.Count - 1))
        randomPlanetSupply = systemSupply.Item(x)
        systemSupply.RemoveAt(x)
        If systemSupply.Count = 0 Then
            repopSystemSupply()
            starmapoptions.galaxySize -= 1
        End If
    End Function
    Private Function randomPlanetDemand(ByVal planetPrefix As String, ByVal planetSupply As List(Of String)) As List(Of String)
        'initiate list
        randomPlanetDemand = New List(Of String)

        'create temporary list of all the supplies
        Dim supplyList As New List(Of String)
        For Each good In defaultSystemSupply
            supplyList.Add(good)
        Next

        'Remove the goods as per supply
        For Each supply In planetSupply
            supplyList.Remove(supply)
        Next
        Dim pair As KeyValuePair(Of String, String)
        For Each pair In planetPrefixDictionary
            If pair.Value = planetPrefix Then supplyList.Remove(pair.Key)
        Next

        'Roll for random demand and return
        Dim x As Integer = Int(Rnd() * (supplyList.Count - 1))
        If planetPrefix = "Tourist" Then
            randomPlanetDemand.Add(supplyList(x))
            supplyList.RemoveAt(x)
            x = Int(Rnd() * (supplyList.Count - 1))
            randomPlanetDemand.Add(supplyList(x))
        Else
            randomPlanetDemand.Add(supplyList(x))
        End If

        If randomPlanetDemand.Count = 0 Then randomPlanetDemand.Add("None")
    End Function
    Private Function randomAgentName() As String
        'check if namelist files exist
        If System.IO.File.Exists("agentFirstNames.txt") = False OrElse System.IO.File.Exists("agentLastNames.txt") = False Then
            MsgBox("Error!  File missing.", MsgBoxStyle.Critical, "Error!")
            Return Nothing
        End If

        'initialise namelists
        Dim firstnamelist As New List(Of String)
        For Each entry In ghostTextList("agentFirstNames.txt")
            firstnamelist.Add(entry)
        Next
        Dim lastnamelist As New List(Of String)
        For Each entry In ghostTextList("agentLastNames.txt")
            lastnamelist.Add(entry)
        Next

        Randomize()
        Dim x As Integer = Int(Rnd() * (firstnamelist.Count - 1) + 1)
        Dim y As Integer = Int(Rnd() * (lastnamelist.Count - 1) + 1)

        Return firstnamelist(x) & " " & lastnamelist(y)
    End Function
    Public Function randomAgentType(ByVal playerinfo As playerinfo) As String
        Dim x As Integer = Int(Rnd() * 3 + 1)

        Select Case playerinfo.faction
            Case "House Illys"
                Select Case x
                    Case 1 : Return "Commander"
                    Case 2 : Return "Diplomat"
                    Case 3 : Return "Espionage"
                    Case Else
                        'bugcatch
                        Return Nothing
                End Select

            Case "House Nyos"
                Select Case x
                    Case 1 : Return "Commander"
                    Case 2 : Return "Diplomat"
                    Case 3 : Return "Espionage"
                    Case Else
                        'bugcatch
                        Return Nothing
                End Select

            Case "House Sen"
                Select Case x
                    Case 1 : Return "Commander"
                    Case 2 : Return "Diplomat"
                    Case 3 : Return "Espionage"
                    Case Else
                        'bugcatch
                        Return Nothing
                End Select

            Case Else
                'bugcatch
                Return Nothing
        End Select
    End Function
    Private Function determinePlanetPrefix(ByVal planetSupply As String) As String
        If planetSupply = "None" Then Return "Tourist" Else Return planetPrefixDictionary(planetSupply)
    End Function

    'repops repopulate certain lists
    Private Sub repopSystemXY()
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
    Private Sub repopStarNameList()
        'names are removed from starNameList as they are assigned to stars
        'must have at least 30 entries; if not repop with own stuff

        'ghosttextlist grabs the entries from starnames.txt and adds them to starnamelist
        starNameList.Clear()
        For Each starName As String In ghostTextList("starnames.txt")
            starNameList.Add(starName)
        Next

        If starNameList.Count < 30 Then
            Dim defaultStarnameList As List(Of String) = repopDefaultStarnameList()
            For i = 1 To (30 - starNameList.Count)
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
    Private Sub repopSystemPlanetLocationList()
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
    Private Sub repopSystemSupply()
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
End Class
