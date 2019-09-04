Public Class DibujarEXP : Inherits Node
    Property Expression As Node
    Public Overrides Function Execute() As Object
        Dim dot = New Graphviz(GraphExpression(), "Expresion" & CodeManager.GetGraphExpressionCount, CodeManager.CurrentPath)
        dot.Graph()
        Return Nothing
    End Function

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Método"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        content += GraphSimpleChild(nodeId, 1, "DibujarAST")
        content += Expression.Graph(nodeId, 2)
        Return content
    End Function

    Sub New(exp As Node)
        Expression = exp
    End Sub

    Private Function GraphExpression() As String
        Dim label = "AST Expresión"
        Dim content = """node0"" [label=""" & label & """, shape=""rectangle""];" & vbCrLf
        content += Expression.Graph("node0", 1)
        Return content
    End Function
End Class
