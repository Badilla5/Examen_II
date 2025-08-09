Public Class Persona
    Public Property Id As Integer
    Public Property Nombre As String
    Public Property Email As String
    Public Sub New()

    End Sub
    Public Function ValidarEmail() As Boolean
        Return Email.Contains("@") AndAlso Email.Contains(".")
    End Function


    Public Function dtToPersona(dataTable As DataTable) As Persona
        If dataTable IsNot Nothing AndAlso dataTable.Rows.Count > 0 Then
            Dim row As DataRow = dataTable.Rows(0)
            Return New Persona() With {
                .Id = Convert.ToInt32(row("Id")),
                .Nombre = Convert.ToString(row("Nombre")),
                .Email = Convert.ToString(row("Email"))
            }
        End If
        Return Nothing
    End Function







End Class
