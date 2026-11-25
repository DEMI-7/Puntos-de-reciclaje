<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Aprender.aspx.cs" Inherits="Puntos_de_reciclaje.Aprender" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Estilos/EstiloPaginaAprender.css" rel="stylesheet" />

    <div class="container body-content mt-4">
        <h1>Aprender sobre reciclaje</h1>

        <div class="container my-4">
            <h2 class="h4 mb-3 fw-bold">Explorar Categorías</h2>

            <!-- grilla de materiales -->
            <div class="row row-cols-2 row-cols-sm-3 row-cols-md-4 row-cols-lg-5 g-3">

                <asp:Repeater ID="Repetidor" runat="server">

                    <ItemTemplate>
                        <div class="col">
                            <div class="card card-materiales h-100 border-0 shadow-sm">

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
                        </div>
                    </ItemTemplate>

                </asp:Repeater>
            </div>
        </div>
        <!--  fin grilla -->

        <!-- TITULO -->
        <div class="d-flex align-items-center gap-2 mt-4 p-3 px-4 panel-hollow">
            <asp:Label ID="txtTitulo" runat="server" CssClass="h3 fw-bold text-success m-0" Text=""></asp:Label>
        </div>

        <!-- CARRUSEL -->
        <div class="container my-4" style="max-width: 800px;">
            <div id="CarruselImagenes" class="carousel slide carrusel-verde overflow-hidden" data-bs-ride="false">
                
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
                    <span class="carousel-control-next-icon btn-carrusel-verde rounded-circle" aria-hidden="true"></span>
                    <span class="visually-hidden">Anterior</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#CarruselImagenes" data-bs-slide="next">
                    <span class="carousel-control-next-icon btn-carrusel-verde rounded-circle" aria-hidden="true"></span>
                    <span class="visually-hidden">Siguiente</span>
                </button>

            </div>
        </div>

        <div class="d-flex flex-column gap-2 mt-4 p-4 panel-hollow">
            <asp:Label ID="txtInfo" runat="server" CssClass="text-secondary fs-5 m-0" Text=""></asp:Label>
        </div>
        <hr class="my-5 border-success border-2 opacity-50" />

    </div>

    
</asp:Content>
