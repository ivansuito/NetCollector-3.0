Imports System.Data.OleDb
Imports System.IO
Imports Microsoft.Office.Interop.Excel
Public Class Form_export

    Private Sub btnTxt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTxt.Click
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Text Files (*.txt)|*.txt"
        sfd.FileName = "PingResults.txt"
        If sfd.ShowDialog() = DialogResult.OK Then
            Dim writer As New StreamWriter(sfd.FileName)
            For Each item As Object In Form_tool.ListBox1.Items
                writer.WriteLine(item.ToString())
            Next
            writer.Close()
            MessageBox.Show("Datos exportados correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub btnAccess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccess.Click
        ' Conexión a la base de datos
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Ivan\Documents\Visual Studio 2010\Projects\NetCollector\BD\exports.accdb;"
        Dim connection As System.Data.OleDb.OleDbConnection = New System.Data.OleDb.OleDbConnection(connectionString)


        ' Abrir la conexión
        connection.Open()

        ' Recorrer cada elemento del ListBox
        For Each item As String In Form_tool.ListBox1.Items
            Dim values() As String = item.Split(vbTab)
            Dim nombrehost As String = values(0)
            Dim ip As String = values(1)
            Dim dominio As String = If(values.Length > 2, values(2), "") ' Comprobamos si hay un tercer valor en la lista y si no, le asignamos una cadena vacía
            Dim command As New OleDbCommand("INSERT INTO Data ([Usuario], [Nombre Dispositivo], [Dirección IP], [Dominio]) VALUES (?, ?, ?, ?)", connection)
            command.Parameters.AddWithValue("Usuario", Form_login.txtUser.Text)
            command.Parameters.AddWithValue("Nombre Dispositivo", nombrehost)
            command.Parameters.AddWithValue("Dirección IP", ip)
            command.Parameters.AddWithValue("Dominio", dominio)
            command.ExecuteNonQuery()
        Next

        ' Cerrar la conexión
        connection.Close()

        ' Mostrar mensaje de éxito
        MessageBox.Show("Los datos se han exportado correctamente a la base de datos.")
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
        Form_tool.Show()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim excelApp As Excel.Application = New Excel.Application
        Dim workbook As Excel.Workbook = excelApp.Workbooks.Add()
        Dim worksheet As Excel.Worksheet = workbook.ActiveSheet

        ' Escribir los datos del ListBox en la hoja de Excel
        For i As Integer = 0 To Form_tool.ListBox1.Items.Count - 1
            worksheet.Cells(i + 1, 1) = Form_tool.ListBox1.Items(i).ToString()
        Next

        ' Guardar el archivo de Excel
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Archivo de Excel (*.xlsx)|*.xlsx"
        saveFileDialog1.Title = "Guardar archivo de Excel"
        saveFileDialog1.ShowDialog()

        If saveFileDialog1.FileName <> "" Then
            workbook.SaveAs(saveFileDialog1.FileName)
            MsgBox("Archivo guardado con éxito")
        End If

        ' Cerrar Excel y liberar los recursos
        excelApp.Quit()
        workbook = Nothing
        worksheet = Nothing
        excelApp = Nothing
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub
End Class