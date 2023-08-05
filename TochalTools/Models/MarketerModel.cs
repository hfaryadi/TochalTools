using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Marketers")]
    public class MarketerModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tell { get; set; }
        public string Age { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int TrackingCode { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
    }
}