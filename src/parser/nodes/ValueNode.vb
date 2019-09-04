Public Class ValueNode : Inherits Node
    Property Value As Object
    Public Overrides Function Execute() As Object
        Return Value
    End Function

    Sub New(o As Object)
        Me.Value = o
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim label = ""
        If Value IsNot Nothing Then
            label = CStr(Value)
        End If
        Dim content = """" & nodeId & """ [label=""" & label & """, shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        Return content
    End Function
End Class
