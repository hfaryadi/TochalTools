using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TochalTools.Classes;

namespace TochalTools.Models
{
    public class UserListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
        public string IsCompleted { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsBlocked { get; set; }
    }
    public class UserListPageViewModel
    {
        public List<UserListViewModel> Users { get; set; }
        public int AllUsersCount { get; set; }
        public int ConfirmedCount { get; set; }
        public int UnConfirmedCount { get; set; }
        public int BlockedCount { get; set; }
        public string ActionId { get; set; }
    }
}