<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaPrincipal.Master" AutoEventWireup="true" CodeBehind="Pesquisa.aspx.cs" Inherits="Topdados20.Pesquisa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="pesquisa.css" type="text/css" rel="stylesheet"/>
    <br> <br>
    <center> 
       
        <div class="card">
            <img src="img/nome pesquisa.png" width="10%">
        <div class="container">
        <h1>Consulta por Nome</h1>
        <table>
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text=></asp:Label></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtnome2" placeholder="Nome" CssClass="inputcpf" runat="server"></asp:TextBox></td>
            <td><asp:Button ID="ButtonNome2" runat="server" Text="Buscar" UseSubmitBehavior="False" OnClick="ButtonNome2_Click" CssClass="button"  /></td>
        </tr>
        <tr>
            <td><center><asp:Label ID="MsgNome2" runat="server" Text="Digite o nome"></asp:Label></center></td>
        </tr>
    </table>

             
    
    <table>
        <tr>
            <td><asp:Label ID="NOME" runat="server" Text=""></asp:Label></td>
            <td><asp:Label ID="NASC" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtnome" placeholder="Nome" CssClass="inputcpf" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="txtdatanasc" placeholder="Data de Nascimento" CssClass="inputcpf" runat="server" ></asp:TextBox></td>
            <td><asp:Button ID="ButtonNome" runat="server" Text="Buscar" OnClick="ButtonNome_Click" CssClass="button"  UseSubmitBehavior="False" /></td>
        </tr>
        <tr>
            <td><center><asp:Label ID="MsgNome" runat="server" Text="Digite o nome"></asp:Label></center></td>
            <td><center><asp:Label ID="MsgData" runat="server" Text="Digite a data"></asp:Label></center></td>
        </tr>
    </table>
</div> 
</div>
</center>
<br> <br>

<center>
    <div class="card">
        <img src="img/email pesquisa.png" width="10%">
    <div class="container">
        <h1>Consulta por Email</h1>
    
    <table>
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtemail" placeholder="Email" CssClass="inputcpf" runat="server"></asp:TextBox></td>
            <td><asp:Button ID="Button_Email" runat="server" Text="Buscar" OnClick="Button_Email_Click" CssClass="button"  UseSubmitBehavior="False" /></td>
        </tr>
        <tr>
            <td> <center><asp:Label ID="MsgEmail" runat="server" Text="Digite o E-mail"></asp:Label> </center></td>
        </tr>
    </table>
</div> 
</div>
</center>
<br> <br>

<center>
<div class="card">
    <img src="img/telefone pesquisa.png" width="10%">
<div class="container">
    <h1>Consulta por Telefone</h1>
    <table>
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txttelefone" placeholder="Telefone" CssClass="inputcpf" runat="server"></asp:TextBox>
            </td>
            <td><asp:Button ID="ButtonTelefone" runat="server" Text="Buscar" UseSubmitBehavior="False" OnClick="ButtonTelefone_Click" CssClass="button"  /></td>
        </tr>
        <tr>
          <td> <center> <asp:Label ID="MsgTelefone" runat="server" Text="Digite o Numero"></asp:Label></center></td> 
        </tr>
    </table>
</div>
</div>
</center>
<br> <br>

    <center>
    <div class="card">
        <img src="img/cep pesquisa.png" width="10%">
    <div class="container">
        <h1>Consulta por CEP</h1>
        
        <table>
            <tr>
                <td><asp:Label ID="cep" runat="server" Text=""></asp:Label></td>
                <td><asp:Label ID="Numero" runat="server" Text=""></asp:Label></td>
                <td><asp:Label ID="Complemento" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtcep" placeholder="CEP" CssClass="inputcpf" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="txtnumero" placeholder="Número" CssClass="inputcpf" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="txtcomplemento" placeholder="Complemento" CssClass="inputcpf" runat="server"></asp:TextBox></td>
                <td><asp:Button ID="ButoonCEP" runat="server" Text="Buscar" OnClick="ButoonCEP_Click" CssClass="button"  UseSubmitBehavior="False" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="MsgCEP" runat="server" Text="Informe o CEP"></asp:Label></td>
               <td><center> <asp:Label ID="MsgNumero" runat="server" Text="Informe o Numero"></asp:Label></center></td> 
            </tr>
        </table>
    </div>
    </div>
</center>
<br> <br>
    <center>
    <div class="card" runat="server" id="pesqplaca">
        <img src="img/veiculo pesquisa.png" width="10%">
    <div class="container">
        <h1>Consulta por Placa</h1>
        <table>
            <tr>
                <td><asp:Label ID="PLACA" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtplaca" placeholder="PLACA" CssClass="inputcpf" runat="server"></asp:TextBox></td>
                <td><asp:Button ID="BtnPlaca" runat="server" Text="Buscar" CssClass="button"  UseSubmitBehavior="False" OnClick="BtnPlaca_Click" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="Lbplaca" runat="server" Text="Informe a Placa"></asp:Label></td>
            </tr>
        </table>
    <br /><br />
    </div>
    </div>
        </center>
    <br /><br />
    <center>
    <div class="card">
         <img src="img/pesquisas.png" width="10%">    
    <div class="container">
   
    <asp:GridView ID="GridConsultaNome" CellPadding="4" CellSpacing="4" GridLines="None" runat="server" EmptyDataText="Nenhuma Informação encontrada">
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
           
    <asp:GridView ID="GridEmail" CellPadding="4" CellSpacing="4" GridLines="None" EmptyDataText="Nenhuma Informação encontrada" runat="server"> 
    <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
        
    <asp:GridView ID="GridTelefones" CellPadding="4" CellSpacing="4" GridLines="None" EmptyDataText="Nenhuma Informação encontrada" runat="server"> 
    <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <asp:GridView ID="GridViewNome" CellPadding="4" CellSpacing="4" GridLines="None" EmptyDataText="Nenhuma Informação encontrada" runat="server">    
    <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <asp:GridView ID="GridEndereco" CellPadding="4" CellSpacing="4" GridLines="None" EmptyDataText="Nenhuma Informação encontrada" runat="server"> 
    <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <asp:GridView ID="Gridplaca" CellPadding="4" CellSpacing="4" GridLines="None" EmptyDataText="Nenhuma Informação encontrada" runat="server"> 
    <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <br> <br>
</div>
</div>
</center>

    
</asp:Content>
