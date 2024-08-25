using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ABMPersonas
{
    public partial class frmPersona : Form
    {
        bool esNuevo = false;
        SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-N2PU4DD\SQLEXPRESS;Initial Catalog=TUPPI;Integrated Security=True");
        SqlCommand comando = new SqlCommand();

        public frmPersona()
        {
            InitializeComponent();
        }

        private void frmPersona_Load(object sender, EventArgs e)
        {
            //Cadena de conexion para el servidor local
            //conexion.ConnectionString = @"Data Source=CX-OSCAR\SQLEXPRESS;Initial Catalog=TUPPI;Integrated Security=True";
            //Cadena de conexion para el servidor de la facultad
            //conexion.ConnectionString = @"Data Source = 172.16.10.196; Initial Catalog = TUPPI2024; User ID = alumno1w1;Password=alumno1w1";

            habilitar(false);
            
            //cargarComboDocumentos();
            //cargarComboCiviles();
            cargarCombo(cboTipoDocumento, "tipo_documento");
            cargarCombo(cboEstadoCivil, "estado_civil");
        }

        private void cargarCombo(ComboBox combo,string nombreTabla)
        {
            conexion.Open();

            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * from "+nombreTabla+" order by 2";

            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());

            conexion.Close();

            combo.DataSource = tabla;
            combo.DisplayMember = tabla.Columns[1].ColumnName;
            combo.ValueMember = tabla.Columns[0].ColumnName;

        }

        //private void cargarComboDocumentos()
        //{
        //    SqlConnection conexion = new SqlConnection();
        //Cadena de conexion para el servidor local
        //    conexion.ConnectionString = @"Data Source=CX-OSCAR\SQLEXPRESS;Initial Catalog=TUPPI;Integrated Security=True";
        //Cadena de conexion para el servidor de la facultad
        //    conexion.ConnectionString = @"Data Source = 172.16.10.196; Initial Catalog = TUPPI2024; User ID = alumno1w1;Password=alumno1w1";
        //    conexion.Open();

        //    SqlCommand comando = new SqlCommand();
        //    comando.Connection = conexion;
        //    comando.CommandType= CommandType.Text;
        //    comando.CommandText = "Select * from tipo_documento order by 2";

        //    DataTable tabla = new DataTable();
        //    tabla.Load(comando.ExecuteReader());

        //    conexion.Close();

        //    cboTipoDocumento.DataSource= tabla;
        //    cboTipoDocumento.DisplayMember = "n_tipo_documento";
        //    cboTipoDocumento.ValueMember = "id_tipo_documento";

        //}

        private void habilitar(bool x)
        {
            txtApellido.Enabled = x;
            txtNombres.Enabled = x;
            cboTipoDocumento.Enabled = x;
            txtDocumento.Enabled = x;
            cboEstadoCivil.Enabled = x;
            dtpFechaNacimiento.Enabled = x;
            rbtFemenino.Enabled = x;
            rbtMasculino.Enabled = x;
            chkFallecio.Enabled = x;
            btnGrabar.Enabled = x;
            btnCancelar.Enabled = x;
            btnNuevo.Enabled = !x;
            btnEditar.Enabled = !x;
            btnBorrar.Enabled = !x;
            btnSalir.Enabled = !x;
            lstPersonas.Enabled = !x;
        }

        private void limpiar()
        {
            txtApellido.Text = "";
            txtNombres.Text = "";
            cboTipoDocumento.SelectedIndex = -1;
            txtDocumento.Text = "";
            cboEstadoCivil.SelectedIndex = -1;
            dtpFechaNacimiento.Value = DateTime.Today;
            rbtFemenino.Checked = false;
            rbtMasculino.Checked = false;
            chkFallecio.Checked = false;
        }
      
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            habilitar(true);
            limpiar();
            txtApellido.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            habilitar(true);
            txtDocumento.Enabled = false;
            txtApellido.Focus();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
                

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
            limpiar();
            habilitar(false);
            esNuevo = false;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
    

            if (esNuevo) 
                {

                    // VALIDAR QUE NO EXISTA LA PK !!!!!! (SI NO ES AUTOINCREMENTAL / IDENTITY)

                    // insert con sentencia SQL tradicional

                    // insert usando parámetros


                habilitar(false);
                esNuevo = false;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro de abandonar la aplicación ?",
                "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();
        }

    }
}
