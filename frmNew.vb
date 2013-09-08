Public Class frmNew
    Public starmapOpt As New starmapOptions

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If ComboBox1.SelectedItem.ToString = "Random" Then
            Randomize()
            Select Case Int(Rnd() * 3 + 1)
                Case 1 : starmapOpt.faction = "House Nyos"
                Case 2 : starmapOpt.faction = "House Illys"
                Case Else : starmapOpt.faction = "House Sen"
            End Select
        Else
            starmapOpt.faction = ComboBox1.SelectedItem.ToString
        End If
        starmapOpt.galaxySize = NumericUpDown1.Value
        starmapOpt.blackholes = chkBlackholes.Checked

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class