Public Class CodeManager
    Public Shared DefinedIncerteza As Decimal
    Public Shared DefinedPath As String
    Public Shared CurrentPath As String
    Public Shared txtConsola As RichTextBox
    Private Shared GraphExpressionCount As Integer

    Public Shared Sub Init()
        DefinedIncerteza = 0
        DefinedPath = ""
        CurrentPath = ""
        GraphExpressionCount = 0
    End Sub

    Public Shared Function GetGraphExpressionCount()
        Dim i = GraphExpressionCount
        GraphExpressionCount += 1
        Return i
    End Function

    Public Shared Sub WriteLine(str As Object)
        txtConsola.AppendText(str & vbCrLf)
    End Sub

End Class
