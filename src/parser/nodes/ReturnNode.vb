Public Class ReturnNode : Inherits Node
    Property Value As Node

    Public Overrides Function Execute() As Object
        ''Throw New NotImplementedException()
        ReturnExp = True
        If Value IsNot Nothing Then
            Return Value.Execute()
        End If
        Return Nothing
    End Function

    Sub New(val As Node)
        Me.Value = val
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId + """ [label=""Retorno"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        If Value IsNot Nothing Then
            content += Value.Graph(nodeId, 1)
        End If
        Return content
    End Function
End Class
