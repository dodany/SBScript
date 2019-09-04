Public Class DeclareVar : Inherits Node
    Property Identifiers As List(Of String)
    Property Type As Symbol.TypeEnum
    Property Value As Node

    Public Overrides Function Execute() As Object
        ''Throw New NotImplementedException()
        Dim val = Nothing
        If Value IsNot Nothing Then
            val = Value.Execute
        End If
        For Each id As String In Identifiers
            Dim s = New Symbol(id, Type)
            If SymbolTableManager.Search(id) Is Nothing Then
                If Value IsNot Nothing Then
                    If val IsNot Nothing And Symbol.GetValueTypeStr(val) = Symbol.GetTypeStr(s.Type) Then
                        s.Value = val
                    Else
                        ''Error semántico los tipos no coinciden
                    End If
                End If
                SymbolTableManager.Add(s)
            Else
                ''Error semántico el simbolo ya existe
            End If
        Next
        Return Nothing
    End Function

    Sub New(s As Stack(Of String), t As Symbol.TypeEnum, val As Node)
        Identifiers = New List(Of String)
        Type = t
        Value = val
        While s.Count > 0
            Identifiers.Add(s.Pop())
        End While
    End Sub
    Sub New(id As String, t As Symbol.TypeEnum, val As Node)
        Identifiers = New List(Of String)
        Type = t
        Value = val
        Identifiers.Add(id)
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Declaración"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" + vbCrLf
        content += GraphSimpleChild(nodeId, 1, Symbol.GetTypeStr(Type))
        content += GraphListaID(nodeId, 2)
        If Value IsNot Nothing Then
            content += GraphSimpleChild(nodeId, 3, "=")
            content += Value.Graph(nodeId, 4)
        End If
        Return content
    End Function

    Private Function GraphListaID(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Lista IDs"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        Dim index = 1
        For Each id As String In Identifiers
            content += GraphSimpleChild(nodeId, index, id)
            index += 1
        Next
        Return content
    End Function

End Class
