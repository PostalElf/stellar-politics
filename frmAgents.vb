Public Class frmAgents
    Dim agentIndex As Integer
    Sub New(Optional ByVal planet As planet = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        refreshDataGridView1()
        popCmbFilterStarName()

        If planet IsNot Nothing Then
            cmbFilterStarName.SelectedItem = planet.starName
            popCmbFilterPlanetNumber()
            cmbFilterPlanetNumber.SelectedItem = romanNumeralDictionary(planet.number)
        End If
    End Sub
    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex = -1 Then Return

        agentIndex = e.RowIndex

        TabControl1.SelectTab(1)
        displayAgent(Form1.agentList.agents(agentIndex))
    End Sub
    Private Sub cmbStarName_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbStarName.SelectedIndexChanged
        cmbPlanetNumber.Items.Clear()

        If cmbStarName.SelectedItem = "Oubliette" Then
            cmbPlanetNumber.Items.Add("0")
            lblDetails.Text = "French for 'the forgotten place', the leaders of each Great House operates from an Oubliette, a secret barge " & _
                                "located in the depths of realspace that has only one subspace portal leading in and out of the location.  " & _
                                "Agents in the Oubliette are perfectly safe but, alas, also perfectly ineffectual."
            butGo.Visible = True
            butGo.Enabled = False
            butGo.Text = "Move"
        ElseIf cmbStarName.SelectedItem = "" Then
            lblDetails.Text = ""
            butGo.Visible = False
        Else
            For Each planet As planet In Form1.starmap.stars(cmbStarName.SelectedIndex).planets
                cmbPlanetNumber.Items.Add(romanNumeralDictionary(planet.number))
            Next
            lblDetails.Text = "Thanks to advances in subspace technology, it only takes one turn for an Agent to " & _
                                "travel between any star or planet. Whilst in transit, however, agents may not be " & _
                                "contacted until they reach their destination."
            butGo.Visible = True
            butGo.Enabled = False
            butGo.Text = "Move"
        End If
    End Sub
    Private Sub cmbPlanetNumber_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbPlanetNumber.SelectedIndexChanged
        If cmbPlanetNumber.SelectedItem <> "" Then
            butGo.Enabled = True
            butGo.Tag = "move"
        End If
    End Sub
    Private Sub cmbFilterStarName_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFilterStarName.SelectedIndexChanged
        popCmbFilterPlanetNumber()
    End Sub
    Private Sub cmbFilterPlanetNumber_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFilterPlanetNumber.SelectedIndexChanged
        Dim filterStarName As String = cmbFilterStarName.SelectedItem.ToString
        Dim filterPlanetNumber As Integer = processRomanNumber(cmbFilterPlanetNumber.SelectedItem.ToString)
        refreshDataGridView1(filterStarName, filterPlanetNumber)
    End Sub
    Private Sub butGo_Click(sender As System.Object, e As System.EventArgs) Handles butGo.Click
        Select Case butGo.Tag
            Case "move" : moveAgentToPlanet(cmbStarName.SelectedItem.ToString, cmbPlanetNumber.SelectedItem.ToString)
            Case Else : Exit Sub
        End Select
    End Sub

    Private Sub displayAgent(ByRef agent As agent)
        cmbStarName.Items.Clear()
        cmbPlanetNumber.Items.Clear()

        lblName.Text = agent.name
        cmbStarName.Items.Add("")
        cmbStarName.Items.Add("Oubliette")
        For Each star As star In Form1.starmap.stars
            cmbStarName.Items.Add(star.name)
        Next
    End Sub
    Private Sub popCmbFilterStarName()
        cmbFilterStarName.Items.Clear()

        cmbFilterStarName.Items.Add("")
        cmbFilterStarName.Items.Add("Oubliette")
        For Each star In Form1.starmap.stars
            cmbFilterStarName.Items.Add(star.name)
        Next
    End Sub
    Private Sub popCmbFilterPlanetNumber()
        cmbFilterPlanetNumber.Items.Clear()

        cmbFilterPlanetNumber.Items.Add("")
        If cmbFilterStarName.SelectedItem.ToString = "" Then
            'do nothing
        ElseIf cmbFilterStarName.SelectedItem.ToString = "Oubliette" Then
            cmbFilterPlanetNumber.Items.Add("0")
        Else
            Dim star As star = Form1.starmap.grabStar(cmbFilterStarName.SelectedItem.ToString)
            For Each planet As planet In star.planets
                cmbFilterPlanetNumber.Items.Add(romanNumeralDictionary(planet.number))
            Next
        End If
    End Sub
    Private Sub refreshDataGridView1(Optional ByVal filterStarName As String = "", Optional ByVal filterPlanetNumber As Integer = -1)
        DataGridView1.Rows.Clear()

        For Each agent In Form1.agentList.agents
            If (filterStarName = "" AndAlso filterPlanetNumber = -1) OrElse _
                    (agent.starName = filterStarName AndAlso agent.planetNumber = filterPlanetNumber) Then
                Dim n As Integer = DataGridView1.Rows.Add()
                Dim str As String = ""
                DataGridView1.Rows.Item(n).Cells(0).Value = agent.name
                DataGridView1.Rows.Item(n).Cells(1).Value = agent.type
                If agent.starName = "Oubliette" Then
                    DataGridView1.Rows.Item(n).Cells(2).Value = agent.starName
                Else
                    DataGridView1.Rows.Item(n).Cells(2).Value = agent.starName & " " & romanNumeralDictionary(agent.planetNumber)
                End If
            End If
        Next
    End Sub
    Private Function checkDestination(ByRef destination As destination) As Boolean
        Dim planet As planet = Form1.starmap.grabPlanet(destination.destStarName, destination.destPlanetNumber)

        'check if valid location
        If planet Is Nothing Then Return False

        'check for space
        Dim planetCapacity As Integer = planet.agentCapacityRemaining(Form1.agentList)
        If planetCapacity < 1 Then Return False Else Return True
    End Function
    Private Sub moveAgentToPlanet(ByVal destinationStarName As String, ByVal destinationPlanetNumber As Integer)
        Dim destination As New destination
        destination.destAgent = Form1.agentList.agents(agentIndex)
        destination.destStarName = destinationStarName
        destination.destPlanetNumber = destinationPlanetnumber

        'check if destination is OK
        If checkDestination(destination) = True Then
            Form1.turnticker.agentsToMove.Add(destination)
            Form1.agentList.agents.RemoveAt(agentIndex)
        Else
            'display error
            Dim str As String = "BY IMPERIAL EDICT:" & vbCrLf & _
                                "------------------" & vbCrLf & vbCrLf & _
                                "No more than one Guild representative may be deployed" & vbCrLf & _
                                "to a city at any time. Any Great House caught disobeying" & vbCrLf & _
                                "this edict will have all rights and privilleges previously" & vbCrLf & _
                                "accorded immediately revoked."
            MsgBox(str, MsgBoxStyle.Exclamation, "Error!")
        End If

        refreshDataGridView1()
        TabControl1.SelectTab(0)
    End Sub

End Class