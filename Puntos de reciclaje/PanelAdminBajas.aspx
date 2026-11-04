<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdminBajas.aspx.cs" Inherits="Puntos_de_reciclaje.PanelAdminBajas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container body-content mt-4">
        <h3>Panel de administración: Bajas</h3>

        <!-- -----------------------------------------------------------------------------------
        Cabecera con el título, el contador y el buscador
        Parte de usuarios dados de baja
        -->
        <div class="d-flex justify-content-between align-items-center mb-3">
            <!-- Título y contador dinámico -->
            <h4 class="mb-0">Usuarios desactivados: <span class="badge bg-secondary">
                <asp:Label ID="LblCantidadUsuariosDesactivados" runat="server" Text="0"></asp:Label></span>
            </h4>

            <!-- Campo de búsqueda -->
            <div class="input-group" style="width: 300px;">
                <asp:TextBox ID="TxtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre o email..."></asp:TextBox>
                <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-primary" OnClick="BtnBuscar_Click" />
            </div>
        </div>

        <!-- Grilla de usuarios desactivados-->
        <asp:GridView ID="GrillaUsuarioDesactivados" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="7"
            OnPageIndexChanging="GrillaUsuarioDesactivados_PageIndexChanging"
            CssClass="table table-bordered table-striped table-hover align-middle"
            GridLines="Both">

            <HeaderStyle CssClass="table-dark" />

            <PagerStyle CssClass="paginacion-grid" />

            <Columns>
                <asp:BoundField DataField="ID_Usuario" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Email" HeaderText="Correo Electrónico" />
                <asp:BoundField DataField="TipoUsuario" HeaderText="Tipo de Usuario" />

                <%-- Columna de Botones de Acción para dar de baja una cuenta --%>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnActivar" runat="server" Text="Activar" CssClass="btn btn-danger btn-sm" CommandName="Activar" CommandArgument='<%# Eval("ID_Usuario") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!-- Mensaje que se muestra si la busqueda no encuentra nada-->
        <asp:Label ID="LblSinResultados" runat="server" Visible="false" CssClass="alert alert-warning d-block mt-3 text-center" Text="No se encontró ningún contenido relacionado a la búsqueda."></asp:Label>

        <!-- -------------------------------------------------------------------------------------
        Cabecera con el título, el contador y el buscador 
        Apartado empresas rechazadas
    -->
        <div class="d-flex justify-content-between align-items-center mb-3">
            <!-- Título y contador dinámico -->
            <h4 class="mb-0">Empresas rechazadas: <span class="badge bg-secondary">
                <asp:Label ID="LblCantidadEmpresasRechazadas" runat="server" Text="0"></asp:Label></span>
            </h4>

            <!-- Campo de búsqueda -->
            <div class="input-group" style="width: 300px;">
                <asp:TextBox ID="TxtBuscarEmpresas" runat="server" CssClass="form-control" placeholder="Buscar por nombre o email..."></asp:TextBox>
                <asp:Button ID="BtnBuscarEmpresas" runat="server" Text="Buscar" CssClass="btn btn-outline-primary" OnClick="BtnBuscarEmpresas_Click" />
            </div>
        </div>

        <!-- Grilla de empresas pendientes de aprobación -->
        <asp:GridView ID="GrillaEmpresasRechazadas" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="7"
            OnPageIndexChanging="GrillaEmpresasRechazadas_PageIndexChanging"
            CssClass="table table-bordered table-striped table-hover align-middle"
            GridLines="Both">

            <HeaderStyle CssClass="table-dark" />
            <PagerStyle CssClass="paginacion-grid" />

            <Columns>
                <asp:BoundField DataField="ID_Usuario" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Email" HeaderText="Correo Electrónico" />
                <asp:BoundField DataField="TipoUsuario" HeaderText="Tipo de Usuario" />

                <%-- Columna de Botones de Acción para dar de baja una cuenta --%>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="BtnSetearPendiente" runat="server" Text="Mandar a pendientes" CssClass="btn btn-danger btn-sm" CommandName="Pendiente" CommandArgument='<%# Eval("ID_Usuario") %>' />

                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <!-- Mensaje que se muestra si la busqueda no encuentra nada-->
        <asp:Label ID="LblSinResultadosEmpresasRechazadas" runat="server" Visible="false" CssClass="alert alert-warning d-block mt-3 text-center" Text="No se encontró ningún contenido relacionado a la búsqueda."></asp:Label>


        <!-- -------------------------------------------------------------------------------------

    -->

        <div class="text-center">
            <asp:Button ID="BtnVolver" runat="server" CssClass="btn btn-outline-primary" Text="Volver" OnClick="BtnVolver_Click" />
        </div>

        <hr />
    </div>
</asp:Content>
