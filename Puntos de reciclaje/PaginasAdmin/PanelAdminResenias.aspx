<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdminResenias.aspx.cs" Inherits="Puntos_de_reciclaje.PaginasAdmin.PanelAdminResenias" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div  class =" container mt-4">
    <h2>Administrador de Reseñas </h2>
    <hr />


    <asp:label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block mb-3" 
        Visible="false"  ></asp:label>

     <!-- Campo oculto para guardar el motivo -->
     <asp:HiddenField ID="hfMotivoEliminar" runat="server" /> 

    <div class="card shadow-sm mb-3">
        <div class="card-header bg-dark text-white">
            <h5 class="mb-0">Todas las reseñas del sistema </h5>
        </div>

        <div class="card-body">
            <asp:GridView ID="GvResenias" runat="server" CssClass="table table-hover table-striped " AutoGenerateColumns="false" OnRowCommand="GvResenias_RowCommand">

                <Columns>
                    <asp:BoundField DataField="IdResenia" HeaderText="ID" />

                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />

                    <asp:BoundField DataField="DireccionPunto" HeaderText="Punto de Reciclaje" />

                    <asp:BoundField DataField="Puntaje" HeaderText="Estrellas" />

                    <asp:BoundField DataField="Comentario" HeaderText="Comentario" />

                    <asp:BoundField DataField="FechaPublicacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />


                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                        
                       
                        <!-- boton de la grilla -->
                            <asp:Button ID ="btnEliminar" runat ="server" Text="Eliminar"

                                cssClass =" btn btn-sm btn-danger"
                                CommandName ="EliminarResenia"
                                CommandArgument='<%# Eval("IdResenia") %>' 
                                OnClientClick="return pedirMotivo(this);" />
                    </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="text-center">
        <asp:Button ID="BtnVolver" runat="server" CssClass="btn btn-outline-primary" Text="Volver" OnClick="BtnVolver_Click" />
    </div>

    <hr />
</div>

                          

                               

<script>
    function pedirMotivo(boton) {
        var motivo = prompt("¿Cual es el motivo de la eliminacion?", "Incumplimiento de normas");


        if (motivo != null && motivo.trim() !== "") {

            // Guardamos el motivo en el campo oculto 

            document.getElementById('<%=hfMotivoEliminar.ClientID %>').value = motivo;

            return confirm("¿Seguro que querés eliminar esta reseña?");
        }

        return false; // Si cancela, no hace nada
    }
</script>

                        
</asp:Content>
