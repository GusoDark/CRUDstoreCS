using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace AbarroteraElTullido_GBD_P3
{
    public partial class frmAbarroteraElTullido : Form
    {
        MySqlConnection conexionDB = ConexiocnElTullido.conexion();
        public frmAbarroteraElTullido()
        {
            InitializeComponent();
        }
        private void btnAlta_Click(object sender, EventArgs e)
        {
            String codigo = txtCodigo.Text;
            String nombreP = txtNombreP.Text;
            double precioC = double.Parse(txtPrecioC.Text);
            double precioV = double.Parse(txtPrecioV.Text);
            String cantidad = txtCantidad.Text;

            DateTime fechaI = dtpFechaIngr.Value;

            //MessageBox.Show(fechaI + "");

            //string sql = "INSERT INTO producto (codigo, nombreP, precioC, precioV, cantidad, fechaI) VALUES ('" +
            //    codigo + "', '" + nombreP + "', " + precioC + ", " + precioV + ", '" + cantidad + "', '" + fechaI;

            string sql = "INSERT INTO producto (codigo, nombreP, precioC, precioV, cantidad, fechaI) " +
                "VALUES (@paramCodigo, @paramNom, @paramPrecioC, @paramPrecioV, @paramCant, @paramFecha)";
            
            conexionDB.Open();

            try
            {
                MySqlCommand alta = new MySqlCommand(sql, conexionDB);
                alta.Parameters.AddWithValue("@paramCodigo", codigo);
                alta.Parameters.AddWithValue("@paramNom", nombreP);
                alta.Parameters.AddWithValue("@paramPrecioC", precioC);
                alta.Parameters.AddWithValue("@paramPrecioV", precioV);
                alta.Parameters.AddWithValue("@paramCant", cantidad);
                alta.Parameters.AddWithValue("@paramfecha", fechaI);

                alta.ExecuteNonQuery();
                MessageBox.Show("Alta exitosa");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al dar alta: " + ex.Message);
            }
            finally 
            {
                conexionDB.Close();
            }
        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //MySqlCommand consult = new MySqlCommand('select*from abarrote.producto', consult);
            MySqlConnection conexionDB = ConexiocnElTullido.conexion();
            MySqlCommand consult = new MySqlCommand("select*from abarrote.producto;", conexionDB);
            MySqlDataAdapter adapt = new MySqlDataAdapter();
            conexionDB.Open();
            adapt.SelectCommand = consult;
            DataTable tabla = new DataTable();
            adapt.Fill(tabla);
            gvConsultas.DataSource = tabla;//GridView
            //lstConsulta.DataSource = tabla.ToString();
            conexionDB.Close();

        }
        private void btnBuscarC_Click(object sender, EventArgs e)
        {
            int codigo = 0;//Busqueda por código
            String sql = "select*from abarrote.producto Where codigo =";

            conexionDB.Open();

            try
            {
                codigo = Convert.ToInt32(txtCodigo.Text);
                MySqlCommand comand = new MySqlCommand(sql + codigo + ";", conexionDB);
                MySqlDataAdapter adapt = new MySqlDataAdapter();
                adapt.SelectCommand = comand;
                DataTable tabla = new DataTable();
                adapt.Fill(tabla);
                gvBusquedaC.Visible = true;
                gvBusquedaC.DataSource = tabla;
            }
            catch (System.FormatException c)
            {
                MessageBox.Show("Error, no se encuentra el codigo");
            }
            finally 
            {
                conexionDB.Close();
            }
            

        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            String codigo = txtCodigo.Text;
            String nombreP = txtNombreP.Text;
            double precioC = double.Parse(txtPrecioC.Text);
            double precioV = double.Parse(txtPrecioV.Text);
            String cantidad = txtCantidad.Text;
            DateTime fechaI = dtpFechaIngr.Value;

            //string sql = "INSERT INTO producto (codigo, nombreP, precioC, precioV, cantidad, fechaI) VALUES ('" +
            //    codigo + "', '" + nombreP + "', " + precioC + ", " + precioV + ", '" + cantidad + "', '" + fechaI;
            //string sql = "UPDATE producto SET nombreP='"+ nombreP +"', precioC="+ precioC +", precioV="+ precioV + ", cantidad= '" + cantidad + "', fechaI= '" + fechaI + "' " + "'Where codigo='" + codigo;
            //UPDATE producto SET nombreP = 'Pepsi', precioC = 16, precioV = 17, cantidad = 12, fechaI = '2022,05,17' WHERE codigo = 1;
            /*
                string sql = "INSERT INTO producto (codigo, nombreP, precioC, precioV, cantidad, fechaI) " +
                "VALUES (@paramCodigo, @paramNom, @paramPrecioC, @paramPrecioV, @paramCant, @paramFecha)";
             */
            string sql = "UPDATE producto SET nombreP= @paramNom, precioC= @paramPrecioC, precioV = @paramPrecioV, cantidad= @paramCant, fechaI= @paramFecha Where codigo=" + codigo;
            conexionDB.Open();
            try
            {
                MySqlCommand update = new MySqlCommand(sql, conexionDB);
                update.Parameters.AddWithValue("@paramCodigo", codigo);
                update.Parameters.AddWithValue("@paramNom", nombreP);
                update.Parameters.AddWithValue("@paramPrecioC", precioC);
                update.Parameters.AddWithValue("@paramPrecioV", precioV);
                update.Parameters.AddWithValue("@paramCant", cantidad);
                update.Parameters.AddWithValue("@paramfecha", fechaI);

                update.ExecuteNonQuery();
                MessageBox.Show("Actualización de producto exitosa");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }

        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            String codigo = txtCodigo.Text;

            string sql = "DELETE FROM producto Where codigo=" + codigo;
            conexionDB.Open();
            try
            {
                MySqlCommand delete = new MySqlCommand(sql, conexionDB);
                delete.Parameters.AddWithValue("@paramCodigo", codigo);
                delete.ExecuteNonQuery();
                MessageBox.Show("Baja de producto exitosa");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            txtNombreP.Text = "";
            txtCantidad.Text = "";
            txtPrecioC.Text = "";
            txtPrecioV.Text = "";
            dtpFechaIngr.Value = DateTime.Now;
        }
    
    
    }
}
