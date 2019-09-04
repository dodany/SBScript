Imports System.IO

Public Class Form1
    Private Sub btnCompilar_Click(sender As Object, e As EventArgs) Handles btnCompilar.Click
        Dim strReader As TextReader
        strReader = New StringReader(txtInput.Text)
        Setup()
        SymbolTableManager.Init()
        CodeManager.Init()
        Parse(strReader)

        Dim principal = SymbolTableManager.SearchMethod("Principal")
        If principal IsNot Nothing Then
            Dim p = New MethodCall("Principal", New Stack(Of Node))
            p.Execute()
        End If
    End Sub
End Class
