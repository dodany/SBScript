Public Class Continuar : Inherits Node

    Public Overrides Function Execute() As Object
        Me.ContinueLoop = True
        Return Nothing
    End Function

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Continuar"", shape=""rectangle""];" + vbCrLf
        content += """" & parent & """ -> """ + nodeId & """;" + vbCrLf
        Return content
    End Function
End Class
