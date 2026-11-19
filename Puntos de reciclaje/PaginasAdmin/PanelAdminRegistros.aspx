<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdminRegistros.aspx.cs" Inherits="Puntos_de_reciclaje.PaginasAdmin.PanelAdminRegistros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container body-content mt-4">
        <h3>Registros:</h3>        

        <div id="PnlDetalleMovimiento" runat="server" class="card border-primary shadow-sm mb-4">
            <!-- cabecera con ID y hora -->
            <div class="card-header bg-light d-flex justify-content-between align-items-center flex-wrap gap-2">
                <div>
                    <asp:Label ID="LblIdRegistro" runat="server" Text=".." Cssclass="badge bg-primary me-2"></asp:Label>
                    <asp:Label ID="LblHora" runat="server" Text="..." Cssclass="text-muted small"></asp:Label>
                </div>
            </div>

            <div class="card-body">
                <!-- el que hizo la accion y usuario afectado -->
                <div class="row g-3 mb-3">
                    <div class="col-md-6">
                        <div class="p-3 border rounded bg-light h-100">
                            <div><asp:Label ID="LblAdmin" Cssclass="text-uppercase small text-muted fw-bold mb-1" runat="server" Text="Administrador:"></asp:Label></div>
                            <div><asp:Label ID="LblNombreAdmin" class="fw-semibold" runat="server" Text=".."></asp:Label></div>
                            <div><asp:Label ID="LblMailAdmin" Cssclass="text-muted small" runat="server" Text=".."></asp:Label></div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="p-3 border rounded bg-light h-100">
                            <div><asp:Label ID="LblUsuario" runat="server" Cssclass="text-uppercase small text-muted fw-bold mb-1" Text="Usuario afectado"></asp:Label></div>
                            <div>
                                <asp:Label ID="LblNombreUsuario" runat="server" Cssclass="fw-semibold" Text=".."></asp:Label>
                                <asp:Label ID="LblTipoCuenta" runat="server" Cssclass="badge bg-secondary ms-1" Text="..."></asp:Label>
                            </div>
                            <div><asp:Label ID="LblMailUsuario" runat="server" Cssclass="text-muted small" Text=".."></asp:Label></div>
                        </div>
                    </div>
                </div>

                <!-- estado anterior y estado nuevo -->
                <div class="row g-3 mb-3">
                    <div class="col-md-6">
                        <div class="p-2 border rounded">
                            <span class="small text-muted d-block">Estado anterior</span>
                            <asp:Label ID="LblEstadoAnterior" Cssclass="badge bg-warning text-dark mt-1" runat="server" Text="..."></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="p-2 border rounded">
                            <span class="small text-muted d-block">Estado nuevo</span>
                            <asp:Label ID="LblEstadoNuevo" Cssclass="badge bg-success mt-1" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                </div>

                <!-- detalle (hasta 300 caracteres) -->
                <div>
                    <label class="form-label fw-bold small text-uppercase text-muted">Motivo de la acción</label>
                    <div style="min-height: 90px; white-space: pre-wrap;" Cssclass="p-3 border rounded bg-white text-break" runat="server" id="DivDetalle"></div>
                </div>
                <div class="d-flex justify-content-end mt-3">
                    <asp:Button ID="BtnCerrarDetalle" runat="server" Text="Cerrar" CssClass="btn btn-outline-danger px-4" OnClick="BtnCerrarDetalle_Click" />
                </div>
            </div>
        </div>





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

                <%-- Columna de Botones de Acción para dar de baja una cuenta --%>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="BtnVerDetalle" runat="server" Text="Ver detalle" CssClass="btn btn-success btn-sm"
                            CommandName="verdetalle" CommandArgument='<%# Eval("ID_Movimiento") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

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
