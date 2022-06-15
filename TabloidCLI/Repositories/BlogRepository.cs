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
                    cmd.Parameters.AddWithValue("@Url", blog.Url);
                    int id = (int)cmd.ExecuteScalar();
                    blog.Id = id;
                }
            }
        }
        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Title, Url FROM Blog";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Blog> blogs = new List<Blog>();

                        while (reader.Read())
                        {
                            Blog blog = new Blog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Url = reader.GetString(reader.GetOrdinal("Url")),
                            };

                            blogs.Add(blog);
                        }
                        return blogs;
                    }
                }
            }
        }

        public Blog Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Title, Url FROM Blog WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    Blog blog = null;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if(blog == null)
                        {
                            blog = new Blog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Url = reader.GetString(reader.GetOrdinal("Url")),
                            };
                        }
                    }
                    reader.Close();
                    return blog;
                }
            }
        }

        public void Update(Blog blog)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Blog SET Title = @title, Url = @url WHERE id = @id";
                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@url", blog.Url);
                    cmd.Parameters.AddWithValue("@id", blog.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Blog WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertTag(Blog blog, Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO BlogTag (BlogId, TagId) VALUES (@blogId, @tagId)";
                    cmd.Parameters.AddWithValue("@blogId", blog.Id);
                    cmd.Parameters.AddWithValue("@tagId", tag.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTag(Blog blog, Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM BlogTag WHERE BlogId = @blogId AND TagId = @tagId";
                    cmd.Parameters.AddWithValue("@blogId", blog.Id);
                    cmd.Parameters.AddWithValue("@tagId", tag.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
