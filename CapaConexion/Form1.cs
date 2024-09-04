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
using CapaModelos;
using CapaDatos;
using System.Reflection;

namespace CapaConexion
{
    public partial class Form1 : Form
    {
        CustomerRepository customerRepository = new CustomerRepository();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            dataGrid.DataSource = customerRepository.ObtenerTodo();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string texto = tbFiltro.Text;
            if (texto != "")
            {
                dataGrid.DataSource = customerRepository.Filtrar(texto);
            }
            else
            {
                dataGrid.DataSource = customerRepository.ObtenerTodo();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CapaDatos.DataBase.ApplicationName = "Programacion 2 ejemplo";
            CapaDatos.DataBase.ConnetionTimeout = 30;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                var consulta = customerRepository.ObtenerPorId(txtBuscar.Text);
                if (consulta != null)
                {
                    txtCompanyName.Text = consulta.CompanyName;
                    txtAddress.Text = consulta.Address;
                    txtCity.Text = consulta.City;
                    txtContactName.Text = consulta.ContactName;
                    txtCustomersID.Text = consulta.CustomerID;
                    txtContactTitle.Text = consulta.ContactTitle;
                }
                else
                {
                    MessageBox.Show("No se encontraron registros con este id");
                }
            }
            catch 
            {
                MessageBox.Show("Ocurrio un error.");
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            var nuevoCliente = ObtenerNuevoCliente();

            var resultado = 0;

            if (ValidarCampoNull() == false)
            {
                resultado = customerRepository.InsertarCliente(nuevoCliente);
                MessageBox.Show($"Cliente insertado con exito {resultado}");
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos por favor.");
            }
            
        }

        private Boolean ValidarCampoNull()
        {
            var objeto = ObtenerNuevoCliente();
            foreach (PropertyInfo property in objeto.GetType().GetProperties())
            {
                object value = property.GetValue(objeto, null);

                if (value == "")
                {
                    return true;
                }
            }
            return false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var editarCliente = ObtenerNuevoCliente();

            if (ValidarCampoNull() == false)
            {
                var resultado = customerRepository.ModificarCliente(editarCliente);
                MessageBox.Show("Cliente modificado.");
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos por favor.");
            }
        }

        private Customers ObtenerNuevoCliente()
        {
            var enuevoCliente = new Customers
            {
                CompanyName = txtCompanyName.Text,
                CustomerID = txtCustomersID.Text,
                ContactName = txtContactName.Text,
                ContactTitle = txtContactTitle.Text,
                Address = txtAddress.Text,
                City = txtCity.Text,
            };

            return enuevoCliente;
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            string id = txtCustomersID.Text;
            int eliminados = customerRepository.EliminarCliente(id);
            MessageBox.Show("Filas afectadas: " + eliminados);
        }
    }
}
