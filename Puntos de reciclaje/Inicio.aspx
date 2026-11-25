<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Puntos_de_reciclaje.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content mt-5">

        <!-- texto de bienvenida -->
        <div class="text-center mb-5">
            <h1 class="display-4 text-success fw-bold">Bienvenido a ReciMap</h1>
            <p class="lead">Encontrá dónde llevar residuos reciclables cerca tuyo!</p>
            <a href="Default.aspx" class="btn btn-success btn-lg mt-2">Ver el mapa</a>
        </div>

        <!-- opciones -->
        <div class="row text-center">
            <div class="col-md-4 mb-3">
                <h4>Buscá puntos</h4>
                <p class="text-muted">Mirá en el mapa los puntos de reciclaje, podés filtrar por el material que quieras llevar.</p>
            </div>
            <div class="col-md-4 mb-3">
                <h4>Aprendé</h4>
                <p class="text-muted">Conocé cómo se recicla cada material y por qué es importante hacerlo.</p>
                <a href="Aprender.aspx" class="btn btn-outline-success">Ir a Aprender</a>
            </div>
            <div class="col-md-4 mb-3">
                <h4>Sumá tu punto</h4>
                <p class="text-muted">Si sos una empresa u organismo gubernamental, podes registrarte y publicar tus puntos de reciclaje.</p>
                <a href="Registro.aspx" class="btn btn-outline-success">Registrarme</a>
            </div>
        </div>

    </div>
</asp:Content>