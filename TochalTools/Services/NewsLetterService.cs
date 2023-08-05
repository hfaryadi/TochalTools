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
    public interface INewsLetter
    {
        Task<List<NewsLetterListViewModel>> ReadAll();
        Task<string> Create(NewsLetterCreateViewModel viewModel);
        Task<bool> Delete(int newsLetterId);
        Task<string[]> ReadAllMobilesForSelect();
    }
    public class NewsLetterService : INewsLetter
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<List<NewsLetterListViewModel>> ReadAll()
        {
            var model = await db.NewsLetters.Where(x => !x.IsDeleted).OrderBy(x => x.Mobile).ToListAsync();
            return model.Select(x => new NewsLetterListViewModel
            {
                Id = x.Id,
                Date = DateTimeHelper.ConvertToShamsi(x.Date),
                Mobile = x.Mobile
            }).ToList();
        }

        public async Task<string> Create(NewsLetterCreateViewModel viewModel)
        {
            var newsLetter = await db.NewsLetters.Where(x => !x.IsDeleted && x.Mobile == viewModel.Mobile).FirstOrDefaultAsync();
            if (newsLetter != null)
            {
                return "شماره شما قبلا در سیستم ثبت شده است.";
            }
            NewsLetterModel model = new NewsLetterModel
            {
                Date = DateTime.Now,
                IsDeleted = false,
                Mobile = viewModel.Mobile
            };
            db.NewsLetters.Add(model);
            await db.SaveChangesAsync();
            return "شماره شما با موفقیت در سیستم ثبت شد.";
        }

        public async Task<bool> Delete(int newsLetterId)
        {
            var model = await db.NewsLetters.FindAsync(newsLetterId);
            if (model == null)
            {
                return false;
            }
            model.IsDeleted = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<string[]> ReadAllMobilesForSelect()
        {
            var mobiles = await db.NewsLetters.Where(x => !x.IsDeleted).Select(x => x.Mobile).ToArrayAsync();
            if (mobiles.Count() > 0)
            {
                return mobiles;
            }
            return null;
        }
    }
}