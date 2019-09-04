Public Class IfNode : Inherits Node
    Property Condition As Node
    Property Instructions As List(Of Node)
    Property ElseInstructions As List(Of Node)

    Public Overrides Function Execute() As Object
        '' Throw New NotImplementedException()
        Dim cond = Condition.Execute
        If cond IsNot Nothing And Symbol.GetValueTypeStr(cond) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
            If cond Then
                SymbolTableManager.OpenContext()
                For Each n As Node In Instructions
                    Dim ret = n.Execute
                    Me.ContinueLoop = n.ContinueLoop
                    Me.ExitLoop = n.ExitLoop
                    Me.ReturnExp = n.ReturnExp
                    If ContinueLoop Or ExitLoop Then
                        SymbolTableManager.CloseContext()
                        Return Nothing
                    ElseIf ReturnExp Then
                        SymbolTableManager.CloseContext()
                        Return ret
                    End If
                Next
                SymbolTableManager.CloseContext()
            ElseIf ElseInstructions IsNot Nothing Then
                SymbolTableManager.OpenContext()
                For Each n As Node In ElseInstructions
                    Dim ret = n.Execute
                    Me.ContinueLoop = n.ContinueLoop
                    Me.ExitLoop = n.ExitLoop
                    Me.ReturnExp = n.ReturnExp
                    If ContinueLoop Or ExitLoop Then
                        SymbolTableManager.CloseContext()
                        Return Nothing
                    ElseIf ReturnExp Then
                        SymbolTableManager.CloseContext()
                        Return ret
                    End If
                Next
                SymbolTableManager.CloseContext()
            End If
        Else
            ''Error semantico se esperaba bool
        End If
        Return Nothing
    End Function

    Sub New(c As Node, s As Queue(Of Node), s1 As Queue(Of Node))
        Me.Condition = c
        Me.Instructions = New List(Of Node)
        While s.Count > 0
            Dim n = s.Dequeue()
            If n IsNot Nothing Then
                Instructions.Add(n)
            End If
        End While

        If s1 IsNot Nothing Then
            Me.ElseInstructions = New List(Of Node)
            While s1.Count > 0
                Dim n = s1.Dequeue()
                If n IsNot Nothing Then
                    ElseInstructions.Add(n)
                End If
            End While
        End If
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Instrucción Si"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        content += GraphSimpleChild(nodeId, 1, "Si")
        content += Condition.Graph(nodeId, 2)
        content += GraphChildNodes(nodeId, 3, Instructions)
        If ElseInstructions IsNot Nothing Then
            content += GraphSimpleChild(nodeId, 4, "Si No")
            content += GraphChildNodes(nodeId, 5, ElseInstructions)
        End If
        Return content
    End Function
End Class
