Public MustInherit Class Node
    Property ExitLoop As Boolean = False
    Property ContinueLoop As Boolean = False
    Property ReturnExp As Boolean = False
    MustOverride Function Execute() As Object

    MustOverride Function Graph(parent As String, i As Integer) As String

    Function GraphSimpleChild(parent As String, i As Integer, label As String) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""" & label & """, shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" + vbCrLf
        Return content
    End Function

    Function GraphChildNodes(parent As String, i As Integer, nodes As List(Of Node)) As String
        Return GraphChildNodes(parent, i, nodes, "Cuerpo")
    End Function

    Function GraphChildNodes(parent As String, i As Integer, nodes As List(Of Node), label As String) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Cuerpo"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        Dim index = 1
        For Each n As Node In nodes
            content += n.Graph(nodeId, index)
            index += 1
        Next
        Return content
    End Function
End Class
