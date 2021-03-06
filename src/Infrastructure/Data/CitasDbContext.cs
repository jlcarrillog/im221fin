using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure
{
    public class CitasDbContext
    {
        private readonly string _connectionString;
        public CitasDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Cita> List()
        {
            var data = new List<Cita>();
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"SELECT [C].[Id],[P].[Id] AS [PacienteId],[P].[Nombre],[P].[Edad],[P].[Direccion],[P].[Telefono],[P].[Expediente],[P].[Foto],[C].[Estatus],[C].[Fecha]
                                        FROM [Cita] [C]
                                        JOIN [Paciente][P] ON [C].[PacienteId] = [P].[Id]", con);
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new Cita
                    {
                        Id = (Guid)dr["Id"],
                        Paciente = new Paciente
                        {
                            Id = (Guid)dr["PacienteId"],
                            Nombre = (string)dr["Nombre"],
                            Edad = (int)dr["Edad"],
                            Direccion = (string)dr["Direccion"],
                            Telefono = (string)dr["Telefono"],
                            Expediente = (string)dr["Expediente"],
                            Foto = (string)dr["Foto"]
                        },
                        Estatus = (short)dr["Estatus"],
                        Fecha = (DateTime)dr["Fecha"]
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

        public Cita Details(Guid id)
        {
            var data = new Cita();
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"SELECT [C].[Id],[P].[Id] AS [PacienteId],[P].[Nombre],[P].[Edad],[P].[Direccion],[P].[Telefono],[P].[Expediente],[P].[Foto],[C].[Estatus],[C].[Fecha]
                                        FROM [Cita] [C]
                                        JOIN [Paciente][P] ON [C].[PacienteId] = [P].[Id] WHERE [C].[Id] = @Id", con);
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data.Id = (Guid)dr["Id"];
                    data.Paciente = new Paciente
                    {
                        Id = (Guid)dr["PacienteId"],
                        Nombre = (string)dr["Nombre"],
                        Edad = (int)dr["Edad"],
                        Direccion = (string)dr["Direccion"],
                        Telefono = (string)dr["Telefono"],
                        Expediente = (string)dr["Expediente"],
                        Foto = (string)dr["Foto"]
                    };
                    data.Estatus = (short)dr["Estatus"];
                    data.Fecha = (DateTime)dr["Fecha"];
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

        public void Create(Cita data)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"INSERT INTO [Cita] ([Id],[PacienteId],[Estatus],[Fecha]) VALUES (NEWID(), @PacienteId, @Estatus, @Fecha);", con);
            cmd.Parameters.Add("@PacienteId", SqlDbType.UniqueIdentifier).Value = data.Paciente.Id;
            cmd.Parameters.Add("@Estatus", SqlDbType.Int).Value = data.Estatus;
            cmd.Parameters.Add("@Fecha", SqlDbType.SmallDateTime).Value = data.Fecha;
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

        public void Edit(Cita data)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"UPDATE [Cita] SET [PacienteId] = @PacienteId, [Estatus] = @Estatus, [Fecha] = @Fecha WHERE [Id] = @id;", con);

            cmd.Parameters.Add("@PacienteId", SqlDbType.UniqueIdentifier).Value = data.Paciente.Id;
            cmd.Parameters.Add("@Estatus", SqlDbType.NVarChar).Value = data.Estatus;
            cmd.Parameters.Add("@Fecha", SqlDbType.SmallDateTime).Value = data.Fecha;
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
            var cmd = new SqlCommand("DELETE FROM [Cita] WHERE [Id] = @id", con);
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