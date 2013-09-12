Imports System.IO
Imports System.Xml
Imports System.Text
Imports Microsoft.VisualBasic.CallType


Public Module xmlGhost

    '   .-.
    '  (o o) boo!
    '  | O \
    '   \   \
    '    `~~~'

    Public Const starmapFilename = "starmap.xml"
    Public Const agentFilename As String = "agents.xml"
    Public Const playerFilename As String = "playerinfo.xml"

    'ghostLoaders read XML and turn them into objects; no ghostLoadPlayerinfo as the New() automatically loads it
    Public Function ghostLoadStarmap() As starmap
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

        ' exit out to /planet; put at the end, after all sub-elements have been read
        xr.ReadEndElement()

        Return planet
    End Function
    Public Function ghostLoadAgentList() As agentList
        Dim agentList As New agentList

        Dim xsettings As New XmlReaderSettings
        xsettings.IgnoreWhitespace = True
        xsettings.IgnoreComments = True
        Dim xr As XmlReader = XmlReader.Create(agentFilename, xsettings)
        While xr.Read()
            If xr.NodeType = XmlNodeType.Element AndAlso xr.Name = "agent" Then
                agentList.agents.Add(ghostLoadAgent(xr))
            End If
        End While

        xr.Close()
        Return agentList
    End Function
    Private Function ghostLoadAgent(ByRef xr As XmlReader) As agent
        Dim agent As New agent

        agent.id = xr.GetAttribute("id")
        xr.ReadToFollowing("name")
        agent.name = xr.ReadString
        xr.ReadToFollowing("type")
        agent.type = xr.ReadString
        xr.ReadToFollowing("starname")
        agent.starName = xr.ReadString
        xr.ReadToFollowing("planetnumber")
        agent.planetNumber = Convert.ToInt32(xr.ReadString)

        Return agent
    End Function
    Public Function ghostLoadPlayerinfo() As playerinfo
        Dim playerinfo As New playerinfo

        Dim xsettings As New XmlReaderSettings
        xsettings.IgnoreWhitespace = True
        xsettings.IgnoreComments = True
        Dim xr As XmlReader = XmlReader.Create(playerFilename, xsettings)
        While xr.Read()
            If xr.NodeType = XmlNodeType.Element Then
                Select Case xr.Name
                    Case "faction" : playerinfo.faction = xr.ReadString
                    Case "investment" : playerinfo.investments.Add(ghostLoadInvestment(xr))
                    Case Else
                        'do nothing
                End Select
            End If
        End While

        xr.Close()

        Return playerinfo
    End Function
    Private Function ghostLoadInvestment(ByRef xr As XmlReader) As investment
        Dim investment As New investment

        xr.ReadToDescendant("name")
        investment.name = xr.ReadString
        xr.ReadToFollowing("starname")
        investment.starName = xr.ReadString
        xr.ReadToFollowing("planetnumber")
        investment.planetNumber = Convert.ToInt32(xr.ReadString)
        xr.ReadToFollowing("wealthperturn")
        investment.wealthPerTurn = Convert.ToInt32(xr.ReadString)

        Return investment
    End Function

    'ghostInfoLoaders read XML files ending with info and return the appropriate information in string
    Public Function ghostInfoLoad(ByVal filename As String, ByVal rootElement As String, ByVal childElement As String) As String
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

    'ghostWriters update whatever element is passed to them into the XML file; use only ghostWriteAll()
    Public Sub ghostWriteAll(ByRef starmap As starmap, ByRef agentList As agentList, ByRef playerinfo As playerinfo)
        ghostWriteStarmap(starmap)
        ghostWriteAgents(agentList)
        ghostWritePlayerinfo(playerinfo)

        'store starmap hash into hashstarmap.txt
        Dim txtFxn As New sharedHashFunctions
        txtFxn.addHashFile("starmap")
        txtFxn.addHashFile("agents")
        txtFxn.addHashFile("playerinfo")
        txtFxn = Nothing
    End Sub
    Private Sub ghostWriteStarmap(ByRef starmap As starmap)
        Dim xwrt As New XmlTextWriter(starmapFilename, System.Text.Encoding.UTF8)
        xwrt.WriteStartDocument(True)
        xwrt.Formatting = Formatting.Indented
        xwrt.Indentation = 2
        xwrt.WriteStartElement("starmap")

        For Each star As star In starmap.stars
            ghostWriteStar(xwrt, star)
        Next

        xwrt.WriteEndElement()  '/starmap
        xwrt.WriteEndDocument()
        xwrt.Close()
        xwrt = Nothing
    End Sub
    Private Sub ghostWriteStar(ByRef xwrt As XmlTextWriter, ByRef star As star)
        xwrt.WriteStartElement("star")
        xwrt.WriteAttributeString("name", star.name)
        xwrt.WriteAttributeString("location", star.location)
        xwrt.WriteAttributeString("type", star.type)

        If star.type = "Blackhole" Then
            'do nothing else
        Else
            For Each planet As planet In star.planets
                ghostWritePlanet(xwrt, planet)
            Next
        End If

        xwrt.WriteEndElement()      '/star
    End Sub
    Private Sub ghostWritePlanet(ByRef xwrt As XmlTextWriter, ByRef planet As planet)
        xwrt.WriteStartElement("planet")
        xwrt.WriteAttributeString("starName", planet.starName)
        xwrt.WriteAttributeString("number", planet.number)
        xwrt.WriteAttributeString("location", planet.location)
        xwrt.WriteElementString("size", planet.size)
        xwrt.WriteElementString("prefix", planet.prefix)
        xwrt.WriteElementString("suffix", planet.suffix)
        xwrt.WriteElementString("habitation", planet.habitation)
        xwrt.WriteElementString("government", planet.government)

        xwrt.WriteStartElement("supply")
        For Each supply As String In planet.supply
            xwrt.WriteElementString("good", supply)
        Next
        xwrt.WriteEndElement()  '/supply
        xwrt.WriteStartElement("demand")
        For Each demand As String In planet.demand
            xwrt.WriteElementString("good", demand)
        Next
        xwrt.WriteEndElement()  '/demand

        xwrt.WriteEndElement()  '/planet
    End Sub
    Private Sub ghostWriteAgents(ByRef agentList As agentList)
        Dim xwrt As New XmlTextWriter(agentFilename, System.Text.Encoding.UTF8)
        xwrt.WriteStartDocument(True)
        xwrt.Formatting = Formatting.Indented
        xwrt.Indentation = 2
        xwrt.WriteStartElement("agents")

        For Each agent As agent In agentList.agents
            xwrt.WriteStartElement("agent")
            xwrt.WriteAttributeString("id", agent.id)
            xwrt.WriteElementString("name", agent.name)
            xwrt.WriteElementString("type", agent.type)
            xwrt.WriteElementString("starname", agent.starName)
            xwrt.WriteElementString("planetnumber", agent.planetNumber)
            xwrt.WriteEndElement()  '/agent
        Next

        xwrt.WriteEndElement()        '/agents
        xwrt.WriteEndDocument()
        xwrt.Close()
        xwrt = Nothing
    End Sub
    Private Sub ghostWritePlayerinfo(ByRef playerinfo As playerinfo)
        Dim xwrt As New XmlTextWriter(playerFilename, System.Text.Encoding.UTF8)
        xwrt.WriteStartDocument(True)
        xwrt.Formatting = Formatting.Indented
        xwrt.Indentation = 2
        xwrt.WriteStartElement("playerinfo")

        xwrt.WriteElementString("faction", playerinfo.faction)

        xwrt.WriteStartElement("investments")
        For Each item As investment In playerinfo.investments
            ghostWriteInvestment(xwrt, item)
        Next
        xwrt.WriteEndElement()    '/investments

        xwrt.WriteEndElement()  '/playerinfo
        xwrt.WriteEndDocument()
        xwrt.Close()
        xwrt = Nothing
    End Sub
    Private Sub ghostWriteInvestment(ByRef xwrt As XmlTextWriter, ByRef investment As investment)
        xwrt.WriteStartElement("investment")
        xwrt.WriteElementString("name", investment.name)
        xwrt.WriteElementString("starname", investment.starName)
        xwrt.WriteElementString("planetnumber", investment.planetNumber)
        xwrt.WriteElementString("wealthperturn", investment.wealthPerTurn)
        xwrt.WriteEndElement()  '/investment
    End Sub

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
