<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdminEmpresasRechazadas.aspx.cs" Inherits="Puntos_de_reciclaje.PaginasAdmin.PanelAdminEmpresasRechazadas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="HfDetalleRegistro" runat="server" />
    
    <div class="container body-content mt-4">

    <asp:Label ID="LblErrorSesion" runat="server" Text="Usted debe iniciar sesión primero." CssClass="alert alert-danger d-block"></asp:Label>

    <h3>Empresas rechazadas</h3>

    
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
        OnRowCommand="GrillaEmpresasRechazadas_RowCommand"
        CssClass="table table-bordered table-striped table-hover align-middle"
        GridLines="Both">

        <HeaderStyle CssClass="table-dark" />
        <PagerStyle CssClass="paginacion-grid" />

        <Columns>
            <asp:BoundField DataField="ID_Usuario" HeaderText="ID" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Email" HeaderText="Correo electrónico" />
            <asp:BoundField DataField="TipoUsuario" HeaderText="Tipo de usuario" />

            <%-- Columna de Botones de Acción para dar de baja una cuenta --%>
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="BtnSetearPendiente" runat="server" Text="Mandar a pendientes" CssClass="btn btn-warning btn-sm"
                        CommandName="Pendiente" CommandArgument='<%# Eval("ID_Usuario") %>'
                        OnClientClick="return pedirDetalle();" />

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


    <script>
        function pedirDetalle() {
            let detalle = prompt("Estas por mandar a pendiente esta cuenta, ingresa algun detalle extra:");

            // caso de que toque cancelar
            if (detalle === null) {
                return false;
            }

            detalle = detalle.trim();
            // si deja el campo vacio
            if (detalle === "") {
                alert("Es obligatorio dejar un comentario.")
                return false;
            }
            if (detalle.length > 300) {
                alert("No puede superar los 300 caracteres.")
                return false;
            }

            // todo validado continuamos
            document.getElementById('<%= HfDetalleRegistro.ClientID %>').value = detalle;
            return true;
        }
    </script>

</asp:Content>
