Public Class Define : Inherits Node
    Property Value As Object

    Public Overrides Function Execute() As Object
        ''Throw New NotImplementedException()
        If Value.GetType.Equals(GetType(String)) Then
            CodeManager.DefinedPath = Value
        ElseIf Value.GetType.Equals(GetType(Integer)) Or Value.GetType.Equals(GetType(Decimal)) Then
            CodeManager.DefinedIncerteza = Value
        End If
        Return Nothing
    End Function

    Sub New(o As Object)
        Value = o
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Throw New NotImplementedException()
    End Function
End Class
