using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Topdados20
{
    public partial class TelefonesVinculados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LbExcl.Visible = false;
                LbTelExis.Visible = false;

                // Obtenha o valor do parâmetro "id" da URL
                string id = Request.QueryString["ID"];
                CarregarTelefone();
                CarregarNome();

            }
        }


        protected void CarregarTelefone()
        {

            string id = Request.QueryString["ID"];
            MSSqlConection sqlMS = new MSSqlConection();

            GridProvTel.DataSource = sqlMS.ReadData(@"SELECT a.contatos_id,a.DDD,a.Telefone,a.ID_TELEFONE
                                                    FROM TopDados.dbo.PROVAVEIS_TELEFONE a
                                                    WHERE(a.CONTATOS_ID = '" + id + "') ");
            GridProvTel.DataBind();

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



        protected void GridProvTel_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
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
                        string connectionString = "Server = 10.128.61.4; Database = TopDados; Uid = SISTEMA_TOPDADOS; Pwd = 12qwaszx@";

                        string query = @"SELECT a.contatos_id, a.DDD, a.Telefone, a.ID_TELEFONE
                                     FROM TopDados.dbo.PROVAVEIS_TELEFONE a
                                     INNER JOIN [TopDados].[Produtivo].[TELEFONE] b ON a.CONTATOS_ID = b.CONTATOS_ID AND a.Telefone = b.TELEFONE
                                     WHERE a.CONTATOS_ID = @id AND a.ID_TELEFONE = @telefone_id";


                        string query2 = @"SELECT a.contatos_id,a.DDD,a.Telefone,a.ID_TELEFONE
                                    FROM TopDados.dbo.PROVAVEIS_TELEFONE a
                                    inner join [TopDados].[Produtivo].[TELEFONE_NOVOS] b on a.CONTATOS_ID = b.CONTATOS_ID and a.Telefone = b.TELEFONE
                                     WHERE a.CONTATOS_ID = @id AND a.ID_TELEFONE = @telefone_id";

                        con = new SqlConnection(connectionString);

                        con2 = new SqlConnection(connectionString);

                        string id = Request.QueryString["ID"];

                        string telefone_id = e.CommandArgument.ToString();



                        SqlCommand cmd = new SqlCommand(query, con);

                        SqlCommand cmd2 = new SqlCommand(query2, con2);


                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@telefone_id", telefone_id);

                        cmd2.Parameters.AddWithValue("@id", id);
                        cmd2.Parameters.AddWithValue("@telefone_id", telefone_id);

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        con2.Open();
                        SqlDataReader reader2 = cmd2.ExecuteReader();





                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                LbTelExis.Visible = true;
                                LbTelExis.Text = "Telefone já conta na Base";
                            }
                        }

                        else if (reader2.HasRows)
                        {
                            while (reader.Read())
                            {
                                LbTelExis.Visible = true;
                                LbTelExis.Text = "Telefone já conta na Base";
                            }

                        }

                        else
                        {
                            SqlConnection con3 = null;
                            SqlConnection con4 = null;
                            try
                            {




                                string query3 = @"insert into Topdados.Produtivo.TELEFONE_NOVOS
                                                    (TELEFONE_NOVOS_ID,CONTATOS_ID ,DDD ,TELEFONE ,ORIGEM_DADO_TOPDADOS ,DT_INCLUSAO)
                                                    SELECT DISTINCT(SELECT MAX(TELEFONE_NOVOS_ID)+1 from Topdados.Produtivo.TELEFONE_NOVOS),a.contatos_id,a.DDD,a.Telefone,'" + ds_usuario + @"',GETDATE()
                                                    FROM TopDados.dbo.PROVAVEIS_TELEFONE a
                                                    left join[TopDados].[Produtivo].[TELEFONE_NOVOS] b on a.CONTATOS_ID = b.CONTATOS_ID and a.Telefone = b.TELEFONE
                                                    WHERE(a.CONTATOS_ID = @id and ID_TELEFONE = @telefone_id)";

                                string query4 = @"delete topdados.dbo.PROVAVEIS_TELEFONE
                                                  WHERE (CONTATOS_ID = @id and ID_TELEFONE = @telefone_id)";

                                con3 = new SqlConnection(connectionString);

                                SqlCommand cmd3 = new SqlCommand(query3, con3);

                                cmd3.Parameters.AddWithValue("@id", id);
                                cmd3.Parameters.AddWithValue("@telefone_id", telefone_id);

                                con3.Open();
                                cmd3.ExecuteNonQuery();


                                con4 = new SqlConnection(connectionString);
                                SqlCommand cmd4 = new SqlCommand(query4, con4);
                                cmd4.Parameters.AddWithValue("@id", id);
                                cmd4.Parameters.AddWithValue("@telefone_id", telefone_id);

                                con4.Open();
                                cmd4.ExecuteNonQuery();


                                CarregarTelefone();
                                LbTelExis.Visible = true;
                                LbTelExis.Text = "Telefone ID = '" + telefone_id + "' adicionado a Base Top Dados";
                                

                            }
                            catch (Exception ex)
                            {

                                LbTelExis.Visible = true;
                                LbTelExis.Text = "Ocorreu uma exceção:" + ex.Message;

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
                        Console.WriteLine("Ocorreu uma exceção: " + ex.Message);

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