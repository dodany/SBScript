Imports System.IO
Public Class Incluye : Inherits Node
    Property File As String

    Public Overrides Function Execute() As Object
        ''Throw New NotImplementedException()
        Try
            Dim texto = My.Computer.FileSystem.ReadAllText(CodeManager.CurrentPath + "\\" + File)
            Dim strReader As TextReader
            strReader = New StringReader(texto)
            Setup()
            Parse(strReader)

        Catch ex As Exception
            '' Error semantico
        End Try

        Return Nothing
    End Function

    Sub New(f As String)
        File = f
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Throw New NotImplementedException()
    End Function
End Class
