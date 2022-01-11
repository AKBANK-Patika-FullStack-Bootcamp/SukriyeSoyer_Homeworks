namespace LibraryAPI.Model
{
    public class Result
    {
        public int? Status { get; set; }

        public string? Message { get; set; }

        public List<User>? Users { get; set; }

        public List<Book>? Books { get; set; }
        
        public List<UsersBooks>? UsersBooks { get; set; }
    }
}
