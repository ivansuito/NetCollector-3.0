Imports System.Data.OleDb

Public Class Form_login

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        'Establece conexion con la base de datos
        Dim path As String = Application.StartupPath
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & path & "\..\..\..\BD\users.accdb"


        'Crear una instancia de OleDbConnection y abrir la conexión
        Using conn As New OleDbConnection(connStr)
            conn.Open()

            'Comprobar usuario y contraseña
            Dim cmdText As String = "SELECT COUNT(*) FROM usuarios WHERE Usuarios = ? AND Contraseñas = ?"
            Using cmd As New OleDbCommand(cmdText, conn)
                cmd.Parameters.AddWithValue("Usuarios", txtUser.Text)
                cmd.Parameters.AddWithValue("Contraseñas", txtPwd.Text)
                Dim count As Integer = CInt(cmd.ExecuteScalar())

                'Mensaje de error si no son correctos los datos de inicio de sesión:
                If count = 0 Then
                    MessageBox.Show("Error de autenticación. Si no estás registrado, usa el botón REGISTER.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtPwd.Text = ""
                    txtUser.Text = ""
                Else
                    Form_tool.Show()
                    Me.Hide()
                    Form_tool.Label6.Text = txtUser.Text
                    txtPwd.Text = ""
                End If
            End Using

            'Cerrar la conexion
            conn.Close()
        End Using

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        End
    End Sub

    Private Sub txtPwd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPwd.TextChanged
        txtPwd.PasswordChar = ("*")
    End Sub

    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Form_register.Show()
        Me.Hide()
    End Sub

    Private Sub txtUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUser.TextChanged

    End Sub
End Class