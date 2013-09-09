Public Class Form1
    Public starmap As New starmap
    Public agentList As New List(Of agent)
    Public playerinfo As New playerinfo

    Private Sub displayStarmap(starmap As starmap)
        'display starmap in form and perform menu activations
        Dim counter As Integer = 0

        For Each star In starmap.stars
            Dim starPopSize As Integer = 0
            Dim starSize As Integer = 0
            For Each planet In star.planets
                starSize += 1
                starPopSize += planet.size
            Next

            Dim starLoc As New Panel
            starLoc.Location = systemXYDictionary(star.location)
            starLoc.BackgroundImage = My.Resources.ResourceManager.GetObject("star" & star.type)
            starLoc.BackColor = Color.Transparent
            starLoc.Size = New Size(30, 30)
            starLoc.Tag = counter
            If star.type = "Blackhole" Then
                Tooltip2.SetToolTip(starLoc, star.name & " (Blackhole)")
            Else
                Tooltip2.SetToolTip(starLoc, star.name & vbCrLf & _
                                            "Planets: " & starSize & vbCrLf & _
                                            "Population: " & starPopSize)
            End If
            AddHandler starLoc.Click, AddressOf starLocClick

            TabPage1.Controls.Add(starLoc)
            counter += 1
        Next

        'menu activations
        ToolsToolStripMenuItem.Enabled = True
        ManageToolStripMenuItem.Enabled = True

        'switch tabpage
        TabControl1.SelectTab(0)
    End Sub
    Private Sub displayStar(ByRef star As star, ByVal index As Integer)
        refreshTabPage2()

        If star.type = "Blackhole" Then
            'do nothing, blackholes have nothing to display
        Else
            For Each planet In star.planets
                Dim planetLoc As New Panel
                planetLoc.Location = systemPlanetXYDictionary(planet.location)
                Dim bgimgstr As String = "bigplanet" & planet.suffix
                planetLoc.BackgroundImage = My.Resources.ResourceManager.GetObject(bgimgstr)
                planetLoc.BackColor = Color.Transparent
                planetLoc.Size = New Size(40, 40)
                planetLoc.BackgroundImageLayout = ImageLayout.Stretch
                planetLoc.Visible = True
                planetLoc.Tag = getPlanetLocTag(index, planet.number - 1)       ' planet.number - 1 because stars() is a 0-based list

                Dim str As String = planet.starName & " " & romanNumeralDictionary(planet.number) & vbCrLf & _
                                    "Type: " & planet.prefix & " " & planet.suffix & vbCrLf & _
                                    "Habitation: " & planet.habitation & vbCrLf & _
                                    "Cities: " & planet.size & vbCrLf & _
                                    "Government: " & planet.government & vbCrLf & _
                                    vbCrLf
                str = str & "Supply: "
                If planet.supply.Count = 0 OrElse planet.supply(0) = "None" Then
                    str = str & "None" & vbCrLf
                Else
                    For Each good In planet.supply
                        str = str & good & " "
                    Next
                End If
                str = str & vbCrLf & "Demand: "
                If planet.demand.Count = 0 OrElse planet.demand(0) = "None" Then
                    str = str & "None" & vbCrLf
                Else
                    For Each good In planet.demand
                        str = str & good & " "
                    Next
                End If
                Tooltip2.SetToolTip(planetLoc, str)
                AddHandler planetLoc.Click, AddressOf planetLocClick
                TabPage2.Controls.Add(planetLoc)
            Next

        End If

        Dim starbgstr As String = "starBG" & star.type
        TabPage2.BackgroundImage = My.Resources.ResourceManager.GetObject(starbgstr)
    End Sub
    Private Sub displayPlanet(ByRef planet As planet)
        refreshTabPage3()

        Dim str As String = "bigplanet" & planet.suffix
        picPlanetType.BackgroundImage = My.Resources.ResourceManager.GetObject(str)
        lblPlanetName.Text = planet.starName & " " & romanNumeralDictionary(planet.number)
        lblSize.Text = planetSizeDictionary(planet.size)
        lblGovernment.Text = planet.government
        lblHabitation.Text = planet.habitation
        lblPrefix.Text = planet.prefix
        lblSuffix.Location = New Point(lblPrefix.Location.X + lblPrefix.Size.Width - 3, lblPrefix.Location.Y)
        lblSuffix.Text = planet.suffix
        Dim counter As Integer = 0
        For Each supply In planet.supply
            addGoodsPic(supply, counter, "supply")
            counter += 1
        Next
        counter = 0
        For Each demand In planet.demand
            addGoodsPic(demand, counter, "demand")
            counter += 1
        Next
        counter = 0
        For Each agent As String In planet.stationedAgents
            addGoodsPic(agent, counter, "agent")
            counter += 1
        Next
    End Sub
    Private Sub refreshTabPage1()
        TabPage1.Controls.Clear()
    End Sub
    Private Sub refreshTabPage2()
        TabPage2.Controls.Clear()

        Dim buttonBack As New Panel
        buttonBack.Size = New Size(60, 60)
        buttonBack.Location = New Point(116, 224)
        buttonBack.BackColor = Color.Transparent
        AddHandler buttonBack.Click, AddressOf buttonBackClickStar
        TabPage2.Controls.Add(buttonBack)
    End Sub
    Private Sub refreshTabPage3()
        lblPlanetName.Text = "Planet Name"
        lblSize.Text = "     "
        lblPrefix.Text = "     "
        lblSuffix.Text = "     "
        lblDescription.Text = ""
        lblHabitation.Text = "     "
        lblGovernment.Text = "     "
        picPlanetType.BackgroundImage = Nothing
        For Each ctrl As Control In TabPage3.Controls
            If (TypeOf ctrl Is Panel) = True Then   ' shortcircuit all non-panels
                'clear all goodPics (aka supply and demand icons)
                'ignore certain panels (prefix pic)

                If ctrl.Name <> "picPlanetType" AndAlso ctrl.Name <> "picPlanetForward" Then TabPage3.Controls.Remove(ctrl)
            End If
        Next
    End Sub

    Private Sub newGalaxy(ByRef starmapOptions As starmapOptions)
        'clear tabpage1
        TabPage1.Controls.Clear()

        starmap = Nothing
        starmap.generateStarmap(starmapOptions)
        starmap = ghostLoadStarmap()

        displayStarmap(starmap)
    End Sub
    Private Sub loadGalaxy()
        ' check hash
        Dim hashFxn As New sharedHashFunctions
        If hashFxn.checkHash("starmap") = False OrElse _
                hashFxn.checkHash("playerinfo") = False OrElse _
                hashFxn.checkHash("agents") = False Then
            'hash does not match
            MsgBox("Files corrupted!  Please generate a new starmap" & vbCrLf & _
                   "and avoid manually editting any of the files in" & vbCrLf & _
                   "the folder.", MsgBoxStyle.Critical, "Error!")
            hashFxn = Nothing
            Me.Close()
        Else
            hashFxn = Nothing

            ' load starmap into memory
            starmap = Nothing
            starmap = ghostLoadStarmap()
            displayStarmap(starmap)

            ' load agents into memory
            agentList = Nothing
            agentList = ghostLoadAgents()
        End If
    End Sub
    Private Function getPlanetLocTag(ByVal index As Integer, ByVal planetNumber As Integer) As String
        'stores both planetNumber as well as index of current star in starmap.stars()
        'index required to pull the correct star out of starmap.stars()
        'planetLocTag only viable when index and planetNumber < 99
        If index < 10 Then
            If planetNumber < 10 Then getPlanetLocTag = "0" & index & "," & "0" & planetNumber Else getPlanetLocTag = "0" & index & "," & planetNumber
        Else
            If planetNumber < 10 Then getPlanetLocTag = index & "," & "0" & planetNumber Else getPlanetLocTag = index & "," & planetNumber
        End If
    End Function
    Private Sub addGoodsPic(ByVal good As String, ByVal counter As Integer, ByVal type As String)
        If good = "None" OrElse good = "000" Then Exit Sub ' exit if there's nothing to display

        Dim picY As Integer
        Select Case type
            Case "supply" : picY = 135
            Case "demand" : picY = 168
            Case "agent" : picY = 201
            Case Else : picY = 135
        End Select

        Dim goodPic As New Panel

        goodPic.Size = New Size(30, 30)
        goodPic.Location = New Point((64 + counter * 30), picY)
        goodPic.BackgroundImageLayout = ImageLayout.Stretch
        goodPic.Tag = good
        Select Case type
            Case "agent"
                addAgentDetails(goodPic, good)
            Case Else
                addGoodDetails(goodPic, good)
        End Select
        TabPage3.Controls.Add(goodPic)
    End Sub
    Private Sub addGoodDetails(ByRef goodpic As Panel, ByVal good As String)
        Tooltip2.SetToolTip(goodpic, good)
        goodpic.BackgroundImage = My.Resources.ResourceManager.GetObject("ico" & good)
        AddHandler goodpic.Click, AddressOf goodPic_Click
    End Sub
    Private Sub addAgentDetails(ByRef goodPic As Panel, ByVal agentNumber As String)
        'when details for the agents are released, run a dictionary check for correct picture and type
        'for now use placeholder icoGenAgent for all agents
        Tooltip2.SetToolTip(goodPic, "Agent " & agentNumber)
        goodPic.BackgroundImage = My.Resources.ResourceManager.GetObject("icoGenAgent")
        AddHandler goodPic.Click, AddressOf agentPic_Click
    End Sub

    Private Sub starLocClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim index As Integer = Convert.ToInt32(DirectCast(sender, Panel).Tag)
        Dim star As star = starmap.stars(index)

        displayStar(star, index)
        TabControl1.SelectTab(1)
    End Sub
    Private Sub planetLocClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim planetLocTag As String = DirectCast(sender, Panel).Tag

        Dim index As Integer = Convert.ToInt32(Mid(planetLocTag, 1, 2))
        Dim planetNumber As Integer = Convert.ToInt32(Mid(planetLocTag, 4, 2))
        Dim planet As planet = starmap.stars(index).planets(planetNumber)

        refreshTabPage3()
        displayPlanet(planet)
        TabControl1.SelectTab(2)
    End Sub
    Private Sub buttonBackClickStar(ByVal sender As Object, ByVal e As System.EventArgs)
        TabControl1.SelectTab(0)
    End Sub
    Private Sub goodPic_Click(sender As System.Object, e As System.EventArgs)
        Dim currentControl As Panel = sender
        lblDescription.Text = ghostInfoLoad("planetinfo", "goods", currentControl.Tag)
        picDescription.BackgroundImage = My.Resources.ResourceManager.GetObject("ico" & currentControl.Tag)
        TabControl1.SelectTab(3)
    End Sub
    Private Sub agentPic_Click(sender As System.Object, e As System.EventArgs)
        Dim currentControl As Panel = sender

        'placeholder until agent descriptions come in
        lblDescription.Text = "Placeholder Text until agentinfo.xml is fully written."
        picDescription.BackgroundImage = My.Resources.ResourceManager.GetObject("icoGenAgent")
        TabControl1.SelectTab(3)
    End Sub
    Private Function getPicDescription(currentControlName As String) As String
        Select Case currentControlName
            Case "Size" : Return "icoWorld"
            Case "Prefix" : Return "icoWorld"
            Case "Suffix" : Return "icoWorld"
            Case "Habitation" : Return "icoWorld"
            Case "Government" : Return "icoWorld"
            Case Else : Return "ico" & currentControlName
        End Select
    End Function
    Private Sub picPlanetType_Click(sender As Object, e As System.EventArgs) Handles picPlanetType.Click
        TabControl1.SelectTab(1)
    End Sub
    Private Sub picPlanetForward_Click(sender As Object, e As System.EventArgs) Handles picPlanetForward.Click
        TabControl1.SelectTab(3)
    End Sub
    Private Sub picDescription_Click(sender As Object, e As System.EventArgs) Handles picDescription.Click
        TabControl1.SelectTab(2)
    End Sub
    Private Sub planetLabel_Click(sender As System.Object, e As System.EventArgs) Handles lblSize.Click, lblPrefix.Click, lblSuffix.Click, lblHabitation.Click, lblGovernment.Click
        Dim currentControl As Label = sender
        Dim currentControlName As String = currentControl.Name.Remove(0, 3)
        lblDescription.Text = ghostInfoLoad("planetinfo", currentControlName, currentControl.Text)

        picDescription.BackgroundImage = My.Resources.ResourceManager.GetObject(getPicDescription(currentControlName))

        TabControl1.SelectTab(3)
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        initialiseStarmapFunctions()
        ToolsToolStripMenuItem.Enabled = False
        ManageToolStripMenuItem.Enabled = False
    End Sub
    Private Sub NewToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles NewToolStripMenuItem.Click
        If frmNew.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            newGalaxy(frmNew.starmapOpt)
        Else
            'do nothing
        End If

        frmNew.starmapOpt = Nothing
        frmNew.Dispose()
    End Sub
    Private Sub LoadToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles LoadToolStripMenuItem.Click
        refreshTabPage1()
        loadGalaxy()
    End Sub
    Private Sub SearchToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SearchToolStripMenuItem.Click
        If frmSearch.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            refreshTabPage3()
            displayPlanet(frmSearch.selectedPlanet)
            TabControl1.SelectTab(2)
        Else
            'do nothing
        End If

        frmSearch.Dispose()
    End Sub
    Private Sub DistanceToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DistanceToolStripMenuItem.Click
        frmDistance.ShowDialog(Me)
        frmDistance.Dispose()
    End Sub
    Private Sub AgentsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AgentsToolStripMenuItem.Click
        If frmAgents.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        Else

        End If
    End Sub
End Class
