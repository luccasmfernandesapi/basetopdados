<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaPrincipal.Master" AutoEventWireup="true" CodeBehind="CONSULTACNPJ.aspx.cs" Inherits="Topdados20.CONSULTACNPJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <center> 
<link href="style.css" type="text/css" rel="stylesheet"/>    
    <div class="pesquisaCNPJ">
    <asp:TextBox ID="txtCNPJ" placeholder="CNPJ" runat="server" MaxLength="14" CssClass="inputcpf"></asp:TextBox>
    <asp:Button ID="BtnCNPJ" runat="server" Text="Buscar CNPJ" OnClick="BtnCNPJ_Click" CssClass="button" UseSubmitBehavior="False" />
        <br />
        <asp:Label ID="MSgCNPJne" runat="server" Text="CNPJ Não Encontrado"></asp:Label>
    </div>
   </center>
    <center>
    <div class="card" runat="server" ID="cardcadCNPJ">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/img/CNPJ pesquisa.png" width="100px"  CssClass="img" />
        <div class="container">
            <h1>Dados Cadastrais</h1>
        <br />
            <table runat="server" id="tbddcad">
                <tr>
                <td class="label"><b>CNPJ</b></td>
                <td class="label"><asp:Label ID="LbCNPJ" runat="server" Text="Label"></asp:Label></td>
            </tr>

            <tr>
                <td class="label"><b>Razão Social</b></td>
                <td class="label"><asp:Label ID="LbRSocial" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td class="label"><b>Nome Fantasia</b></td>
                <td class="label"><asp:Label ID="LbNFatasia" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td class="label"><b>Data Abertura</b></td>
                <td class="label"><asp:Label ID="LbDtFund" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td class="label"><b>Situação Cadastral</b></td>
                <td class="label"><asp:Label ID="LbStCad" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td class="label"><b>Motivo da Situação Cadastral</b></td>
                <td class="label"><asp:Label ID="LbMotStCad" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td class="label"><b>Data da Situação</b></td>
                <td class="label"><asp:Label ID="LbDtSt" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td class="label"><b>CNAE</b></td>
                <td class="label"><asp:Label ID="LbCnae" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td class="label"><b>Natureza Jurídica</b></td>
                <td class="label"><asp:Label ID="LbNatJur" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td class="label"><b>Capital Social</b></td>
                <td class="label"><asp:Label ID="LbCapSocial" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td class="label"><b>Porte</b></td>
                <td class="label"><asp:Label ID="LbPorte" runat="server" Text="Label"></asp:Label></td>
            </tr>
            
        </table>
    </div> </div>
        </center>
    <center>
        <br /><br />
    <div class="card">
        <img src="img/mapa.png" />
        <div class="container"><h1>Endereço</h1>
            <asp:GridView id="GridEndCNPJ" GridLines="None" CellPadding="4" CellSpacing="4" runat="server">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>

        </div>
        </div>
        </center>

    <center>
        <br /><br />
    <div class="card">
        <img src="img/filial.png" width="100px" />
        <div class="container"><h1>FILIAIS</h1>
            <asp:GridView id="GridFiliais" GridLines="None" CellPadding="4" CellSpacing="4" runat="server" CssClass="tabela" Font-Size="Smaller">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>

        </div>
        </div>
        </center>
    <br /><br />
    <center>
    <div class="card">
        <img src="img/parceiros.png" width="100px" />
        <div class="container">
            <h1>Sócios</h1>
            <br />
            <asp:GridView id="GridSocios" GridLines="None" CellPadding="4" CellSpacing="4" runat="server">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>

        </div>
        </div>
        </center>
    <br /><br />
    <center>
    <div class="card">
        <img src="img/telefone.png" width="100px" />
        <div class="container">
            <h1>Telefones</h1>
            <br />
            <asp:GridView id="GridTelefone" GridLines="None" CellPadding="4" CellSpacing="4" runat="server">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>

        </div>
        </div>
        </center>
    <br /><br />
    <center>
    <div class="card">
        <img src="img/email.png" width="100px" />
        <div class="container">
            <h1>E-MAIL</h1>
            <br />
            <asp:GridView id="GridEmail" GridLines="None" CellPadding="4" CellSpacing="4" runat="server">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>

        </div>
        </div>
        </center>
    <br /> <br />
             <center>
                <div class="card" width="100%" runat="server" id="Div1">    
                    <asp:Image ID="Image9" runat="server" ImageUrl="~/img/veiculo.png" Width="100px" CssClass="img" />
             <div class="container" width="100%">
                 <h1>Veiculos</h1>
                 <br />
            <asp:GridView ID="GridVeiculos" GridLines="None" CellPadding="4" CellSpacing="4" runat="server" Font-Size="Smaller">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />

        </asp:GridView>
             </div> </div>
             </center>   
</asp:Content>
