Public Class FileManager


    Function ReadFile(ByRef out As String) As String
        Dim ofd As New OpenFileDialog
        Dim path As String = ""
        Dim texto As String = ""

        ofd.Filter = "sbs files (*.sbs)|*.sbs"
        ofd.Title = "Seleccione archivo  sbs ..."


        If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            path = ofd.FileName
            out = path
        End If

        Try
            texto = My.Computer.FileSystem.ReadAllText(path)
            Return texto
        Catch ex As Exception
            MessageBox.Show("Error al leer el fichero debido a :" + ex.ToString)
        End Try

        Return texto

    End Function

    Function CreateFile(ByVal texto As String) As String

        Try
            Dim objeto As Object
            Dim archivo As Object

            Dim sdf As New SaveFileDialog()
            sdf.Filter = "sbs files (*.sbs)|*.sbs"

            If sdf.ShowDialog() = DialogResult.OK Then
                objeto = CreateObject("Scripting.FileSystemObject")
                archivo = objeto.CreateTextFile(sdf.FileName)
                archivo.writeLine(texto)
                archivo.close()

                Return sdf.FileName

            End If

        Catch ex As Exception
            MessageBox.Show("Error al crear el fichero debido a :" + ex.ToString)
        End Try
        Return ""
    End Function

End Class
