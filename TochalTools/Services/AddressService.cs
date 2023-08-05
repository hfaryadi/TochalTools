using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TochalTools.Classes;
using TochalTools.Models;

namespace TochalTools.Services
{
    public interface IAddress
    {
        Task<IEnumerable<CountryListViewModel>> ReadAllCountries();
        Task CreateCountry(CountryCreateViewModel viewModel);
        Task<CountryEditViewModel> ReadCountryByIdForEdit(int countryId);
        Task EditCountry(CountryEditViewModel viewModel);
        Task<bool> DeleteCountry(int countryId);
        Task<StateListPageViewModel> ReadAllStatesByCountryId(int countryId);
        Task CreateState(StateCreateViewModel viewModel);
        Task<StateEditViewModel> ReadStateByIdForEdit(int stateId);
        Task EditState(StateEditViewModel viewModel);
        Task<bool> DeleteState(int stateId);
        Task<CityListPageViewModel> ReadAllCitiesByStateId(int stateId);
        Task CreateCity(CityCreateViewModel viewModel);
        Task<CityEditViewModel> ReadCityByIdForEdit(int cityId);
        Task EditCity(CityEditViewModel viewModel);
        Task<bool> DeleteCity(int cityId);
        Task<List<SelectListItem>> ReadAllCountriesForSelect();
        Task<List<SelectListItem>> ReadAllStatesByCountryIdForSelect(int countryId);
        Task<List<SelectListItem>> ReadAllCitiesByStateIdForSelect(int stateId);
    }
    public class AddressService : IAddress
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public async Task<IEnumerable<CountryListViewModel>> ReadAllCountries()
        {
            return await db.Countries.Where(x => !x.IsDeleted).OrderBy(x => x.Name).Select(x => new CountryListViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task CreateCountry(CountryCreateViewModel viewModel)
        {
            var country = await db.Countries.Where(x => !x.IsDeleted && x.Name == viewModel.Name).FirstOrDefaultAsync();
            if (country == null)
            {
                CountryModel model = new CountryModel
                {
                    IsDeleted = false,
                    Name = viewModel.Name
                };
                db.Countries.Add(model);
                await db.SaveChangesAsync();
            }
        }

        public async Task<CountryEditViewModel> ReadCountryByIdForEdit(int countryId)
        {
            var model = await db.Countries.FindAsync(countryId);
            return new CountryEditViewModel
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public async Task EditCountry(CountryEditViewModel viewModel)
        {
            var country = await db.Countries.Where(x => !x.IsDeleted && x.Id != viewModel.Id && x.Name == viewModel.Name).FirstOrDefaultAsync();
            if (country == null)
            {
                var model = await db.Countries.FindAsync(viewModel.Id);
                model.Name = viewModel.Name;
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteCountry(int countryId)
        {
            var model = await db.Countries.FindAsync(countryId);
            if (model == null)
            {
                return false;
            }
            model.IsDeleted = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<StateListPageViewModel> ReadAllStatesByCountryId(int countryId)
        {
            var states = await db.States.Where(x => !x.IsDeleted && x.CountryId == countryId).OrderBy(x => x.Name).Select(x => new StateListViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            var countryName = await db.Countries.Where(x => x.Id == countryId).Select(x => x.Name).FirstOrDefaultAsync();
            return new StateListPageViewModel
            {
                CountryName = countryName,
                States = states
            };
        }

        public async Task CreateState(StateCreateViewModel viewModel)
        {
            var state = await db.States.Where(x => !x.IsDeleted && x.Name == viewModel.Name && x.CountryId == viewModel.CountryId).FirstOrDefaultAsync();
            if (state == null)
            {
                StateModel model = new StateModel
                {
                    CountryId = viewModel.CountryId,
                    IsDeleted = false,
                    Name = viewModel.Name
                };
                db.States.Add(model);
                await db.SaveChangesAsync();
            }
        }

        public async Task<StateEditViewModel> ReadStateByIdForEdit(int stateId)
        {
            var model = await db.States.FindAsync(stateId);
            return new StateEditViewModel
            {
                Id = model.Id,
                CountryId = model.CountryId,
                Name = model.Name
            };
        }

        public async Task EditState(StateEditViewModel viewModel)
        {
            var state = await db.States.Where(x => !x.IsDeleted && x.Id != viewModel.Id && x.Name == viewModel.Name && x.CountryId == viewModel.CountryId).FirstOrDefaultAsync();
            if (state == null)
            {
                var model = await db.States.FindAsync(viewModel.Id);
                model.Name = viewModel.Name;
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteState(int stateId)
        {
            var model = await db.States.FindAsync(stateId);
            if (model == null)
            {
                return false;
            }
            model.IsDeleted = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<CityListPageViewModel> ReadAllCitiesByStateId(int stateId)
        {
            var cities = await db.Cities.Where(x => !x.IsDeleted && x.StateId == stateId).OrderBy(x => x.Name).Select(x => new CityListViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            var stateName = await db.States.Where(x => x.Id == stateId).Select(x => x.Name).FirstOrDefaultAsync();
            return new CityListPageViewModel
            {
                Cities = cities,
                StateName = stateName
            };
        }

        public async Task CreateCity(CityCreateViewModel viewModel)
        {
            var city = await db.Cities.Where(x => !x.IsDeleted && x.Name == viewModel.Name && x.StateId == viewModel.StateId).FirstOrDefaultAsync();
            if (city == null)
            {
                CityModel model = new CityModel
                {
                    IsDeleted = false,
                    Name = viewModel.Name,
                    StateId = viewModel.StateId
                };
                db.Cities.Add(model);
                await db.SaveChangesAsync();
            }
        }

        public async Task<CityEditViewModel> ReadCityByIdForEdit(int cityId)
        {
            var model = await db.Cities.FindAsync(cityId);
            return new CityEditViewModel
            {
                Id = model.Id,
                StateId = model.StateId,
                Name = model.Name
            };
        }

        public async Task EditCity(CityEditViewModel viewModel)
        {
            var city = await db.Cities.Where(x => !x.IsDeleted && x.Id != viewModel.Id && x.Name == viewModel.Name && x.StateId == viewModel.StateId).FirstOrDefaultAsync();
            if (city == null)
            {
                var model = await db.Cities.FindAsync(viewModel.Id);
                model.Name = viewModel.Name;
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteCity(int cityId)
        {
            var model = await db.Cities.FindAsync(cityId);
            if (model == null)
            {
                return false;
            }
            model.IsDeleted = true;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<SelectListItem>> ReadAllCountriesForSelect()
        {
            return await db.Countries.Where(x => !x.IsDeleted).OrderBy(x => x.Name).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToListAsync();
        }

        public async Task<List<SelectListItem>> ReadAllStatesByCountryIdForSelect(int countryId)
        {
            return await db.States.Where(x => !x.IsDeleted && x.CountryId == countryId).OrderBy(x => x.Name).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToListAsync();
        }

        public async Task<List<SelectListItem>> ReadAllCitiesByStateIdForSelect(int stateId)
        {
            return await db.Cities.Where(x => !x.IsDeleted && x.StateId == stateId).OrderBy(x => x.Name).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToListAsync();
        }
    }
}