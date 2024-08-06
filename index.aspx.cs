using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Topdados20
{
    public partial class index : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String usuario = txtusuario.Text;
            String senha = txtsenha.Text;


            //capturar a string de conexão
            SqlConnection con = new SqlConnection("Data Source=WIN-A3IL05BR0C6;Initial Catalog=TopDados;Persist Security Info=True;User ID=SISTEMA_TOPDADOS;Password=12qwaszx@");
            SqlCommand command = new SqlCommand("SELECT  id_usuario, ds_usuario, ds_nome, id_perfil FROM Produtivo.USUARIO WHERE ds_usuario = @usuario AND ds_senha = HASHBYTES('SHA2_256', CONVERT(nvarchar(32), @senha)) AND     ic_ativo = 'S'", con);
            
            con.Open();
            command.Parameters.AddWithValue("usuario",usuario);
            command.Parameters.AddWithValue("senha", senha);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read()) {

                    string login = reader["ds_usuario"].ToString();
                    string id = reader["id_usuario"].ToString();
                    string perfil = reader["id_perfil"].ToString();

                    MSSqlConection sqlms = new MSSqlConection();

                    sqlms.ExecuteNonQuery(@"insert into [TopDados].[Produtivo].[USUARIO_LOG]  (ID_USUARIO,DT_ATIVIDADE,DS_ATIVIDADE)  values('"+id+"',GETDATE(),'Logar')");


                    // Cria o ticket e adiciona as roles
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, this.txtusuario.Text, DateTime.Now, DateTime.Now.AddDays(7), false, "CRM");

                // Criptografa o Ticket
                String encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                // Cria o cookie, e então adiciona o ticket criptografado com os dados
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                // Adiciona o cookie na coleção
                Response.Cookies.Add(authCookie);

                //session
                var htParametros = new Hashtable();

                htParametros.Add("ds_usuario", usuario);
                htParametros.Add("id_usuario", id);
                htParametros.Add("id_perfil", perfil);
                Session["parametros"] = htParametros;

                
                //direcionar para a pagina principal
                Response.Redirect("~/ConsultaCPF.aspx");
            }
            }
            else
            {
                MsgErro.Visible = true;
                MsgErro.Text = "login ou senha inválido";
            }
            con.Close();
        }
    }
}