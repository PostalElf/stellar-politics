Imports System.IO
Imports System.Xml
Imports System.Xml.XPath
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

        ' Read first planet
        xr.ReadToDescendant("planet")
        star.planets.Add(ghostLoadPlanet(xr))

        ' Read subsequent planets
        While xr.ReadToNextSibling("planet") = True
            star.planets.Add(ghostLoadPlanet(xr))
        End While

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

        xr.Read()
        While xr.Name = "supply"
            planet.supply.Add(xr.ReadString)
            xr.Read()
        End While
        While xr.Name = "demand"
            planet.demand.Add(xr.ReadString)
            xr.Read()
        End While

        Return planet
    End Function

    'ghostInfoLoaders read XML files ending with info and return the appropriate information in string
    Function ghostInfoLoad(ByVal filename As String, ByVal rootElement As String, ByVal childElement As String) As String
        'the info XML file should not be more complex than /filename/rootElement/childElement/
        'eg. within planetinfo.xml, /planetinfo/prefix/cultural

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

    '-----------

    'function graveyard
    Function ghostCountElement(ByVal elementName As String) As Integer
        Dim counter As Integer = 0

        Dim xr As XmlReader = XmlReader.Create(starmapFilename)
        While xr.Read()
            If xr.NodeType = XmlNodeType.Element AndAlso xr.Name = elementName Then counter += 1
        End While

        Return counter
    End Function
End Module
