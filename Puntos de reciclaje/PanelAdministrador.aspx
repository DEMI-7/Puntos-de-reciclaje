<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdministrador.aspx.cs" Inherits="Puntos_de_reciclaje.PanelAdmintrador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container body-content mt-4">
        <h3> Panel de administración.</h3>

        <!-- -----------------------------------------------------------------------------------
            Cabecera con el título, el contador y el buscador
            Parte de usuarios activos
         -->
        <div class="d-flex justify-content-between align-items-center mb-3">
            <!-- Título y contador dinámico -->
            <h4 class="mb-0">Usuarios activos: <span class="badge bg-secondary">
                <asp:Label ID="LblCantidadUsuariosActivos" runat="server" Text="0"></asp:Label></span>
            </h4>

            <!-- Campo de búsqueda -->
            <div class="input-group" style="width: 300px;">
                <asp:TextBox ID="TxtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre o email..."></asp:TextBox>
                <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-primary" OnClick="btnBuscar_Click" />
            </div>
        </div>

        <!-- Grilla de usuarios activos-->
        <asp:GridView ID="GrillaUsuarioActivos" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="7"
            OnPageIndexChanging="GrillaUsuarioActivos_PageIndexChanging"
            CssClass="table table-bordered table-striped table-hover align-middle"
            GridLines="Both">

            <HeaderStyle CssClass="table-dark" />

            <PagerStyle CssClass="paginacion-grid"/>

            <Columns>
                <asp:BoundField DataField="ID_Usuario" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Email" HeaderText="Correo Electrónico" />
                <asp:BoundField DataField="TipoUsuario" HeaderText="Tipo de Usuario" />

                <%-- Columna de Botones de Acción para dar de baja una cuenta --%>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnDesactivar" runat="server" Text="Desactivar" CssClass="btn btn-danger btn-sm" CommandName="Desactivar" CommandArgument='<%# Eval("ID_Usuario") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!-- Mensaje que se muestra si la busqueda no encuentra nada-->
        <asp:Label ID="LblSinResultados" runat="server" Visible="false" CssClass="alert alert-warning d-block mt-3 text-center" Text="No se encontró ningún contenido relacionado a la búsqueda."></asp:Label>
    
        <!-- -------------------------------------------------------------------------------------
            Cabecera con el título, el contador y el buscador 
            Apartado empresas pendientes de aprobación
        -->
        <div class="d-flex justify-content-between align-items-center mb-3">
            <!-- Título y contador dinámico -->
            <h4 class="mb-0">Empresas pendientes de aprobación: <span class="badge bg-secondary">
                <asp:Label ID="LblCantidadEmpresasPendientes" runat="server" Text="0"></asp:Label></span>
            </h4>

            <!-- Campo de búsqueda -->
            <div class="input-group" style="width: 300px;">
                <asp:TextBox ID="TxtBuscarEmpresas" runat="server" CssClass="form-control" placeholder="Buscar por nombre o email..."></asp:TextBox>
                <asp:Button ID="BtnBuscarEmpresas" runat="server" Text="Buscar" CssClass="btn btn-outline-primary" OnClick="BtnBuscarEmpresas_Click" />
            </div>
        </div>

        <!-- Grilla de empresas pendientes de aprobación -->
        <asp:GridView ID="GrillaEmpresasPendientes" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="7"
            OnPageIndexChanging="GrillaEmpresasPendientes_PageIndexChanging"
            OnRowCommand="GrillaEmpresasPendientes_RowCommand"
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
                        <asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success btn-sm" CommandName="Aceptar" CommandArgument='<%# Eval("ID_Usuario") %>' />
                        <asp:Button ID="BtnRechazar" runat="server" Text="Rechazar" CssClass="btn btn-danger btn-sm" CommandName="Rechazar" CommandArgument='<%# Eval("ID_Usuario") %>' />
                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <!-- Mensaje que se muestra si la busqueda no encuentra nada-->
        <asp:Label ID="LblSinResultadosEmpresasPendientes" runat="server" Visible="false" CssClass="alert alert-warning d-block mt-3 text-center" Text="No se encontró ningún contenido relacionado a la búsqueda."></asp:Label>
    

        <!-- -------------------------------------------------------------------------------------

        -->
        
        <div class="text-center">
            <asp:Button ID="BtnVerBajas" runat="server" CssClass="btn btn-outline-primary" Text="Ver dados de baja" OnClick="BtnVerBajas_Click"/>
        </div>

        <hr />
    </div>
</asp:Content>
