using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Orders")]
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Tell { get; set; }
        public string ReceiverName { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string SendType { get; set; }
        public int PrintId { get; set; }
        public string UserId { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool IsAdminArchived { get; set; }
        public bool IsUserArchived { get; set; }
        public bool IsAdminDeleted { get; set; }
        public bool IsUserDeleted { get; set; }
        public DateTime Date { get; set; }
    }
}