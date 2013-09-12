Public Class turnticker
    'everything that's going to happen at the end of a turn goes here

    Public agentsToMove As List(Of destination)

    Sub New()
        If agentsToMove Is Nothing Then agentsToMove = New List(Of destination)
    End Sub
    Public Sub turnEnd()
        moveAllAgents()
    End Sub

    Private Sub moveAllAgents()
        For Each destination As destination In agentsToMove
            'moving agents are removed from agentlist (as they can no longer be interacted with until they finish moving)
            'they are added back when they successfully move at turn's end

            Dim agent As agent = destination.destAgent
            agent.moveAgent(destination.destStarName, destination.destPlanetNumber)
            Form1.agentList.agents.Add(agent)
        Next

        agentsToMove.Clear()            'onced moved, all agents are removed from turnticker.agentstomove
    End Sub
End Class

Public Class destination
    Public destAgent As agent
    Public destStarName As String
    Public destPlanetNumber As Integer
End Class