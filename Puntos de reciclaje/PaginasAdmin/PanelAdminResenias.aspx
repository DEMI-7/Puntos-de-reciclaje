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

                       <!-- Fila superior: Checkbox a la izquierda, buscador a la derecha -->
            <div class="d-flex justify-content-between align-items-center mb-4">
                
                <!-- switch para ver las reseñas eliminadas -->
                <div class="form-check form-switch ms-2">
                    <asp:CheckBox ID="chkVerEliminadas" runat="server" Text="Ver reseñas eliminadas" AutoPostBack="true"
                        OnCheckedChanged="chkVerEliminadas_CheckedChanged" />
                </div>

                <!-- Buscador -->
                <div class="input-group" style="width: 350px;">
                    <asp:TextBox ID="TxtBuscar" runat="server" CssClass="form-control"
                        placeholder="Buscar por usuario o comentario..."></asp:TextBox>
                    <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="BtnBuscar_Click" />
                </div>
            </div>


            <!-- La Grilla -->
            <asp:GridView ID="GvResenias" runat="server" CssClass="table table-hover table-striped align-middle" AutoGenerateColumns="false" OnRowCommand="GvResenias_RowCommand">
                <Columns>
                    <asp:BoundField DataField="IdResenia" HeaderText="ID" />

                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />

                    <asp:BoundField DataField="DireccionPunto" HeaderText="Punto de Reciclaje" />

                    <asp:BoundField DataField="Puntaje" HeaderText="Estrellas" />

                    <asp:BoundField DataField="Comentario" HeaderText="Comentario" />

                    <asp:BoundField DataField="FechaPublicacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />

                    <asp:BoundField DataField="MotivoEliminacion" HeaderText="Motivo Elim." />


                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <!-- BOTON ELIMINAR (Solo se ve si no estamos viendo las eliminadas) -->
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                                CssClass="btn btn-sm btn-danger"
                                CommandName="EliminarResenia"
                                CommandArgument='<%# Eval("IdResenia") %>'
                                OnClientClick="return pedirMotivo();" 
                                Visible='<%# !chkVerEliminadas.Checked %>' />

                            <!-- boton  RESTAURAR (Solo se ve si estamos viendo las eliminadas) -->
                            <asp:Button ID="btnRestaurar" runat="server" Text="Restaurar"
                                CssClass="btn btn-sm btn-success"
                                CommandName="RestaurarResenia"
                                CommandArgument='<%# Eval("IdResenia") %>'
                                Visible='<%# chkVerEliminadas.Checked %>' />
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
