<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Inicio.aspx.vb" Inherits="Examen_II.Clientes1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <asp:HiddenField ID="hfClienteId" runat="server" />
     <div class="row mb-3">
        <div class="col-md-4">
              <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label>
         

            <div class="form-group mb-3">
                <label for="NOmbre">Nombre </label>
                <asp:TextBox ID="TxtNombreCliente" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group mb-3">
                <label for="TxtContacto">Apellido</label>
                <asp:TextBox ID="TxtApellidoCliente" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group mb-3">
                <label for="TxtTelefono">Telefono</label>
                <asp:TextBox  ID="TxtTelefono" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-floating  mb-3">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Email"></asp:TextBox>
                <label for="MainContent_txtEmail">Email</label>
            </div>

            <div class="form-group mb-3">
                <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
            </div>
            <div class="form-group mb-3">
                <asp:Button ID="BtnCancelar" CssClass="btn btn-primary" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" />
            </div>
            </div>
            <asp:Label ID="LblMensaje" runat="server" Text=""></asp:Label>
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ClienteID" DataSourceID="SqlDataSource1"
             OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
             OnRowDeleted="GridView1_RowDeleted"
             Width="794px" Height="189px">
             <Columns>
                 <asp:CommandField ShowSelectButton="true" />
                 <asp:BoundField DataField="ClienteID" HeaderText="ClienteID" InsertVisible="False" ReadOnly="True" SortExpression="ClienteID" />
                 <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                 <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido" />
                 <asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" />
                 <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                 <asp:CommandField ShowDeleteButton="true" />
             </Columns>
         </asp:GridView>
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:II-46ConnectionString %>"
             SelectCommand="SELECT [ClienteID], [Nombre], [Telefono], [Apellido], [Email] FROM [Clientes]"
             DeleteCommand="DELETE FROM Clientes WHERE ClienteID = @ClienteID;"></asp:SqlDataSource>
     </div>
</asp:Content>
