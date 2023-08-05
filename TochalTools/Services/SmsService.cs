using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TochalTools.Classes;
using TochalTools.Models;

namespace TochalTools.Services
{
    public interface ISms
    {
        Task<List<SmsListViewModel>> ReadAll(string type = null);
        Task Create(SmsCreateViewModel viewModel);
        Task<bool> Archive(int smsId);
        Task<bool> Delete(int smsId);
    }
    public class SmsService : ISms
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<List<SmsListViewModel>> ReadAll(string type = null)
        {
            var model = new List<SmsModel> { };
            if (type == null || type == "")
            {
                model = await db.Smses.Where(x => !x.IsDeletet && !x.IsArchived).OrderByDescending(x => x.Id).ToListAsync();
            }
            else if (type.ToLower() == "archived")
            {
                model = await db.Smses.Where(x => !x.IsDeletet && x.IsArchived).OrderByDescending(x => x.Id).ToListAsync();
            }
            return model.Select(x => new SmsListViewModel
            {
                Id = x.Id,
                Content = x.Content,
                Date = DateTimeHelper.ConvertToShamsi(x.Date),
                Groups = x.Groups,
                IsArchived = x.IsArchived
            }).ToList();
        }

        public async Task Create(SmsCreateViewModel viewModel)
        {
            var mobiles = new List<string>();
            var Groups = "";
            if (viewModel.IsNewsLetters)
            {
                NewsLetterService newsLetterService = new NewsLetterService();
                var newsLetterMobiles = await newsLetterService.ReadAllMobilesForSelect();
                if (newsLetterMobiles != null && newsLetterMobiles.Count() > 0)
                {
                    mobiles.AddRange(newsLetterMobiles);
                }
                Groups = "خبرنامه پیامکی,";
            }
            if (viewModel.IsUsers)
            {
                ProfileService profileService = new ProfileService();
                var userMobiles = await profileService.ReadAllMobilesByRoleForSelect("Administrator,User");
                if (userMobiles != null && userMobiles.Count() > 0)
                {
                    mobiles.AddRange(userMobiles);
                }
                Groups = Groups + "کاربران,";
            }
            if (viewModel.Mobiles != null && viewModel.Mobiles != "")
            {
                var customMobiles = viewModel.Mobiles.Split(',');
                mobiles.AddRange(customMobiles);
            }
            if (Groups != "")
            {
                Groups = Groups.Remove(Groups.Length - 1, 1);
            }
            if (mobiles.Count() > 0)
            {
                var Mobiles = mobiles.Distinct().ToArray();
                SmsHelper sms = new SmsHelper();
                sms.SendSMS(Mobiles, viewModel.Content);
            }
            SmsModel model = new SmsModel
            {
                Content = viewModel.Content,
                Date = DateTime.Now,
                Groups = Groups,
                IsArchived = false,
                IsDeletet = false
            };
            db.Smses.Add(model);
            await db.SaveChangesAsync();
        }

        public async Task<bool> Archive(int smsId)
        {
            var model = await db.Smses.FindAsync(smsId);
            if (model == null)
            {
                return false;
            }
            model.IsArchived = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int smsId)
        {
            var model = await db.Smses.FindAsync(smsId);
            if (model == null)
            {
                return false;
            }
            model.IsDeletet = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }
    }
}