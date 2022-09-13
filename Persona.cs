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
    public partial class Persona : Form
    {
        SqlConnection conn;
        public Persona(SqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                String sql = "SELECT * FROM usuarios";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                dgvListado.DataSource = dt;
                dgvListado.Refresh();
            } 
            else
            {
                MessageBox.Show("La conexión esta cerrada");
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //trim sin parametros sirve para quitar espacios en blanco
            String nombre = txtNombre.Text.Trim();
            if (nombre.Length == 0)
            {
                MessageBox.Show("No has colocado nada oe");
            } else if (conn.State == ConnectionState.Open && nombre.Length > 0)
            {
                /*
                String sql = "SELECT * FROM usuarios WHERE usuario_nombre = '" + nombre + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                dgvListado.DataSource = dt;
                dgvListado.Refresh();
                */
                List<PersonModel> personModel = new List<PersonModel>();

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@LastName";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Value = nombre;

                SqlCommand cmd = new SqlCommand("selectWorkersByName", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(parameter);

                SqlDataReader reader = cmd.ExecuteReader();
                
                /*
                DataTable dt = new DataTable();
                dt.Load(reader);

                dgvListado.DataSource = dt;
                dgvListado.Refresh();
                */

                while (reader.Read())
                {
                    //crear modelos e insertar
                    PersonModel person = new PersonModel 
                    {
                        PersonID = reader["PersonID"] != DBNull.Value ? (int)reader["PersonID"] : 0,
                        FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : string.Empty,
                        LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : string.Empty,
                    };

                    personModel.Add(person);
                }

                dgvListado.DataSource = personModel;
                dgvListado.Refresh();
            } else
            {
                MessageBox.Show("La conexión esta cerrada");
            }
        }
    }
}
