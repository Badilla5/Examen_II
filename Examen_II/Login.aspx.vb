Imports System.Data.SqlClient
Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Function ValidarUsuario(usuario As Usuario) As Boolean
        Try
            Dim helper As New DatabaseHelper()
            Dim parametros As New List(Of SqlParameter) From {
            New SqlParameter("@Email", usuario.Email),
            New SqlParameter("@Password", usuario.Contraseña)
        }
            ' Ejecutar la consulta para verificar el usuario
            Dim query As String = "SELECT * FROM Usuarios WHERE Email = @Email AND Contraseña = @Password;"
            Dim dataTable As DataTable = helper.ExecuteQuery(query, parametros)
            Dim usuariodt As New Usuario()

            ' Verificar si se encontró el usuario
            If dataTable.Rows.Count > 0 Then
                ' Usuario encontrado, puedes redirigir o realizar otra acción
                usuariodt = usuariodt.dtToUsuario(dataTable)
                Session.Add("UsuarioId", usuariodt.Id.ToString())
                Session.Add("UsuarioNombre", usuariodt.Nombre.ToString())
                Session.Add("UsuarioApellido", usuariodt.Apellido.ToString())
                Session.Add("UsuarioEmail", usuariodt.Email.ToString())
                Return True
            Else
                ' Usuario no encontrado, manejar el error
                Return False

            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)

        Dim Usuario As New Usuario() With {
            .Email = txtEmail.Text,
            .Contraseña = txtPass.Text
        }

        If ValidarUsuario(Usuario) Then
            ' Redirigir al usuario a la página de inicio o dashboard
            Response.Redirect("Inicio.aspx")
        Else
            ' Mostrar mensaje de error si las credenciales son incorrectas
            lblError.Text = "Credenciales incorrectas. Por favor, inténtelo de nuevo."
            lblError.Visible = True
        End If

    End Sub
End Class