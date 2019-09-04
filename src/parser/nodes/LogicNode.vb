Public Class LogicNode : Inherits Node
    Property Arg1 As Node
    Property Arg2 As Node
    Property Op As String

    Public Overrides Function Execute() As Object
        ''Throw New NotImplementedException()
        If Arg1 IsNot Nothing AndAlso Arg2 IsNot Nothing Then
            Dim res1 = Arg1.Execute()
            Dim res2 = Arg2.Execute()
            Select Case Op
                Case "&&"
                    If res1 IsNot Nothing And Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) And res2 IsNot Nothing And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                        Return res1 And res2
                    Else
                        ''Error semántico se esperaba bool y bool
                    End If
                Case "||"
                    If res1 IsNot Nothing And Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) And res2 IsNot Nothing And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                        Return res1 Or res2
                    Else
                        ''Error semántico se esperaba bool y bool
                    End If
                Case "!&"
                    If res1 IsNot Nothing And Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) And res2 IsNot Nothing And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                        Return res1 Xor res2
                    Else
                        ''Error semántico se esperaba bool y bool
                    End If
                Case ">"
                    Return res1 > res2
                Case "<"
                    Return res1 < res2
                Case "<="
                    Return res1 <= res2
                Case ">="
                    Return res1 >= res2
                Case "=="
                    Return res1 = res2
                Case "!="
                    Return res1 <> res2
                Case "~"
                    If res1 IsNot Nothing And res2 IsNot Nothing Then
                        If Symbol.GetValueTypeStr(res1) = Symbol.GetValueTypeStr(res2) Then
                            Select Case Symbol.GetValueTypeStr(res1)
                                Case Symbol.GetTypeStr(Symbol.TypeEnum.STRINGTYPE)
                                    Return UCase(RTrim(LTrim(res1))) = UCase(RTrim(LTrim(res2)))
                                Case Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE)
                                    Dim incerteza = CodeManager.DefinedIncerteza
                                    Return Math.Abs(res1 - res2) <= incerteza
                            End Select
                        End If
                    End If
                        Return res1 Xor res2
            End Select
        ElseIf Arg1 IsNot Nothing AndAlso Arg2 Is Nothing AndAlso Op = "!" Then
            Dim res1 = Arg1.Execute
            If res1 IsNot Nothing And Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                Return Not res1
            Else
                ''Error semántico se esperaba bool
            End If
        End If
        Return Nothing
    End Function

    Sub New(n1 As Node, n2 As Node, s As String)
        Arg1 = n1
        Arg2 = n2
        Op = s
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Expresión"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        If Arg1 IsNot Nothing And Arg2 IsNot Nothing Then
            content += Arg1.Graph(nodeId, 1)
            content += GraphSimpleChild(nodeId, 2, Op)
            content += Arg2.Graph(nodeId, 3)
        ElseIf Arg2 IsNot Nothing Then
            content += GraphSimpleChild(nodeId, 1, Op)
            content += Arg1.Graph(nodeId, 2)
        End If
        Return content
    End Function
End Class