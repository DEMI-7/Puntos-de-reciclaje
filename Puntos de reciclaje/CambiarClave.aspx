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
                <!-- palabras de recuperacion -->
                <hr class="my-4" />
                <h4>Palabras de recuperación</h4>

                <div class="alert alert-warning">
                    Estas dos palabras te sirven para recuperar la clave si te la olvidas.<br>
                    Guardalas bien, si las perdés vas a tener que contactar a un administrador.
                </div>

                <div class="row">
                    <div class="col-6 mb-3">
                        <label class="form-label">Palabra de recuperación 1:</label>
                        <asp:TextBox ID="TxtNuevaPalabra1" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="col-6 mb-3">
                        <label class="form-label">Palabra de recuperación 2:</label>
                        <asp:TextBox ID="TxtNuevaPalabra2" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label">Clave actual (para confirmar):</label>
                    <asp:TextBox ID="TxtClaveActualPalabras" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>

                <asp:Label ID="LblErrorPalabras" runat="server" CssClass="text-danger fw-bold d-block mb-3" Visible="false"></asp:Label>
                <asp:Label ID="LblExitoPalabras" runat="server" CssClass="text-success fw-bold d-block mb-3" Visible="false"></asp:Label>

                <asp:Button ID="BtnGuardarPalabras" runat="server" Text="Guardar palabras" CssClass="btn btn-success" OnClick="BtnGuardarPalabras_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
