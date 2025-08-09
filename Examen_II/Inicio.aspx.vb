Imports System.Data.SqlClient

Public Class Clientes1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim cliente As New Clientes() With {
            .Nombre = TxtNombreCliente.Text,
            .Apellido = TxtApellidoCliente.Text,
            .Telefono = TxtTelefono.Text,
            .FechaRegistro = DateTime.Now
        }
        Try
            Dim helper As New DatabaseHelper()
            Dim query As String = "INSERT INTO Clientes (Nombre, Apellido, Telefono) VALUES (@Nombre, @Apellido, @Telefono)"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Nombre", cliente.Nombre),
                New SqlParameter("@Apellido", cliente.Apellido),
                New SqlParameter("@Telefono", cliente.Telefono)
            }
            If helper.ExecuteNonQuery(query, parametros) Then
                LblMensaje.Text = "Cliente guardado exitosamente."
                LblMensaje.ForeColor = System.Drawing.Color.Green
                GridView1.DataBind()
            Else
                LblMensaje.Text = "Error al guardar el cliente."
                LblMensaje.ForeColor = System.Drawing.Color.Red
            End If
        Catch ex As Exception
            LblMensaje.Text = "Ocurrió un error: " & ex.Message
            LblMensaje.ForeColor = System.Drawing.Color.Red
        End Try

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim index As Integer = GridView1.SelectedIndex
        Dim clienteId As Integer = Convert.ToInt32(GridView1.DataKeys(index).Value)
        If index >= 0 Then
            Dim cliente As New Clientes()
            cliente.Nombre = GridView1.SelectedRow.Cells(1).Text
            cliente.Apellido = GridView1.SelectedRow.Cells(2).Text
            cliente.Telefono = GridView1.SelectedRow.Cells(3).Text
            ' Aquí puedes hacer algo con el cliente seleccionado, como mostrar sus detalles o editarlo.
        End If

    End Sub

    Protected Sub GridView1_RowDeleted(sender As Object, e As GridViewDeletedEventArgs)
        Try
            Dim helper As New DatabaseHelper()
            Dim query As String = "DELETE FROM Clientes WHERE Id = @Id"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Id", e.Keys("Id"))
            }
        Catch ex As Exception

        End Try




    End Sub
End Class