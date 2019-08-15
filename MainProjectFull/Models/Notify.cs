using System;

namespace MainProjectFull.Models
{
    public class Notify
    {
        public int id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public int? PortfolioId { get; set; }
        public DateTime Date { get; set; }
        public int UsersId { get; set; }
        public string Photo { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public int? SenderProfileId { get; set; }
        public int? SenderId { get; set; }
        public Users User { get; set; }
    }

}
