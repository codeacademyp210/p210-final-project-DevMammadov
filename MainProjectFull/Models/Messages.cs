using System;

namespace MainProjectFull.Models
{
    public class Messages
    {
        public int id { get; set; }
        public int SenderId { get; set; }
        public int GetterId { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
    }

}
