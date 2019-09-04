Public Class MethodCall : Inherits Node
    Property ID As String
    Property Parameters As List(Of Node)

    Public Overrides Function Execute() As Object
        ''Throw New NotImplementedException()
        Dim Values = New Queue(Of Object)
        Dim fullID = ID
        For Each n As Node In Parameters
            Dim val = n.Execute
            fullID = fullID + "_" + Symbol.GetValueTypeStr(val)
            Values.Enqueue(val)
        Next
        Select Case ID
            Case "Mostrar"
                Dim val = Values.Dequeue
                Dim str = ""

                If val IsNot Nothing Then
                    str = CStr(val)
                End If

                Dim x = 0
                While Values.Count > 0
                    Dim rep = ""
                    Dim v = Values.Dequeue
                    If v IsNot Nothing Then
                        rep = CStr(v)
                    End If
                    str = str.Replace("{" + CStr(x) + "}", rep)
                    x = x + 1
                End While
                CodeManager.WriteLine(str)

            Case Else
                Dim method = SymbolTableManager.Search(fullID)
                If method IsNot Nothing AndAlso method.IsMethod Then
                    Dim args = method.Arguments
                    SymbolTableManager.OpenMethodContext()
                    For Each arg As Symbol In args
                        Dim var = New Symbol(arg.ID, arg.Type)
                        var.Value = Values.Dequeue
                        SymbolTableManager.Add(var)
                    Next

                    For Each n As Node In method.Instructions
                        Dim ret = n.Execute
                        If n.ReturnExp Then
                            SymbolTableManager.CloseMethodContext()
                            Return ret
                        End If
                    Next
                    SymbolTableManager.CloseMethodContext()
                End If
        End Select
        Return Nothing
    End Function

    Sub New(id As String, s As Stack(Of Node))
        Me.ID = id
        Parameters = New List(Of Node)
        While s.Count > 0
            Parameters.Add(s.Pop())
        End While
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Método"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        content += GraphSimpleChild(nodeId, 1, ID)
        If Parameters.Count > 0 Then
            content += GraphChildNodes(nodeId, 2, Parameters, "Parámetros")
        End If
        Return content
    End Function
End Class
