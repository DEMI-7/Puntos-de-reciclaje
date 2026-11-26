<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Puntos_de_reciclaje.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container body-content mt-4">

    <div class="text-center mb-4">
        <h2>Contacto</h2>
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

</div>
</asp:Content>
