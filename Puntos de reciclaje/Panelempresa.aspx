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
        <!-- Boton lblerror  -->
        <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block mb-3" Visible="false"></asp:Label>

        <div class="mb-4">
            <asp:Button ID="btnNuevoPunto" runat="server" Text="+ Crear Nuevo Punto de Reciclaje" CssClass="btn btn-success" OnClick="BtnNuevoPunto_Click" />
        </div>

        <div class="card shadow-sm">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0">Mis Puntos Publicados</h5>
            </div>
            <div class="card-body">
              

                <asp:GridView ID="gvPuntos" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="False" EmptyDataText="Todavía no tenés puntos creados." OnRowCommand="GvPuntos_RowCommand">
                    <Columns>


                        <asp:TemplateField HeaderText="Acciones">

    <ItemTemplate>
        <!-- Boton and metodo  Eliminar -->
        <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" 
            CssClass="btn btn-sm btn-dark" 
            CommandName="EliminarPunto" 
            CommandArgument='<%# Eval("IdPunto") %>' 
            OnClientClick="return confirm('¿Estás seguro de que queres eliminar este punto?');" />

         <!-- Boton editar ;  -->
        <a href='<%# "CrearPunto.aspx?id=" + Eval("IdPunto") %>' class="btn btn-sm btn-primary">Editar</a>

                      </ItemTemplate>
                </asp:TemplateField>

                        <asp:BoundField DataField="IdPunto" HeaderText="ID" />

                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" />

                        <asp:BoundField DataField="Horario" HeaderText="Horario" />
                       

                       <%-- <asp:BoundField DataField="Materiales" HeaderText="Materiales" /> --%>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>


