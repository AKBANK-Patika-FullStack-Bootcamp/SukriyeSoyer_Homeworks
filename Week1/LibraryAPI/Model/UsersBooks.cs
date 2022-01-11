namespace LibraryAPI.Model
{
    public class UsersBooks
    {
        public int Id { get; set; }

       // public int UserId { get; set; }

        public string Name { get; set; }

        public string? Author { get; set; }

        public string? Category { get; set; }

        public String? ReleaseDate { get; set; }
    }
}
