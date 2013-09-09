Option Explicit On

Public Class frmSearch
    Dim starmap As starmap = Form1.starmap
    Dim agentList As agentList = Form1.agentList
    Dim goodsList As List(Of String)
    Dim planetList As List(Of planet)
    Dim comboboxlist As New List(Of ComboBox)
    Public selectedPlanet As planet

    Private Sub frmSearch_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        initialiseFrmSearch()
    End Sub

    Private Sub initialiseFrmSearch()
        'populate comboboxes
        If goodsList Is Nothing Then goodsList = New List(Of String)
        repopGoodsList()
        For Each good In goodsList
            cmbSupply.Items.Add(good)
            cmbDemand.Items.Add(good)
        Next
        cmbStar.Items.Add("")
        For Each star In starmap.stars
            If star.type <> "Blackhole" Then cmbStar.Items.Add(star.name)
        Next
        cmbCities.Items.Add("")
        For i As Integer = 1 To 5
            cmbCities.Items.Add(i)
        Next
        cmbSuffix.Items.Add("")
        For i As Integer = 1 To planetSuffixDictionary.Count Step 1
            cmbSuffix.Items.Add(planetSuffixDictionary(i))
        Next
        'cmbGovernment and cmbPrefix are all added manually


        ' add all the comboboxes into comboboxlist
        For Each control As Control In Me.Controls
            If (TypeOf control Is ComboBox) Then comboboxlist.Add(control)
        Next
    End Sub
    Private Sub repopGoodsList()
        goodsList.Add("")
        For Each item In defaultSystemSupply
            goodsList.Add(item)
        Next
        goodsList.Add("None")
    End Sub

    Private Sub runSearch()
        ' if no control is selected then exit sub
        If allIsEmpty(comboboxlist) = True Then Exit Sub

        If planetList Is Nothing Then
            planetList = New List(Of planet)
        Else
            planetList.Clear()
        End If

        For Each star As star In starmap.stars
            If star.type <> "Blackhole" Then
                For Each planet As planet In star.planets
                    'create a list of agents that reside on the planet
                    Dim stationedAgents As New List(Of agent)
                    For Each agent As agent In agentList.agents
                        If agent.starName = star.name AndAlso agent.planetNumber = planet.number Then stationedAgents.Add(agent)
                    Next

                    'every planet that meets the search criteria goes into planetList
                    'if nothing is selected for a particular criteria the function automatically returns True
                    If isStar(planet.starName) = True AndAlso _
                            isSupply(planet.supply) = True AndAlso _
                            isDemand(planet.demand) = True AndAlso _
                            isCities(planet.size) = True AndAlso _
                            isGovernment(planet.government) = True AndAlso _
                            isPrefix(planet.prefix) = True AndAlso _
                            isSuffix(planet.suffix) = True AndAlso _
                            isAgent(stationedAgents) = True Then
                        planet.stationedAgents = stationedAgents
                        planetList.Add(planet)
                    End If
                Next
            End If
        Next

        For Each planet In planetList
            Dim n As Integer = DataGridView1.Rows.Add()
            Dim str As String = ""
            DataGridView1.Rows.Item(n).Cells(0).Value = planet.starName & " " & romanNumeralDictionary(planet.number)
            DataGridView1.Rows.Item(n).Cells(1).Value = planet.size
            DataGridView1.Rows.Item(n).Cells(2).Value = planetTypeShortformDictionary(planet.prefix)
            DataGridView1.Rows.Item(n).Cells(3).Value = planetTypeShortformDictionary(planet.suffix)
            For Each supply As String In planet.supply
                str = str & supply & " "
            Next
            DataGridView1.Rows.Item(n).Cells(4).Value = str
            str = ""
            For Each demand As String In planet.demand
                str = str & demand & " "
            Next
            DataGridView1.Rows.Item(n).Cells(5).Value = str
            DataGridView1.Rows.Item(n).Cells(6).Value = planet.stationedAgents.Count
        Next
    End Sub
    Private Function allIsEmpty(comboboxlist As List(Of ComboBox)) As Boolean
        allIsEmpty = True
        For Each cmb As ComboBox In comboboxlist
            If cmb.SelectedItem <> Nothing Then allIsEmpty = False
        Next
    End Function
    Private Function isStar(ByVal starName As String) As Boolean
        If cmbStar.SelectedItem = Nothing Then
            Return True
        Else
            If cmbStar.SelectedItem.ToString = starName Then Return True Else Return False
        End If
    End Function
    Private Function isCities(ByVal cities As Integer) As Boolean
        If cmbCities.SelectedItem = Nothing Then
            Return True
        Else
            If Convert.ToInt32(cmbCities.SelectedItem.ToString) = cities Then Return True Else Return False
        End If
    End Function
    Private Function isSupply(ByVal supply As List(Of String)) As Boolean
        If cmbSupply.SelectedItem = Nothing Then
            Return True
        Else
            Dim str As String = cmbSupply.SelectedItem.ToString
            For Each good As String In supply
                If good = str Then Return True
            Next
            Return False
        End If
    End Function
    Private Function isDemand(ByVal demand As List(Of String)) As Boolean
        If cmbDemand.SelectedItem = Nothing Then
            Return True
        Else
            Dim str As String = cmbDemand.SelectedItem.ToString
            For Each good As String In demand
                If good = str Then Return True
            Next
            Return False
        End If
    End Function
    Private Function isGovernment(ByVal government As String) As Boolean
        If cmbGovernment.SelectedItem = Nothing Then
            Return True
        Else
            If cmbGovernment.SelectedItem.ToString = government Then Return True Else Return False
        End If
    End Function
    Private Function isPrefix(ByVal prefix As String) As Boolean
        If cmbPrefix.SelectedItem = Nothing Then
            Return True
        Else
            If cmbPrefix.SelectedItem.ToString = prefix Then Return True Else Return False
        End If
    End Function
    Private Function isSuffix(ByVal suffix As String) As Boolean
        If cmbSuffix.SelectedItem = Nothing Then
            Return True
        Else
            If cmbSuffix.SelectedItem.ToString = suffix Then Return True Else Return False
        End If
    End Function
    Private Function isAgent(ByVal stationedAgents As List(Of agent)) As Boolean
        If cmbAgents.SelectedItem = Nothing Then Return True

        Select Case cmbAgents.SelectedIndex
            Case 1
                ' no agents
                If stationedAgents.Count = 0 Then Return True Else Return False

            Case 2
                'at least 1
                If stationedAgents.Count > 0 Then Return True Else Return False

            Case 3
                'more than 1
                If stationedAgents.Count > 1 Then Return True Else Return False

            Case Else
                'bugcatch
                Return False
        End Select

        Return False
    End Function
    Private Function realNumberofAgents(ByVal stationedAgents As List(Of String)) As Integer
        If stationedAgents.Item(0) = "000" Then
            Return 0
        Else
            Return stationedAgents.Count
        End If
    End Function

    Private Sub ComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbSupply.SelectedIndexChanged, _
                                                                                                        cmbDemand.SelectedIndexChanged, _
                                                                                                        cmbCities.SelectedIndexChanged, _
                                                                                                        cmbStar.SelectedIndexChanged, _
                                                                                                        cmbPrefix.SelectedIndexChanged, _
                                                                                                        cmbSuffix.SelectedIndexChanged, _
                                                                                                        cmbGovernment.SelectedIndexChanged, _
                                                                                                        cmbAgents.SelectedIndexChanged
        DataGridView1.Rows.Clear()
        runSearch()
    End Sub
    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex = -1 Then Return

        selectedPlanet = planetList(e.RowIndex)
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class