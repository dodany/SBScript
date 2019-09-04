Public Class ListNodeError
    Property Nodes As List(Of NodeError)


    Sub New(n As Node, s As Stack(Of NodeError))
        Nodes = New List(Of NodeError)
        While s.Count > 0
            Nodes.Add(s.Pop())
        End While
    End Sub




End Class