using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Inbox")]
    public class InboxModel
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tell { get; set; }
        public string Website { get; set; }
        public string ReceiverId { get; set; }
        public string UserId { get; set; }
        public string Group { get; set; }
        public bool IsRead { get; set; }
        public bool IsUserDeleted { get; set; }
        public bool IsReceiverDeleted { get; set; }
        public DateTime Date { get; set; }
    }
}