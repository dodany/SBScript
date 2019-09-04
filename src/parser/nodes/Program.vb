Public Class Program
    Property Header As Node
    Property Nodes As List(Of Node)

    Sub New(h As Node, s As Stack(Of Node))
        Header = h
        Nodes = New List(Of Node)
        While s.Count > 0
            Nodes.Add(s.Pop())
        End While
    End Sub

    Public Sub Execute()
        If Header IsNot Nothing Then
            Header.Execute()
        End If
        For Each n In Nodes
            n.Execute()
        Next
    End Sub
End Class
