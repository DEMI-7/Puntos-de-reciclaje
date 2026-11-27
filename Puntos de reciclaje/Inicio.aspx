<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Puntos_de_reciclaje.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="Estilos/EstiloInicio.css" rel="stylesheet" />
    
    <div class="container body-content mt-1">

        <div class="text-center mb-1">
            <img src="ImagenesSistema/reciMapLogo.png" class="img-fluid logo-grande d-block mx-auto" />
        </div>

        <!-- texto de bienvenida -->
        <div class="text-center mb-5">
            <h1 class="display-4 text-success fw-bold">Bienvenido a ReciMap</h1>
            <p class="lead">Encontrá dónde llevar residuos reciclables cerca tuyo!</p>
        </div>

        <!-- opciones -->
        <div class="row text-center">
            <div class="col-md-4 mb-3">
                <div class="card panel-hollow h-100 p-4">
                    <div class="card-body d-flex flex-column">
                        <h4>Buscá puntos</h4>
                        <p class="text-muted">Mirá en el mapa los puntos de reciclaje, podés filtrar por el material que quieras llevar.</p>
                        <a href="Default.aspx" class="btn btn-success btn-lg mt-2">Ver el mapa</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mb-3">
                <div class="card panel-hollow h-100 p-4">
                    <div class="card-body d-flex flex-column">

                        <h4>Aprendé</h4>
                        <p class="text-muted">Conocé cómo se recicla cada material y por qué es importante hacerlo.</p>
                        <a href="Aprender.aspx" class="btn btn-outline-success">Ir a Aprender</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mb-3">
                <div class="card panel-hollow h-100 p-4">
                    <div class="card-body d-flex flex-column">
                        <h4>Sumá tu punto</h4>
                        <p class="text-muted">Si sos una empresa u organismo gubernamental, podes registrarte y publicar tus puntos de reciclaje.</p>
                        <a href="Registro.aspx" class="btn btn-outline-success">Registrarme</a>
                    </div>
                </div>
            </div>
        </div>


        <img src="ImagenesSistema/recimapLineaPunteada.png"
            class="img-fluid d-block mx-auto rounded-3" 
            style="max-width: 900px;"/>
        <hr class="my-5 border-success border-2 opacity-50" />
    </div>
</asp:Content>