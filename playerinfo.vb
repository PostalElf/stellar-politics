Public Class playerinfo
    Public faction As String
    Public investments As List(Of investment)
    ReadOnly Property totalWealthPerTurn As Integer
        Get
            Dim twpt As Integer = 0

            If investments.Count = 0 Then
                Return 0
            Else
                For Each investment As investment In investments
                    twpt += investment.wealthPerTurn
                Next
                Return twpt
            End If
        End Get
    End Property

    Sub New()
        If investments Is Nothing Then investments = New List(Of investment)
    End Sub

    Public Function getAssetList() As List(Of investment)
        Dim tempList As New List(Of investment)

        For Each investment As investment In investments
            If investment.wealthPerTurn >= 0 Then tempList.Add(investment)
        Next

        Return tempList
    End Function
    Public Function getLiabilityList() As List(Of investment)
        Dim tempList As New List(Of investment)

        For Each investment As investment In investments
            If investment.wealthPerTurn < 0 Then tempList.Add(investment)
        Next

        Return tempList
    End Function
    Public Function getInvestment(ByVal name As String, ByVal starName As String, ByVal planetNumber As Integer)
        For Each investment As investment In investments
            If investment.name = name AndAlso investment.starName = starName AndAlso investment.planetNumber = planetNumber Then Return investment
        Next

        Return Nothing
    End Function
    Public Function getInvestmentsOnPlanet(ByVal starName As String, ByVal planetNumber As Integer) As List(Of investment)
        Dim tempList As New List(Of investment)

        For Each item As investment In investments
            If item.starName = starName AndAlso item.planetNumber = planetNumber Then tempList.Add(item)
        Next

        Return tempList
    End Function
    Public Function addInvestment(ByVal iName As String, ByVal iStarName As String, ByVal iPlanetNumber As Integer, ByVal iWealthPerTurn As Integer) As Boolean
        'check to see if investment exists: if it does, return false
        'if not, add investment to investments and return true
        Dim investment As New investment
        With investment
            .name = iName
            .starName = iStarName
            .planetNumber = iPlanetNumber
            .wealthPerTurn = iWealthPerTurn
        End With

        For Each item As investment In investments
            If item.name = investment.name AndAlso _
                item.starName = investment.starName AndAlso _
                item.planetNumber = investment.planetNumber AndAlso _
                item.wealthPerTurn = investment.wealthPerTurn Then
                Return False
            End If
        Next

        investments.Add(investment)
        Return True
    End Function
    Public Sub removeInvestment(ByRef investment As investment)
        'remove investment that is passed to it from investments list and set it to nothing
        'this should leave no identifable trace of the investment; when garbage collector calls it should be destroyed

        If investments.Contains(investment) = True Then
            investments.Remove(investment)
        Else
            'do nothing
        End If
        investment = Nothing
    End Sub
End Class


Public Class investment
    'investments are held here, stored in playerinfo and retrieved on an adhoc basis
    'not all investments need to return positive wealthPerTurn; indeed, many are negative
    'universal investments are stored as starName = "Oubliette", planetNumber = 0

    Public name As String
    Public starName As String
    Public planetNumber As Integer
    Public wealthPerTurn As Integer
End Class