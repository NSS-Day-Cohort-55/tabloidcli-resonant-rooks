using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using Microsoft.Data.SqlClient;

namespace TabloidCLI.Repositories
{
    public class NoteRepository : DatabaseConnector, IRepository<Note>
    {
        public NoteRepository(string connectionString) : base(connectionString) { }
        public List<Note> GetAll()
        {
            List<Note> notes = new List<Note>();
            return notes;
        }
        public List<Note> GetAll(int postId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT n.Title, n.Content, n.CreateDateTime
                                        FROM Note n
                                        JOIN Post p ON p.Id = n.PostId
                                        WHERE n.PostId = postId";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Note> notes = new List<Note>();

                        while (reader.Read())
                        {
                            Note note = new Note()
                            {
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Content = reader.GetString(reader.GetOrdinal("Content")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            };
                            notes.Add(note);
                        }
                        return notes;
                    }
                }
            }
        }
        public Note Get(int id)
        {
            Note noteId = new Note();
            return noteId;
        }
        public void Insert(Note entry)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT into Note (Title, Content, CreateDateTime, PostId)
                                        OUTPUT INSERTED.Id
                                        VALUES (@Title, @Content, @CreateDateTime, @PostId)";
                    cmd.Parameters.AddWithValue("@Title", entry.Title);
                    cmd.Parameters.AddWithValue("@Content", entry.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", entry.CreateDateTime);
                    cmd.Parameters.AddWithValue("@PostId", entry.PostId);
                    int id = (int)cmd.ExecuteScalar();

                    entry.Id=id;
                }
            }
        }
        public void Update(Note entry)
        {

        }
        public void Delete(int id)
        {
  
        }
    }
}
