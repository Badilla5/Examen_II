Imports System.Data.SqlClient
Imports System.Net.NetworkInformation
Imports Microsoft.Ajax.Utilities

Public Class Clientes1
    Inherits System.Web.UI.Page

    Private ReadOnly repo As New ClienteRepository()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsPostBack Then
            ' Inicializar el GridView o cualquier otro control necesario
            GridView1.DataBind()
        End If
    End Sub

    Protected Sub limpiarCampos()
        TxtNombreCliente.Text = String.Empty
        TxtApellidoCliente.Text = String.Empty
        TxtTelefono.Text = String.Empty
        txtEmail.Text = String.Empty

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim cliente As New Clientes() With {
            .Nombre = TxtNombreCliente.Text.Trim(),
            .Apellido = TxtApellidoCliente.Text.Trim(),
            .Telefono = TxtTelefono.Text.Trim(),
            .Email = txtEmail.Text.Trim()
        }
        If Not validarDatos(cliente) Then
            LblMensaje.Text = "Por favor, complete todos los campos correctamente."
            LblMensaje.ForeColor = System.Drawing.Color.Red
            Return
        End If
        Try
            Dim exito As Boolean
            If String.IsNullOrEmpty(hfClienteId.Value) Then
                ' Agregar nuevo cliente
                exito = repo.AgregarCliente(cliente)
                If exito Then
                    LblMensaje.Text = "Cliente agregado exitosamente."
                    LblMensaje.ForeColor = System.Drawing.Color.Green
                Else
                    LblMensaje.Text = "Error al agregar el cliente."
                    LblMensaje.ForeColor = System.Drawing.Color.Red
                End If
            Else
                ' Actualizar cliente existente
                cliente.ClienteID = Convert.ToInt32(hfClienteId.Value)
                exito = repo.ActualizarCliente(cliente)
                If exito Then
                    LblMensaje.Text = "Cliente actualizado exitosamente."
                    LblMensaje.ForeColor = System.Drawing.Color.Green
                Else
                    LblMensaje.Text = "Error al actualizar el cliente."
                    LblMensaje.ForeColor = System.Drawing.Color.Red
                End If
            End If
            limpiarCampos()
            hfClienteId.Value = String.Empty
            GridView1.DataBind()
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
        If e.Exception IsNot Nothing Then
            LblMensaje.Text = "Error al eliminar el cliente: " & e.Exception.Message
            LblMensaje.ForeColor = System.Drawing.Color.Red
            e.ExceptionHandled = True
        Else
            LblMensaje.Text = "Cliente eliminado exitosamente."
            LblMensaje.ForeColor = System.Drawing.Color.Green
            limpiarCampos()
        End If
        GridView1.DataBind()

    End Sub

    Protected Sub BtnCancelar_Click(sender As Object, e As EventArgs)
        limpiarCampos()
        LblMensaje.Text = String.Empty
        LblMensaje.ForeColor = System.Drawing.Color.Black
        GridView1.DataBind()
    End Sub
End Class