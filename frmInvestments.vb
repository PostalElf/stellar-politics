Public Class frmInvestments
    Sub New(Optional ByVal filterStarname As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'refresh datagrids
        refreshDataGridView1(Form1.playerinfo)
        refreshDataGridView2(Form1.playerinfo)

        'update totalwealthperturn
        Label3.Text = processTWPT(Form1.playerinfo.totalWealthPerTurn)

        'populate filter
        cmbFilter.Items.Add("")
        cmbFilter.Items.Add("Oubliette")
        For Each star As star In Form1.starmap.stars
            cmbFilter.Items.Add(star.name)
        Next

        'select item
        cmbFilter.SelectedItem = filterStarname
    End Sub
    Private Sub cmbFilter_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFilter.SelectedIndexChanged
        refreshDataGridView1(Form1.playerinfo, cmbFilter.SelectedItem.ToString)
        refreshDataGridView2(Form1.playerinfo, cmbFilter.SelectedItem.ToString)
    End Sub

    Private Sub refreshDataGridView1(ByRef playerinfo As playerinfo, Optional ByVal filter As String = "")
        viewAssets.Rows.Clear()

        For Each asset In playerinfo.getAssetList
            If filter = "" OrElse asset.starName = filter Then
                Dim n As Integer = viewAssets.Rows.Add()
                viewAssets.Rows.Item(n).Cells(0).Value = asset.name
                If asset.starName = "Oubliette" Then
                    viewAssets.Rows.Item(n).Cells(1).Value = asset.starName
                Else
                    viewAssets.Rows.Item(n).Cells(1).Value = asset.starName & " " & romanNumeralDictionary(asset.planetNumber)
                End If
                viewAssets.Rows.Item(n).Cells(2).Value = asset.wealthPerTurn
            Else
                'do nothing
            End If
        Next
    End Sub
    Private Sub refreshDataGridView2(ByRef playerinfo As playerinfo, Optional ByVal filter As String = "")
        viewLiabilities.Rows.Clear()

        For Each liability In playerinfo.getLiabilityList
            If filter = "" OrElse liability.starName = filter Then
                Dim n As Integer = viewLiabilities.Rows.Add()
                viewLiabilities.Rows.Item(n).Cells(0).Value = liability.name
                If liability.starName = "Oubliette" Then
                    viewLiabilities.Rows.Item(n).Cells(1).Value = liability.starName
                Else
                    viewLiabilities.Rows.Item(n).Cells(1).Value = liability.starName & " " & romanNumeralDictionary(liability.planetNumber)
                End If
                viewLiabilities.Rows.Item(n).Cells(2).Value = liability.wealthPerTurn
            Else
                'do nothing
            End If
        Next
    End Sub
    Private Function processTWPT(ByVal TWPT As Integer) As String
        Dim str As String
        If TWPT >= 0 Then
            str = "+" & TWPT
        Else
            str = "-" & TWPT
        End If
        Return "Total WPT: " & str
    End Function

End Class