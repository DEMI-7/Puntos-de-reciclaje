<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Puntos_de_reciclaje._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Estilos/EstiloMapa.css" rel="stylesheet" />

    <main>

        <div class="map-layout-wrapper">

            <!-- PANEL DE FILTROS -->
            <div class="panel-filtros">
                <h4>Filtrar por material</h4>
                <div id="contenedor-checkboxes"></div>
            </div>

            <!-- PANEL INFO -->
            <div class="panel-info" id="panel-info-punto" style="display: none;">

                <div class="d-flex justify-content-between align-items-center mb-2">
                    <h4 class="m-0 fw-bold">Información:</h4>

                    <!-- Boton de cruz para cerrar el menu -->
                    <button type="button" class="btn-close" aria-label="Cerrar" onclick="CerrarPanelInfo()"></button>
                </div>

                <!-- Ahora el contenedor de imagenes tiene un carroussel para las imagenes -->
                <div class="imagen-punto-contenedor position-relative mb-3">
                    <img id="ImgPunto" src="." alt="Imagen del punto de reciclaje" class="img-fluid rounded w-100" style="height: 200px; object-fit: cover;" />
                
                    <div class="d-flex justify-content-between align-items-center mt-2">
                        <!-- Boton anterior-->
                        <button type="button" class="btn btn-outline-dark btn-sm px-3" onclick="cambiarImagenPunto(-1)">
                            <i class="bi bi-chevron-left">&lt; </i>
                        </button>
                    
                        <span id="contadorImagenes" class="text-muted small">1 / 1</span>

                         <!-- Boton siguiente-->
                        <button type="button" class="btn btn-outline-dark btn-sm px-3" onclick="cambiarImagenPunto(1)">
                            <i class="bi bi-chevron-right"> &gt;</i>
                        </button>
                    </div>

                </div>

                <p><b>Direccion:</b> <span id="LblDireccion">-</span></p>
                <p><b>Horario:</b> <span id="LblHorario">-</span></p>
                <p><b>Materiales:</b> <span id="LblMateriales">-</span></p>
            </div>


            <hr />
                <h5 class="fw-bold">Reseñas</h5>
                
                <!-- Contenedor donde se cargaran las reseñas con Js  -->
                <div id="lista-resenas" style="max-height: 200px; overflow-y: auto;" class="mb-3">
                   
                </div>

                <!--  -->
                <div class="card bg-light p-2 mb-3">
                    <label class="form-label fw-bold small">Dejá tu reseña:</label>
                    <div class="mb-2">
                        <select id="selectPuntaje" class="form-select form-select-sm">
                            <option value="5">⭐⭐⭐⭐⭐ (Buenardopolis)</option>
                            <option value="4">⭐⭐⭐⭐ (Muy bueno)</option>
                            <option value="3">⭐⭐⭐ (Bueno)</option>
                            <option value="2">⭐⭐ (Regular)</option>
                            <option value="1">⭐ (Malardo)</option>
                        </select>
                    </div>
                    <textarea id="txtComentario" class="form-control form-control-sm mb-2" rows="2" placeholder="¿Qué te pareció este punto?"></textarea>
                    <button type="button" class="btn btn-success btn-sm w-100" onclick="EnviarResena()">Enviar Reseña</button>
                </div>




            <!-- el div tiene el mapa y despues cargamos los scripts-->
            <div id="mapa"></div>
            <script src="Libreria/leaflet/leaflet-src.js"></script>

            <!-- Script que carga los puntos -->
            <script>
                // VARIABLES GLOBAL PARA MANIPULAR EL CARRUSEL DE IMAGENES
                var imagenesPuntoActual = [];
                var indiceImagenActual = 0;

                // FUNCION DE CERRAR EL PANEL DE INFO
                function CerrarPanelInfo() {
                    var panel = document.getElementById('panel-info-punto');
                    panel.style.display = 'none';
                    imagenesPuntoActual = [];
                    indiceImagenActual = 0;
                }

                // FUNCION CAMBIAR IMAGEN CON LOS BOTONES
                function cambiarImagenPunto(direccion) { 
                    if (imagenesPuntoActual.length === 0) return; // si no tiene imagenes
                    indiceImagenActual += direccion;

                    //control de limites
                    if (indiceImagenActual >= imagenesPuntoActual.length) { 
                        indiceImagenActual = 0; // reinicia si se pasa
                    } else if (indiceImagenActual < 0) {
                        indiceImagenActual = imagenesPuntoActual.length - 1; // va al final si ya esta en la primera
                    }
                    // se actualiza la imagen y el contador
                    actualizarImagenPanel();
                }

                // ACTUALIZA LA IMAGEN MOSTRADA EN EL PANEL
                function actualizarImagenPanel() {
                    var imagen = document.getElementById('ImgPunto');
                    var contador = document.getElementById('contadorImagenes');

                    if (imagenesPuntoActual.length > 0) {
                        imagen.src = imagenesPuntoActual[indiceImagenActual].UrlImagen;
                        imagen.style.display = 'block';
                        if (contador) { 
                            contador.innerText = (indiceImagenActual + 1) + " / " + imagenesPuntoActual.length;
                        }
                    } else {
                        imagen.style.display = 'none';
                        if (contador) contador.innerText = "0 / 0";
                    }
                }


                // con add event listener espera a que termine de cargar la pagina para comenzar a dibujar los puntos
                document.addEventListener("DOMContentLoaded", function () { 

                    // mapa es una variable y L.map es una funcion de leaflet para crear un objeto de tipo mapa
                    const mapa = L.map('mapa', { zoomControl: false }).setView([-34.534306, -58.792742], 13)
                    L.control.zoom({
                        position: 'bottomright'
                    }).addTo(mapa);

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

                        // EVENTO CLICK E EL MARCADOR QUE ABRE EL PANEL DE INFO CON LOS DATOS DEL PUNTO
                        // la funcion marker on click obtiene los datos del punto que clickeamos y la muestra en la ventanita flotante
                        marker.on('click', function () {
                            var panel = document.getElementById('panel-info-punto');
                            panel.style.display = 'block';

                            var imagen = document.getElementById('ImgPunto');

                            // guardo las imagenes de este punto en las variables globales
                            imagenesPuntoActual = punto.ListaUrlImagenes || [];
                            indiceImagenActual = 0;

                            actualizarImagenPanel();

                            document.getElementById('LblDireccion').innerText = punto.Direccion;
                            document.getElementById('LblHorario').innerText = punto.Horario;
                            document.getElementById('LblMateriales').innerText = materialesTexto;

                        })

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
