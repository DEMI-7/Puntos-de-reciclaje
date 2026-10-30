<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Puntos_de_reciclaje._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Estilos/EstiloMapa.css" rel="stylesheet" />

    <main>

        <div class="map-layout-wrapper">

        <!-- el div tiene el mapa y despues cargamos los scripts-->
        <div id="mapa"></div>
        <script src="Libreria/leaflet/leaflet-src.js"></script>
        <script src="Scripts/scriptMapa.js"></script>
        </div>

    </main>

</asp:Content>
