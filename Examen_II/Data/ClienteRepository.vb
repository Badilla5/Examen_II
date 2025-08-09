Imports System.Data.SqlClient

Public Class ClienteRepository
    Private ReadOnly connectionString As String = ConfigurationManager.ConnectionStrings("conexionDB").ConnectionString
    ' Método para obtener un cliente por ID
    Public Function ObtenerClientePorId(clienteId As Integer) As Clientes
        Dim query As String = "SELECT * FROM Clientes WHERE ClienteID = @ClienteID"
        Dim parametros As New List(Of SqlParameter) From {
            New SqlParameter("@ClienteID", clienteId)
        }
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddRange(parametros.ToArray())
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim cliente As New Clientes() With {
                            .ClienteID = Convert.ToInt32(reader("ClienteID")),
                            .Nombre = Convert.ToString(reader("Nombre")),
                            .Apellido = Convert.ToString(reader("Apellido")),
                            .Telefono = Convert.ToString(reader("Telefono")),
                            .Email = Convert.ToString(reader("Email"))
                        }
                        Return cliente
                    End If
                End Using
            End Using
        End Using
        Return Nothing ' Si no se encuentra el cliente
    End Function
    ' Método para agregar un nuevo cliente
    Public Function AgregarCliente(cliente As Clientes) As Boolean
        If cliente Is Nothing OrElse Not cliente.validarApellido() OrElse Not cliente.ValidarTelefono() Then
            Throw New ArgumentException("Datos del cliente inválidos.")
        End If
        Dim query As String = "INSERT INTO Clientes (Nombre, Apellido, Telefono, Email) VALUES (@Nombre, @Apellido, @Telefono, @Email)"
        Dim parametros As New List(Of SqlParameter) From {
            New SqlParameter("@Nombre", cliente.Nombre),
            New SqlParameter("@Apellido", cliente.Apellido),
            New SqlParameter("@Telefono", cliente.Telefono),
            New SqlParameter("@Email", cliente.Email)
        }
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddRange(parametros.ToArray())
                conn.Open()
                Return cmd.ExecuteNonQuery() > 0 ' Retorna True si se insertó al menos una fila
            End Using
        End Using
    End Function
    ' Método para actualizar un cliente existente
    Public Function ActualizarCliente(cliente As Clientes) As Boolean
        If cliente Is Nothing OrElse cliente.ClienteID <= 0 OrElse Not cliente.validarApellido() OrElse Not cliente.ValidarTelefono() Then
            Throw New ArgumentException("Datos del cliente inválidos.")
        End If
        Dim query As String = "UPDATE Clientes SET Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono, Email = @Email WHERE ClienteID = @ClienteID"
        Dim parametros As New List(Of SqlParameter) From {
            New SqlParameter("@ClienteID", cliente.ClienteID),
            New SqlParameter("@Nombre", cliente.Nombre),
            New SqlParameter("@Apellido", cliente.Apellido),
            New SqlParameter("@Telefono", cliente.Telefono),
            New SqlParameter("@Email", cliente.Email)
        }
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddRange(parametros.ToArray())
                conn.Open()
                Return cmd.ExecuteNonQuery() > 0 ' Retorna True si se actualizó al menos una fila
            End Using
        End Using
    End Function



End Class
