Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

Public Class sharedHashFunctions
    Private Function generateHash(input As String) As String
        If input = Nothing Then Return Nothing

        Dim hash As MD5 = MD5.Create()
        Dim data As Byte() = hash.ComputeHash(Encoding.UTF8.GetBytes(input))
        Dim sBuilder As New StringBuilder

        Dim i As Integer = 0
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next

        hash.Dispose()
        Return sBuilder.ToString
    End Function
    Private Sub writeWholeFile(ByVal inputString As String, ByVal filename As String)
        'inputstring is the stringread version of a file, usually pulled as the result of readWholeFile(hashFileName), eg. readWholeFile("hashstarmap.xml")
        'filename should be the name of the hash file, eg. hashstarmap.txt

        If System.IO.File.Exists(filename) = False Then System.IO.File.Create(filename).Dispose()

        Dim writer As New System.IO.StreamWriter(filename, False)       ' append = false causes it to overwrite the old file
        writer.WriteLine(inputString)
        writer.Dispose()
    End Sub
    Private Function readWholeFile(ByVal filename As String) As String
        'pulls entire file as a string
        'usually used to get xml file for hashing and retrieving hash file

        If System.IO.File.Exists(filename) Then
            Dim txtr As IO.TextReader = New IO.StreamReader(filename)
            Dim output As String = txtr.ReadToEnd
            If output = Nothing Then Return Nothing Else Return output.ToString()
        Else
            Return Nothing
        End If
    End Function

    Public Sub addHashFile(ByVal type As String)
        Dim hashFilename As String = type & ".hash"
        Dim xmlFilename As String = type & ".xml"

        Dim inputString As String = readWholeFile(xmlFilename)
        writeWholeFile(generateHash(inputString), hashFilename)
    End Sub

    Public Function checkHash(ByVal type As String) As Boolean
        Dim hashFilename As String = type & ".hash"
        Dim xmlFilename As String = type & ".xml"
        Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase

        Dim hash1 As String = readWholeFile(hashFilename)
        If hash1 = Nothing Then Return Nothing
        hash1 = hash1.Replace(vbCrLf, "")       ' remove any linebreaks
        Dim hash2 As String = generateHash(readWholeFile(xmlFilename))
        If hash2 = Nothing Then Return Nothing

        If hash1 = hash2 Then Return True Else Return False
    End Function
End Class
