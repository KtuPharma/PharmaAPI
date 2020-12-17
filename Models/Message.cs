using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.DTO;

namespace API.Models
{
    [Table("Message")]
    public class Message
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Topic { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Employee Author { get; set; }

        public Message() { }

        public Message(string topic, string text, Employee author)
        {
            Topic = topic;
            Text = text;
            Date = DateTime.Now;
            Author = author;
        }

        public Message(PostMessageDTO m, Employee author)
        {
            Topic = m.Topic;
            Text = m.Text;
            Date = DateTime.Now;
            Author = author;
        }
    }
}
