Imports System.Xml
Imports Microsoft.VisualBasic.CallType

Public Class starmap
    Public stars As List(Of star)

    Sub New()
        Randomize()
        If stars Is Nothing Then stars = New List(Of star)
    End Sub

    'generators create stars and planets and writes them into the appropriate XML file.  Note: they do NOT load the object into memroy
    Public Sub generateStarmap(ByRef starmapOptions As starmapOptions)
        ' create a new starmap and write it to starmap.xml
        ' also writes the hashdata for starmap.xml into hashStarmap.txt
        ' galaxySize = min number of times systemSupply will be repopped

        'generate playerinfo and agents first
        generatePlayerInfo(starmapOptions)
        generateAgents(starmapOptions)

        'initialise xwrt
        Dim xwrt As New XmlTextWriter(starmapFilename, System.Text.Encoding.UTF8)
        xwrt.WriteStartDocument(True)
        xwrt.Formatting = Formatting.Indented
        xwrt.Indentation = 2
        xwrt.WriteStartElement("starmap")

        'generate the galaxy's stars
        While starmapOptions.galaxySize > 0
            generateStar(xwrt, starmapOptions)
        End While

        xwrt.WriteEndElement()  '/starmap
        xwrt.WriteEndDocument()
        xwrt.Close()
        xwrt = Nothing

        'store starmap hash into hashstarmap.txt
        Dim txtFxn As New sharedHashFunctions
        txtFxn.addHashFile("starmap")
        txtFxn.addHashFile("agents")
        txtFxn.addHashFile("playerinfo")
        txtFxn = Nothing
    End Sub
    Private Sub generateStar(ByRef xwrt As XmlTextWriter, ByRef starmapOptions As starmapOptions)
        Dim starName As String = randomStarName()
        Dim blackholes As Integer = Int(starmapOptions.galaxySize / 2)

        xwrt.WriteStartElement("star")
        xwrt.WriteAttributeString("name", starName)
        xwrt.WriteAttributeString("location", randomStarXY())

        If blackholes > 0 AndAlso starmapOptions.blackholes = True AndAlso Int(Rnd() * 100 + 1) > 86 Then
            ' Generate blackhole (10% chance) if menu is ticked
            ' Can generate a maximum number of blackholes = galaxySize/2 (rounded down)
            xwrt.WriteAttributeString("type", "Blackhole")
            blackholes -= 1
        Else
            ' Generate normal star
            Dim planetNumber As Integer = 1
            Dim starSize As Integer = Int(Rnd() * 4) + 6    'measures total planetSize in star
            xwrt.WriteAttributeString("type", randomStarType(starSize))
            While starSize > 0
                generatePlanet(xwrt, starmapOptions.galaxySize, starSize, planetNumber, starName)
            End While
            repopSystemPlanetLocationList()
        End If

        xwrt.WriteEndElement()
    End Sub
    Private Sub generatePlanet(ByRef xwrt As XmlTextWriter, ByRef galaxySize As Integer, ByRef starSize As Integer, ByRef planetNumber As Integer, ByVal starName As String)
        Dim planetLocation As Integer = randomPlanetLocation()
        Dim planetSize As Integer = randomPlanetSize(starSize)
        Dim planetPrefix As String
        Dim planetSuffix As String = randomPlanetSuffix()
        Dim planetGovernment As String = randomPlanetGovernment()
        Dim planetHabitation As String = randomPlanetHabitation(planetSuffix)
        Dim planetSupply As New List(Of String)
        Dim planetDemand As New List(Of String)

        'Generate prefix and supply
        Dim tempPlanetSupply As String = randomPlanetSupply(galaxySize)       ' pull random supply from remaining systemSupply list
        If tempPlanetSupply = "Tourist" Then
            planetSupply.Add("None")
            planetPrefix = "Tourist"
        ElseIf tempPlanetSupply = "Commercial" Then
            planetSupply.Add(defaultSystemSupply.Item(Int(Rnd() * (defaultSystemSupply.Count - 1))))    ' adds a pure random roll supply
            planetPrefix = "Commercial"
        Else
            planetSupply.Add(tempPlanetSupply)
            planetPrefix = determinePlanetPrefix(tempPlanetSupply)
        End If

        'Generate demand
        For Each demand In randomPlanetDemand(planetPrefix, planetSupply)
            planetDemand.Add(demand)
        Next

        '------

        ' Write into XML
        xwrt.WriteStartElement("planet")
        xwrt.WriteAttributeString("starName", starName)
        xwrt.WriteAttributeString("number", planetNumber)
        xwrt.WriteAttributeString("location", planetLocation)
        planetNumber += 1
        xwrt.WriteElementString("size", planetSize)
        starSize -= planetSize
        xwrt.WriteElementString("prefix", planetPrefix)
        xwrt.WriteElementString("suffix", planetSuffix)
        xwrt.WriteElementString("habitation", planetHabitation)
        xwrt.WriteElementString("government", planetGovernment)

        xwrt.WriteStartElement("supply")
        For Each supply As String In planetSupply
            xwrt.WriteElementString("good", supply)
        Next
        xwrt.WriteEndElement()  '/supply
        xwrt.WriteStartElement("demand")
        For Each demand As String In planetDemand
            xwrt.WriteElementString("good", demand)
        Next
        xwrt.WriteEndElement()  '/demand

        xwrt.WriteEndElement()  '/planet
    End Sub
    Private Sub generateAgents(ByRef starmapOptions As starmapOptions)
        'populate home with default agents

        Dim xwrt As New XmlTextWriter(agentFilename, System.Text.Encoding.UTF8)
        xwrt.WriteStartDocument(True)
        xwrt.Formatting = Formatting.Indented
        xwrt.Indentation = 2
        xwrt.WriteStartElement("agents")

        For i As Integer = 1 To 5
            Dim agentID As String = processAgentID(i)
            Dim agentName As String = randomAgentName()
            Dim agentType As String = randomAgentType(Form1.playerinfo)
            Dim agentStarName As String = "Oubliette"
            Dim agentPlanetNumber As Integer = 0

            xwrt.WriteStartElement("agent")
            xwrt.WriteAttributeString("id", agentID)
            xwrt.WriteElementString("name", agentName)
            xwrt.WriteElementString("type", agentType)
            xwrt.WriteElementString("starname", agentStarName)
            xwrt.WriteElementString("planetnumber", agentPlanetNumber)
            xwrt.WriteEndElement()  '/agent
        Next

        xwrt.WriteEndElement()        '/agents
        xwrt.WriteEndDocument()
        xwrt.Close()
        xwrt = Nothing
    End Sub
    Private Sub generatePlayerInfo(ByRef starmapoptions As starmapOptions)
        Dim xwrt As New XmlTextWriter(playerFilename, System.Text.Encoding.UTF8)
        xwrt.WriteStartDocument(True)
        xwrt.Formatting = Formatting.Indented
        xwrt.Indentation = 2
        xwrt.WriteStartElement("playerinfo")

        xwrt.WriteElementString("faction", starmapoptions.faction)

        xwrt.WriteEndElement()    '/playerinfo
        xwrt.WriteEndDocument()
        xwrt.Close()
        xwrt = Nothing
    End Sub

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
    Private Function randomPlanetSupply(ByRef galaxySize As Integer) As String
        Dim x As Integer = Int(Rnd() * (systemSupply.Count - 1))
        randomPlanetSupply = systemSupply.Item(x)
        systemSupply.RemoveAt(x)
        If systemSupply.Count = 0 Then
            repopSystemSupply()
            galaxySize -= 1
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

    Public Function grabStar(ByVal starName As String) As star
        For Each star As star In stars
            If star.name = starName Then Return star
        Next

        Return Nothing
    End Function
    Public Function grabPlanet(ByVal starName As String, ByVal planetNumber As Integer) As planet
        Dim star As star = grabStar(starName)

        If star Is Nothing Then
            'star not found due to invalid starName
            Return Nothing
        Else
            For Each planet As planet In star.planets
                If planet.number = planetNumber Then Return planet
            Next
        End If

        Return Nothing
    End Function
    Public Function grabStationedAgents(ByVal starName As String, ByVal planetNumber As Integer) As List(Of agent)
        Dim tempList As New List(Of agent)
        Dim planet As planet = grabPlanet(starName, planetNumber)
        Dim agentList As agentList = Form1.agentList

        For Each agent As agent In agentList.agents
            If agent.starName = planet.starName AndAlso agent.planetNumber = planet.number Then tempList.Add(agent)
        Next

        Return tempList
    End Function
