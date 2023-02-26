Imports System.Data.OleDb
Public Class Form_register

    Private Sub Form_register_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        End
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Form_login.Show()
        Me.Hide()
    End Sub

    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        ' Verificar si los campos no están vacíos
        If Not String.IsNullOrEmpty(txtUser.Text) AndAlso Not String.IsNullOrEmpty(txtPwd.Text) Then
            ' Establecer la cadena de conexión
            Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Ivan\Documents\Visual Studio 2010\Projects\NetCollector\BD\users.accdb"

            ' Comprobar si el usuario ya está registrado
            Using conn As New OleDbConnection(connStr)
                conn.Open()
                Dim cmdText As String = "SELECT COUNT(*) FROM usuarios WHERE Usuarios = ?"
                Using cmd As New OleDbCommand(cmdText, conn)
                    cmd.Parameters.AddWithValue("Usuarios", txtUser.Text)
                    Dim count As Integer = CInt(cmd.ExecuteScalar())
                    If count > 0 Then
                        MessageBox.Show("El usuario ya está registrado.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txtPwd.Text = ""
                        txtUser.Text = ""
                        Return
                    End If
                End Using
            End Using

            ' Insertar los datos en la tabla "usuarios"
            Using conn As New OleDbConnection(connStr)
                conn.Open()
                Dim cmdText As String = "INSERT INTO usuarios (Usuarios, Contraseñas) VALUES (?, ?)"
                Using cmd As New OleDbCommand(cmdText, conn)
                    cmd.Parameters.AddWithValue("Usuarios", txtUser.Text)
                    cmd.Parameters.AddWithValue("Contraseñas", txtPwd.Text)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("El usuario se ha registrado correctamente.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPwd.Text = ""
                    txtUser.Text = ""
                End Using
            End Using

            ' Ir al formulario de inicio de sesión
            Dim frmLogin As New Form_login()
            frmLogin.Show()
            Me.Hide()
        Else
            MessageBox.Show("Los campos de usuario y contraseña son obligatorios.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPwd.Text = ""
            txtUser.Text = ""
        End If
        
    End Sub

    Private Sub txtPwd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPwd.TextChanged
        txtPwd.PasswordChar = "*"
    End Sub
End Class