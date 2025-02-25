
// id - int, case_id - int, message - text, timestamp - timestamp
namespace Server.Models
{
    public class Message
    {
        public int? id { get; set; }
        public int? case_id { get; set }
        public string? text { get; set; }
        public DateTime? timestamp { get; set; }

        public Message(
            int? id = null,
            int? case_id = null,
            string? text = "No message",
            DateTime? timestamp = null)
        {
            this.id = id;
            this.case_id = case_id;
            this.text = text;
            this.timestamp = timestamp;
        }
    }
}