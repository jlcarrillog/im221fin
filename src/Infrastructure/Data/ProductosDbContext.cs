using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure
{
    public class ProductosDbContext
    {
        private readonly string _connectionString;
        public ProductosDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Producto> List()
        {
            var data = new List<Producto>();
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT [Id],[Nombre],[Descripcion],[Tipo],[Precio],[Cantidad],[Foto] FROM [Producto]", con);
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new Producto
                    {
                        Id = (Guid)dr["Id"],
                        Nombre = (string)dr["Nombre"],
                        Descripcion = (string)dr["Descripcion"],
                        Tipo = (short)dr["Tipo"],
                        Precio = (double)dr["Precio"],
                        Cantidad = (int)dr["Cantidad"],
                        Foto = (string)dr["Foto"]
                    });
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public Producto Details(Guid id)
        {
            var data = new Producto();
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT [Id],[Nombre],[Descripcion],[Tipo],[Precio],[Cantidad],[Foto] FROM [Producto] WHERE [Id] = @id", con);
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data.Id = (Guid)dr["Id"];
                    data.Nombre = (string)dr["Nombre"];
                    data.Descripcion = (string)dr["Descripcion"];
                    data.Tipo = (short)dr["Tipo"];
                    data.Precio = (double)dr["Precio"];
                    data.Cantidad = (int)dr["Cantidad"];
                    data.Foto = (string)dr["Foto"];
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void Create(Producto data)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"INSERT INTO [Producto] ([Id],[Nombre],[Descripcion],[Tipo],[Precio],[Cantidad],[Foto]) VALUES (NEWID(), @Nombre, @Descripcion, @Tipo, @Precio, @Cantidad, @Foto);", con);
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
            cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar).Value = data.Descripcion;
            cmd.Parameters.Add("@Tipo", SqlDbType.SmallInt).Value = data.Tipo;
            cmd.Parameters.Add("@Precio", SqlDbType.Float).Value = data.Precio;
            cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = data.Cantidad;
            cmd.Parameters.Add("@Foto", SqlDbType.NVarChar).Value = data.Foto;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void Edit(Producto data)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"UPDATE [Producto] SET [Nombre] = @Nombre, [Descripcion] = @Descripcion, [Tipo] = @Tipo, [Precio] = @Precio, [Cantidad] = @Cantidad, [Foto] = @Foto WHERE [Id] = @Id;", con);
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
            cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar).Value = data.Descripcion;
            cmd.Parameters.Add("@Tipo", SqlDbType.SmallInt).Value = data.Tipo;
            cmd.Parameters.Add("@Precio", SqlDbType.Float).Value = data.Precio;
            cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = data.Cantidad;
            cmd.Parameters.Add("@Foto", SqlDbType.NVarChar).Value = data.Foto;
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = data.Id;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void Delete(Guid id)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("DELETE FROM [Producto] WHERE [Id] = @id", con);
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }
}