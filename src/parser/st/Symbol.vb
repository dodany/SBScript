Public Class Symbol
    Public Enum TypeEnum
        NUMBERTYPE
        STRINGTYPE
        BOOLEANTYPE
        VOIDTYPE
    End Enum
    Property ID As String
    Property SimpleMethodID As String
    Property Value As Object
    Property IsMethod As Boolean
    Property Type As TypeEnum
    Property Arguments As List(Of Symbol)
    Property Instructions As List(Of Node)

    Sub New(id As String, type As TypeEnum)
        Me.ID = id
        Me.Type = type
        Me.IsMethod = False


    End Sub

    Sub New(id As String, simpleId As String, type As TypeEnum, args As List(Of Symbol), instr As List(Of Node))
        Me.ID = id
        Me.SimpleMethodID = simpleId
        Me.Type = type
        Me.Arguments = args
        Me.Instructions = instr
        Me.IsMethod = True
    End Sub

    Public Shared Function GetTypeStr(t As TypeEnum) As String
        Select Case t
            Case TypeEnum.NUMBERTYPE
                Return "Number"
            Case TypeEnum.BOOLEANTYPE
                Return "Boolean"
            Case TypeEnum.STRINGTYPE
                Return "String"
            Case Else
                Return "Void"
        End Select
    End Function

    Public Shared Function GetValueTypeStr(obj As Object) As String
        If obj IsNot Nothing Then
            Dim t = obj.GetType
            Select Case t
                Case GetType(String)
                    Return "String"
                Case GetType(Integer)
                    Return "Number"
                Case GetType(Decimal)
                    Return "Number"
                Case GetType(Double)
                    Return "Number"
                Case GetType(Boolean)
                    Return "Boolean"
                Case Else
                    Return "Void"
            End Select
        End If
        Return Nothing
    End Function
End Class
