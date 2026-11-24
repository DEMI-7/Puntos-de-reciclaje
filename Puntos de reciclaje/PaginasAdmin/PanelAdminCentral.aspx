<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdminCentral.aspx.cs" Inherits="Puntos_de_reciclaje.PaginasAdmin.PanelAdminCentral" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Estilos/EstiloPanelAdminCentral.css" rel="stylesheet" />
    <div class="container body-content mt-4">

        <div class="row justify-content-center">


            <div class="col-12 col-sm-6 col-md-4 mb-4 d-flex justify-content-center">
                <div class="card shadow-sm h-100 tarjeta">
                    <div class="text-center pt-3">
                        <img src="../ImagenesSistema/usuarios%20logo.png" class="card-img-top imagen-centrada" />
                    </div>
                    <div class="card-body text-center d-flex flex-column justify-content-between">
                        <asp:Button ID="BtnUsuariosActivos" runat="server" CssClass="btn btn-success" Text="Administrar usuarios" OnClick="BtnUsuariosActivos_Click" />
                    </div>
                </div>
            </div>

            <div class="col-12 col-sm-6 col-md-4 mb-4 d-flex justify-content-center">
                <div class="card shadow-sm h-100 tarjeta">
                    <div class="text-center pt-3">
                        <img src="../ImagenesSistema/empresas.png" class="card-img-top imagen-centrada" />
                    </div>
                    <div class="card-body text-center d-flex flex-column justify-content-between">
                        <asp:Button ID="BtnEmpresasPendientes" runat="server" CssClass="btn btn-success" Text="Administrar empresas" OnClick="BtnEmpresasPendientes_Click" />
                    </div>
                </div>
            </div>

            <div class="col-12 col-sm-6 col-md-4 mb-4 d-flex justify-content-center">
                <div class="card shadow-sm h-100 tarjeta">
                    <div class="text-center pt-3">
                        <img src="../ImagenesSistema/materialesAdmin.png" class="card-img-top imagen-centrada" />
                    </div>
                    <div class="card-body text-center d-flex flex-column justify-content-between">
                        <asp:Button ID="BtnAdministrarMateriales" runat="server" CssClass="btn btn-success" Text="Administrar materiales" OnClick="BtnAdministrarMateriales_Click" />
                    </div>
                </div>
            </div>

            <div class="col-12 col-sm-6 col-md-4 mb-4 d-flex justify-content-center">
                <div class="card shadow-sm h-100 tarjeta">
                    <div class="text-center pt-3">
                        <img src="../ImagenesSistema/registros.png" class="card-img-top imagen-centrada" />
                    </div>
                    <div class="card-body text-center d-flex flex-column justify-content-between">
                        <asp:Button ID="BtnRegistros" runat="server" CssClass="btn btn-success" Text="Ver registros de acciones" OnClick="BtnRegistros_Click"/>
                    </div>
                </div>
            </div>


            <div class="col-12 col-sm-6 col-md-4 mb-4 d-flex justify-content-center">
    <div class="card shadow-sm h-100 tarjeta">
        <div class="text-center pt-3">
            <img src="../ImagenesSistema/reseniasLogo.png" class="card-img-top imagen-centrada" />
        </div>
        <div class="card-body text-center d-flex flex-column justify-content-between">
            <asp:Button ID="BtnAdministrarResenias" runat="server" CssClass="btn btn-success" Text="Administrar Reseñas" OnClick="BtnAdministrarResenias_Click" />
        </div>
    </div>
</div>




        </div>
    </div>


</asp:Content>
