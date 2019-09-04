Public Class SymbolTable
    Dim symbols As Dictionary(Of String, Symbol)
    Property Parent As SymbolTable

    Sub New()
        symbols = New Dictionary(Of String, Symbol)
    End Sub

    Public Function Exists(id As String) As Boolean
        Return symbols.ContainsKey(id)
    End Function

    Public Function Search(id As String) As Symbol
        Dim s As Symbol = Nothing
        symbols.TryGetValue(id, s)
        Return s
    End Function

    Public Sub Add(id As String, symbol As Symbol)
        symbols.Add(id, symbol)
    End Sub

    Public Function GetDictionary() As Dictionary(Of String, Symbol)
        Return symbols
    End Function

End Class
