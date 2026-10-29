<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Puntos_de_reciclaje.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-6">
            <!-- texto del mail-->
            <div class="mb-3">
                <asp:Label ID="LblEmail" runat="server" Text="Direccion de email" for="TxtEmail" class="form-label"></asp:Label>
                <asp:TextBox ID="TxtEmail" runat="server" class="form-control"></asp:TextBox>
            </div>

            <!-- texto de la contraseña-->
            <div class="mb-3">
                <asp:Label ID="LblContraseña" runat="server" Text="Contraseña" for="TxtContraseña" class="form-label"></asp:Label>
                <asp:TextBox ID="TxtContraseña" runat="server" class="form-control" type="password"></asp:TextBox>
            </div>

            <asp:Button ID="BotonLogin" runat="server" Text="Ingresar" class="btn btn-primary" />
        </div>
    </div>
</asp:Content>
