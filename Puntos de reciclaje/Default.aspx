<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Puntos_de_reciclaje._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Estilos/EstiloMapa.css" rel="stylesheet" />

    <main>

        <div class="map-layout-wrapper">

            <!-- el div tiene el mapa y despues cargamos los scripts-->
            <div id="mapa"></div>
            <script src="Libreria/leaflet/leaflet-src.js"></script>
            <!-- <script src="Scripts/scriptMapa.js"></script> -->

            <script>
                // con add event listener espera a que termine de cargar la pagina para comenzar a dibujar los puntos
                document.addEventListener("DOMContentLoaded", function () { 

                    // mapa es una variable y L.map es una funcion de leaflet para crear un objeto de tipo mapa
                    const mapa = L.map('mapa').setView([-34.534306, -58.792742], 13)

                    // Aca le damos un origen de datos a nuestro objeto de mapa, en este caso de openstreetmap.org
                    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(mapa)

                    // traemos la lista en formato json que se creó en el code behind
                    var puntos = <%= ListaPuntosJson %>;

                    puntos.forEach(function (punto) {

                        // añadimos los marcadores con el bucle
                        var marker = L.marker([punto.Latitud, punto.Longitud]).addTo(mapa);

                        // le agregamos la informacion adicional en un popup
                        marker.bindPopup("<b>Dirección:</b> " + punto.Direccion + "<br><b>Horario:</b> " + punto.Horario + "<br><b>Materiales:</b> " + punto.ListaMaterialesID);
                    });

                });
            </script>
        </div>

    </main>

</asp:Content>
