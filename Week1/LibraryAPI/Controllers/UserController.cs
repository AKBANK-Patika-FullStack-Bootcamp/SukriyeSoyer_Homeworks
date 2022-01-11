using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Model;


namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        
        List<UsersBooks> UsersBooksList = new List<UsersBooks>();
        Result result = new Result();

        [Route("controller/AddBook")]
        [HttpPost]
        public List<UsersBooks> AddBook()
        {
           
            UsersBooksList.Add(new UsersBooks { Id =1, Name = "The Little Prince", Author = "Antoine de Saint", Category = "Childrens-Books", ReleaseDate = DateTime.Now.ToString("yyyyMMdd") });
            UsersBooksList.Add(new UsersBooks { Id =2, Name = "Superintelligence", Author = "Nick Bostrom", Category = "Computing", ReleaseDate = DateTime.Now.ToString("yyyyMMdd") });
            UsersBooksList.Add(new UsersBooks { Id =3, Name = "Gone Girl", Author = "Gillian Flynn", Category = "Crime-Thriller", ReleaseDate = DateTime.Now.ToString("yyyyMMdd")  });
            UsersBooksList.Add(new UsersBooks { Id =4, Name = "Harry Potter and the Philosophers Stone", Author = "J. K. Rowling", Category = "Teen-Young-Adult", ReleaseDate = DateTime.Now.ToString("yyyyMMdd") });
            UsersBooksList.Add(new UsersBooks { Id =5, Name = "Start With Why", Author = "Simon Sinek", Category = "History-Archaeology", ReleaseDate = DateTime.Now.ToString("yyyyMMdd") });

            return UsersBooksList;

        }

       

        [HttpGet]
        public List<UsersBooks> GetUsersBooksList()
        {
            UsersBooksList = AddBook().OrderBy(x => x.Id).ToList();
            return UsersBooksList;

        }

        
        [HttpGet("{BookId}")]
        public UsersBooks GetBook(int BookId)
        {
            UsersBooksList = AddBook();

            UsersBooks resultObj = new UsersBooks();
            resultObj = UsersBooksList.FirstOrDefault(x => x.Id == BookId);

            return resultObj;
        }


        [Route("controller/PostBook")]
        [HttpPost]
        public Result PostBook(UsersBooks Book)
        {

            //liste doluyor
            UsersBooksList = AddBook();

            // yeni eklenen listede var mý kontrolu 

            bool BookCheck = UsersBooksList.Select(x => Book.Id == x.Id || Book.Name == x.Name).FirstOrDefault();
            if (!BookCheck)
            {
                // yoksa ekle 
                UsersBooksList.Add(Book);
                result.Status = 1;
                result.Message = "New Book is added!";
            }
            else
            {
                result.Status = 0;
                result.Message = "This Book already in our list!";
            }
            return result;

        }



        [HttpPut("{BookId}")]
        public Result UpdateBook(int BookId, UsersBooks newBook)
        {
            UsersBooksList = AddBook();

            UsersBooks? oldBook = UsersBooksList.Find(x => x.Id == BookId);

            if (oldBook != null)
            {
                UsersBooksList.Remove(oldBook);
                oldBook = newBook;
                UsersBooksList.Add(oldBook);


                result.Status = 1;
                result.Message = "Update successful";
                result.UsersBooks = UsersBooksList;
            }
            else
            {
                result.Status = 0;
                result.Message = "Book not found";
            }

            return result;
        }



        [HttpDelete("{BookId}")]
        public Result DeleteBook(int BookId)
        {
            try
            {
                UsersBooksList = AddBook();

                UsersBooks? oldBook = UsersBooksList.Find(x => x.Id == BookId);
                if (oldBook != null)
                {
                    UsersBooksList.Remove(oldBook);
                    result.Status = 1;
                    result.Message = "Book deleted!";
                    result.UsersBooks = UsersBooksList;

                }
                else
                {
                    result.Status = 0;
                    result.Message = "Book  not found !";
                    result.UsersBooks = UsersBooksList;

                }
            }
            catch (Exception ex)
            {


            }

            return result;
        }
    }
}