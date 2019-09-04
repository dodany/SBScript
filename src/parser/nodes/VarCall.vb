Public Class VarCall : Inherits Node
    Property ID As String
    Public Overrides Function Execute() As Object
        ''Throw New NotImplementedException()
        Return SymbolTableManager.Search(ID).Value
    End Function

    Sub New(id As String)
        Me.ID = id
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""" & ID & """, shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        Return content
    End Function
End Class
