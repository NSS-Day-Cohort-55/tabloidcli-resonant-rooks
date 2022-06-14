using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString) { }


        public void Insert(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime)
                                        OUTPUT INSERTED.Id
                                        VALUES (@Title, @Content, @CreateDateTime)";
                    cmd.Parameters.AddWithValue("@Title", journal.Title);
                    cmd.Parameters.AddWithValue("@Content", journal.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", journal.CreateDateTime);
                    int id = (int)cmd.ExecuteScalar();
                    journal.Id = id;
                }
            }
        }
        public List<Journal> GetAll()
        {
            List<Journal> list = new List<Journal>();
            return list;
        }
        public Journal Get(int id)
        {
            Journal test = new Journal();
            return test;
        }
        public void Update(Journal entry)
        {
    
        }
        public void Delete(int id)
        {

        }
    }
}
