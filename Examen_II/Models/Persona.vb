Public Class Persona
    Public Property ClienteID As Integer
    Public Property Nombre As String
    Public Property Email As String
    Public Sub New()

    End Sub
    Public Function ValidarEmail() As Boolean
        Return Email.Contains("@") AndAlso Email.Contains(".")
    End Function

    Public Function validarNombre() As Boolean
        Return Not String.IsNullOrEmpty(Nombre) AndAlso
                   Nombre.Length >= 3 AndAlso
                   Not Nombre.Any(Function(c) Char.IsDigit(c))
    End Function


    Public Function dtToPersona(dataTable As DataTable) As Persona
        If dataTable IsNot Nothing AndAlso dataTable.Rows.Count > 0 Then
            Dim row As DataRow = dataTable.Rows(0)
            Return New Persona() With {
                .ClienteID = Convert.ToInt32(row("ClienteID")),
                .Nombre = Convert.ToString(row("Nombre")),
                .Email = Convert.ToString(row("Email"))
            }
        End If
        Return Nothing
    End Function







End Class
