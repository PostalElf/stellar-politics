Public Class frmNew
    Public starmapOpt As New starmapOptions

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        starmapOpt.galaxySize = NumericUpDown1.Value
        starmapOpt.blackholes = chkBlackholes.Checked

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class