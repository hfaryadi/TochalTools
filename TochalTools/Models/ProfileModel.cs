using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TochalTools.Models
{
    [Table("Profiles")]
    public class ProfileModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Mobile { get; set; }
        public string Tell { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string Telegram { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string GooglePlus { get; set; }
        public string Linkedin { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Code { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
        public DateTime Update { get; set; }
    }
}