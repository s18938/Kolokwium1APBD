
using Kolokwium1APBD.DTO.Requests;
using Kolokwium1APBD.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Kolokwium1APBD.Services
{
    public class AnimalSqlServerDbService : AnimalDbService
    {
        public IEnumerable FindAnimals(string orderBy)
        {
            using (var con = new SqlConnection(@"Data Source=DESKTOP-5G2FL6J\SQLEXPRESS;Initial Catalog=APBD_4;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                try
                {
                    var list = new List<Animal>();

                    com.Connection = con;
                    con.Open();
                    if (orderBy != null)
                    {
                        com.CommandText = $"SELECT Name, Type, Admissiondate, LastName FROM Animal join Owner on Animal.IdOwner = Owner.IdOwner order by {orderBy}";
                    }
                    else
                    {
                        com.CommandText = $"SELECT Name, Type, Admissiondate, LastName FROM Animal join Owner on Animal.IdOwner = Owner.IdOwner order by AdmissionDate desc";
                    }

                    using var dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        var animal = new Animal();
                        animal.Name = (dr["Name"].ToString());
                        animal.AnimalType = (dr["Type"].ToString());
                        animal.DateOfAdmision = DateTime.Parse(dr["AdmissionDate"].ToString());
                        animal.LastNameOfOwner = (dr["LastName"].ToString());
                        list.Add(animal);
                    }
                    return list;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public void InsertAnimal(AnimalRequest request)
        {
            using (var con =
               new SqlConnection(@"Data Source=DESKTOP-5G2FL6J\SQLEXPRESS;Initial Catalog=APBD_4;Integrated Security=True"))
            using (var com = new SqlCommand())

            {
                com.Connection = con;
                var tran = con.BeginTransaction();

                try
                {
                    com.CommandText =
                    "insert into Animal (IdAnimal,Name,Type,AdmissionDate,IdOwner)" +
                    " values(@IdAnimal,@Name,@Type,@AdmissionDate,@IdOwner)";
                    com.Parameters.AddWithValue("IdAnimal", request.IdAnimal);
                    com.Parameters.AddWithValue("Name", request.Name);
                    com.Parameters.AddWithValue("Type", request.AnimalType);
                    com.Parameters.AddWithValue("AdmissionDate", request.DateOfAdmision);
                    com.Parameters.AddWithValue("IdOwner", request.IdOwner);

                    foreach (Procedure procedure in request.Procedures)
                    {
                        com.CommandText =
                       "insert into Procedure_Animal (IdProcedure,IdAnimal,Date)" +
                       " values(@IdProcedure,@IdAnimal,@Date)";
                        com.Parameters.AddWithValue("IdProcedure", procedure.IdProcedure);
                        com.Parameters.AddWithValue("IdAnimal", request.IdAnimal);
                        com.Parameters.AddWithValue("Date", procedure.Date);
                    }
                }
                catch
                {
                    tran.Rollback();
                    throw new Exception();
                }
                tran.Commit();
                com.ExecuteNonQuery();
            }
        }
    }
}
