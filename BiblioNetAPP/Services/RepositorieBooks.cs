using BiblioNetAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BiblioNetAPP.Services
{

    public interface IRepositorieBook
    {
        void Create(Book book);
    }

    public class RepositorieBook : IRepositorieBook
    {
        private readonly string _connectionString;

        public RepositorieBook(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Create(Book book)
        {
            var connection = new SqlConnection(_connectionString);
            var id = connection.QuerySingle<int>($@"INSERT INTO Book (BookName, Author, Price) VALUES (@BookName, @Author, @Price); SELECT SCOPE_IDENTITY();", book);
            book.Id = id;
        }
    }
}
