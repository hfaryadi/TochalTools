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
    public interface IMarketer
    {
        Task<List<MarketerListViewModel>> ReadAll(string type = null);
        Task<MarketerDetailsViewModel> ReadById(int id, bool isTrackingCode = false);
        Task<int> Create(MarketerCreateViewModel viewModel);
        Task<bool> Archive(int marketerId);
        Task<bool> Delete(int marketerId);

    }
    public class MarketerService : IMarketer
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<List<MarketerListViewModel>> ReadAll(string type = null)
        {
            var model = new List<MarketerModel> { };
            var Countries = await db.Countries.ToListAsync();
            var States = await db.States.ToListAsync();
            var Cities = await db.Cities.ToListAsync();
            if (type == null || type == "")
            {
                model = await db.Marketers.Where(x => !x.IsDeleted && !x.IsArchived).OrderByDescending(x => x.Id).ToListAsync();
            }
            else if (type.ToLower() == "archived")
            {
                model = await db.Marketers.Where(x => !x.IsDeleted && x.IsArchived).OrderByDescending(x => x.Id).ToListAsync();
            }
            return model.Select(x => new MarketerListViewModel
            {
                Id = x.Id,
                Age = x.Age,
                CityName = Cities.Where(z => z.Id == x.CityId).Select(z => z.Name).FirstOrDefault(),
                CountryName = Countries.Where(z => z.Id == x.CountryId).Select(z => z.Name).FirstOrDefault(),
                Date = DateTimeHelper.ConvertToShamsi(x.Date),
                IsArchived = x.IsArchived,
                Name = x.Name,
                StateName = States.Where(z => z.Id == x.StateId).Select(z => z.Name).FirstOrDefault(),
                Tell = x.Tell,
                TrackingCode = x.TrackingCode
            }).ToList();
        }

        public async Task<MarketerDetailsViewModel> ReadById(int id, bool isTrackingCode = false)
        {
            MarketerModel model = null;
            if (isTrackingCode)
            {
                model = await db.Marketers.Where(x => !x.IsDeleted && x.TrackingCode == id).FirstOrDefaultAsync();
            }
            else
            {
                model = await db.Marketers.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            }
            if (model == null)
            {
                return null;
            }
            return new MarketerDetailsViewModel
            {
                Id = model.Id,
                Address = model.Address,
                Age = model.Age,
                CityName = await db.Cities.Where(x => x.Id == model.CityId).Select(x => x.Name).FirstOrDefaultAsync(),
                CountryName = await db.Countries.Where(x => x.Id == model.CountryId).Select(x => x.Name).FirstOrDefaultAsync(),
                Date = DateTimeHelper.ConvertToShamsi(model.Date),
                Description = model.Description,
                Name = model.Name,
                StateName = await db.States.Where(x => x.Id == model.StateId).Select(x => x.Name).FirstOrDefaultAsync(),
                Tell = model.Tell,
                TrackingCode = model.TrackingCode
            };
        }

        public async Task<int> Create(MarketerCreateViewModel viewModel)
        {
            Random rnd = new Random();
            bool e = false;
            int TrackingCode = 0;
            while (!e)
            {
                TrackingCode = rnd.Next(100000, 999999);
                var Exist = await db.Marketers.Where(x => x.TrackingCode == TrackingCode).FirstOrDefaultAsync();
                if (Exist == null)
                {
                    e = true;
                }
            }
            MarketerModel model = new MarketerModel
            {
                Address = viewModel.Address,
                Age = viewModel.Age,
                CityId = viewModel.CityId,
                CountryId = viewModel.CountryId,
                Date = DateTime.Now,
                Description = viewModel.Description,
                IsArchived = false,
                IsDeleted = false,
                Name = viewModel.Name,
                StateId = viewModel.StateId,
                Tell = viewModel.Tell,
                TrackingCode = TrackingCode
            };
            db.Marketers.Add(model);
            await db.SaveChangesAsync();
            return model.TrackingCode;
        }


        public async Task<bool> Archive(int maketerId)
        {
            var model = await db.Marketers.FindAsync(maketerId);
            if (model == null)
            {
                return false;
            }
            model.IsArchived = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int maketerId)
        {
            var model = await db.Marketers.FindAsync(maketerId);
            if (model == null)
            {
                return false;
            }
            model.IsDeleted = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }
    }
}