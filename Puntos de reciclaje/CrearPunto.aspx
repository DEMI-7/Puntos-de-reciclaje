<%@ Page Title="Crear Puntos pa" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
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
                    <label class="form-label">Materiales que recibe:</label>
                    <asp:CheckBoxList ID="cblMateriales" runat="server" CssClass="form-check" RepeatLayout="UnorderedList">
                    </asp:CheckBoxList>
                </div>

                <asp:HiddenField ID="HfLatitud" runat="server" />
                <asp:HiddenField ID="HfLongitud" runat="server" />

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Punto" CssClass="btn btn-success btn-lg w-100" OnClick="btnGuardar_Click" />
                <a href="PanelEmpresa.aspx" class="btn btn-link w-100 mt-2">Cancelar</a>
            </div>

            <div class="col-md-7">
                <div id="mapaCrear"></div>
            </div>
        </div>
    </div>

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
            // Pasamos las coordenadas a los campos ocultos de C#
            document.getElementById('<%= HfLatitud.ClientID %>').value = e.latlng.lat;
            document.getElementById('<%= HfLongitud.ClientID %>').value = e.latlng.lng;
        });
    </script>

</asp:Content>
