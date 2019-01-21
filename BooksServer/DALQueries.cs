using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksServer
{
    public class DALQueries
    {
        public enum QueryType
        {
            GetUser,
            GetAllBooks,
            GetBooksById,
            AddBookToUser,
            ReturnFromBorrow
        }

        public static Dictionary<QueryType, string> queriesDict = new Dictionary<QueryType, string>
        {
            { QueryType.GetUser , "SELECT * FROM users where idnumber = "},
            { QueryType.GetAllBooks , "SELECT * FROM Books"},
            { QueryType.GetBooksById, @" SELECT Books.Id, Books.Name , Books.Author , Books.Genre , Books.PublishYear
                                         FROM Map_Books_Users
                                         INNER JOIN Books on books.Id = Map_Books_Users.BookId
                                         WHERE IdNumber = " },
            { QueryType.AddBookToUser, "INSERT INTO Map_Books_Users " },
            { QueryType.ReturnFromBorrow, "DELETE FROM Map_Books_Users WHERE " }
        };
    }
}
