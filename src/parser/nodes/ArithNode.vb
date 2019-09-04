Public Class ArithNode : Inherits Node
    Property Arg1 As Node
    Property Arg2 As Node
    Property Sign As Char

    Public Overrides Function Execute() As Object
        ''Throw New NotImplementedException()
        If Arg1 IsNot Nothing AndAlso Arg2 IsNot Nothing Then
            Dim res1 = Arg1.Execute()
            Dim res2 = Arg2.Execute()
            If res1 IsNot Nothing And res2 IsNot Nothing Then
                Select Case Sign
                    Case "+"
                        If Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                            ''Boolean + Boolean
                            Return res1 Or res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                            ''Number + Boolean
                            If res2 Then
                                res2 = 1
                            Else
                                res2 = 0
                            End If
                            Return res1 + res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.STRINGTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                            ''String + Boolean
                            Return res1 + CStr(res2)
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''Boolean + Number
                            If res1 Then
                                res1 = 1
                            Else
                                res1 = 0
                            End If
                            Return res1 + res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''Number + Number
                            Return res1 + res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.STRINGTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''String + Number
                            Return res1 + CStr(res2)
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.STRINGTYPE) Then
                            ''Boolean + String
                            Return CStr(res1) + res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                            ''Number + String
                            Return CStr(res1) + res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.STRINGTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.STRINGTYPE) Then
                            Return res1 + res2
                        Else
                            ''Error semántico
                            Return Nothing
                        End If
                        ''Return res1 + res2
                    Case "-"
                        If Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                            ''Number + Boolean
                            If res2 Then
                                res2 = 1
                            Else
                                res2 = 0
                            End If
                            Return res1 - res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''Boolean + Number
                            If res1 Then
                                res1 = 1
                            Else
                                res1 = 0
                            End If
                            Return res1 - res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''Number + Number
                            Return res1 - res2
                        Else
                            ''Error semántico
                            Return Nothing
                        End If
                        ''Return res1 - res2
                    Case "/"
                        If Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                            ''Number + Boolean
                            If res2 Then
                                res2 = 1
                            Else
                                res2 = 0
                            End If
                            Return res1 / res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''Boolean + Number
                            If res1 Then
                                res1 = 1
                            Else
                                res1 = 0
                            End If
                            Return res1 / res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''Number + Number
                            Return res1 / res2
                        Else
                            ''Error semántico
                            Return Nothing
                        End If
                    Case "%"
                        If Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                            ''Number + Boolean
                            If res2 Then
                                res2 = 1
                            Else
                                res2 = 0
                            End If
                            Return res1 Mod res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''Boolean + Number
                            If res1 Then
                                res1 = 1
                            Else
                                res1 = 0
                            End If
                            Return res1 Mod res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''Number + Number
                            Return res1 Mod res2
                        Else
                            ''Error semántico
                            Return Nothing
                        End If
                    Case "*"
                        If Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                            ''Boolean + Boolean
                            Return res1 And res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                            ''Number + Boolean
                            If res2 Then
                                res2 = 1
                            Else
                                res2 = 0
                            End If
                            Return res1 * res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''Boolean + Number
                            If res1 Then
                                res1 = 1
                            Else
                                res1 = 0
                            End If
                            Return res1 * res2
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''Number + Number
                            Return res1 * res2
                        Else
                            ''Error semántico
                            Return Nothing
                        End If
                        ''Return res1 * res2
                    Case "^"
                        If Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) Then
                            ''Number + Boolean
                            If res2 Then
                                res2 = 1
                            Else
                                res2 = 0
                            End If
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.BOOLEANTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''Boolean + Number
                            If res1 Then
                                res1 = 1
                            Else
                                res1 = 0
                            End If
                        ElseIf Symbol.GetValueTypeStr(res1) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) And Symbol.GetValueTypeStr(res2) = Symbol.GetTypeStr(Symbol.TypeEnum.NUMBERTYPE) Then
                            ''Number + Number
                            Return Math.Pow(res1, res2)
                        Else
                            ''Error semántico
                            Return Nothing
                        End If
                        Return Math.Pow(res1, res2)
                End Select
            Else
                'Error semántico
            End If

        ElseIf Arg1 IsNot Nothing AndAlso Arg2 Is Nothing AndAlso Sign = "-" Then
            Dim res1 = Arg1.Execute
            If res1 IsNot Nothing And (res1.GetType.Equals(GetType(Decimal)) Or res1.GetType.Equals(GetType(Integer))) Then
                Return res1 * -1
            Else
                ''Error semántico
                Return Nothing
            End If
        End If
        Return Nothing
    End Function

    Sub New(n1 As Node, n2 As Node, s As String)
        Arg1 = n1
        Arg2 = n2
        Sign = s
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Expresión"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" + vbCrLf
        If Arg1 IsNot Nothing And Arg2 IsNot Nothing Then
            content += Arg1.Graph(nodeId, 1)
            content += GraphSimpleChild(nodeId, 2, CStr(Sign))
            content += Arg2.Graph(nodeId, 3)
        ElseIf Arg2 IsNot Nothing Then
            content += GraphSimpleChild(nodeId, 1, CStr(Sign))
            content += Arg1.Graph(nodeId, 2)
        End If
        Return content
    End Function
End Class
