<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Puntos_de_reciclaje.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content mt-5 text-center">
        <h1 class="display-4">Ups...</h1>
        <h3 class="mb-3">No deberías estar acá...</h3>
        <p class="text-muted">
            No tenés permiso para ver esta página.
            Si creés que es un error, <a href="Login.aspx">iniciá sesión</a> con una cuenta que tenga acceso.
        </p>
        <a href="Default.aspx" class="btn btn-success mt-2">Volver al mapa</a>
    </div>
</asp:Content>