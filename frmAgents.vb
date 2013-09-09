Public Class frmAgents
    Dim starmap As starmap = Form1.starmap
    Dim agentList As agentList = Form1.agentList
    Dim agentIndex As Integer

    Private Sub frmAgents_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        refreshDataGridView1()
    End Sub
    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex = -1 Then Return

        agentIndex = e.RowIndex

        TabControl1.SelectTab(1)
        displayAgent(agentList.agents(agentIndex))
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox2.Items.Clear()

        For Each planet As planet In starmap.stars(ComboBox1.SelectedIndex).planets
            ComboBox2.Items.Add(romanNumeralDictionary(planet.number))
        Next
    End Sub
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim agent As agent = agentList.agents(agentIndex)
        Dim destinationStarname As String = ComboBox1.SelectedItem
        Dim destinationPlanetnumber As Integer = 0
        For Each entry As KeyValuePair(Of Integer, String) In romanNumeralDictionary
            If ComboBox2.SelectedItem = entry.Value Then destinationPlanetnumber = entry.Key
        Next

        agentList.moveAgent(agent.id, destinationStarname, destinationPlanetnumber)

        refreshDataGridView1()
        TabControl1.SelectTab(0)
    End Sub

    Private Sub displayAgent(ByRef agent As agent)
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()

        lblName.Text = agent.name
        For Each star As star In starmap.stars
            ComboBox1.Items.Add(star.name)
        Next
    End Sub
    Private Sub refreshDataGridView1()
        DataGridView1.Rows.Clear()

        For Each agent In agentList.agents
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

End Class