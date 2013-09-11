﻿Public Class frmNew
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
    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Select Case CheckBox1.Checked
            Case True
                NumericUpDown1.Value = 1
                NumericUpDown1.Maximum = 1
            Case False
                NumericUpDown1.Value = 1
                NumericUpDown1.Maximum = 5
        End Select
    End Sub

    Private Sub frmNew_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
    End Sub

End Class