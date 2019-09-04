Public Class Header : Inherits Node
    Property Includes As List(Of Node)
    Property Defines As List(Of Node)

    Public Overrides Function Execute() As Object
        ''Throw New NotImplementedException()
        For Each inc As Node In Includes
            inc.Execute()
        Next
        For Each def As Node In Defines
            def.Execute()
        Next
        Return Nothing
    End Function

    Sub New(s As Stack(Of Node), s1 As Stack(Of Node))
        Includes = New List(Of Node)
        Defines = New List(Of Node)
        If s IsNot Nothing Then
            While s.Count > 0
                Includes.Add(s.Pop())
            End While
        End If

        If s1 IsNot Nothing Then
            While s1.Count > 0
                Defines.Add(s1.Pop())
            End While
        End If
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Throw New NotImplementedException()
    End Function
End Class
