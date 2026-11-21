<%@ Page Title="Crear Punto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CrearPunto.aspx.cs" Inherits="Puntos_de_reciclaje.CrearPunto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <style>
        #mapaCrear { height: 450px; width: 100%; border-radius: 8px; border: 1px solid #ccc; }
    </style>

    <asp:HiddenField ID="HfIdPunto" runat="server" />

    <div class="container body-content mt-4">
        <h2>
            <asp:Label ID="lblTitulo" runat="server" Text="Publicar nuevo punto de reciclaje"></asp:Label></h2>
        <hr />

        <div class="row">

            <div class="col-md-5">
               <div class="mb-3">
               <label class="form-label">Dirección:</label>
               <div class="input-group">
               <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ej: Av. San Martín 123"></asp:TextBox>
               <!-- Boton para buscar en el mapa -->
               <button class="btn btn-outline-primary" type="button" onclick="buscarDireccion()">🔍 Buscar</button>
               </div>
               </div>

                <div class="mb-3">
                    <label class="form-label">Días de atención:</label>
                    <asp:CheckBoxList ID="cblDias" runat="server" RepeatDirection="Horizontal" CellPadding="4">
                        <asp:ListItem Value="0" Selected="True">Lun</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">Mar</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">Mie</asp:ListItem>
                        <asp:ListItem Value="3" Selected="True">Jue</asp:ListItem>
                        <asp:ListItem Value="4" Selected="True">Vie</asp:ListItem>
                        <asp:ListItem Value="5">Sab</asp:ListItem>
                        <asp:ListItem Value="6">Dom</asp:ListItem>
                    </asp:CheckBoxList>
                </div>

                <div class="mb-3">
                    <label class="form-label">Horario:</label>
                    <!-- <asp:TextBox ID="txtHorario" runat="server" CssClass="form-control" placeholder="Ej: Lun a Vie 8-17hs"></asp:TextBox> -->
                    <div class="d-flex align-items-center gap-2">
                        <span>De</span>
                        <asp:DropDownList ID="ddlHoraDesde" runat="server" CssClass="form-select w-auto"></asp:DropDownList>
                        <span>a</span>
                        <asp:DropDownList ID="ddlHoraHasta" runat="server" CssClass="form-select w-auto"></asp:DropDownList>
                    </div>
                </div>



                <div class="mb-3">
                    <label class="form-label">Materiales que recibe:</label>
                    <asp:CheckBoxList ID="cblMateriales" runat="server" CssClass="form-check" RepeatLayout="UnorderedList">
                    </asp:CheckBoxList>
                </div>

                <!-- Envolvemos la zona de guardado en un UpdatePanel -->
                <asp:UpdatePanel ID="UpdatePanelGuardar" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <!-- Campos ocultos del mapa -->
                        <input type="hidden" id="latitudMapa" name="latitudMapa" />
                        <input type="hidden" id="longitudMapa" name="longitudMapa" />

                        <!-- Etiqueta para mostrar errores -->
                        <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block mb-2" Visible="false"></asp:Label>

                        <!-- Boton Guardar -->
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Punto" CssClass="btn btn-success btn-lg w-50" OnClick="BtnGuardar_Click" />
                    </ContentTemplate>
                    <Triggers>

                        <asp:PostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                </asp:UpdatePanel>

                <!-- Botón Cancelar (Afuera del UpdatePanel) -->
                <a href="PanelEmpresa.aspx" class="btn btn-danger btn-lg w-50 mt-2">Cancelar</a>
            </div>

            <div class="col-md-7">
                <div id="mapaCrear" cssclass="mb-3"></div>

                <div id="divSubidaImagenes" runat="server" Cssclass="border rounded p-3 bg-3">
                    <asp:Label ID="Label1" runat="server" Text="Fotos del lugar (Opcional):" CssClass="form-label fw-bold d-block mb-3"></asp:Label>

                    <div class="d-flex gap-2 mb-3">
                        <asp:FileUpload ID="fuImagen" accept=".jpg, .jpeg, .png" runat="server" CssClass="form-control" />
                        <asp:Button ID="BtnSubirFoto" runat="server" Text="Subir foto" OnClick="BtnSubirFoto_Click" CssClass="btn btn-success text-nowrap" />
                        <asp:Label ID="LblMensajeSubidaImagen" runat="server" Text="Primero seleccione un archivo."></asp:Label>
                    </div>

                    <!-- Panel para ver y borrar fotos existentes (solo en edición) -->
                    <div class="mb-3">
                        <asp:Repeater ID="rptImagenes" runat="server" OnItemCommand="rptImagenes_ItemCommand">
                            <HeaderTemplate>
                                <div class="d-flex flex-wrap">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="me-2 mb-2 text-center border p-1 rounded" style="background-color: #f8f9fa;">
                                    <asp:Image ID="img" runat="server" ImageUrl='<%# Eval("UrlImagen") %>' Style="width: 100%; height: 90px; object-fit: cover;" CssClass="rounded mb-1" />
                                    <br />
                                    <asp:LinkButton ID="btnBorrarImg" runat="server" CommandName="EliminarImg" CommandArgument='<%# Eval("IdImagen") %>' CssClass="btn btn-sm btn-danger w-100"
                                        OnClientClick="return confirm('¿Borrar esta foto?');" Style="font-size: 10px;">Borrar</asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate></div></FooterTemplate>
                        </asp:Repeater>
                    </div>

                </div>

            </div>
        </div>
    </div>
     <!-- java scrip - mapa -  -->
    <script src="Libreria/leaflet/leaflet-src.js"></script>
    <script>
        var map = L.map('mapaCrear').setView([-34.534306, -58.792742], 13);
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(map);
        var marker;

        map.on('click', function (e) {
            if (marker) {
                marker.setLatLng(e.latlng);
            } else {
                marker = L.marker(e.latlng).addTo(map);
            }
            // Pasamos las coordenadas 
            document.getElementById('latitudMapa').value = e.latlng.lat.toString().replace(',', '.');

            document.getElementById('longitudMapa').value = e.latlng.lng.toString().replace(',', '.');
        });

