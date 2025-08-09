<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Clientes.aspx.vb" Inherits="Examen_II.Clientes1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row mb-3">
        <div class="col-md-4">

            <div class="form-group mb-3">
                <label for="NombreEmpresa">Nombre Empresa</label>
                <asp:TextBox ID="TxtNombreEmpresa" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group mb-3">
                <label for="TxtContacto">Contacto</label>
                <asp:TextBox ID="TxtContacto" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group mb-3">
                <label for="TxtTelefono">Telefono</label>
                <asp:TextBox  ID="TxtTelefono" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group mb-3">
                <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
            </div>

            </div>
            <asp:Label ID="LblMensaje" runat="server" Text=""></asp:Label>

       
     </div>
</asp:Content>
