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
End Class


Public Class agent
    Public id As String
    Public name As String
    Public type As String
    Public starName As String
    Public planetNumber As Integer

    Public Sub moveAgent(ByVal destinationStarName As String, ByVal destinationPlanetNumber As Integer)
        starName = destinationStarName
        planetNumber = destinationPlanetNumber
    End Sub
End Class

