Public Class Selecciona : Inherits Node
    Property Expression As Node
    Property CaseList As List(Of CaseNode)
    Property DefaultCase As CaseNode

    Public Overrides Function Execute() As Object
        ''Throw New NotImplementedException()
        Dim value = Expression.Execute
        Dim match = False
        For Each c As CaseNode In CaseList
            match = match Or c.Value = value
            If match Then
                For Each n As Node In c.Instructions
                    Dim ret = n.Execute()
                    Me.ContinueLoop = n.ContinueLoop
                    If ContinueLoop Or n.ExitLoop Then
                        Return Nothing
                    ElseIf n.ReturnExp Then
                        Me.ReturnExp = True
                        Return ret
                    End If
                Next
            End If
        Next
        If Not match AndAlso DefaultCase IsNot Nothing Then
            For Each n As Node In DefaultCase.Instructions
                Dim ret = n.Execute()
                Me.ContinueLoop = n.ContinueLoop
                If ContinueLoop Or n.ExitLoop Then
                    Return Nothing
                ElseIf n.ReturnExp Then
                    Me.ReturnExp = True
                    Return ret
                End If
            Next
        End If
        Return Nothing
    End Function

    Sub New(exp As Node, s As Stack(Of CaseNode), def As CaseNode)
        Me.Expression = exp
        CaseList = New List(Of CaseNode)
        While s.Count > 0
            CaseList.Add(s.Pop)
        End While
        Me.DefaultCase = def
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Selecciona"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        content += Expression.Graph(nodeId, 1)
        content += GraphCases(nodeId, 2)
        If DefaultCase IsNot Nothing Then
            content += DefaultCase.Graph(nodeId, 3)
        End If
        Return content
    End Function

    Private Function GraphCases(parent As String, i As Integer) As String
        Dim nodeId = parent & i
        Dim content = """" & nodeId & """ [label=""Casos"", shape=""rectangle""];" & vbCrLf
        content += """" & parent & """ -> """ & nodeId & """;" & vbCrLf
        Dim index = 1
        For Each c As CaseNode In CaseList
            content += c.Graph(nodeId, index)
            index += 1
        Next
        Return content
    End Function
End Class