End Class


Public Class star
    Public name As String
    Public location As Integer
    Public type As String
    Public planets As List(Of planet)

    Sub New()
        If planets Is Nothing Then planets = New List(Of planet)
    End Sub
End Class


Public Class planet
    Public starName As String
    Public number As Integer
    Public location As Integer
    Public size As Integer
    Public prefix As String
    Public suffix As String
    Public habitation As String
    Public government As String

    Public supply As List(Of String)
    Public demand As List(Of String)

    Public stationedAgents As List(Of agent)        'used only within certain methods, not globally initiated

    Sub New()
        If supply Is Nothing Then supply = New List(Of String)
        If demand Is Nothing Then demand = New List(Of String)
        If stationedAgents Is Nothing Then stationedAgents = New List(Of agent)
    End Sub

    Sub addSupply(ByVal good As String)
        If supply.Contains(good) = True Then
            ' do nothing as it already exists
        Else
            supply.Add(good)
        End If
    End Sub
    Sub addDemand(ByVal good As String)
        If demand.Contains(good) = True Then
            ' do nothing as it already exists
        Else
            demand.Add(good)
        End If
    End Sub
    Sub removeSupply(ByVal good As String)
        If supply.Contains(good) = False Then
            ' do nothing as item does not exist
        Else
            supply.Remove(good)
        End If

        If supply.Count = 0 Then supply.Add("None")
    End Sub
    Sub removeDemand(ByVal good As String)
        If demand.Contains(good) = False Then
            ' do nothing as item does not eist
        Else
            demand.Remove(good)
        End If

        If demand.Count = 0 Then demand.Add("None")
    End Sub
End Class


Public Class agentList
    Public agents As List(Of agent)

    Sub New()
        If agents Is Nothing Then agents = New List(Of agent)
    End Sub

    'all private functions with prefix really is used to call a byref to the object in question for changing
    Public Function grabAgent(ByVal agentID As String) As agent
        For Each agent As agent In agents
            If agent.id = agentID Then Return agent
        Next

        Return Nothing
    End Function
    Public Sub moveAgent(ByVal agentID As String, ByVal destinationStarName As String, ByVal destinationPlanetNumber As Integer)
        Dim agent As agent = grabAgent(agentID)
        reallyMoveAgent(agent, destinationStarName, destinationPlanetNumber)
    End Sub
    Private Sub reallyMoveAgent(ByRef agent As agent, ByVal destinationStarName As String, ByVal destinationPlanetNumber As Integer)
        agent.starName = destinationStarName
        agent.planetNumber = destinationPlanetNumber
    End Sub
End Class


Public Class agent
    ' note that this class is not stored inside each planet's StationedAgents
    ' stationedAgents only holds the ID, which is then referenced by agents.xml to find out more about the agent in question

    Public id As String
    Public name As String
    Public type As String
    Public starName As String
    Public planetNumber As Integer
End Class


Public Class playerinfo
    Public faction As String
End Class