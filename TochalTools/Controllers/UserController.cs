using TochalTools.Models;
using TochalTools.Services;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TochalTools.Classes;

namespace TochalTools.Controllers
{
    [Authorize(Roles = "Developer,SuperAdministrator")]
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        ProfileService profileService;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public async Task<ActionResult> List(string id = null)
        {
            var role = "Others";
            if (User.IsInRole("Developer"))
            {
                role = "Developer";
            }
            var Profiles = await db.Profiles.Where(x => (role == "Developer" || x.Role != "Developer")).ToListAsync();
            List<UserListViewModel> users = new List<UserListViewModel>();
            var UserIds = UserManager.Users.Select(x => x.Id).ToList();
            UserListPageViewModel model = new UserListPageViewModel
            {
                AllUsersCount = Profiles.Where(x => UserIds != null && UserIds.Contains(x.Id)).Count(),
                BlockedCount = Profiles.Where(x => UserIds != null && UserIds.Contains(x.Id) && x.IsBlocked).Count(),
                ConfirmedCount = Profiles.Where(x => UserIds != null && UserIds.Contains(x.Id) && x.IsConfirmed).Count(),
                UnConfirmedCount = Profiles.Where(x => UserIds != null && UserIds.Contains(x.Id) && !x.IsConfirmed).Count(),
            };
            if (id != null && id != "")
            {
                switch (id.ToLower())
                {
                    case "confirmed":
                        Profiles = await db.Profiles.Where(x => (role == "Developer" || x.Role != "Developer") && x.IsConfirmed).ToListAsync();
                        break;
                    case "unconfirmed":
                        Profiles = await db.Profiles.Where(x => (role == "Developer" || x.Role != "Developer") && !x.IsConfirmed).ToListAsync();
                        break;
                    case "blocked":
                        Profiles = await db.Profiles.Where(x => (role == "Developer" || x.Role != "Developer") && x.IsBlocked).ToListAsync();
                        break;
                }
                model.ActionId = id.ToLower();
            }
            foreach (var item in UserManager.Users)
            {
                var profile = Profiles.Where(x => x.Id == item.Id).FirstOrDefault();
                if (profile != null)
                {
                    UserListViewModel user = new UserListViewModel
                    {
                        Id = profile.Id,
                        IsBlocked = profile.IsBlocked,
                        IsCompleted = (profile.IsCompleted) ? "checked" : "",
                        IsConfirmed = profile.IsConfirmed,
                        Mobile = profile.Mobile,
                        Name = profile.Name,
                        Role = RoleHelper.GetPersianRoleName(profile.Role),
                    };
                    users.Add(user);
                }
            }
            model.Users = users;
            return View(model);
        }
        public async Task<ActionResult> Create()
        {
            var role = "Others";
            if (User.IsInRole("Developer"))
            {
                role = "Developer";
            }
            var mainRoles = await RoleManager.Roles.Where(x => (role == "Developer" && (x.Name == "Developer" || x.Name == "SuperAdministrator" || x.Name == "Administrator" || x.Name == "User")) || (x.Name == "Administrator" || x.Name == "User")).ToListAsync();
            ViewBag.MainRoles = mainRoles.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = RoleHelper.GetPersianRoleName(x.Name)
            }).ToList();

            var subRoles = await RoleManager.Roles.Where(x => x.Name != "Developer" && x.Name != "SuperAdministrator" && x.Name != "Administrator" && x.Name != "User").ToListAsync();
            ViewBag.SubRoles = subRoles.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = RoleHelper.GetPersianRoleName(x.Name)
            }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Mobile,Password,ConfirmPassword,MainRole,SubRoles")] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { PhoneNumber = model.Mobile, UserName = model.Mobile };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, model.MainRole);
                    if (model.SubRoles != null)
                    {
                        await UserManager.AddToRolesAsync(user.Id, model.SubRoles);
                    }
                    profileService = new ProfileService();
                    await profileService.CreateUser(user.Id, model.MainRole, user.PhoneNumber, user.UserName, true);
                }
            }
            return RedirectToAction("List");
        }

        public async Task<ActionResult> Edit(string id)
        {
            var role = "Others";
            if (User.IsInRole("Developer"))
            {
                role = "Developer";
            }
            var user = await UserManager.FindByIdAsync(id);
            var userRoles = await UserManager.GetRolesAsync(id);
            var userMainRole = userRoles.Where(x => x == "Developer" || x == "SuperAdministrator" || x == "Administrator" || x == "User").FirstOrDefault();
            var userSubRoles = userRoles.Where(x => x != "Developer" && x != "SuperAdministrator" && x != "Administrator" && x != "User").ToArray();
            var mainRoles = await RoleManager.Roles.Where(x => (role == "Developer" && (x.Name == "Developer" || x.Name == "SuperAdministrator" || x.Name == "Administrator" || x.Name == "User")) || (x.Name == "Administrator" || x.Name == "User")).ToListAsync();
            ViewBag.MainRoles = mainRoles.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = RoleHelper.GetPersianRoleName(x.Name)
            }).ToList();

            var subRoles = await RoleManager.Roles.Where(x => x.Name != "Developer" && x.Name != "SuperAdministrator" && x.Name != "Administrator" && x.Name != "User").ToListAsync();
            ViewBag.SubRoles = subRoles.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = RoleHelper.GetPersianRoleName(x.Name)
            }).ToList();
            var model = new RegisterEditViewModel
            {
                Id = user.Id,
                MainRole = userMainRole,
                Mobile = user.PhoneNumber,
                SubRoles = userSubRoles
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Mobile,MainRole,SubRoles")] RegisterEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                profileService = new ProfileService();
                var user = await UserManager.FindByIdAsync(model.Id);
                var userRoles = await UserManager.GetRolesAsync(model.Id);
                var userMainRole = userRoles.Where(x => x == "Developer" || x == "SuperAdministrator" || x == "Administrator" || x == "User").FirstOrDefault();
                var userSubRoles = userRoles.Where(x => x != "Developer" && x != "SuperAdministrator" && x != "Administrator" && x != "User").ToArray();
                var profileModel = await db.Profiles.FindAsync(model.Id);
                user.UserName = model.Mobile;
                user.PhoneNumber = model.Mobile;
                var updateResult = await UserManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    profileModel.Mobile = model.Mobile;
                    profileModel.Username = model.Mobile;
                    profileModel.Update = DateTime.Now;
                    db.Entry(profileModel).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                var roleUpdateResult = await UserManager.AddToRoleAsync(model.Id, model.MainRole);
                if (roleUpdateResult.Succeeded)
                {
                    profileModel.Role = model.MainRole;
                    profileModel.Update = DateTime.Now;
                    db.Entry(profileModel).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    await UserManager.RemoveFromRoleAsync(model.Id, userMainRole);
                }
                if (userSubRoles != null)
                {
                    await UserManager.RemoveFromRolesAsync(model.Id, userSubRoles);
                }
                if (model.SubRoles != null)
                {
                    await UserManager.AddToRolesAsync(model.Id, model.SubRoles);
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                profileService = new ProfileService();
                await profileService.Delete(id);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Confirm(string id)
        {
            profileService = new ProfileService();
            return Json(await profileService.Confirm(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Block(string id)
        {
            profileService = new ProfileService();
            return Json(await profileService.Block(id), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> EditProfile(string id)
        {
            profileService = new ProfileService();
            return View(await profileService.ReadByIdForEdit(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile([Bind(Include = "Id,Name,Tell,Mobile,Fax,CountryId,StateId,CityId,PostalCode,Address,Email,Website,Description,Telegram,Instagram,Twitter,Facebook,GooglePlus,Linkedin")] ProfileEditViewModel viewModel, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                profileService = new ProfileService();
                await profileService.Edit(viewModel);
                StorageHelper.SaveFile(File, "Images", "Profile", viewModel.Id, ".jpg");
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<JsonResult> SendInfo(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                var removeResult = await UserManager.RemovePasswordAsync(id);
                if (removeResult.Succeeded)
                {
                    Random rnd = new Random();
                    var password = rnd.Next(100000, 999999);
                    var addResult = await UserManager.AddPasswordAsync(id, password.ToString());
                    if (addResult.Succeeded)
                    {
                        SmsHelper sms = new SmsHelper();
                        var mobile = new[] { user.UserName };
                        var content = "کاربر گرامی" + System.Environment.NewLine +
                        "مشخصات ورود شما به شرح ذیل می باشد:" + System.Environment.NewLine +
                        "نام کاربری: " + user.UserName + System.Environment.NewLine +
                        "کلمه عبور: " + password + System.Environment.NewLine +
                        "توچال تولز";
                        sms.SendSMS(mobile, content);
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}