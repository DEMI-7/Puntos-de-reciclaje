<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Puntos_de_reciclaje._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Estilos/EstiloMapa.css" rel="stylesheet" />

    <main>

        <div class="map-layout-wrapper">

            <div class="panel-filtros">
                <h4>Filtrar por material</h4>
                <div id="contenedor-checkboxes"></div>
            </div>

            <!-- el div tiene el mapa y despues cargamos los scripts-->
            <div id="mapa"></div>
            <script src="Libreria/leaflet/leaflet-src.js"></script>

            <!-- Script que carga los puntos -->
            <script>
                // con add event listener espera a que termine de cargar la pagina para comenzar a dibujar los puntos
                document.addEventListener("DOMContentLoaded", function () { 

                    // mapa es una variable y L.map es una funcion de leaflet para crear un objeto de tipo mapa
                    const mapa = L.map('mapa').setView([-34.534306, -58.792742], 13)

                    // Aca le damos un origen de datos a nuestro objeto de mapa, en este caso de openstreetmap.org
                    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(mapa)


                    // ---------------------------
                    // Dibujar los puntos en el mapa
                    //---------------------------

                    // traemos la lista en formato json que se creó en el code behind
                    var puntos = <%= ListaPuntosJson %>;

                    var marcadoresCreados = [];

                    puntos.forEach(function (punto) {

                        // añadimos los marcadores con el bucle
                        var marker = L.marker([punto.Latitud, punto.Longitud]).addTo(mapa);

                        var materialesTexto = "Sin materiales asociados";

                        // Validación estricta
                        if (punto.ListaMateriales && punto.ListaMateriales.length > 0) {
                            materialesTexto = punto.ListaMateriales.map(function (mat) {
                                return mat.Nombre;
                            }).join(', ');
                        }

                        // le agregamos la informacion adicional en un popup
                        marker.bindPopup("<b>Dirección:</b> " + punto.Direccion + "<br><b>Horario:</b> " + punto.Horario + "<br><b>Materiales:</b> " + materialesTexto);

                        // guarda la referencia para usar en el filtro
                        marcadoresCreados.push({
                            marker: marker,
                            idsMateriales: punto.ListaMateriales ? punto.ListaMateriales.map(m => m.IdMaterial) : []
                        });
                    });

                    // -----------------------
                    // Cargar los botoncitos para los filrtos
                    // ----------------------

                    var listaMateriales = <%= ListaMaterialesJson %>;

                    var contenedorCheckboxes = document.getElementById('contenedor-checkboxes');
                    contenedorCheckboxes.innerHTML = "";

                    listaMateriales.forEach(function (mat) {
                        var label = document.createElement('label');
                        label.className = 'filtro-item';

                        var checkBox = document.createElement('input');
                        checkBox.type = 'checkbox';
                        checkBox.value = mat.IdMaterial;
                        checkBox.className = 'check-material';

                        label.appendChild(checkBox);
                        label.appendChild(document.createTextNode(" " + mat.Nombre));

                        contenedorCheckboxes.appendChild(label);
                    });

                    // -------------------
                    // Evento para aplicar los filtros
                    // ------------------

                    document.addEventListener('change', function (e) {
                        if (e.target && e.target.classList.contains('check-material')) {

                            var seleccionados = Array.from(document.querySelectorAll('.check-material:checked')).map(cb => parseInt(cb.value));

                            marcadoresCreados.forEach(item => {
                                if (seleccionados.length == 0) {
                                    item.marker.addTo(mapa);
                                } else {
                                    var coincide = item.idsMateriales.some(idMat => seleccionados.includes(idMat));
                                    if (coincide) {
                                        item.marker.addTo(mapa);
                                    } else {
                                        mapa.removeLayer(item.marker);
                                    }
                                }
                            });
                        }
                    });


                });
            </script>


        </div>

    </main>

</asp:Content>
