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
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Journal";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Journal> journalEntries = new List<Journal>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int idValue = reader.GetInt32(idColumnPosition);
                            int titleColumnPosition = reader.GetOrdinal("Title");
                            string titleValue = reader.GetString(titleColumnPosition);
                            int contentColumnPosition = reader.GetOrdinal("Content");
                            string contentValue = reader.GetString(contentColumnPosition);
                            int dateColumnPosition = reader.GetOrdinal("CreateDateTime");
                            DateTime dateValue = reader.GetDateTime(dateColumnPosition);

                            Journal journal = new Journal
                            {
                                Id = idValue,
                                Title = titleValue,
                                Content = contentValue,
                                CreateDateTime = dateValue
                            };
                            journalEntries.Add(journal);
                        }
                        return journalEntries;
                    }
                }
            }
        }
        public Journal Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Journal WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Journal journal = null;

                        if (reader.Read())
                        {
                            journal = new Journal()
                            {
                                Id = id,
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Content = reader.GetString(reader.GetOrdinal("Content")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                            };
                        }
                        return journal;
                    }
                }
            }
        }
        public void Update(Journal entry)
        {
    
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Journal
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
