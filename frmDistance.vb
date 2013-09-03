Public Class frmDistance
    Dim starmap As starmap = ghostLoadStarmap()

    Private Sub frmDistance_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'populate star comboboxes
        For Each star In starmap.stars
            ComboBox1.Items.Add(star.name)
            ComboBox2.Items.Add(star.name)
        Next
    End Sub
    Private Sub ComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged, ComboBox2.SelectedIndexChanged
        If ComboBox1.SelectedItem <> Nothing And ComboBox2.SelectedItem <> Nothing Then Button1.Enabled = True
    End Sub
    Private Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim location1 As New Point
        Dim location2 As New Point

        For Each star In starmap.stars
            If star.name = ComboBox1.SelectedItem.ToString Then location1 = systemXYDictionary(star.location)
            If star.name = ComboBox2.SelectedItem.ToString Then location2 = systemXYDictionary(star.location)
        Next

        Button1.Font = New Font("Courier New", 7)
        Button1.Text = calculateDistanceBetween(location1, location2)
    End Sub
End Class