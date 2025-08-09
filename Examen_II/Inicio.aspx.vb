Imports System.Data.SqlClient

Public Class Clientes1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub limpiarCampos()
        TxtNombreCliente.Text = String.Empty
        TxtApellidoCliente.Text = String.Empty
        TxtTelefono.Text = String.Empty
        txtEmail.Text = String.Empty

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim cliente As New Clientes() With {
            .Nombre = TxtNombreCliente.Text,
            .Apellido = TxtApellidoCliente.Text,
            .Telefono = TxtTelefono.Text,
            .Email = txtEmail.Text,
            .FechaRegistro = DateTime.Now
        }
        Try
            Dim helper As New DatabaseHelper()
            Dim query As String = "INSERT INTO Clientes (Nombre, Apellido, Telefono, Email) VALUES (@Nombre, @Apellido, @Telefono, @Email)"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Nombre", cliente.Nombre),
                New SqlParameter("@Apellido", cliente.Apellido),
                New SqlParameter("@Telefono", cliente.Telefono),
                New SqlParameter("@Email", cliente.Email)
            }
            If helper.ExecuteNonQuery(query, parametros) Then
                LblMensaje.Text = "Cliente guardado exitosamente."
                LblMensaje.ForeColor = System.Drawing.Color.Green
                GridView1.DataBind()
                limpiarCampos()
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
            cliente.Id = clienteId
            cliente.Nombre = GridView1.SelectedRow.Cells(1).Text
            cliente.Apellido = GridView1.SelectedRow.Cells(2).Text
            cliente.Telefono = GridView1.SelectedRow.Cells(3).Text
            ' Aquí puedes hacer algo con el cliente seleccionado, como mostrar sus detalles o editarlo.
            TxtNombreCliente.Text = cliente.Nombre
            TxtApellidoCliente.Text = cliente.Apellido
            TxtTelefono.Text = cliente.Telefono

        End If

    End Sub

    Protected Sub GridView1_RowDeleted(sender As Object, e As GridViewDeletedEventArgs)
        Try
            Dim helper As New DatabaseHelper()
            Dim clienteId As Integer = Convert.ToInt32(e.Keys("ClienteID"))
            Dim query As String = "DELETE FROM Clientes WHERE ClienteID = @ClienteID"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@ClienteID", e.Keys("ClienteID"))
            }
            ' Ejecutar la consulta para eliminar el cliente
            If e.Exception IsNot Nothing Then
                LblMensaje.Text = "Error al eliminar el cliente: " & e.Exception.Message
                LblMensaje.ForeColor = System.Drawing.Color.Red
                e.ExceptionHandled = True
                Return
            End If
            If helper.ExecuteNonQuery(query, parametros) Then
                LblMensaje.Text = "Cliente eliminado exitosamente."
                LblMensaje.ForeColor = System.Drawing.Color.Green
                GridView1.DataBind()
            Else
                LblMensaje.Text = "Error al eliminar el cliente."
                LblMensaje.ForeColor = System.Drawing.Color.Red
            End If
        Catch ex As Exception

        End Try




    End Sub
End Class