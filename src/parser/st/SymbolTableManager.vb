Public Class SymbolTableManager
    Shared st As Stack(Of SymbolTable)
    Shared auxiliar As Stack(Of Stack(Of SymbolTable))

    Shared globalst As SymbolTable


    Public Shared Sub Init()
        st = New Stack(Of SymbolTable)
        auxiliar = New Stack(Of Stack(Of SymbolTable))
        globalst = New SymbolTable
        st.Push(globalst)
    End Sub

    Public Shared Function Add(sym As Symbol)
        If Not Exists(sym.ID) Then
            st.Peek().Add(sym.ID, sym)
            Return True
        End If
        Return False

    End Function
    Public Shared Function Exists(id As String)
        Return st.Peek().Exists(id)
    End Function

    Public Shared Function Search(id As String) As Symbol
        Dim context As SymbolTable = st.Peek()
        While context IsNot Nothing
            If context.Exists(id) Then
                Return context.Search(id)
            Else
                context = context.Parent
            End If
        End While
        Return Nothing
    End Function

    Public Shared Function SearchMethod(id As String) As Symbol
        If globalst.Exists(id) Then
            Return globalst.Search(id)
        End If
        Return Nothing
    End Function

    Public Shared Function SearchAllMethod(id As String) As List(Of Symbol)
        Dim methods = New List(Of Symbol)
        For Each key As String In globalst.GetDictionary.Keys
            Dim sym = globalst.Search(key)
            If sym.IsMethod And sym.SimpleMethodID = id Then
                methods.Add(sym)
            End If
        Next
        Return methods
    End Function

    Public Shared Sub OpenContext()
        Dim context As SymbolTable = New SymbolTable()
        context.Parent = st.Peek()
        st.Push(context)
    End Sub

    Public Shared Sub CloseContext()
        st.Pop()
    End Sub

    Public Shared Sub OpenMethodContext()
        Dim aux = New Stack(Of SymbolTable)
        While st.Count > 1
            aux.Push(st.Pop())
        End While
        auxiliar.Push(aux)

        OpenContext()
    End Sub

    Public Shared Sub CloseMethodContext()
        CloseContext()
        Dim aux = auxiliar.Pop()
        While aux.Count > 0
            st.Push(aux.Pop())
        End While
    End Sub
End Class
