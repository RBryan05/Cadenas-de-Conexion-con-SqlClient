using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CapaModelos;

namespace CapaDatos
{
    public class CustomerRepository
    {
        List<Customers> Clientes = new List<Customers>();
        public List<Customers> ObtenerTodo()
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                String selectFrom = "";
                selectFrom = selectFrom + "SELECT " + "\n";
                selectFrom = selectFrom + "      [CustomerID] " + "\n";
                selectFrom = selectFrom + "      ,[CompanyName] " + "\n";
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

                using (SqlCommand comando = new SqlCommand(selectFrom, conexion))
                {
                    SqlDataReader reader = comando.ExecuteReader();
                    List<Customers> Customers = new List<Customers>();

                    while (reader.Read())
                    {
                        Customers customers = new Customers();
                        customers.CustomerID = reader["CustomerID"] == DBNull.Value ? "" : (String)reader["CustomerID"];
                        customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
                        customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
                        customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
                        customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
                        customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
                        customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
                        customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
                        customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
                        customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
                        customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];

                        Customers.Add(customers);
                        Clientes.Add(customers);
                    }
                    return Customers;
                }
            }
        }

        public Customers ObtenerPorId(string id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                String selectFrom = "";
                selectFrom = selectFrom + "SELECT " + "\n";
                selectFrom = selectFrom + "      [CustomerID] " + "\n";
                selectFrom = selectFrom + "      ,[CompanyName] " + "\n";
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
                selectFrom = selectFrom + "  WHERE [CustomerID] = @id" + "\n";

                using (SqlCommand comando = new SqlCommand(selectFrom, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = comando.ExecuteReader();
                    Customers Customers = null;

                    while (reader.Read())
                    {
                        Customers = LeerDelDataReader(reader);                      
                    }
                    return Customers;
                }
            }
        }

        private Customers LeerDelDataReader(SqlDataReader reader)
        {
            Customers Customers = new Customers();
            Customers.CustomerID = reader["CustomerID"] == DBNull.Value ? "" : (String)reader["CustomerID"];
            Customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
            Customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
            Customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
            Customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
            Customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
            Customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
            Customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
            Customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
            Customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
            Customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];

            return Customers;
        }

        public List<Customers> Filtrar(string nombre)
        {
            ObtenerTodo();
            List<Customers> Customers = new List<Customers>();
            var filtrar = Clientes.FindAll(x => x.CompanyName.StartsWith(nombre));
            return filtrar;
        }

        public int InsertarCliente(Customers cliente)
        {
            using(var conexion = DataBase.GetSqlConnection())
            {
                String insertInto = "";
                insertInto = insertInto + "INSERT INTO [dbo].[Customers] " + "\n";
                insertInto = insertInto + "           ([CustomerID] " + "\n";
                insertInto = insertInto + "           ,[CompanyName] " + "\n";
                insertInto = insertInto + "           ,[ContactName] " + "\n";
                insertInto = insertInto + "           ,[ContactTitle] " + "\n";
                insertInto = insertInto + "           ,[Address] " + "\n";
                insertInto = insertInto + "           ,[City]) " + "\n";
                insertInto = insertInto + "     VALUES " + "\n";
                insertInto = insertInto + "           (@CustomerID" + "\n";
                insertInto = insertInto + "           ,@CompanyName " + "\n";
                insertInto = insertInto + "           ,@ContactName " + "\n";
                insertInto = insertInto + "           ,@ContactTitle " + "\n";
                insertInto = insertInto + "           ,@Address " + "\n";
                insertInto = insertInto + "           ,@City)" + "\n";

                using(var comando = new SqlCommand(insertInto, conexion))
                {
                    comando.Parameters.AddWithValue("CustomerID", cliente.CustomerID);
                    comando.Parameters.AddWithValue("CompanyName", cliente.CompanyName);
                    comando.Parameters.AddWithValue("ContactName", cliente.ContactName);
                    comando.Parameters.AddWithValue("ContactTitle", cliente.ContactTitle);
                    comando.Parameters.AddWithValue("Address", cliente.Address);
                    comando.Parameters.AddWithValue("City", cliente.City);
                    var insertados = comando.ExecuteNonQuery();
                    return insertados;
                }
            }   
        }

        public int ModificarCliente(Customers cliente)
        {
            using(var conexion = DataBase.GetSqlConnection())
            {
                String update = "";
                update = update + "UPDATE [dbo].[Customers] " + "\n";
                update = update + "   SET [CustomerID] = @CustomerID " + "\n";
                update = update + "      ,[CompanyName] = @CompanyName " + "\n";
                update = update + "      ,[ContactName] = @ContactName " + "\n";
                update = update + "      ,[ContactTitle] = @ContactTitle " + "\n";
                update = update + "      ,[Address] = @Address " + "\n";
                update = update + "      ,[City] = @City " + "\n";
                update = update + " WHERE [CustomerID] = @CustomerID";

                using (var comando = new SqlCommand(update, conexion))
                {
                    int insertados = parametrosCliente(cliente, comando);
                    return insertados;
                };
            }
        }

        private int parametrosCliente(Customers cliente, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("CustomerID", cliente.CustomerID);
            comando.Parameters.AddWithValue("CompanyName", cliente.CompanyName);
            comando.Parameters.AddWithValue("ContactName", cliente.ContactName);
            comando.Parameters.AddWithValue("ContactTitle", cliente.ContactTitle);
            comando.Parameters.AddWithValue("Address", cliente.Address);
            comando.Parameters.AddWithValue("City", cliente.City);
            var insertados = comando.ExecuteNonQuery();
            return insertados;
        }
    }
}
