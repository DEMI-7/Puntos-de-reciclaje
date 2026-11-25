<%@ Page Title="Cambiar Clave" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambiarClave.aspx.cs" Inherits="Puntos_de_reciclaje.CambiarClave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content mt-4">
        <div class="row">
            <div class="col-6">
                <h3>Cambiar clave</h3>

                <div class="mb-3">
                    <label class="form-label">Clave actual:</label>
                    <asp:TextBox ID="TxtClaveActual" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>

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

                <asp:Button ID="BtnCambiarClave" runat="server" Text="Cambiar clave" CssClass="btn btn-success" OnClick="BtnCambiarClave_Click" />
            </div>
        </div>
    </div>
</asp:Content>
