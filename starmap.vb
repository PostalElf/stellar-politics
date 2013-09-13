Imports System.Xml
Imports Microsoft.VisualBasic.CallType

Public Class starmap
    Public stars As List(Of star)
    Public activeStar As star
    Public activePlanet As planet

    Sub New()
        Randomize()
        If stars Is Nothing Then stars = New List(Of star)
    End Sub

    Public Sub generateAll(ByRef starmapOptions As starmapOptions)
        Dim starmapGen As New starmapGenerator

        'create them as objects
        Dim playerinfo As playerinfo = starmapGen.generatePlayerInfo(starmapOptions)
        Dim agentlist As agentList = starmapGen.generateAgents(starmapOptions, playerinfo)
        Dim starmap As starmap = starmapGen.generateStarmap(starmapOptions)
        Dim turnticker As New turnticker

        'write them into files
        ghostWriteAll(starmap, agentlist, playerinfo, turnticker)

        'load into memory
        Form1.playerinfo = playerinfo
        Form1.agentList = agentlist
        Form1.starmap = starmap
        'ignore turnticker as it will always be empty at the start

        'store starmap hash into hashstarmap.txt
        Dim txtFxn As New sharedHashFunctions
        txtFxn.addHashFile("starmap")
        txtFxn.addHashFile("agents")
        txtFxn.addHashFile("playerinfo")
        txtFxn.addHashFile("turnticker")
        txtFxn = Nothing

        starmapGen = Nothing
    End Sub
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
    ReadOnly Property stationedAgents As List(Of agent)
        Get
            Dim tempList As New List(Of agent)
            Dim agentList As agentList = Form1.agentList

            For Each agent As agent In agentList.agents
                If agent.starName = starName AndAlso agent.planetNumber = number Then tempList.Add(agent)
            Next

            Return tempList
        End Get
    End Property
    ReadOnly Property agentCapacityRemaining(ByVal agentlist As agentList) As Integer
        Get
            Return size - stationedAgents.Count
        End Get
    End Property

    Sub New()
        If supply Is Nothing Then supply = New List(Of String)
        If demand Is Nothing Then demand = New List(Of String)
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