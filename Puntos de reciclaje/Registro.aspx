<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Puntos_de_reciclaje.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content mt-4">
        <div class="row">
            <div class="col-6">
                <h3>Crear cuenta</h3>

                <!-- tipo de cuenta -->
                <div class="mb-3">
                    <label class="form-label">Tipo de cuenta:</label>
                    <asp:DropDownList ID="DdlTipoCuenta" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Usuario" Value="basico"></asp:ListItem>
                        <asp:ListItem Text="Empresa" Value="empresa"></asp:ListItem>
                        <asp:ListItem Text="Organismo gubernamental" Value="gubernamental"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <!-- nombre -->
                <div class="mb-3">
                    <label class="form-label">Nombre, razon social o nombre de entidad:</label>
                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>

                <!-- email -->
                <div class="mb-3">
                    <label class="form-label">Correo electrónico:</label>
                    <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>

                <!-- contraseña -->
                <div class="mb-3">
                    <label class="form-label">Contraseña:</label>
                    <asp:TextBox ID="TxtContraseña" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label">Repetir contraseña:</label>
                    <asp:TextBox ID="TxtRepetirContraseña" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>

                <!-- mensajes -->
                <asp:Label ID="LblError" runat="server" CssClass="text-danger fw-bold d-block mb-3" Visible="false"></asp:Label>
                <asp:Label ID="LblExito" runat="server" CssClass="text-success fw-bold d-block mb-3" Visible="false"></asp:Label>

                <asp:Button ID="BtnRegistrarme" runat="server" Text="Registrarme" CssClass="btn btn-success" OnClick="BtnRegistrarme_Click" />

                <p class="mt-3">Ya tenes una cuenta? <a href="Login.aspx">Iniciá sesión</a></p>
            </div>
        </div>
    </div>
</asp:Content>
