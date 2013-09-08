Imports System.IO
Imports System.Xml
Imports System.Text

Public Module xmlGhost

    '   .-.
    '  (o o) boo!
    '  | O \
    '   \   \
    '    `~~~'

    Public Const starmapFilename = "starmap.xml"

    'ghostLoaders read XML and turn them into objects
    Function ghostLoadStarmap() As starmap
        Dim starmap As New starmap

        Dim xsettings As New XmlReaderSettings
        xsettings.IgnoreWhitespace = True
        xsettings.IgnoreComments = True
        Dim xr As XmlReader = XmlReader.Create(starmapFilename, xsettings)
        While xr.Read()
            If xr.NodeType = XmlNodeType.Element AndAlso xr.Name = "star" Then
                Dim star As star = ghostLoadStar(xr)
                starmap.stars.Add(star)
            End If
        End While

        xr.Close()
        Return starmap
    End Function
    Private Function ghostLoadStar(ByRef xr As XmlReader) As star
        Dim star As New star
        star.name = xr.GetAttribute("name")
        star.location = Convert.ToInt32(xr.GetAttribute("location"))
        star.type = xr.GetAttribute("type")

        If star.type = "Blackhole" Then
            ' do nothing because blackholes have no planets
        Else
            ' Read first planet
            xr.ReadToDescendant("planet")
            star.planets.Add(ghostLoadPlanet(xr))

            ' Read subsequent planets
            While xr.ReadToNextSibling("planet") = True
                star.planets.Add(ghostLoadPlanet(xr))
            End While
        End If

        Return star
    End Function
    Private Function ghostLoadPlanet(ByRef xr As XmlReader) As planet
        Dim planet As New planet

        planet.starName = xr.GetAttribute("starName")
        planet.number = xr.GetAttribute("number")
        planet.location = xr.GetAttribute("location")
        If xr.ReadToFollowing("size") = True Then planet.size = Convert.ToInt32(xr.ReadString)
        If xr.ReadToFollowing("prefix") = True Then planet.prefix = xr.ReadString
        If xr.ReadToFollowing("suffix") = True Then planet.suffix = xr.ReadString
        If xr.ReadToFollowing("habitation") = True Then planet.habitation = xr.ReadString
        If xr.ReadToFollowing("government") = True Then planet.government = xr.ReadString

        If xr.ReadToFollowing("supply") = True Then
            xr.ReadToDescendant("good")
            While xr.Name = "good"
                planet.supply.Add(xr.ReadString)
                xr.Read()
            End While
        End If
        If xr.ReadToFollowing("demand") = True Then
            xr.ReadToDescendant("good")
            While xr.Name = "good"
                planet.demand.Add(xr.ReadString)
                xr.Read()
            End While
        End If

        If xr.ReadToFollowing("agents") = True Then
            xr.ReadToDescendant("agent")
            While xr.Name = "agent"
                planet.stationedAgents.Add(xr.ReadString)
                xr.Read()
            End While
        End If

        ' exit out to /planet; put at the end, after all sub-elements have been read
        xr.ReadEndElement()

        Return planet
    End Function
    Public Function ghostLoadAgents() As List(Of agent)
        Dim agentlist As New List(Of agent)

        Dim xsettings As New XmlReaderSettings
        xsettings.IgnoreWhitespace = True
        xsettings.IgnoreComments = True
        Dim xr As XmlReader = XmlReader.Create("agents.xml", xsettings)
        While xr.Read()
            If xr.NodeType = XmlNodeType.Element AndAlso xr.Name = "agent" Then
                Dim agent As New agent

                agent.id = xr.GetAttribute("id")
                xr.ReadToFollowing("name")
                agent.name = xr.ReadString
                xr.ReadToFollowing("type")
                agent.type = xr.ReadString

                agentlist.Add(agent)
            End If
        End While
        xr.Close()

        Return agentlist
    End Function

    'ghostInfoLoaders read XML files ending with info and return the appropriate information in string
    Function ghostInfoLoad(ByVal filename As String, ByVal rootElement As String, ByVal childElement As String) As String
        'the info XML file should not be more complex than /filename/rootElement/childElement/
        'eg. within planetinfo.xml, /planetinfo/prefix/cultural

        If System.IO.File.Exists(filename & ".xml") = False Then
            MsgBox("A file is missing!  Please redownload the package " & vbCrLf & _
                   "and extract all the files into the same folder. Do" & vbCrLf & _
                   "not edit any of the files in the folder manually.", MsgBoxStyle.Critical, "Missing File")
            Form1.Close()
            Return Nothing
        End If

        Dim xsettings As New XmlReaderSettings
        xsettings.IgnoreWhitespace = True
        xsettings.IgnoreComments = True
        Dim xr As XmlReader = XmlReader.Create(filename & ".xml", xsettings)
        Dim tempStr As String = ""
        While xr.Read()
            If xr.NodeType = XmlNodeType.Element AndAlso xr.Name = rootElement.ToLower Then
                Dim newChild As String = childElement.Replace(" ", "")      ' remove the spaces
                newChild = newChild.ToLower
                xr.ReadToDescendant(newChild)
                tempStr = xr.ReadString         ' use tempStr so as to be able to close the xmlreader
            End If
        End While

        xr.Close()
        Return tempStr
    End Function

    'ghostWriters update whatever element they are passed into the XML file
    Sub ghostWriteStar(ByRef star As star)
        'edits basic star details

        Const filename As String = "starmap.xml"

        Dim xDoc As New XmlDocument
        xDoc.Load(filename)
        Dim xpath As String = "/starmap/star[@name='" & star.name & "']"
        Dim xNode As XmlNode = xDoc.SelectSingleNode(xpath)

        If xNode Is Nothing Then
            'throw error because node is invalid
        Else
            Dim xAtt As XmlAttribute = xNode.Attributes("name")
            xAtt.Value = star.name
            xAtt = xNode.Attributes("type")
            xAtt.Value = star.type
        End If


        'save and close
        xDoc.Save(filename)
        xDoc = Nothing


        'change hash
        Dim hashFxn As New sharedHashFunctions
        hashFxn.addHashFile("starmap")
        hashFxn = Nothing
    End Sub
    Sub ghostWritePlanetBasic(ByRef planet As planet)
        'edits the basic planet details: size, prefix, suffix, habitation, government
        'does not edit supply or demand

        Const filename As String = "starmap.xml"

        Dim xDoc As New XmlDocument
        xDoc.Load(filename)
        Dim xpath As String = "/starmap/star[@name='" & planet.starName & "']/planet[@number='" & planet.number & "']"
        Dim xNode As XmlNode = xDoc.SelectSingleNode(xpath)

        If xNode Is Nothing Then
            'throw error because node is invalid
        Else
            xNode.ChildNodes(0).InnerText = planet.size
            xNode.ChildNodes(1).InnerText = planet.prefix
            xNode.ChildNodes(2).InnerText = planet.suffix
            xNode.ChildNodes(3).InnerText = planet.habitation
            xNode.ChildNodes(4).InnerText = planet.government
        End If


        'save and close
        xDoc.Save(filename)
        xDoc = Nothing


        'change hash
        Dim hashFxn As New sharedHashFunctions
        hashFxn.addHashFile("starmap")
        hashFxn = Nothing
    End Sub
    Sub ghostWritePlanetSub(ByRef planet As planet, ByVal typename As String)
        'edits planet's supply

        Const filename As String = "starmap.xml"

        Dim xDoc As New XmlDocument
        xDoc.Load(filename)
        Dim xpath As String = "/starmap/star[@name='" & planet.starName & "']/planet[@number='" & planet.number & "']/" & typename
        Dim xNode As XmlNode = xDoc.SelectSingleNode(xpath)


    End Sub

    'ghostGrabbers grab stars and planets based on search criteria
    Function ghostGrabStar(ByRef starmap As starmap, ByVal starname As String) As star
        For Each star As star In starmap.stars
            If star.name = starname Then Return star
        Next

        Return Nothing
    End Function
    Function ghostGrabPlanet(ByRef star As star, ByVal planetNumber As Integer) As planet
        For Each planet As planet In star.planets
            If planet.number = planetNumber Then Return planet
        Next

        Return Nothing
    End Function
    Function ghostGrabPlanetFromFile(ByVal starName As String, ByVal planetNumber As Integer) As planet
        'ghostLoads starmap and runs search for a particular planet based on star name and planet number

        Dim starmap As starmap = ghostLoadStarmap()
        Dim star As star = ghostGrabStar(starmap, starName)
        Return ghostGrabPlanet(star, planetNumber)
    End Function
    Function ghostGrabStationedAgentsFromFile(ByVal starname As String, ByVal planetNumber As Integer) As List(Of String)
        'each agent is identified by a unique 3 digit number, eg. 001, 002
        'this function returns a list with all the identifiers
        'agent 000 is always reserved as a null string, eg. no agent

        Dim planet As planet = ghostGrabPlanetFromFile(starname, planetNumber)
        Return planet.stationedAgents
    End Function

    'ghostTextList grab and return textfiles in a list
    Function ghostTextList(ByVal filename As String)
        Dim textlist As New List(Of String)

        If System.IO.File.Exists(filename) = False Then File.Create(filename).Dispose()

        Using txtr As StreamReader = New StreamReader(filename)
            While txtr.ReadLine <> Nothing
                textlist.Add(txtr.ReadLine)
            End While
        End Using

        Return textlist
    End Function
End Module
