using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure
{
    public class PacientesDbContext
    {
        private readonly string _connectionString;
        public PacientesDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Paciente> List()
        {
            var data = new List<Paciente>();
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT [Id],[Nombre],[Edad],[Direccion],[Telefono],[Correo],[Expediente],[Foto] FROM [Paciente]", con);
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new Paciente
                    {
                        Id = (Guid)dr["Id"],
                        Nombre = (string)dr["Nombre"],
                        Edad = (int)dr["Edad"],
                        Direccion = (string)dr["Direccion"],
                        Telefono = (string)dr["Telefono"],
                        Correo = (string)dr["Correo"],
                        Expediente = (string)dr["Expediente"],
                        Foto = (string)dr["Foto"]
                    });
                }                return data;
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

        public Paciente Details(Guid id)
        {
            var data = new Paciente();
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT [Id],[Nombre],[Edad],[Direccion],[Telefono],[Correo],[Expediente],[Foto] FROM [Paciente] WHERE [Id] = @id", con);
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data.Id = (Guid)dr["Id"];
                    data.Nombre = (string)dr["Nombre"];
                    data.Edad = (int)dr["Edad"];
                    data.Direccion = (string)dr["Direccion"];
                    data.Telefono = (string)dr["Telefono"];
                    data.Correo = (string)dr["Correo"];
                    data.Expediente = (string)dr["Expediente"];
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

        public void Create(Paciente data)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"INSERT INTO [Paciente] ([Id], [Nombre], [Edad], [Direccion], [Telefono], [Correo], [Expediente], [Foto]) VALUES(NEWID(), @Nombre, @Edad, @Direccion, @Telefono, @Correo, @Expediente, @Foto)", con);
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
            cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = data.Edad;
            cmd.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = data.Direccion;
            cmd.Parameters.Add("@Telefono", SqlDbType.NVarChar).Value = data.Telefono;
            cmd.Parameters.Add("@Correo", SqlDbType.NVarChar).Value = data.Correo;
            cmd.Parameters.Add("@Expediente", SqlDbType.NVarChar).Value = data.Expediente;
            cmd.Parameters.Add("@Foto", SqlDbType.NVarChar).Value = data.Foto;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { throw; }
            finally
            {
                con.Close();
            }
        }

        public void Edit(Paciente data)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"UPDATE [Paciente] SET [Nombre] = @Nombre, [Edad] = @Edad, [Direccion] = @Direccion, [Telefono] = @Telefono, [Correo] = @Correo, [Expediente] = @Expediente, [Foto] = @Foto WHERE [Id] = @id;", con);
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
            cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = data.Edad;
            cmd.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = data.Direccion;
            cmd.Parameters.Add("@Telefono", SqlDbType.NVarChar).Value = data.Telefono;
            cmd.Parameters.Add("@Correo", SqlDbType.NVarChar).Value = data.Correo;
            cmd.Parameters.Add("@Expediente", SqlDbType.NVarChar).Value = data.Expediente;
            cmd.Parameters.Add("@Foto", SqlDbType.NVarChar).Value = data.Foto;
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = data.Id;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { throw; }
            finally
            {
                con.Close();
            }
        }

        public void Delete(Guid id)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("DELETE FROM [Paciente] WHERE [Id] = @id", con);
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
