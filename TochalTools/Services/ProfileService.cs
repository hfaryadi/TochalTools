using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TochalTools.Models;

namespace TochalTools.Services
{
    public interface IProfile
    {
        Task CreateUser(string userId, string role, string mobile, string username, bool isConfirmed);
        Task<ProfileEditViewModel> ReadByIdForEdit(string userId);
        Task Edit(ProfileEditViewModel viewModel);
        Task<bool> Delete(string userId);
        Task<bool> Confirm(string userId);
        Task<bool> Block(string userId);
        Task<bool> ReadProfileIsBlock(string userId);
        Task<List<SelectListItem>> ReadAllByRoleForSelect(string role);
        Task<string[]> ReadAllMobilesByRoleForSelect(string role);
        Task<string> ReadMobileById(string id);
        Task<string> ReadUserIdByMobile(string mobile);
        Task<SelectListItem> CreateUserForAspAndProfile(string mobile, string name, string tell, int countryId, int stateId, int cityId, string postalCode, string address);
        Task<string> CreateCodeForUser(string id);
        Task<bool> ReadProfileCode(string id, string code);
    }
    public class ProfileService : IProfile
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public ProfileService()
        {
        }

        public ProfileService(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public async Task CreateUser(string userId, string role, string mobile, string username, bool isConfirmed)
        {
            var now = DateTime.Now;
            ProfileModel model = new ProfileModel
            {
                Id = userId,
                Date = now,
                IsBlocked = false,
                IsCompleted = false,
                IsConfirmed = isConfirmed,
                IsDeleted = false,
                Mobile = mobile,
                Role = role,
                Update = now,
                Username = username
            };
            db.Profiles.Add(model);
            await db.SaveChangesAsync();
        }

        public async Task<ProfileEditViewModel> ReadByIdForEdit(string userId)
        {
            AddressService addressService = new AddressService();
            var model = await db.Profiles.FindAsync(userId);
            return new ProfileEditViewModel
            {
                Id = model.Id,
                Address = model.Address,
                CityId = model.CityId,
                CountryId = model.CountryId,
                Description = model.Description,
                Email = model.Email,
                Facebook = model.Facebook,
                Fax = model.Fax,
                GooglePlus = model.GooglePlus,
                Instagram = model.Instagram,
                Linkedin = model.Linkedin,
                Mobile = model.Mobile,
                Name = model.Name,
                PostalCode = model.PostalCode,
                StateId = model.StateId,
                Telegram = model.Telegram,
                Tell = model.Tell,
                Twitter = model.Twitter,
                Website = model.Website,
                CountriesList = await addressService.ReadAllCountriesForSelect(),
                StatesList = await addressService.ReadAllStatesByCountryIdForSelect(model.CountryId),
                CitiesList = await addressService.ReadAllCitiesByStateIdForSelect(model.StateId)
            };
        }

        public async Task Edit(ProfileEditViewModel viewModel)
        {
            var model = await db.Profiles.FindAsync(viewModel.Id);
            var email = model.Email;
            var mobile = model.Mobile;
            model.Address = viewModel.Address;
            model.CityId = viewModel.CityId;
            model.CountryId = viewModel.CountryId;
            model.Description = viewModel.Description;
            model.Email = viewModel.Email;
            model.Facebook = viewModel.Facebook;
            model.Fax = viewModel.Fax;
            model.GooglePlus = viewModel.GooglePlus;
            model.Instagram = viewModel.Instagram;
            model.Linkedin = viewModel.Linkedin;
            model.Mobile = viewModel.Mobile;
            model.Name = viewModel.Name;
            model.PostalCode = viewModel.PostalCode;
            model.StateId = viewModel.StateId;
            model.Telegram = viewModel.Telegram;
            model.Tell = viewModel.Tell;
            model.Twitter = viewModel.Twitter;
            model.Website = viewModel.Website;
            model.IsCompleted = true;
            model.Update = DateTime.Now;
            var user = await UserManager.FindByIdAsync(viewModel.Id);
            if (mobile != viewModel.Mobile)
            {
                model.Username = viewModel.Mobile;
                user.UserName = viewModel.Mobile;
                user.PhoneNumber = viewModel.Mobile;
            }
            if (email != viewModel.Email)
            {
                user.Email = viewModel.Email;
            }
            await UserManager.UpdateAsync(user);
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<bool> Delete(string userId)
        {
            var model = await db.Profiles.FindAsync(userId);
            if (model == null)
            {
                return false;
            }
            model.IsDeleted = true;
            model.Update = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Confirm(string userId)
        {
            var model = await db.Profiles.FindAsync(userId);
            if (model == null)
            {
                return false;
            }
            model.IsConfirmed = true;
            model.Update = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Block(string userId)
        {
            var model = await db.Profiles.FindAsync(userId);
            if (model == null)
            {
                return false;
            }
            model.IsBlocked = !model.IsBlocked;
            model.Update = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReadProfileIsBlock(string userId)
        {
            var profile = await db.Profiles.FindAsync(userId);
            if (profile == null)
            {
                return true;
            }
            return profile.IsBlocked;
        }

        public async Task<List<SelectListItem>> ReadAllByRoleForSelect(string role)
        {
            var roles = role.Split(',');
            var Profiles = await db.Profiles.Where(x => !x.IsDeleted && !x.IsBlocked && x.IsConfirmed && x.IsCompleted).ToListAsync();
            var profiles = new List<ProfileModel>();
            foreach (var item in roles)
            {
                profiles.AddRange(Profiles.Where(x => x.Role == item).ToList());
            }
            profiles = profiles.Distinct().ToList();
            return profiles.Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Name
            }).ToList();
        }

        public async Task<string[]> ReadAllMobilesByRoleForSelect(string role)
        {
            var roles = role.Split(',');
            var Profiles = await db.Profiles.Where(x => !x.IsDeleted && !x.IsBlocked).ToListAsync();
            var profiles = new List<ProfileModel>();
            foreach (var item in roles)
            {
                profiles.AddRange(Profiles.Where(x => x.Role == item).ToList());
            }
            profiles = profiles.Distinct().ToList();
            if (profiles.Count() > 0)
            {
                return profiles.Select(x => x.Mobile).ToArray();
            }
            return null;
        }

        public async Task<string> ReadMobileById(string id)
        {
            var user = await db.Profiles.FindAsync(id);
            if (user != null)
            {
                return user.Mobile;
            }
            return "";
        }

        public async Task<string> ReadUserIdByMobile(string mobile)
        {
            var user = await UserManager.FindByNameAsync(mobile);
            if (user != null)
            {
                return user.Id;
            }
            return "";
        }

        public async Task<SelectListItem> CreateUserForAspAndProfile(string mobile, string name, string tell, int countryId, int stateId, int cityId, string postalCode, string address)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string password = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
            var user = new ApplicationUser() { PhoneNumber = mobile, UserName = mobile };
            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var now = DateTime.Now;
                await UserManager.AddToRoleAsync(user.Id, "User");
                ProfileModel model = new ProfileModel
                {
                    Id = user.Id,
                    Address = address,
                    CityId = cityId,
                    CountryId = countryId,
                    Date = now,
                    IsBlocked = false,
                    IsCompleted = true,
                    IsConfirmed = false,
                    IsDeleted = false,
                    Mobile = mobile,
                    Name = name,
                    PostalCode = postalCode,
                    Role = "User",
                    StateId = stateId,
                    Tell = tell,
                    Update = now,
                    Username = mobile
                };
                db.Profiles.Add(model);
                await db.SaveChangesAsync();
                return new SelectListItem
                {
                    Value = user.Id,
                    Text = password
                };
            }
            return null;
        }

        public async Task<string> CreateCodeForUser(string id)
        {
            var model = await db.Profiles.Where(x => !x.IsDeleted && !x.IsBlocked && x.Id == id).FirstOrDefaultAsync();
            if (model == null)
            {
                return "";
            }
            Random rnd = new Random();
            bool e = false;
            int code = 0;
            while (!e)
            {
                code = rnd.Next(100000, 999999);
                var Exist = await db.Profiles.Where(x => x.Code == code.ToString()).FirstOrDefaultAsync();
                if (Exist == null)
                {
                    e = true;
                }
            }
            model.Code = code.ToString();
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model.Code;
        }

        public async Task<bool> ReadProfileCode(string id, string code)
        {
            var model = await db.Profiles.Where(x => !x.IsDeleted && !x.IsBlocked && x.Id == id && x.Code == code).FirstOrDefaultAsync();
            if (model == null)
            {
                return false;
            }
            model.Code = null;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }
    }
}