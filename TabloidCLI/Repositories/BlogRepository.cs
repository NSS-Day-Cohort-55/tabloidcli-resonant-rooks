using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.Repositories
{
    internal class BlogRepository
    {
        public BlogRepository(string connectionString) : base(connectionString) { }
    }
}
