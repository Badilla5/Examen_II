Public Class Clientes
    Inherits Persona

    Public Property Apellido As String
    Public Property Telefono As String
    Public Property Direccion As String
    Public Property FechaRegistro As DateTime

    Public Sub New(apellido As String, telefono As String, direccion As String, fechaRegistro As Date)
        Me.Apellido = apellido
        Me.Telefono = telefono
        Me.Direccion = direccion
        Me.FechaRegistro = fechaRegistro
    End Sub
    Public Sub New()
        ' Constructor por defecto
    End Sub
    Public Function MostrarInformacion() As String
        Return $"ID: {Id}, Nombre: {Nombre}, Apellido: {Apellido}, Email: {Email}, Telefono: {Telefono}, Direccion: {Direccion}, Fecha de Registro: {FechaRegistro.ToShortDateString()}"
    End Function

    Public Function dtToCliente(dataTable As DataTable) As Clientes
        If dataTable IsNot Nothing AndAlso dataTable.Rows.Count > 0 Then
            Dim row As DataRow = dataTable.Rows(0)
            Return New Clientes() With {
                .Id = Convert.ToInt32(row("Id")),
                .Nombre = Convert.ToString(row("Nombre")),
                .Apellido = Convert.ToString(row("Apellido")),
                .Email = Convert.ToString(row("Email")),
                .Telefono = Convert.ToString(row("Telefono")),
                .Direccion = Convert.ToString(row("Direccion")),
                .FechaRegistro = Convert.ToDateTime(row("FechaRegistro"))
            }
        End If
        Return Nothing
    End Function

    Public Function ValidarTelefono() As Boolean
        Return Not String.IsNullOrEmpty(Telefono) AndAlso Telefono.Length >= 10
    End Function

    Public Function ValidarDatos() As Boolean
        Return Not String.IsNullOrEmpty(Nombre) AndAlso Not String.IsNullOrEmpty(Apellido) AndAlso ValidarTelefono() AndAlso ValidarEmail()
    End Function





End Class
