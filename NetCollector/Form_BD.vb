Imports System.Data.OleDb
Public Class Form_BD

    Private Sub Form_BD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Dim path As String = Application.StartupPath
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & path & "\..\..\..\BD\exports.accdb"


        Dim conn As New OleDbConnection(connStr)

        Dim sql As String = "SELECT * FROM Data"
        Dim adapter As New OleDbDataAdapter(sql, conn)
        Dim ds As New DataSet()
        adapter.Fill(ds, "Data")
        DataGridView1.DataSource = ds.Tables("Data")
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.Hide()
        Form_export.Show()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        End
    End Sub
End Class