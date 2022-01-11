using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Model;


namespace LibraryAPI.Controllers
{
    public class AdminController : Controller
    {
        List<User> UserList = new List<User>();
        Result result = new Result();
       

        [Route("controller/AddUser")]
        [HttpPost]
        public List<User> AddUser()
        {
            UserList.Add(new User { Id = 1, Name = "Elif Uzun",Age = 12 });
            UserList.Add(new User { Id = 2, Name = "Ayşe Yıldız",Age = 24});
            UserList.Add(new User { Id = 3, Name = "Ahmet Yavuz", Age =32 });
            UserList.Add(new User { Id = 4, Name = "Ali Erken", Age = 65});
            UserList.Add(new User { Id = 5, Name = "Buse Kaya",  Age = 70});

            return UserList;

        }


        
        [HttpGet]
        public List<User> GetUserList()
        {
            UserList = AddUser().OrderBy(x => x.Id).ToList();
            return UserList;

        }



        [HttpGet("{UserId}")]
        public User GetUser(int UserId)
        {
            UserList = AddUser();

            User resultObj = new User();
            resultObj = UserList.FirstOrDefault(x => x.Id == UserId);

            return resultObj;
        }


        [Route("controller/PostUser")]
        [HttpPost]
        public Result PostUser(User User)
        {

            //liste doluyor
            UserList = AddUser();

            // yeni eklenen listede var mı kontrolu 

            bool UserCheck = UserList.Select(x => User.Id == x.Id || User.Name == x.Name).FirstOrDefault();
            if (!UserCheck)
            {
                // yoksa ekle 
                UserList.Add(User);
                result.Status = 1;
                result.Message = "New User is added!";
            }
            else
            {
                result.Status = 0;
                result.Message = "This User already in our Users!";
            }
            return result;

        }



        [HttpPut("{UserId}")]
        public Result Update(int UserId, User newUser)
        {
            UserList = AddUser();

            User? oldUser = UserList.Find(x => x.Id == UserId);

            if (oldUser != null)
            {
                UserList.Remove(oldUser);
                oldUser = newUser;
                UserList.Add(oldUser);


                result.Status = 1;
                result.Message = "Update successful";
                result.Users = UserList;
            }
            else
            {
                result.Status = 0;
                result.Message = "User not found";
            }

            return result;
        }



        [HttpDelete("{UserId}")]
        public Result Delete(int UserId)
        {
            try 
            {
                UserList = AddUser();

                User? oldUser = UserList.Find(x => x.Id == UserId);
                if (oldUser != null)
                {
                    UserList.Remove(oldUser);
                    result.Status = 1;
                    result.Message = "User deleted!";
                    result.Users = UserList;

                }
                else
                {
                    result.Status = 0;
                    result.Message = "User  not found !";
                    result.Users = UserList;

                }
            }
            catch (Exception ex)
            {


            }

            return result;
        }

    }
}

