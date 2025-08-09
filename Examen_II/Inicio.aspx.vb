Imports System.Data.SqlClient
Imports System.Net.NetworkInformation
Imports Microsoft.Ajax.Utilities

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
        Dim cliente As New Clientes()
        cliente.Nombre = TxtNombreCliente.Text.Trim()
        cliente.Apellido = TxtApellidoCliente.Text.Trim()
        cliente.Telefono = TxtTelefono.Text.Trim()
        cliente.Email = txtEmail.Text.Trim()
        If Not validarDatos(cliente) Then
            LblMensaje.Text = "Por favor, complete todos los campos correctamente."
            LblMensaje.ForeColor = System.Drawing.Color.Red
            Return
        End If
        Dim helper As New DatabaseHelper()
        Dim query As String
        Dim parametros As New List(Of SqlParameter)()
        If String.IsNullOrEmpty(hfClienteId.Value) Then
            ' Insertar nuevo cliente
            query = "INSERT INTO Clientes (Nombre, Apellido, Telefono, Email) VALUES (@Nombre, @Apellido, @Telefono, @Email)"
            parametros.Add(New SqlParameter("@Nombre", cliente.Nombre))
            parametros.Add(New SqlParameter("@Apellido", cliente.Apellido))
            parametros.Add(New SqlParameter("@Telefono", cliente.Telefono))
            parametros.Add(New SqlParameter("@Email", cliente.Email))
            LblMensaje.Text = "Cliente guardado exitosamente."
            LblMensaje.ForeColor = System.Drawing.Color.Green
        Else
            ' Actualizar cliente existente
            query = "UPDATE Clientes SET Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono, Email = @Email WHERE ClienteID = @ClienteID"
            parametros.Add(New SqlParameter("@ClienteID", Convert.ToInt32(hfClienteId.Value)))
            parametros.Add(New SqlParameter("@Nombre", cliente.Nombre))
            parametros.Add(New SqlParameter("@Apellido", cliente.Apellido))
            parametros.Add(New SqlParameter("@Telefono", cliente.Telefono))
            parametros.Add(New SqlParameter("@Email", cliente.Email))
            LblMensaje.Text = "Cliente actualizado exitosamente."
            LblMensaje.ForeColor = System.Drawing.Color.Green
        End If
        Try
            If helper.ExecuteNonQuery(query, parametros) Then
                limpiarCampos()
                GridView1.DataBind()
                hfClienteId.Value = String.Empty
            Else
                LblMensaje.Text = "Error al guardar el cliente."
                LblMensaje.ForeColor = System.Drawing.Color.Red
            End If
        Catch ex As Exception
            LblMensaje.Text = "Error: " & ex.Message
            LblMensaje.ForeColor = System.Drawing.Color.Red
        End Try

    End Sub
    Private Function validarDatos(cliente As Clientes) As Boolean
        ' Validar que los campos no estén vacíos 
        If Not cliente.validarNombre() OrElse Not cliente.validarApellido() OrElse Not cliente.ValidarTelefono() OrElse Not cliente.ValidarEmail() Then
            Return False
        End If
        Return True
    End Function
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim clienteId As Integer = Convert.ToInt32(GridView1.SelectedDataKey("ClienteID"))
        Dim helper As New DatabaseHelper()
        Dim query As String = "SELECT * FROM Clientes WHERE ClienteID = @ClienteID"
        Dim parametros As New List(Of SqlParameter) From {
            New SqlParameter("@ClienteID", clienteId)
        }
        hfClienteId.Value = clienteId.ToString()
        Dim dataTable As DataTable = helper.ExecuteQuery(query, parametros)
        If dataTable.Rows.Count > 0 Then
            hfClienteId.Value = clienteId.ToString()
            Dim clienteObj As New Clientes()
            Dim cliente As Clientes = clienteObj.dtToCliente(dataTable)
            TxtNombreCliente.Text = cliente.Nombre
            TxtApellidoCliente.Text = cliente.Apellido
            TxtTelefono.Text = cliente.Telefono
            txtEmail.Text = cliente.Email
        Else
            LblMensaje.Text = "Cliente no encontrado."
            LblMensaje.ForeColor = System.Drawing.Color.Red
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

    Protected Sub BtnCancelar_Click(sender As Object, e As EventArgs)
        limpiarCampos()
        LblMensaje.Text = String.Empty
        LblMensaje.ForeColor = System.Drawing.Color.Black
        GridView1.DataBind()
    End Sub
End Class