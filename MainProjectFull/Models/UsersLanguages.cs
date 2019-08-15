namespace MainProjectFull.Models
{
    public class UsersLanguages
    {
        public int id { get; set; }
        public int UsersId { get; set; }
        public string Level { get; set; }

        public int LanguagesId { get; set; }
        public Users User { get; set; }
        public Languages Language { get; set; }
    }
}
