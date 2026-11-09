<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Aprender.aspx.cs" Inherits="Puntos_de_reciclaje.Aprender" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Estilos/EstiloPaginaAprender.css" rel="stylesheet" />

    <div class="container body-content mt-4">
        <h1>Aprender sobre reciclaje</h1>

        <div class="container my-4">
            <h2 class="h4 mb-3 fw-bold">Explorar Categorías</h2>

            <!-- Contenedor deslizable -->
            <div class="slider-materiales d-flex gap-3 pb-3">

                <asp:Repeater ID="Repetidor" runat="server">

                    <ItemTemplate>
                        <div class="card card-materiales flex-shrink-0 border-0 shadow-sm">

                            <asp:ImageButton ID="ImgLogo" runat="server" CssClass="card-img-top"
                                ImageUrl='<%# Eval("UrlLogo") %>'
                                AlternateText='<%#Eval("Nombre") %>'
                                CommandArgument='<%# Eval("IdMaterial") %>'
                                CommandName="SeleccionarMaterial"
                                OnCommand="ImgLogo_Command" />
                            
                            <div class="card-body text-center">
                                <h5 class="card-title h6 fw-bold m-0"><%#Eval("Nombre") %></h5>
                            </div>
                        </div>
                    </ItemTemplate>
                    
                </asp:Repeater>
            </div>
        </div>
        <!--  fin contenedor deslizable -->

        <div class="d-flex flex-column gap-2 mt-4 p-3 bg-light rounded shadow-sm">
            <asp:Label ID="txtTitulo" runat="server" CssClass="h3 fw-bold text-dark" Text=""></asp:Label>
        </div>

        <div class="container my-4" style="max-width: 800px;">
            <div id="CarruselImagenes" class="carousel slide shadow rounded overflow-hidden" data-bs-ride="false">
                
                <div class="carousel-inner">
                    <asp:Repeater ID="RepetidorImgMateriales" runat="server">
                        <ItemTemplate>
                            <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                <img src="<%#Eval("URL_Imagen") %>" alt="Material de reciclaje"
                                    class="d-block w-100"
                                    style="height: 350px; object-fit: cover;"/>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <!-- Controles de Anterior y Siguiente -->
                <button class="carousel-control-prev" type="button" data-bs-target="#CarruselImagenes" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon bg-dark rounded-circle p-2" aria-hidden="true"></span>
                    <span class="visually-hidden">Anterior</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#CarruselImagenes" data-bs-slide="next">
                    <span class="carousel-control-next-icon bg-dark rounded-circle p-2" aria-hidden="true"></span>
                    <span class="visually-hidden">Siguiente</span>
                </button>

            </div>
        </div>

        <div class="d-flex flex-column gap-2 mt-4 p-3 bg-light rounded shadow-sm">
            <asp:Label ID="txtInfo" runat="server" CssClass="text-secondary fs-5 m-0" Text=""></asp:Label>
        </div>
        <hr />

    </div>

    
</asp:Content>
