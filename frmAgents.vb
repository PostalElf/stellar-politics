Public Class frmAgents
    Dim agents As List(Of agent) = ghostLoadAgents()
    Dim agentIndex As Integer
    Dim starmap As starmap = ghostLoadStarmap()

    Private Sub frmAgents_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        For Each agent In agents
            Dim n As Integer = DataGridView1.Rows.Add()
            Dim str As String = ""
            DataGridView1.Rows.Item(n).Cells(0).Value = agent.name
            DataGridView1.Rows.Item(n).Cells(1).Value = agent.type
            If agent.starName = "Oubliette" Then
                DataGridView1.Rows.Item(n).Cells(2).Value = agent.starName
            Else
                DataGridView1.Rows.Item(n).Cells(2).Value = agent.starName & " " & romanNumeralDictionary(agent.planetNumber)
            End If
        Next
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex = -1 Then Return

        agentIndex = e.RowIndex
        TabControl1.SelectTab(1)
    End Sub

End Class