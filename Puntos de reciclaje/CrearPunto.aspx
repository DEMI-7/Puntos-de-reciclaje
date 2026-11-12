<%@ Page Title="Crear Punto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CrearPunto.aspx.cs" Inherits="Puntos_de_reciclaje.CrearPunto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <style>
        #mapaCrear { height: 450px; width: 100%; border-radius: 8px; border: 1px solid #ccc; }
    </style>

    <div class="container body-content mt-4">
        <h2>Publicar Nuevo Punto de Reciclaje</h2>
        <hr />

        <div class="row">
             
               <div class="col-md-5">
                <div class="mb-3">
                    <label class="form-label">Dirección:</label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ej: Av. San Martín 123"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label">Horario:</label>
                    <asp:TextBox ID="txtHorario" runat="server" CssClass="form-control" placeholder="Ej: Lun a Vie 8-17hs"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label">Foto del lugar (Opcional):</label>
                    <asp:FileUpload ID="fuImagen" runat="server" CssClass="form-control" />
                </div>
                    <!-- Panel para ver y borrar fotos existentes (solo en edición) -->
                <div class="mb-3">
                    <asp:Repeater ID="rptImagenes" runat="server" OnItemCommand="rptImagenes_ItemCommand">
                        <HeaderTemplate><div class="d-flex flex-wrap"></HeaderTemplate>
                        <ItemTemplate>
                            <div class="me-2 mb-2 text-center border p-1 rounded" style="background-color:#f8f9fa;">
                                <asp:Image ID="img" runat="server" ImageUrl='<%# Eval("UrlImagen") %>' Width="80px" Height="60px" CssClass="rounded mb-1" />
                                <br />
                                <asp:LinkButton ID="btnBorrarImg" runat="server" CommandName="EliminarImg" CommandArgument='<%# Eval("IdImagen") %>' CssClass="btn btn-sm btn-danger w-100"
                                    OnClientClick="return confirm('¿Borrar esta foto?');" style="font-size: 10px;">Borrar</asp:LinkButton>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate></div></FooterTemplate>
                    </asp:Repeater>
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
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Punto" CssClass="btn btn-success btn-lg w-100" OnClick="BtnGuardar_Click" />
                    </ContentTemplate>
                    <Triggers>
                      
                        <asp:PostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                </asp:UpdatePanel>
                
                <!-- Botón Cancelar (Afuera del UpdatePanel) -->
                <a href="PanelEmpresa.aspx" class="btn btn-link w-100 mt-2">Cancelar</a>
            </div>
     
            <div class="col-md-7">
                <div id="mapaCrear"></div>
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

    </script>

</asp:Content>
