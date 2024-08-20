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
    }
}
