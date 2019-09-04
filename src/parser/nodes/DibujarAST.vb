Public Class DibujarAST : Inherits Node
    Property ID As String

    Public Overrides Function Execute() As Object
        Dim methods = SymbolTableManager.SearchAllMethod(ID)
        If methods.Count > 0 Then
            For Each sym In methods
                Dim dot = New Graphviz(GraphSymbol(sym), sym.ID, CodeManager.DefinedPath)
                dot.Graph()
            Next
        Else
            ''Error semántico el método no existe
        End If
        Return Nothing
    End Function

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Método"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        content += GraphSimpleChild(nodeId, 1, "DibujarAST")
        content += GraphSimpleChild(nodeId, 2, ID)
        Return content
    End Function

    Sub New(id As String)
        Me.ID = id
    End Sub

    Private Function GraphSymbol(sym As Symbol) As String
        Dim label = Symbol.GetTypeStr(sym.Type) & ":" & sym.SimpleMethodID & "("
        For Each arg In sym.Arguments
            label += Symbol.GetTypeStr(arg.Type) & ","
        Next
        label += ")"
        Dim content = """node0"" [label=""" & label & """, shape=""rectangle""];" + vbCrLf
        Dim index = 1
        For Each n As Node In sym.Instructions
            content += n.Graph("node0", index)
            index += 1
        Next
        Return content
    End Function

End Class
