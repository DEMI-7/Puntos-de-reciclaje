<%@ Page Title="Panel de Empresa" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelEmpresa.aspx.cs" Inherits="Puntos_de_reciclaje.PanelEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="d-flex justify-content-between align-items-center mt-4 mb-3">
        <h2>Panel de Control</h2>
        <h4 class="text-success">
            <asp:Label ID="LblNombreEmpresa" runat="server" Text="Cargando..."></asp:Label>
        </h4>
    </div>
    
    <hr />

    <div class="mb-4">
        <asp:Button ID="BtnNuevoPunto" runat="server" Text="+ Crear Nuevo Punto de Reciclaje" CssClass="btn btn-success" OnClick="BtnNuevoPunto_Click" />
    </div>

    <div class="card shadow-sm">
        <div class="card-header bg-dark text-white">
            <h5 class="mb-0">Mis Puntos Publicados</h5>
        </div>
        <div class="card-body">
            
            <asp:GridView ID="GvPuntos" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="False" EmptyDataText="Todavía no tenés puntos creados.">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                    <asp:BoundField DataField="TipoMaterial" HeaderText="Material que recibe" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="BtnVerMapa" runat="server" Text="Ver en Mapa" CssClass="btn btn-sm btn-info" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </div>

</asp:Content>