Public Class Usuario
    Public Property Id As Integer
    Public Property Nombre As String
    Public Property Apellido As String
    Public Property Contraseña As String
    Public Property Email As String

    Public Sub New()

    End Sub
    Public Function ValidarUsuario() As Boolean
        Return Not String.IsNullOrEmpty(email) AndAlso Not String.IsNullOrEmpty(Contraseña)

    End Function
    Public Function ValidarEmail() As Boolean
        Return email.Contains("@") AndAlso email.Contains(".")
    End Function
    Public Function dtToUsuario(dataTable As DataTable) As Usuario
        If dataTable IsNot Nothing AndAlso dataTable.Rows.Count > 0 Then
            Dim row As DataRow = dataTable.Rows(0)
            Return New Usuario() With {
                .Id = Convert.ToInt32(row("Id")),
                .Nombre = Convert.ToString(row("Nombre")),
                .Apellido = Convert.ToString(row("Apellido")),
                .Email = Convert.ToString(row("Email")),
                .Contraseña = Convert.ToString(row("Contraseña"))
            }
        End If
        Return Nothing
    End Function
    ' Método para obtener el nombre completo del usuario
    Public Function ObtenerNombreCompleto() As String
        Return $"{Nombre} {Apellido}"
    End Function
    ' Método para mostrar información del usuario
    Public Function MostrarInformacion() As String
        Return $"ID: {Id}, Nombre: {ObtenerNombreCompleto()}, Email: {email}"
    End Function


End Class
