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
namespace CapaConexion
{
    public partial class Form1 : Form
    {
        List<Customers> Clientes = new List<Customers>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection
                ("Data Source=DESKTOP-03PS3SB\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");

            MessageBox.Show("Conexion creada");
            conexion.Open();

            // Codigo de guia VII
            //string selectFrom = "SELECT * FROM [dbo].[Customers]";
            //SqlCommand comando = new SqlCommand(selectFrom, conexion);
            //SqlDataReader reader = comando.ExecuteReader();
        
            //------------------------
   
            String selectFrom = "";
                
            selectFrom = selectFrom + "SELECT [CompanyName] " + "\n";                
            selectFrom = selectFrom + "      ,[ContactName] " + "\n";            
            selectFrom = selectFrom + "      ,[ContactTitle] " + "\n";            
            selectFrom = selectFrom + "      ,[Address] " + "\n";            
            selectFrom = selectFrom + "      ,[City] " + "\n";           
            selectFrom = selectFrom + "      ,[Region] " + "\n";            
            selectFrom = selectFrom + "      ,[PostalCode] " + "\n";            
            selectFrom = selectFrom + "      ,[Country] " + "\n";           
            selectFrom = selectFrom + "      ,[Phone] " + "\n";
            selectFrom = selectFrom + "      ,[Fax] " + "\n";            
            selectFrom = selectFrom + "  FROM [dbo].[Customers]";
                
            //-----------------------                
            SqlCommand comando = new SqlCommand(selectFrom, conexion);                
            SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())            
            {
                Customers clientes = new Customers();
                clientes.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
                clientes.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
                clientes.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
                clientes.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
                clientes.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
                clientes.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
                clientes.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (string)reader["PostalCode"];
                clientes.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
                clientes.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
                clientes.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];

                Clientes.Add(clientes);
            }

            dataGrid.DataSource = Clientes;
            MessageBox.Show("Conexion cerrada");               
            conexion.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var filtro = Clientes.FindAll(X => X.CompanyName.StartsWith(tbFiltro.Text));
            dataGrid.DataSource = filtro;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CapaDatos.DataBase.ApplicationName = "Programacion 2 ejemplo";
            CapaDatos.DataBase.ConnetionTimeout = 30;
            var cadenaConexion = CapaDatos.DataBase.GetSqlConnection();

        }
    }
}
