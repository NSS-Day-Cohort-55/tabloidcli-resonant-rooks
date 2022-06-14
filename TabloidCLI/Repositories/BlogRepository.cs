using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.Repositories
{
    internal class BlogRepository : DatabaseConnector
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public void Insert(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Blog (Title, Url)
                                        OUTPUT INSERTED.Id
                                        VALUES (@Title, @Url)";
                    cmd.Parameters.AddWithValue("@Title", blog.Title);
                    cmd.Parameters.AddWithValue("@Content", blog.Url);
                    int id = (int)cmd.ExecuteScalar();
                    blog.Id = id;
                }
            }
        }
        public List<Blog> GetAll()
        {
            List<Blog> list = new List<Blog>();
            return list;
        }
        public Blog Get(int id)
        {
            Blog entry = new Blog();
            return entry;
        }
        public void Update(Blog blog)
        {

        }
        public void Delete(int id)
        {

        }


    }
}
