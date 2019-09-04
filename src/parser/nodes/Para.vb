Public Class Para : Inherits Node
    Property Declaration As Node
    Property Condition As Node
    Property StepAction As Node
    Property Instructions As List(Of Node)
    Public Overrides Function Execute() As Object
        ''Throw New NotImplementedException()
        Declaration.Execute()

        While True
            Dim cond = Condition.Execute
            If cond Then
                For Each n As Node In Instructions
                    SymbolTableManager.OpenContext()
                    Dim ret = n.Execute()
                    If n.ExitLoop Then
                        SymbolTableManager.CloseContext()
                        Return Nothing
                    ElseIf n.ContinueLoop Then
                        StepAction.Execute()
                        SymbolTableManager.CloseContext()
                        Exit For
                    ElseIf n.ReturnExp Then
                        Me.ReturnExp = True
                        SymbolTableManager.CloseContext()
                        Return ret
                    End If
                    SymbolTableManager.CloseContext()
                Next
                StepAction.Execute()
            Else
                Exit While
            End If
        End While
        Return Nothing
    End Function

    Sub New(dec As DeclareVar, c As Node, s As Queue(Of Node), stepact As String)
        Me.Declaration = dec
        Me.Condition = c
        Me.Instructions = New List(Of Node)
        While s.Count > 0
            Dim n = s.Dequeue()
            If n IsNot Nothing Then
                Instructions.Add(n)
            End If
        End While

        Select Case stepact
            Case "++"
                StepAction = New AssignNode(dec.Identifiers(0), New ArithNode(New VarCall(dec.Identifiers(0)), New ValueNode(1), "+"))
            Case "--"
                StepAction = New AssignNode(dec.Identifiers(0), New ArithNode(New VarCall(dec.Identifiers(0)), New ValueNode(1), "-"))
        End Select
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Para"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        content += Condition.Graph(nodeId, 1)
        content += GraphChildNodes(nodeId, 2, Instructions)
        content += StepAction.Graph(nodeId, 3)
        Return content
    End Function
End Class
