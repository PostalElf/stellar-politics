﻿Public Class Form1
    Private Sub displayStarmap(starmap As starmap)
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
    End Sub
    Private Sub displayStar(ByRef star As star, ByVal index As Integer)
        refreshTabPage2()

        If star.type = "Blackhole" Then

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
        'ToolTip1.SetToolTip(lblGovernment, ghostInfoLoad("planetinfo", "government", planet.government))
        lblHabitation.Text = planet.habitation
        'ToolTip1.SetToolTip(lblHabitation, ghostInfoLoad("planetinfo", "habitation", planet.habitation))
        lblType.Text = planet.prefix & " " & planet.suffix
        str = ghostInfoLoad("planetinfo", "prefix", planet.prefix) & _
                            vbCrLf & vbCrLf & _
                            "---" & _
                            vbCrLf & vbCrLf & _
        ghostInfoLoad("planetinfo", "suffix", planet.suffix)
        ToolTip1.SetToolTip(lblType, str)

        lblSupply.Text = ""
        For Each supply In planet.supply
            lblSupply.Text = lblSupply.Text & supply & " "
        Next
        lblDemand.Text = ""
        For Each demand In planet.demand
            lblDemand.Text = lblDemand.Text & demand & " "
        Next
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
        lblType.Text = "     "
        lblHabitation.Text = "     "
        lblGovernment.Text = "     "
        picPlanetType.BackgroundImage = Nothing
    End Sub

    Private Sub newGalaxy(ByVal galaxySize As Integer)
        'clear tabpage1
        TabPage1.Controls.Clear()

        Dim starmap As New starmap
        starmap.generateStarmap(galaxySize)
        starmap = ghostLoadStarmap()
        displayStarmap(starmap)

        ToolsToolStripMenuItem.Enabled = True
    End Sub
    Private Sub loadGalaxy()
        Dim starmap As New starmap
        starmap = ghostLoadStarmap()
        displayStarmap(starmap)
        ToolsToolStripMenuItem.Enabled = True
    End Sub
    Private Function getPlanetLocTag(ByVal index As Integer, planetNumber As Integer) As String
        'stores both planetNumber as well as index of current star in starmap.stars()
        'index required to pull the correct star out of starmap.stars()
        'planetLocTag only viable when index and planetNumber < 99
        If index < 10 Then
            If planetNumber < 10 Then getPlanetLocTag = "0" & index & "," & "0" & planetNumber Else getPlanetLocTag = "0" & index & "," & planetNumber
        Else
            If planetNumber < 10 Then getPlanetLocTag = index & "," & "0" & planetNumber Else getPlanetLocTag = index & "," & planetNumber
        End If
    End Function

    Private Sub starLocClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim index As Integer = Convert.ToInt32(DirectCast(sender, Panel).Tag)
        Dim star As star = ghostLoadStarmap().stars(index)

        displayStar(star, index)
        TabControl1.SelectTab(1)
    End Sub
    Private Sub planetLocClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim planetLocTag As String = DirectCast(sender, Panel).Tag

        Dim index As Integer = Convert.ToInt32(Mid(planetLocTag, 1, 2))
        Dim planetNumber As Integer = Convert.ToInt32(Mid(planetLocTag, 4, 2))
        Dim planet As planet = ghostLoadStarmap.stars(index).planets(planetNumber)

        displayPlanet(planet)
        TabControl1.SelectTab(2)
    End Sub
    Private Sub buttonBackClickStar(ByVal sender As Object, ByVal e As System.EventArgs)
        TabControl1.SelectTab(0)
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        initialiseStarmapFunctions()
        ToolsToolStripMenuItem.Enabled = False

        'loadGalaxy()
    End Sub
    Private Sub Size1GalaxyToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles Size1GalaxyToolStripMenuItem.Click
        newGalaxy(1)
    End Sub
    Private Sub Size2GalaxyToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles Size2GalaxyToolStripMenuItem.Click
        newGalaxy(2)
    End Sub
    Private Sub Size3GalaxyToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles Size3GalaxyToolStripMenuItem.Click
        newGalaxy(3)
    End Sub
    Private Sub Size4GalaxyToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles Size4GalaxyToolStripMenuItem.Click
        newGalaxy(4)
    End Sub
    Private Sub Size5GalaxyToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles Size5GalaxyToolStripMenuItem.Click
        newGalaxy(5)
    End Sub
    Private Sub LoadToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles LoadToolStripMenuItem.Click
        loadGalaxy()
    End Sub
    Private Sub picPlanetType_Click(sender As Object, e As System.EventArgs) Handles picPlanetType.Click
        TabControl1.SelectTab(1)
    End Sub
    Private Sub picPlanetForward_Click(sender As Object, e As System.EventArgs) Handles picPlanetForward.Click
        TabControl1.SelectTab(3)
    End Sub
    Private Sub SearchToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SearchToolStripMenuItem.Click
        If frmSearch.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
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
End Class
