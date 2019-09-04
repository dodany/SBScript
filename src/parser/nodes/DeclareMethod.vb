Public Class DeclareMethod : Inherits Node
    Property ID As String
    Property SimpleMethodID As String

    Property Type As Symbol.TypeEnum
    Property Arguments As List(Of Symbol)
    Property Instructions As List(Of Node)

    Public Overrides Function Execute() As Object
        ''Throw New NotImplementedException()
        Dim s = New Symbol(ID, SimpleMethodID, Type, Arguments, Instructions)
        If SymbolTableManager.SearchMethod(ID) Is Nothing Then
            SymbolTableManager.Add(s)
        Else
            ''Error semántico, el metodo ya existe
        End If
        Return Nothing
    End Function

    Sub New(id As String, t As Symbol.TypeEnum, args As Stack(Of Symbol), s As Queue(Of Node))
        Me.ID = id
        Me.SimpleMethodID = id
        Type = t
        Arguments = New List(Of Symbol)
        While args.Count > 0
            Dim arg = args.Pop()
            Me.ID = Me.ID + "_" + Symbol.GetTypeStr(arg.Type)
            Arguments.Add(arg)
        End While
        Instructions = New List(Of Node)
        While s.Count > 0
            Instructions.Add(s.Dequeue())
        End While
    End Sub

    Public Overrides Function Graph(parent As String, i As Integer) As String
        Throw New NotImplementedException()
    End Function
End Class
