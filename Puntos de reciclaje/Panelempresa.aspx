<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="PanelEmpresa.aspx.cs" Inherits="Puntos_de_reciclaje.PanelEmpresa" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container body-content mt-4">
        <div class="d-flex justify-content-between align-items-center mt-4 mb-3">
            <h2>Panel de Control</h2>
            <h4 class="text-success">
               
                <asp:Label ID="lblNombreEmpresa" runat="server" Text="Cargando..."></asp:Label>
            </h4>
        </div>
        
        <hr />

        <div class="mb-4">
            <asp:Button ID="btnNuevoPunto" runat="server" Text="+ Crear Nuevo Punto de Reciclaje" CssClass="btn btn-success" OnClick="btnNuevoPunto_Click" />
        </div>

        <div class="card shadow-sm">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0">Mis Puntos Publicados</h5>
            </div>
            <div class="card-body">
              
                <asp:GridView ID="gvPuntos" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="False" EmptyDataText="Todavía no tenés puntos creados.">
                    <Columns>
                        <asp:BoundField DataField="IdPunto" HeaderText="ID" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                        <asp:BoundField DataField="Horario" HeaderText="Horario" />
                        <asp:BoundField DataField="Materiales" HeaderText="Materiales" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>


