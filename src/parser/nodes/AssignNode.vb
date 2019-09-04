Public Class AssignNode : Inherits Node
    Property ID As String
    Property Value As Node
    Public Overrides Function Execute() As Object
        '' Throw New NotImplementedException()
        Dim s = SymbolTableManager.Search(ID)
        If s IsNot Nothing Then
            Dim val = Value.Execute
            If val IsNot Nothing And Symbol.GetValueTypeStr(val) = Symbol.GetTypeStr(s.Type) Then
                s.Value = Value.Execute
            Else
                ''Error semántico Tipos de datos incompatibles
            End If
        Else
            ''Error semántico (la variable no existe)
        End If
        Return Nothing
    End Function

    Sub New(id As String, val As Node)
        Me.ID = id
        Me.Value = val
    End Sub

    Sub New(val As Node)
        Me.Value = val
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Asignación"", shape=""rectangle""];" + vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        content += GraphSimpleChild(nodeId, 1, ID)
        content += GraphSimpleChild(nodeId, 2, "=")
        content += Value.Graph(nodeId, 3)
        Return content
    End Function
End Class
