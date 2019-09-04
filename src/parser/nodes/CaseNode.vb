Public Class CaseNode : Inherits Node
    Property Value As Object
    Property Instructions As List(Of Node)

    Sub New(val As Object, s As Queue(Of Node))
        Me.Value = val
        Instructions = New List(Of Node)
        Dim n = s.Dequeue()
        If n IsNot Nothing Then
            Instructions.Add(n)
        End If
    End Sub

    Public Overrides Function Execute() As Object
        Throw New NotImplementedException()
    End Function

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim label = "Default:"
        If Value IsNot Nothing Then
            label = "Caso " & CStr(Value) & ":"
        End If
        Dim content = """" & nodeId & """ [label=""" & label & """, shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId + """;" & vbCrLf
        content += GraphChildNodes(nodeId, 1, Instructions)
        Return content
    End Function
End Class
