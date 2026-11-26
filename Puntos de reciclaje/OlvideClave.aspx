<%@ Page Title="Recuperar clave" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OlvideClave.aspx.cs" Inherits="Puntos_de_reciclaje.OlvideClave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content mt-4">
        <div class="row">
            <div class="col-6">
                <h3>Recuperar clave</h3>
                <p class="text-muted">
                    Ingresá tu mail y las dos palabras de recuperación que elegiste al crear tu cuenta:
                </p>

                <!-- datos para verificar -->
                <div class="mb-3">
                    <label class="form-label">Mail:</label>
                    <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>

                <div class="row">
                    <div class="col-6 mb-3">
                        <label class="form-label">Palabra de recuperación 1:</label>
                        <asp:TextBox ID="TxtPalabra1" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="col-6 mb-3">
                        <label class="form-label">Palabra de recuperación 2:</label>
                        <asp:TextBox ID="TxtPalabra2" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    </div>
                </div>

                <hr />

                <!-- clave nueva -->
                <div class="mb-3">
                    <label class="form-label">Clave nueva:</label>
                    <asp:TextBox ID="TxtClaveNueva" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label">Repetir clave nueva:</label>
                    <asp:TextBox ID="TxtRepetirClave" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>

                <!-- mensajes -->
                <asp:Label ID="LblError" runat="server" CssClass="text-danger fw-bold d-block mb-3" Visible="false"></asp:Label>
                <asp:Label ID="LblExito" runat="server" CssClass="text-success fw-bold d-block mb-3" Visible="false"></asp:Label>

                <asp:Button ID="BtnRecuperar" runat="server" Text="Cambiar clave" CssClass="btn btn-success" OnClick="BtnRecuperar_Click" />

                <div class="alert alert-secondary mt-4">
                    Si no recordás tus palabras, por favor contactá a un administrador para recuperar tu cuenta.
                </div>

                <p><a href="Login.aspx">Volver a iniciar sesión</a></p>
            </div>
        </div>
    </div>
</asp:Content>
