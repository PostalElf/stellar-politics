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