function moverMapa(lat, lng) {
    if (lat && lng) {
        var nuevaPosicion = [lat, lng];
        map.setView(nuevaPosicion, 16); // aplicamos zoom cercano

        if (marker) {
            marker.setLatLng(nuevaPosicion);
        } else {
            marker = L.marker(nuevaPosicion).addTo(map);
        }
    }
}

// Funcion para buscar la dirección y mover el mapa
function buscarDireccion() {
    // Obtenemos el texto del TextBox (usando ClientID 
    var direccion = document.getElementById('<%= txtDireccion.ClientID %>').value;

    if (direccion.trim() === "") {
        alert("Por favor, escribí una dirección primero.");
        return;
    }

    // Le agregamos ", Argentina" para forzar la busqueda en el pais
    var url = 'https://nominatim.openstreetmap.org/search?format=json&q=' + encodeURIComponent(direccion + ', Argentina');

    // Hacemos la llamada a la API de OpenStreetMap
    fetch(url)
        .then(response => response.json())
        .then(data => {
            if (data && data.length > 0) {
                // Tomamos las coordenadas del primer resultado
                var lat = parseFloat(data[0].lat);
                var lon = parseFloat(data[0].lon);

                // Movemos el mapa usando la función existente y ponemos la chincheta ;  
                moverMapa(lat, lon);
                map.setView([lat, lon], 16); // Hacemos un zoom mas cercano

                // Guardamos las coordenadas en los campos ocultos
                // para que cuando el usuario guarde, sepa dónde está el punto
                document.getElementById('latitudMapa').value = lat;
                document.getElementById('longitudMapa').value = lon;
            } else {
                alert("No se encontro la direccion. Intenta siendo mas específico (Ej: Calle, Altura, Localidad).");
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert("Hubo un error al buscar la dirección. Revisa tu conexion a internet.");
        });
}


    </script>

</asp:Content>
