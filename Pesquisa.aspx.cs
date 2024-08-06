using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Topdados20
{
    public partial class Pesquisa : System.Web.UI.Page

    {
        SqlConnection con = new SqlConnection("Data Source=WIN-A3IL05BR0C6;Initial Catalog=TopDados;Persist Security Info=True;User ID=SISTEMA_TOPDADOS;Password=12qwaszx@");
        protected void Page_Load(object sender, EventArgs e)
        {
            MsgNome.Visible = false;
            MsgNome2.Visible = false;
            MsgData.Visible = false;
            MsgEmail.Visible = false;
            MsgTelefone.Visible = false;
            MsgCEP.Visible = false;
            MsgNumero.Visible = false;
            Lbplaca.Visible = false;
            GridConsultaNome.Visible = false;
            GridEmail.Visible = false;
            GridTelefones.Visible = false;
            GridEndereco.Visible = false;
            GridViewNome.Visible=false;
            Gridplaca.Visible = false;
            
        }

        protected void ButtonNome_Click(object sender, EventArgs e)
        {
            GridConsultaNome.Visible = true;
            GridEmail.Visible = false;
            GridTelefones.Visible = false;
            GridEndereco.Visible = false;
            GridViewNome.Visible = false;
            Gridplaca.Visible = false;


            if (String.IsNullOrEmpty(txtnome.Text))
            {
                MsgNome.Visible = true;
            }

            else if (String.IsNullOrEmpty(txtnome.Text))
            {
                MsgData.Visible = true;
            }

            else
            {
                String nome1 = txtnome.Text;
                String data1 = txtdatanasc.Text;

                //capturar a string de conexão
                //cria um objeto de conexão
                SqlCommand cmd = new SqlCommand("select CPF,NOME, Convert(varchar(10), NASC,103) AS 'DT NASC',A.NOME_MAE AS 'NOME DA MÃE',\r\n(select STRING_AGG (CIDADE + ' - '+ UF , ' / ') \r\nfrom \r\n(select c.CIDADE,c.UF from Produtivo.ENDERECO c where c.CONTATOS_ID = a.CONTATOS_ID\r\nunion\r\nselect c.CIDADE,c.UF from Produtivo.ENDERECO_NOVOS c where c.CONTATOS_ID = a.CONTATOS_ID\r\n)TMP) ENDERECOS\r\nfrom Produtivo.PESSOA_FISICA a \r\nwhere a.NOME LIKE @NOME1 AND NASC= @NASC1 ORDER BY NOME ASC", con);
                //adiciona os parametros
                cmd.Parameters.AddWithValue("NOME1", String.Format("{0}%", nome1));
                cmd.Parameters.AddWithValue("NASC1",data1);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridConsultaNome.DataSource = dt;
                GridConsultaNome.DataBind();
                con.Close();
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "scrollToBottom", "window.scrollTo(0, document.body.scrollHeight);", true);


        }

        protected void Button_Email_Click(object sender, EventArgs e)
        {
            GridConsultaNome.Visible = false;
            GridEmail.Visible = true;
            GridTelefones.Visible = false;
            GridEndereco.Visible = false;
            GridViewNome.Visible = false;
            Gridplaca.Visible = false;

            if (String.IsNullOrEmpty(txtemail.Text))
            {
                MsgEmail.Visible = true;
            }
            else
            {
                String email1 = txtemail.Text;

                //capturar a string de conexão
                //cria um objeto de conexão
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Produtivo.PESSOA_FISICA.cpf,Produtivo.PESSOA_FISICA.NOME,CONVERT (varchar(10), Produtivo.PESSOA_FISICA.NASC, 103) AS 'DATA NASCIMENTO',Produtivo.PESSOA_FISICA.NOME_MAE as 'NOME MÃE',Produtivo.EMAIL.EMAIL FROM Produtivo.EMAIL left join Produtivo.PESSOA_FISICA on Produtivo.EMAIL.CONTATOS_ID = Produtivo.PESSOA_FISICA.CONTATOS_ID where Produtivo.EMAIL.email = @EMAIL1 ORDER BY NOME ASC", con);
                //adiciona os parametros
                cmd.Parameters.AddWithValue("EMAIL1", email1);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridEmail.DataSource = dt;
                GridEmail.DataBind();
                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "scrollToBottom", "window.scrollTo(0, document.body.scrollHeight);", true);

        }

        protected void ButtonTelefone_Click(object sender, EventArgs e)
        {
            GridConsultaNome.Visible = false;
            GridEmail.Visible = false;
            GridTelefones.Visible = true;
            GridEndereco.Visible = false;
            GridViewNome.Visible = false;
            Gridplaca.Visible = false;

            if (String.IsNullOrEmpty(txttelefone.Text))
            {
                MsgTelefone.Visible = true;
            }
            else
            {
                String telefone1 = txttelefone.Text;

                //capturar a string de conexão
                //cria um objeto de conexão
                SqlCommand cmd = new SqlCommand("select distinct b.CPF,b.NOME,CONVERT (varchar(10), b.NASC, 103) AS 'DATA NASCIMENTO',b.NOME_MAE as 'NOME MÃE', a.DDD + a.TELEFONE as TELEFONE\r\nfrom Produtivo.TELEFONE a\r\ninner join Produtivo.PESSOA_FISICA b on a.CONTATOS_ID = b.CONTATOS_ID\r\nwhere TELEFONE = substring(@TELEFONE, 3, 9) and ddd = left(@TELEFONE, 2) \r\n\r\nunion\r\n\r\nselect distinct b.CPF,b.NOME,CONVERT (varchar(10), b.NASC, 103) AS 'DATA NASCIMENTO',b.NOME_MAE as 'NOME MÃE', a.DDD + a.TELEFONE as TELEFONE \r\nfrom Produtivo.TELEFONE_NOVOS a\r\ninner join Produtivo.PESSOA_FISICA b on a.CONTATOS_ID = b.CONTATOS_ID\r\nwhere TELEFONE = substring(@TELEFONE, 3, 9) and ddd = left(@TELEFONE, 2) ORDER BY b.NOME ASC", con);
                //adiciona os parametros
                cmd.Parameters.AddWithValue("TELEFONE", telefone1);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridTelefones.DataSource = dt;
                GridTelefones.DataBind();
                con.Close();
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "scrollToBottom", "window.scrollTo(0, document.body.scrollHeight);", true);

        }

        protected void ButoonCEP_Click(object sender, EventArgs e)
        {
            GridConsultaNome.Visible = false;
            GridEmail.Visible = false;
            GridTelefones.Visible = false;
            GridEndereco.Visible = true;
            GridViewNome.Visible = false;
            Gridplaca.Visible = false;

            if (String.IsNullOrEmpty(txtcep.Text))
            {
                MsgCEP.Visible = true;
            }
            else if (String.IsNullOrEmpty(txtnumero.Text))
            {
                MsgNumero.Visible = true;
            }
            else
            {
                String cep = txtcep.Text;
                String numero = txtnumero.Text;
                String complemento = txtcomplemento.Text;

                //capturar a string de conexão
                //cria um objeto de conexão
                SqlCommand cmd = new SqlCommand("select top (200) b.CPF,b.NOME,CONVERT (varchar(10), b.NASC, 103) AS 'DATA NASCIMENTO',b.NOME_MAE AS 'NOME MÃE',a.ENDERECO\r\nfrom Produtivo.ENDERECO a\r\ninner join Produtivo.PESSOA_FISICA b on a.CONTATOS_ID = b.CONTATOS_ID\r\nwhere a.CEP = right ('00000000' + @CEP,8) and a.ENDERECO like @NUMERO AND a.LOGR_COMPLEMENTO LIKE @COMPLEMENTO\r\n\r\nUNION\r\n\r\nselect top (100) b.CPF,b.NOME,CONVERT (varchar(10), b.NASC, 103) AS DATA_NASCIMENTO,b.NOME_MAE,a.ENDERECO\r\nfrom Produtivo.ENDERECO_NOVOS a\r\ninner join Produtivo.PESSOA_FISICA b on a.CONTATOS_ID = b.CONTATOS_ID\r\nwhere a.CEP = right ('00000000' + @CEP,8) and a.ENDERECO like @NUMERO AND a.ENDERECO LIKE @COMPLEMENTO\r\norder by NOME ASC", con);
                //adiciona os parametros
                cmd.Parameters.AddWithValue("CEP", cep);
                cmd.Parameters.AddWithValue("NUMERO","%"+numero+"%");
                cmd.Parameters.AddWithValue("COMPLEMENTO", "%" + complemento + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridEndereco.DataSource = dt;
                GridEndereco.DataBind();
                con.Close();
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "scrollToBottom", "window.scrollTo(0, document.body.scrollHeight);", true);

        }

        protected void ButtonNome2_Click(object sender, EventArgs e)
        {
            GridConsultaNome.Visible = false;
            GridEmail.Visible = false;
            GridTelefones.Visible = false;
            GridEndereco.Visible = false;
            GridViewNome.Visible = true;
            Gridplaca.Visible = false;

            if (String.IsNullOrEmpty(txtnome2.Text))
            {
                MsgNome2.Visible = true;
            }

            else
            {
                String nome2 = txtnome2.Text;
                
                //capturar a string de conexão
                //cria um objeto de conexão
                SqlCommand cmd = new SqlCommand("\r\nselect TOP (300) a.CPF,a.NOME, Convert(varchar(10), a.NASC,103) AS 'DT NASC',a.NOME_MAE as 'NOME DA MÃE',\r\n(select STRING_AGG (CIDADE + ' - '+ UF , ' / ') \r\nfrom \r\n(select c.CIDADE,c.UF from Produtivo.ENDERECO c where c.CONTATOS_ID = a.CONTATOS_ID\r\nunion\r\nselect c.CIDADE,c.UF from Produtivo.ENDERECO_NOVOS c where c.CONTATOS_ID = a.CONTATOS_ID\r\n)TMP) ENDERECOS\r\nfrom Produtivo.PESSOA_FISICA a \r\nwhere a.NOME like @NOME2  ORDER BY NOME ASC, ENDERECOS ASC", con);
                cmd.CommandTimeout = 100;
                //adiciona os parametros
                cmd.Parameters.AddWithValue("NOME2", nome2  + "%");
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridViewNome.DataSource = dt;
                GridViewNome.DataBind();
                con.Close();
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "scrollToBottom", "window.scrollTo(0, document.body.scrollHeight);", true);

        }

        protected void BtnPlaca_Click(object sender, EventArgs e)
        {
            GridConsultaNome.Visible = false;
            GridEmail.Visible = false;
            GridTelefones.Visible = false;
            GridEndereco.Visible = false;
            GridViewNome.Visible = false;
            Gridplaca.Visible = true;

            if (String.IsNullOrEmpty(txtplaca.Text))
            {
                Lbplaca.Visible = true;
            }

            else
            {
                String placa = txtplaca.Text;

                //capturar a string de conexão
                //cria um objeto de conexão
                SqlCommand cmd = new SqlCommand("SELECT [CPF]  as 'CPF/CNPJ' \r\n      ,PROPRI\r\n\t  ,[PLACA]\r\n      ,[MARCA]\r\n      ,[ANOFAB] \r\n      ,[END] +', '+[NUM]+' '+[COMPL] as 'ENDERECO'\r\n      ,[CIDADE] \r\n      ,[ESTADO] \r\n      \r\n  FROM [TopDados].[dbo].[VEICULO_PESSOA_FISICA]\r\n  WHERE PLACA = @PLACA\r\n\r\nunion\r\n\r\n  SELECT [CNPJ] as 'CPF/CNPJ'\r\n      ,PROPRI\r\n\t  ,[PLACA] \r\n      ,[MARCA]  \r\n      ,[ANOFAB]  \r\n      ,[ENDERECO]  \r\n      ,[CIDADE] \r\n      ,[ESTADO] \r\n  FROM [TopDados].[dbo].[VEICULOS_EMPRESAS]\r\n  WHERE PLACA = @PLACA", con);
                cmd.CommandTimeout = 100;
                //adiciona os parametros
                cmd.Parameters.AddWithValue("PLACA", placa);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Gridplaca.DataSource = dt;
                Gridplaca.DataBind();
                con.Close();
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "scrollToBottom", "window.scrollTo(0, document.body.scrollHeight);", true);    
        }
    }
}
