<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdminMateriales.aspx.cs" Inherits="Puntos_de_reciclaje.PanelAdminMateriales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container body-content mt-4">
        <h3>Panel de administración: materiales</h3>

        <!-- -----------------------------------------------------------------------------------
        Cabecera con el título y el contador
        Listado de materiales
        -->
        <h4 class="mb-0">Materiales: <span class="badge bg-secondary">
            <asp:Label ID="LblCantidadMateriales" runat="server" Text="0"></asp:Label></span>
        </h4>

        <!-- Grilla de materiales -->
        <asp:GridView ID="GrillaMateriales" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="7"
            OnPageIndexChanging="GrillaMateriales_PageIndexChanging"
            CssClass="table table-bordered table-striped table-hover align-middle mt-3"
            GridLines="Both">

            <HeaderStyle CssClass="table-dark" />
            <PagerStyle CssClass="paginacion-grid" />

            <Columns>
                <asp:BoundField DataField="IdMaterial" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            </Columns>
        </asp:GridView>

        <div class="text-center">
            <asp:Button ID="BtnVolver" runat="server" CssClass="btn btn-outline-primary" Text="Volver" OnClick="BtnVolver_Click" />
        </div>

        <hr />
    </div>
</asp:Content>