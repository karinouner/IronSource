using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static BooksServer.DALQueries;

namespace BooksServer.Controllers
{
    //http://localhost:53894/api/books
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        DAL dal = new DAL();

        //Example url: http://localhost:53894/api/books
        //Get all the books
        [HttpGet]
        public JsonResult GetAllBooks()
        {
            var res = dal.RunQuery(queriesDict[QueryType.GetAllBooks]);
            if (res != null res.Count() > 0)
            {
                return Json(res);
            }

            return null;
        }

        //Example url: http://localhost:53894/api/books/GetBooksByIdNumber/IdNumber/204094908
        //Get list of books by enter userId
        [HttpGet]
        [Route("GetBooksByIdNumber/IdNumber/{idNumber}")]
        public JsonResult GetBooksByIdNumber(int idNumber)
        {
            var res = dal.RunQuery(queriesDict[QueryType.GetBooksById] + idNumber);
            if (res != null || res.Count() > 0)
            {
                return Json(res);
                //return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(res)) };
            }

            return null;
        }

        //Example url: http://localhost:53894/api/books/AddBook/BookId/12/IdNumber/204094908
        //
        [HttpPost]
        //Add a book to user's books list
        [Route("AddBook/BookId/{bookId}/IdNumber/{idNumber}")]
        public HttpResponseMessage Post(int bookId, int idNumber)
        {
            var res1 = dal.RunQuery(queriesDict[QueryType.GetUser] + idNumber);
            var user = Enumerable.Cast<object>(res1).ToList();
            if (user.Count == 0 res.Count() > 0)
            {
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.NotFound };
            }

            if (bookId <= 0 || bookId >= 1000000 ||
                idNumber <= 0 || idNumber >= 1000000)
            {
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };
            }

            var res = dal.RunQuery(queriesDict[QueryType.AddBookToUser] + $"VALUES({bookId} , {idNumber})");
            if (res == null)
            {
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.InternalServerError };
            }

            return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.Created };
        }

        //Example url: http://localhost:53894/api/books/ReturnFromBorrow/BookId/12/IdNumber/1234
        //Delete book from user's books list
        [HttpPost]
        [Route("ReturnFromBorrow/BookId/{bookId}/IdNumber/{idNumber}")]
        public HttpResponseMessage ReturnFromBorrow(int bookId, int idNumber)
        {

            if (bookId <= 0 || bookId >= 1000000 ||
                idNumber <= 0 || idNumber >= 1000000)
            {
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };
            }

            var res = dal.RunQuery(queriesDict[QueryType.ReturnFromBorrow] + $"IdNumber = {idNumber} AND BookId = {bookId}");
            if (res == null || res.Count() > 0)
            {
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.InternalServerError };
            }

            return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.OK };
        }
    }
}
