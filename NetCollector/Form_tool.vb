Imports System.Net.NetworkInformation
Imports System.Net


Public Class Form_tool

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        ' Deshabilitamos el botón de inicio para evitar que se inicie un escaneo adicional mientras se está realizando uno
        btnStart.Enabled = False

        ' Obtenemos la subred a escanear y generamos las direcciones IP para escanear
        Dim subnet As String = txtIP.Text
        Dim baseIP As String = subnet.Substring(0, subnet.LastIndexOf(".") + 1)
        Dim addresses As New List(Of String)
        For i As Integer = 2 To 100
            addresses.Add(baseIP & i.ToString())
        Next

        ' Configuramos la barra de progreso
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = addresses.Count
        ProgressBar1.Value = 0

        ' Realizamos el escaneo de ping para cada dirección IP en la lista y mostramos los resultados en el ListBox
        For Each address As String In addresses
            Dim pingSender As New Ping()
            Dim options As New PingOptions()
            options.DontFragment = True
            Dim data As String = "ping"
            Dim buffer As Byte() = System.Text.Encoding.ASCII.GetBytes(data)
            Dim timeout As Integer = 254
            Dim reply As PingReply = pingSender.Send(address, timeout, buffer, options)
            If reply.Status = IPStatus.Success Then
                Try
                    Dim hostEntry As IPHostEntry = Dns.GetHostEntry(address)
                    Dim hostName As String = hostEntry.HostName
                    Dim domainName As String = hostEntry.HostName.Split("."c).LastOrDefault()
                    ListBox1.Items.Add(hostName & vbTab & address & vbTab & domainName)
                Catch ex As Exception
                    ListBox1.Items.Add("No Disp" & vbTab & address)
                End Try
            End If
            ProgressBar1.Value += 1
            Application.DoEvents()
        Next

        ' Habilitamos el botón de inicio nuevamente después de que se complete el escaneo
        btnStart.Enabled = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
        Form_export.Show()
    End Sub

    Private Sub Form_tool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label6.Text = Form_login.txtUser.Text
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Form_login.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ListBox1.Items.Clear()
        txtIP.Text = ""
    End Sub
End Class
