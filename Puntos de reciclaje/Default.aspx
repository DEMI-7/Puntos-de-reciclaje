<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Puntos_de_reciclaje._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>

        <!-- el div tiene el mapa y despues cargamos los scripts-->
        <div id="mapa"></div>
        <script src="Libreria/leaflet/leaflet-src.js"></script>
        <script src="Scripts/scriptMapa.js"></script>

    </main>

</asp:Content>
