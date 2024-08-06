using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Topdados20
{
    public partial class EnderecosVinculados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LbExcl.Visible = false;
                LbEndExis.Visible = false;
                // Obtenha o valor do parâmetro "id" da URL
                string id = Request.QueryString["ID"];
                CarregarEndereco();
                CarregarNome();

            }
        }


        protected void CarregarEndereco()
        {

            string id = Request.QueryString["ID"];
            MSSqlConection sqlMS = new MSSqlConection();

            GridProvEnd.DataSource = sqlMS.ReadData(@"SELECT a.ID_ENDERECO,a.CONTATOS_ID, a.logradouro, a.BAIRRO, a.CIDADE, a.UF, right('00000000' + a.CEP, 8) as CEP
                                                    FROM TopDados.dbo.PROVAVEIS_endereco a
                                                    WHERE(a.CONTATOS_ID = '" + id + "') ");
            GridProvEnd.DataBind();

            sqlMS.MSSqlConectionClose();
        }
        protected void CarregarNome()
        {

            string id = Request.QueryString["ID"];
            MSSqlConection sqlMS = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"SELECT a.NOME
                                                    FROM TopDados.Produtivo.PESSOA_FISICA a
                                                    WHERE(a.CONTATOS_ID = '" + id + "')");
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    LbNome.Text = reader["NOME"].ToString();

                }
            }
        }

        protected void GridProvEnd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";
            var ds_usuario = "";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
                ds_usuario = htParametros["ds_usuario"].ToString();
            }

            SqlConnection con = null;
            SqlConnection con2 = null;
            GridView grv1 = ((GridView)sender);
            grv1.SelectedIndex = -1;
            switch (e.CommandName)
            {
                case "validar":

                    try
                    {
                        string id = Request.QueryString["ID"];
                        string endereco_id = e.CommandArgument.ToString();
                        string connectionString = "Server = 10.128.61.4; Database = TopDados; Uid = SISTEMA_TOPDADOS; Pwd = 12qwaszx@";

                        string query = @"insert into Topdados.Produtivo.ENDERECO_NOVOS
                                        (ENDERECO_NOVOS_ID,CONTATOS_ID,ENDERECO,BAIRRO,CIDADE,UF,CEP ,ORIGEM_DADO_TOPDADOS ,DT_INCLUSAO)
                                        SELECT (SELECT MAX(ENDERECO_NOVOS_ID)+1 from Topdados.Produtivo.ENDERECO_NOVOS),CONTATOS_ID,Logradouro,Bairro,Cidade,UF,right('00000000' + CEP,8),'" + ds_usuario +@"' ,GETDATE()
                                        FROM TopDados.dbo.PROVAVEIS_ENDERECO a
                                        WHERE(a.CONTATOS_ID = @id and ID_ENDERECO = @endereco_id)";

                        string query2 = @"delete topdados.dbo.PROVAVEIS_ENDERECO
                                           where ID_ENDERECO = '353347'";


                        con = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@endereco_id", endereco_id);
                        
                        con.Open();
                        cmd.ExecuteNonQuery();


                        con2 = new SqlConnection(connectionString);
                        SqlCommand cmd2 = new SqlCommand(query2, con2);
                        cmd2.Parameters.AddWithValue("@id", id);
                        cmd2.Parameters.AddWithValue("@endereco_id", endereco_id);

                        con2.Open();
                        cmd2.ExecuteNonQuery();



                        CarregarEndereco();
                        LbEndExis.Visible = true;
                        LbEndExis.Text = "Endereço ID = '" + endereco_id + "' adicionado a Base Top Dados";

                    }
                    catch (Exception ex)
                    {
                        // Lidar com a exceção aqui, por exemplo, registrar em log ou exibir uma mensagem de erro adequada.
                        LbEndExis.Visible = true;
                        LbEndExis.Text = "Ocorreu uma exceção: " + ex.Message;

                    }
                    finally
                    {
                        // Garantir que as conexões sejam sempre fechadas, mesmo em caso de exceções.
                        if (con != null)
                            con.Close();

                        if (con2 != null)
                            con2.Close();

                    }

                    break;
            }
        }
    }
}