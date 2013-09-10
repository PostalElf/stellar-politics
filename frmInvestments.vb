Public Class frmInvestments
    Private Sub frmInvestments_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim playerinfo As playerinfo = Form1.playerinfo

        'refresh datagrids
        refreshDataGridView1(playerinfo)
        refreshDataGridView2(playerinfo)

        'update totalwealthperturn
        Label3.Text = processTWPT(playerinfo.totalWealthPerTurn)
    End Sub

    Private Sub refreshDataGridView1(ByRef playerinfo As playerinfo)
        DataGridView1.Rows.Clear()

        For Each asset In playerinfo.getAssetList
            Dim n As Integer = DataGridView1.Rows.Add()
            DataGridView1.Rows.Item(n).Cells(0).Value = asset.name
            If asset.starName = "Oubliette" Then
                DataGridView1.Rows.Item(n).Cells(1).Value = asset.starName
            Else
                DataGridView1.Rows.Item(n).Cells(1).Value = asset.starName & " " & romanNumeralDictionary(asset.planetNumber)
            End If
            DataGridView1.Rows.Item(n).Cells(2).Value = asset.wealthPerTurn
        Next
    End Sub
    Private Sub refreshDataGridView2(ByRef playerinfo As playerinfo)
        DataGridView2.Rows.Clear()

        For Each liability In playerinfo.getLiabilityList
            Dim n As Integer = DataGridView2.Rows.Add()
            DataGridView2.Rows.Item(n).Cells(0).Value = liability.name
            If liability.starName = "Oubliette" Then
                DataGridView2.Rows.Item(n).Cells(1).Value = liability.starName
            Else
                DataGridView2.Rows.Item(n).Cells(1).Value = liability.starName & " " & romanNumeralDictionary(liability.planetNumber)
            End If
            DataGridView2.Rows.Item(n).Cells(2).Value = liability.wealthPerTurn
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