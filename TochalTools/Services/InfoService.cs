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
    public interface IInfo
    {
        Task<List<InfoListViewModel>> ReadAll();
        Task<InfoModel> Create(InfoCreateViewModel viewModel);
        Task<InfoEditViewModel> ReadByIdForEdit(int infoId);
        Task Edit(InfoEditViewModel viewModel);
        Task<bool> Delete(int infoId);
    }
    public class InfoService : IInfo
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public async Task<List<InfoListViewModel>> ReadAll()
        {
            var model = await db.Infos.Where(x => !x.IsDeleted).ToListAsync();
            return model.Select(x => new InfoListViewModel
            {
                Id = x.Id,
                CompanyName = x.CompanyName,
                FirstMobile = x.FirstMobile,
                FirstTell = x.FirstTell,
                Language = LanguageHelper.ReadLanguageByValue(x.Language),
                WebsiteTitle = x.WebsiteTitle
            }).ToList();
        }

        public async Task<InfoModel> Create(InfoCreateViewModel viewModel)
        {
            InfoModel model = new InfoModel
            {
                Address = viewModel.Address,
                CityId = viewModel.CityId ?? 0,
                CompanyName = viewModel.CompanyName,
                CountryId = viewModel.CountryId ?? 0,
                Description = viewModel.Description,
                Email = viewModel.Email,
                Facebook = viewModel.Facebook,
                Fax = viewModel.Fax,
                FirstMobile = viewModel.FirstMobile,
                FirstTell = viewModel.FirstTell,
                GooglePlus = viewModel.GooglePlus,
                Instagram = viewModel.Instagram,
                IsDeleted = false,
                Language = viewModel.Language,
                Linkedin = viewModel.Linkedin,
                OptimizationDescription = viewModel.OptimizationDescription,
                OptimizationKeywords = viewModel.OptimizationKeywords,
                OptimizationTitle = viewModel.OptimizationTitle,
                PostalCode = viewModel.PostalCode,
                SecondMobile = viewModel.SecondMobile,
                SecondTell = viewModel.SecondTell,
                StateId = viewModel.StateId ?? 0,
                Telegram = viewModel.Telegram,
                Twitter = viewModel.Twitter,
                WebsiteTitle = viewModel.WebsiteTitle,
                WebsiteUrl = viewModel.WebsiteUrl,
                WorkingHours = viewModel.WorkingHours
            };
            db.Infos.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public async Task<InfoEditViewModel> ReadByIdForEdit(int infoId)
        {
            AddressService addressService = new AddressService();
            var model = await db.Infos.FindAsync(infoId);
            return new InfoEditViewModel
            {
                Id = model.Id,
                Address = model.Address,
                CityId = model.CityId,
                CompanyName = model.CompanyName,
                CountryId = model.CountryId,
                Description = model.Description,
                Email = model.Email,
                Facebook = model.Facebook,
                Fax = model.Fax,
                FirstMobile = model.FirstMobile,
                FirstTell = model.FirstTell,
                GooglePlus = model.GooglePlus,
                Instagram = model.Instagram,
                Language = model.Language,
                Linkedin = model.Linkedin,
                OptimizationDescription = model.OptimizationDescription,
                OptimizationKeywords = model.OptimizationKeywords,
                OptimizationTitle = model.OptimizationTitle,
                PostalCode = model.PostalCode,
                SecondMobile = model.SecondMobile,
                SecondTell = model.SecondTell,
                StateId = model.StateId,
                Telegram = model.Telegram,
                Twitter = model.Twitter,
                WebsiteTitle = model.WebsiteTitle,
                WebsiteUrl = model.WebsiteUrl,
                WorkingHours = model.WorkingHours,
                CountriesList = await addressService.ReadAllCountriesForSelect(),
                StatesList = await addressService.ReadAllStatesByCountryIdForSelect(model.CountryId),
                CitiesList = await addressService.ReadAllCitiesByStateIdForSelect(model.StateId)
            };
        }

        public async Task Edit(InfoEditViewModel viewModel)
        {
            var model = await db.Infos.FindAsync(viewModel.Id);
            model.Address = viewModel.Address;
            model.CityId = viewModel.CityId ?? 0;
            model.CompanyName = viewModel.CompanyName;
            model.CountryId = viewModel.CountryId ?? 0;
            model.Description = viewModel.Description;
            model.Email = viewModel.Email;
            model.Facebook = viewModel.Facebook;
            model.Fax = viewModel.Fax;
            model.FirstMobile = viewModel.FirstMobile;
            model.FirstTell = viewModel.FirstTell;
            model.GooglePlus = viewModel.GooglePlus;
            model.Instagram = viewModel.Instagram;
            model.Language = viewModel.Language;
            model.Linkedin = viewModel.Linkedin;
            model.OptimizationDescription = viewModel.OptimizationDescription;
            model.OptimizationKeywords = viewModel.OptimizationKeywords;
            model.OptimizationTitle = viewModel.OptimizationTitle;
            model.PostalCode = viewModel.PostalCode;
            model.SecondMobile = viewModel.SecondMobile;
            model.SecondTell = viewModel.SecondTell;
            model.StateId = viewModel.StateId ?? 0;
            model.Telegram = viewModel.Telegram;
            model.Twitter = viewModel.Twitter;
            model.WebsiteTitle = viewModel.WebsiteTitle;
            model.WebsiteUrl = viewModel.WebsiteUrl;
            model.WorkingHours = viewModel.WorkingHours;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<bool> Delete(int infoId)
        {
            var model = await db.Infos.FindAsync(infoId);
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