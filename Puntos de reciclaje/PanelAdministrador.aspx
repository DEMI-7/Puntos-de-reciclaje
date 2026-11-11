<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdministrador.aspx.cs" Inherits="Puntos_de_reciclaje.PanelAdmintrador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container body-content mt-4">
        <h3> Panel de administración.</h3>

                <!-- -----------------------------------------------------------------------------------
            Formulario para modificar un usuario (se muestra al tocar Editar)
        -->
        <asp:Panel ID="PnlEditarUsuario" runat="server" Visible="false" CssClass="card shadow-sm mb-4">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0">Modificar usuario</h5>
            </div>
            <div class="card-body">

                <!-- guarda el ID del usuario que se esta editando -->
                <asp:HiddenField ID="HfIdUsuario" runat="server" />

                <div class="mb-3">
                    <label class="form-label">Nombre:</label>
                    <asp:TextBox ID="TxtNombreUsuario" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label">Correo electrónico:</label>
                    <asp:TextBox ID="TxtEmailUsuario" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label">Tipo de usuario:</label>
                    <asp:DropDownList ID="DdlTipoUsuario" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Admin" Value="admin"></asp:ListItem>
                        <asp:ListItem Text="Empresa" Value="empresa"></asp:ListItem>
                        <asp:ListItem Text="Gubernamental" Value="gubernamental"></asp:ListItem>
                        <asp:ListItem Text="Básico" Value="basico"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <asp:Label ID="LblErrorUsuario" runat="server" CssClass="text-danger fw-bold d-block mb-3" Visible="false"></asp:Label>

                <asp:Button ID="BtnGuardarUsuario" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnGuardarUsuario_Click" />
                <asp:Button ID="BtnCancelarUsuario" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary" OnClick="BtnCancelarUsuario_Click" />
            </div>
        </asp:Panel>

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
            OnRowCommand="GrillaUsuarioActivos_RowCommand"
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
                        <asp:Button ID="BtnEditarUsuario" runat="server" Text="Editar" CssClass="btn btn-primary btn-sm" CommandName="Editar" CommandArgument='<%# Eval("ID_Usuario") %>' />
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
            <asp:Button ID="BtnMateriales" runat="server" CssClass="btn btn-outline-primary" Text="Administrar materiales" OnClick="BtnMateriales_Click"/>
        </div>

        <hr />
    </div>
</asp:Content>