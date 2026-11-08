<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Aprender.aspx.cs" Inherits="Puntos_de_reciclaje.Aprender" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Estilos/EstiloPaginaAprender.css" rel="stylesheet" />

    <div class="container body-content mt-4">
        <h1>Aprender sobre reciclaje</h1>

        <div class="container my-4">
            <h2 class="h4 mb-3 fw-bold">Explorar Categorías</h2>

            <!-- Contenedor deslizable -->
            <div class="slider-categorias d-flex gap-3 pb-3">


                <asp:Repeater ID="Repetidor" runat="server">

                    <ItemTemplate>
                        <div class="card card-categoria flex-shrink-0 border-0 shadow-sm">
                            <img src="<%#Eval("PrimerImagen") %>" class="card-img-top" alt="<%#Eval("Nombre") %>">
                            <div class="card-body text-center">
                                <h5 class="card-title h6 fw-bold m-0"><%#Eval("Nombre") %></h5>
                            </div>
                        </div>
                    </ItemTemplate>

                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
