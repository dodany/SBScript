Imports System.IO

Public Class frmMain

    Dim file As New FileManager


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'ListNodeError.Add

        'Dim s = New Queue(Of Node)
        'Dim n = .Item(0).Data
        'If n IsNot Nothing Then
        '    s.Enqueue(n)
        'End If
        'Return s

        'Dim s1 = New Queue(Of NodeError)
        'Dim n = "a"
        's1.Enqueue(n)
        'Return s
    End Sub

    Private Sub GuardarComoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarComoToolStripMenuItem.Click


        Dim c As Control
        c = GetNextControl(tbcArchivo.SelectedTab, True)
        ''c.Text = file.ReadFile(c.Name)
        c.Name = file.CreateFile(c.Text)


    End Sub

    Private Sub AbrirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirToolStripMenuItem.Click

        Dim c As Control
        c = GetNextControl(tbcArchivo.SelectedTab, True)
        c.Text = file.ReadFile(c.Name)


    End Sub

    Private Sub AcercaDeSBScriptUSACCompiladores2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AcercaDeSBScriptUSACCompiladores2ToolStripMenuItem.Click


    End Sub


    Private Sub CerrarPestañaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CerrarPestañaToolStripMenuItem1.Click

        If tbcArchivo.TabPages.Count > 1 Then
            tbcArchivo.TabPages.Remove(tbcArchivo.SelectedTab)
        End If

    End Sub

    Private Sub NuevaVentanaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevaVentanaToolStripMenuItem.Click

        Dim myTabPage As New TabPage()
        Dim myRich As New RichTextBox

        myTabPage.Text = "Archivos"
        tbcArchivo.TabPages.Add(myTabPage)
        myRich.Dock = DockStyle.Fill
        myTabPage.Controls.Add(myRich)


    End Sub

    Private Sub NuevoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevoToolStripMenuItem.Click

        Dim c As Control
        c = GetNextControl(tbcArchivo.SelectedTab, True)
        c.Text = ""

    End Sub

    Private Sub EjecutarArchivoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EjecutarArchivoToolStripMenuItem1.Click

        Dim c As Control
        c = GetNextControl(tbcArchivo.SelectedTab, True)
        'Ejecutar(c.text)

        Dim strReader As TextReader
        strReader = New StringReader(c.Text)
        Setup()
        SymbolTableManager.Init()
        CodeManager.Init()
        CodeManager.CurrentPath = Path.GetDirectoryName(c.Name)
        Parse(strReader)

        CodeManager.txtConsola = rtbConsola

        Dim principal = SymbolTableManager.SearchMethod("Principal")
        If principal IsNot Nothing Then
            Dim p = New MethodCall("Principal", New Stack(Of Node))
            p.Execute()
        End If
    End Sub
End Class
