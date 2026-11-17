<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdminRegistros.aspx.cs" Inherits="Puntos_de_reciclaje.PaginasAdmin.PanelAdminRegistros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container body-content mt-4">
        <h3>Registros:</h3>

        <!-- -----------------------------------------------------------------------------------
        Cabecera con el título, el contador y el buscador
        Parte de usuarios activos
     -->
        <div class="d-flex justify-content-between align-items-center mb-3">
            <!-- Campo de búsqueda -->
            <div class="input-group" style="width: 300px;">
                <asp:TextBox ID="TxtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre o email..."></asp:TextBox>
                <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-primary" OnClick="BtnBuscar_Click" />
            </div>
        </div>

        <!-- Grilla de usuarios activos-->
        <asp:GridView ID="GrillaRegistros" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="7"
            OnPageIndexChanging="GrillaRegistros_PageIndexChanging"
            OnRowCommand="GrillaRegistros_RowCommand"
            CssClass="table table-bordered table-striped table-hover align-middle"
            GridLines="Both">

            <HeaderStyle CssClass="table-dark" />

            <PagerStyle CssClass="paginacion-grid" />

            <Columns>
                <asp:BoundField DataField="ID_Movimiento" HeaderText="ID" />
                <asp:BoundField DataField="Admin.Nombre" HeaderText="Administrador" />
                <asp:BoundField DataField="UsuarioAfectado.Nombre" HeaderText="Usuario Afectado" />
                <asp:BoundField DataField="EstadoAnterior" HeaderText="Estado Anterior" />
                <asp:BoundField DataField="EstadoNuevo" HeaderText="Estado Nuevo" />
                <asp:BoundField DataField="HoraMovimiento" HeaderText="Hora" />
            </Columns>
        </asp:GridView>

        <!-- Mensaje que se muestra si la busqueda no encuentra nada-->
        <asp:Label ID="LblSinResultados" runat="server" Visible="false" CssClass="alert alert-warning d-block mt-3 text-center" Text="No se encontró ningún contenido relacionado a la búsqueda."></asp:Label>



        <!-- -------------------------------------------------------------------------------------

    -->

        <div class="text-center">
            <asp:Button ID="BtnVolver" runat="server" Text="Volver" CssClass="btn btn-outline-primary" OnClick="BtnVolver_Click" />
        </div>

        <hr />
    </div>


</asp:Content>
