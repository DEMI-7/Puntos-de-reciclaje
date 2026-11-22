<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdminResenias.aspx.cs" Inherits="Puntos_de_reciclaje.PaginasAdmin.PanelAdminResenias" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div  class =" container mt-4">
    <h2>Administrador de Reseñas </h2>
    <hr />


    <asp:label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block mb-3" 
        Visible="false"  ></asp:label>

    <div class ="card shadow-sm">
        <div class ="card-header bg-dark text-white">
            <h5 class="mb-0">Todas las reseñas del sistema </h5>
            </div>

        <div class ="card-body">
            <asp:GridView ID="gvResenias" runat="server" CssClass="table table-hover table-striped " AutoGenerateColumns="false" OnRowCommand=" gvResenias_RowCommand">

                <Columns>
                    <asp:BoundField DataField="IdResenia" HeaderText="ID" />

                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />

                     <asp:BoundField DataField="DireccionPunto" HeaderText="Punto de Reciclaje" />

                    <asp:BoundField DataField="Puntaje" HeaderText="Estrellas" />

                    <asp:BoundField DataField="Comentario" HeaderText="Comentario" />

                     <asp:BoundField DataField="FechaPublicacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />


                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                                 CssClass="btn btn-sm btn-danger" 
                                    CommandName="EliminarResenia" 
                                    CommandArgument='<%# Eval("IdResenia") %>' 
                                    OnClientClick="return confirm('¿Seguro que queres borrar esta reseña?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
          </div>
       </div>

</div>
</asp:Content>
