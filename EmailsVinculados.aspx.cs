using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Topdados20
{
    public partial class EmailsVinculados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LbExcl.Visible = false;
                LbEmailExis.Visible = false;

                // Obtenha o valor do parâmetro "id" da URL
                string id = Request.QueryString["ID"];
                CarregarEmail();
                CarregarNome();

            }
        }

        protected void CarregarEmail()
        {

            string id = Request.QueryString["ID"];
            MSSqlConection sqlMS = new MSSqlConection();

            GridProvEmail.DataSource = sqlMS.ReadData(@"SELECT a.contatos_id,a.Email,ID_EMAIL
                                                    FROM TopDados.dbo.PROVAVEIS_Email a
                                                    WHERE(a.CONTATOS_ID = '" + id + "') ");
            GridProvEmail.DataBind();

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

        protected void GridProvEmail_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        string email_id = e.CommandArgument.ToString();


                        string connectionString = "Server = 10.128.61.4; Database = TopDados; Uid = SISTEMA_TOPDADOS; Pwd = 12qwaszx@";
                        string query = @"SELECT top(1) a.Contatos_id, a.Email, a.ID_EMAIL
                                        FROM TopDados.dbo.PROVAVEIS_EMAIL a
                                        inner JOIN [TopDados].[Produtivo].EMAIL b ON a.CONTATOS_ID = b.CONTATOS_ID AND a.Email = b.EMAIL
                                        WHERE a.CONTATOS_ID = @id AND a.ID_EMAIL = @email_id";

                        con = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@email_id", email_id);

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();


                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                LbEmailExis.Visible = true;
                                LbEmailExis.Text = "Email já conta na Base";
                            }
                        }

                        else
                        {
                            SqlConnection con3 = null;
                            SqlConnection con4 = null;
                            try
                            {


                                string query3 = @"insert into Topdados.Produtivo.EMAIL
                                                (CONTATOS_ID,EMAIL ,ORIGEM_DADO_TOPDADOS ,DT_INCLUSAO)
                                                SELECT top(1) a.contatos_id,a.Email,'" + ds_usuario + @"',GETDATE()
                                                FROM TopDados.dbo.PROVAVEIS_EMAIL a
                                                left join[TopDados].[Produtivo].EMAIL b on a.CONTATOS_ID = b.CONTATOS_ID
                                                WHERE(a.CONTATOS_ID = @id and ID_EMAIL = @email_id)";

                                string query4 = @"delete topdados.dbo.PROVAVEIS_EMAIL
                                                  WHERE (CONTATOS_ID = @id and ID_EMAIL = @email_id)";


                                con3 = new SqlConnection(connectionString);
                                SqlCommand cmd3 = new SqlCommand(query3, con3);
                                cmd3.Parameters.AddWithValue("@id", id);
                                cmd3.Parameters.AddWithValue("@email_id", email_id);

                                con3.Open();
                                cmd3.ExecuteNonQuery();


                                con4 = new SqlConnection(connectionString);
                                SqlCommand cmd4 = new SqlCommand(query4, con4);
                                cmd4.Parameters.AddWithValue("@id", id);
                                cmd4.Parameters.AddWithValue("@email_id", email_id);

                                con4.Open();
                                cmd4.ExecuteNonQuery();


                                CarregarEmail();
                                LbEmailExis.Visible = true;
                                LbEmailExis.Text = "Email ID = '" + email_id + "' adicionado a Base Top Dados";
                            }

                            catch (Exception ex)
                            {

                                LbEmailExis.Visible = true;
                                LbEmailExis.Text = "Ocorreu uma exceção:" + ex.Message;

                            }
                            finally
                            {
                                // Garantir que as conexões sejam sempre fechadas, mesmo em caso de exceções.
                                if (con3 != null)
                                    con3.Close();
                                if (con4 != null)
                                    con4.Close();
                            }
                        }

                    }

                    catch (Exception ex)
                    {
                        // Lidar com a exceção aqui, por exemplo, registrar em log ou exibir uma mensagem de erro adequada.
                        LbEmailExis.Visible = true;
                        LbEmailExis.Text="Ocorreu uma exceção: " + ex.Message;

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