<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Puntos_de_reciclaje.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container body-content mt-4">

        <div class="row justify-content-center mb-5">
            <div class="col-lg-8 text-center">
                <h1 class="fw-bold mb-3 text-dark">Acerca de ReciMap</h1>
                <img src="ImagenesSistema/ReciMapBanner1.png"
                    class="img-fluid d-block mx-auto mb-4 rounded-3" 
                    style="max-width: 650px;"/>
                <p class="lead text-muted mb-4">
                    ReciMap es una plataforma orientada a democratizar el acceso al reciclaje para todas las personas. 
                    <strong>¿Cuántas veces te pasó que te dicen que recicles, pero después no sabés a dónde ir?</strong>
                </p>
                <p class="lead text-muted mb-4">
                    Nacida como un proyecto universitario, la idea de ReciMap escaló y hoy en día ya es una plataforma oficial.
                </p>
                <p class="lead text-muted mb-4">
                    El objetivo central es devolverle al planeta un poco de todo lo que nos da. Por eso, ReciMap busca que sea más fácil informarse y conocer cómo reciclar, para que cada uno de nosotros pueda aportar con un pequeño gran cambio.
                </p>
            </div>
        </div>





    <div class="text-center mb-4">
        <h1 class="fw-bold mb-3 text-dark">Contacto</h1>
        <p class="text-muted">Integrantes del equipo.</p>
    </div>

    <!-- grilla de cada uno de nosotros -->
    <div class="row row-cols-1 row-cols-md-3 g-4">

        <div class="col">
            <div class="card h-100 shadow-sm text-center">
                <div class="card-body">
                    <h5 class="card-title">Demian Agustin Gomez</h5>
                    <a href="https://github.com/DEMI-7" target="_blank" class="btn btn-outline-success btn-sm">GitHub</a>
                </div>
            </div>
        </div>

        <div class="col">
            <div class="card h-100 shadow-sm text-center">
                <div class="card-body">
                    <h5 class="card-title">Ezequiel Bernal</h5>
                    <a href="https://github.com/EzeeeBeee" target="_blank" class="btn btn-outline-success btn-sm">GitHub</a>
                </div>
            </div>
        </div>

        <div class="col">
            <div class="card h-100 shadow-sm text-center">
                <div class="card-body">
                    <h5 class="card-title">Mariano Sebastian Mastrocolo</h5>
                    <a href="https://github.com/Marian-Zh" target="_blank" class="btn btn-outline-success btn-sm">GitHub</a>
                </div>
            </div>
        </div>

    </div>

    <!-- link al repositorio del proyecto -->
    <div class="text-center mt-5">
        <a href="https://github.com/DEMI-7/Puntos-de-reciclaje" target="_blank" class="btn btn-success">Ver repositorio en GitHub</a>
    </div>


        <img src="ImagenesSistema/recimapLineaPunteada.png"
            class="img-fluid d-block mx-auto rounded-3"
            style="max-width: 900px;" />
        <hr class="my-5 border-success border-2 opacity-50" />
</div>
</asp:Content>
