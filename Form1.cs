using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lab03
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void txtServidor_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            String servidor = txtServidor.Text;
            String bd = txtBaseDatos.Text;
            String user = txtUsuario.Text;
            String pwd = txtPassword.Text;

            String str = "Server=" + servidor + ";DataBase=" + bd + ";";

            if (chkAutenticacion.Checked)
            {
                str += "Integrated Security=true";
            } else
            {
                str += "User Id=" + user + ";Password=" + pwd + ";";
            }

            try
            {
                //establecer nueva conexión
                conn = new SqlConnection(str);
                //abrir puente de conexión
                conn.Open();
                //mostrar mensaje si se logra conectar
                MessageBox.Show("Conectado satisfactoriamente");
                //habilitar la posibilidad de desconectarse una vez conectado
                btnDesconectar.Enabled = true;
            } catch (Exception ex)
            {
                MessageBox.Show("Error al conectar el servidor: \n"+ ex.ToString());
            }

        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            try
            {
                //si hay puente de conexión entonces realizar lo siguiente
                if (conn.State == ConnectionState.Open)
                {
                    MessageBox.Show("Estado del servidor: " + conn.State +
                        "\n Versión del servidor: " + conn.ServerVersion +
                        "\n Base de datos: " + conn.Database);
                } else
                {
                    MessageBox.Show("Estado del servidor: " + conn.State);
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Imposible determinar el estado del servidor: \n" + ex.ToString());
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State != ConnectionState.Closed)
                {
                    //cerrar puente de conexión
                    conn.Close();
                    MessageBox.Show("Conexión cerrada satisfactoriamente");
                } else
                {
                    MessageBox.Show("La conexión ya estaba cerrada");
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al cerrar la conexión "+ ex.ToString());
            }
        }

        private void chkAutenticacion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutenticacion.Checked)
            {
                txtUsuario.Enabled = false;
                txtPassword.Enabled = false;
            } else
            {
                txtUsuario.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void btnPersonas_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona(conn);
            persona.Show();
        }
    }
}
