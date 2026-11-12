<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdminMateriales.aspx.cs" Inherits="Puntos_de_reciclaje.PanelAdminMateriales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container body-content mt-4">
        <h3>Panel de administración: materiales</h3>

                <!-- -----------------------------------------------------------------------------------
        Formulario para cargar un material nuevo o modificar uno existente
        -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0">
                    <asp:Label ID="LblTituloFormulario" runat="server" Text="Nuevo material"></asp:Label>
                </h5>
            </div>
            <div class="card-body">

                <!-- guarda el ID del material que se esta editando, vacio si es uno nuevo -->
                <asp:HiddenField ID="HfIdMaterial" runat="server" />
                <asp:HiddenField ID="HfUrlLogo" runat="server" />

                <div class="row">
                    <!-- COLUMNA IZQUIERDA -->
                    <div class="col-md-7">
                        <div class="mb-3">
                            <label class="form-label">Nombre:</label>
                            <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Información:</label>
                            <asp:TextBox ID="TxtInfo" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                        </div>
                    </div>

                    <!-- COLUMNA DERECHA -->
                    <div class="col-md-5">
                        <div class="mb-3">
                            <asp:Label ID="LblLogoMaterial" runat="server" Text="Subir logo:"></asp:Label>
                            <asp:FileUpload ID="SubirImagenLogo" runat="server" CssClass="form-control" accept=".jpg, .jpeg, .png" />
                        </div>

                        <div class="border rounded big-light d-flex align-item-center justify-content-center overflow-hidden" style="height: 175px;">
                            <asp:Image ID="ImgLogo" runat="server" CssClass="img-fluid" Style="max-height: 100px; object-fit: contain;" />
                        </div>
                    </div>
                </div>

                <hr class="my-3" />

                <asp:Label ID="LblError" runat="server" CssClass="text-danger fw-bold d-block mb-3" Visible="false"></asp:Label>

                <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnGuardar_Click" />
                <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary" OnClick="BtnCancelar_Click" />
            </div>
        </div>

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
            OnRowCommand="GrillaMateriales_RowCommand"
            CssClass="table table-bordered table-striped table-hover align-middle mt-3"
            GridLines="Both">

            <HeaderStyle CssClass="table-dark" />
            <PagerStyle CssClass="paginacion-grid" />

            <Columns>
                <asp:BoundField DataField="IdMaterial" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <%-- Columna de botones de accion --%>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="BtnEditar" runat="server" Text="Editar" CssClass="btn btn-primary btn-sm" CommandName="Editar" CommandArgument='<%# Eval("IdMaterial") %>' />
                         <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("IdMaterial") %>'
                            OnClientClick="return confirm('¿Estás seguro de que querés eliminar este material?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="text-center">
            <asp:Button ID="BtnVolver" runat="server" CssClass="btn btn-outline-primary" Text="Volver" OnClick="BtnVolver_Click" />
        </div>

        <hr />
    </div>
</asp:Content>