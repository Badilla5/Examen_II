Imports System.Data.SqlClient
Imports System.Net.NetworkInformation
Imports Microsoft.Ajax.Utilities

Public Class Clientes1
    Inherits System.Web.UI.Page

    Private ReadOnly repo As New ClienteRepository()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("UsuarioId") Is Nothing Then

            Response.Redirect("Login.aspx")
        Else
            ' Si hay sesión, mostrar el nombre del usuario
            lblNombre.Text = "Bienvenido, " & Session("UsuarioNombre").ToString()


        End If
    End Sub
    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsPostBack Then
            GridView1.DataBind()
        End If
    End Sub

    Protected Sub limpiarCampos()
        ' Limpia los campos de texto y el mensaje
        TxtNombreCliente.Text = String.Empty
        TxtApellidoCliente.Text = String.Empty
        TxtTelefono.Text = String.Empty
        txtEmail.Text = String.Empty

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        ' Valida y guarda los datos del cliente
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
        ' Carga los datos del cliente seleccionado en los campos de texto
        Dim clienteId As Integer = Convert.ToInt32(GridView1.SelectedDataKey.Value)
        Dim cliente As Clientes = repo.ObtenerClientePorId(clienteId)
        If cliente IsNot Nothing Then
            hfClienteId.Value = cliente.ClienteID.ToString()
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
        ' Eliminael cliente seleccionado
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
        ' Limpia los campos y el mensaje
        limpiarCampos()
        LblMensaje.Text = String.Empty
        LblMensaje.ForeColor = System.Drawing.Color.Black
        GridView1.DataBind()
    End Sub
End Class