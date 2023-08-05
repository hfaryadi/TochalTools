using TochalTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using TochalTools.Classes;

namespace TochalTools.Services
{
    public interface ISiteMapService
    {
        Sitemap Index();
        Task<IEnumerable<SiteMapListViewModel>> ReadAll();
        Task Create(SiteMapCreateViewModel model);
        Task<SiteMapEditViewModel> ReadByIdForEdit(int siteMapId);
        Task Edit(SiteMapEditViewModel viewModel);
        Task<bool> Delete(int siteMapId);
    }
    public class SiteMapService : ISiteMapService     
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public Sitemap Index()
        {
            Sitemap sm = new Sitemap();
            var SiteMaps = db.SiteMap.Where(x => !x.IsDeleted).ToList();
            var Categories = db.Categories.Where(x => !x.IsDeleted && x.IsPublicationActive).ToList();
            var SiteMapDynamic = SiteMaps.Select(x => new SiteMapIndexViewModel
            {
                Id = x.Id,
                ChangeFrequency = SiteMapHelper.ReturnFrequency(x.ChangeFrequency),
                LastModified = x.LastModified.ToUniversalTime(),
                Priority = x.Priority,
                Title = x.Title,
                Url = x.Url
            }).ToList();

            foreach (var item in SiteMapDynamic)
            {
                sm.Add(new Location()
                {
                    Url = item.Url,
                    ChangeFrequency = item.ChangeFrequency,
                    Priority = Convert.ToDouble(item.Priority),
                    //LastModified = item.LastModified
                });
            }

            //Blogs
            var Blogs = db.Blogs.Where(x => !x.IsDeleted && x.IsPublicationActive == true).Select(x => new SiteMapListViewModel { Id = x.Id, Title = x.Title, LastModified = x.Update }).ToList();
            foreach (var item in Blogs)
            {
                var Title = item.Title.Replace(" ", "-");
                var Date = item.LastModified.ToUniversalTime();
                sm.Add(new Location()
                {
                    Url = string.Format("http://TochalTools.com/Blog/Details/{0}/{1}", item.Id, Title),
                    ChangeFrequency = Location.eChangeFrequency.daily,
                    LastModified = Date,
                    Priority = 0.8
                });
            }

            //Tags
            var Tags = db.Tags.Where(x => !x.IsDeleted).Select(x => new SiteMapListViewModel { Id = x.Id, Title = x.Title, LastModified = x.Update }).ToList();
            foreach (var item in Tags)
            {
                var Title = item.Title.Replace(" ", "-");
                var Date = item.LastModified.ToUniversalTime();
                sm.Add(new Location()
                {
                    Url = string.Format("http://TochalTools.com/Tag/Details/{0}/{1}", item.Id, item.Title),
                    ChangeFrequency = Location.eChangeFrequency.daily,
                    LastModified = Date,
                    Priority = 0.8
                });
            }

            //Products
            var Products = db.Products.Where(x => !x.IsDeleted && x.IsPublicationActive == true).Select(x => new SiteMapListViewModel { Id = x.Id, Title = x.Title, LastModified = x.Update }).ToList();
            foreach (var item in Products)
            {
                var Title = item.Title.Replace(" ", "-");
                var Date = item.LastModified.ToUniversalTime();
                sm.Add(new Location()
                {
                    Url = string.Format("http://TochalTools.com/Product/Details/{0}/{1}", item.Id, Title),
                    ChangeFrequency = Location.eChangeFrequency.daily,
                    LastModified = Date,
                    Priority = 0.8
                });
            }

            var BlogCategories = Categories.Where(x => x.Group == "Blog").Select(x => new SiteMapListViewModel { Title = x.Name, LastModified = x.Date }).ToList();

            foreach (var item in BlogCategories)
            {
                var Title = item.Title.Replace(" ", "-");
                var Date = item.LastModified.ToUniversalTime();
                sm.Add(new Location()
                {
                    Url = string.Format("http://TochalTools.com/Blog/{0}", Title),
                    ChangeFrequency = Location.eChangeFrequency.weekly,
                    LastModified = Date,
                    Priority = 0.9
                });
            }

            var ProductCategories = Categories.Where(x => x.Group == "Product" && x.Parents == null || x.Parents == "").Select(x => new CategorySiteMapViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Date = x.Date,
                Childs = Categories.Where(z => z.Group == "Product" && z.Parents != null && z.Parents.Contains(x.Id)).Select(z => new CategorySiteMapViewModel
                {
                    Id = z.Id,
                    Name = z.Name,
                    Date = z.Date
                }).ToList()
            }).ToList();

            foreach (var item in ProductCategories)
            {
                var Title = item.Name.Replace(" ", "-");
                var Date = item.Date.ToUniversalTime();
                if (item.Childs.Count() > 0)
                {
                    foreach (var jtem in item.Childs)
                    {
                        var childTitle = jtem.Name.Replace(" ", "-");
                        var childDate = jtem.Date.ToUniversalTime();
                        sm.Add(new Location()
                        {
                            Url = string.Format("http://TochalTools.com/Product/{0}/{1}", Title, childTitle),
                            ChangeFrequency = Location.eChangeFrequency.weekly,
                            LastModified = childDate,
                            Priority = 0.9
                        });
                    }
                }
                else
                {
                    sm.Add(new Location()
                    {
                        Url = string.Format("http://TochalTools.com/Product/{0}", Title),
                        ChangeFrequency = Location.eChangeFrequency.weekly,
                        LastModified = Date,
                        Priority = 0.9
                    });
                }
            }

            //Pages
            var Pages = db.Pages.Where(x => !x.IsDeleted).Select(x => new SiteMapListViewModel { Title = x.Title, LastModified = x.Date }).ToList();
            foreach (var item in Pages)
            {
                var Title = item.Title.Replace(" ", "-");
                var Date = item.LastModified.ToUniversalTime();
                sm.Add(new Location()
                {
                    Url = string.Format("http://TochalTools.com/Page/Details/{0}", Title),
                    ChangeFrequency = Location.eChangeFrequency.weekly,
                    LastModified = Date,
                    Priority = 0.7
                });
            }
            return sm;

        }
        public async Task<IEnumerable<SiteMapListViewModel>> ReadAll()
        {
            var SiteMaps = await db.SiteMap.Where(x => !x.IsDeleted).ToListAsync();
            return SiteMaps.Select(x => new SiteMapListViewModel
            {
                Id = x.Id,
                ChangeFrequency = SiteMapHelper.ReturnPersian(x.ChangeFrequency),
                LastModified = x.LastModified,
                Priority = x.Priority,
                Title = x.Title,
                Url = x.Url
            }).ToList(); 
        }

        public async Task Create(SiteMapCreateViewModel model)
        {
            var SiteMap = new SiteMapModel()
            {
                ChangeFrequency = model.ChangeFrequency,
                LastModified = DateTime.Now,
                Priority = model.Priority,
                Title = model.Title,
                Url = model.Url
            };
            db.SiteMap.Add(SiteMap);
            await db.SaveChangesAsync();
        }

        public async Task<SiteMapEditViewModel> ReadByIdForEdit(int siteMapId)
        {
            var model = await db.SiteMap.FindAsync(siteMapId);
            if (model == null)
            {
                return null;
            }
            return new SiteMapEditViewModel
            {
                Id = model.Id,
                ChangeFrequency = model.ChangeFrequency,
                Priority = model.Priority,
                Title = model.Title,
                Url = model.Url
            };
        }

        public async Task Edit(SiteMapEditViewModel viewModel)
        {
            var model = await db.SiteMap.FindAsync(viewModel.Id);
            if (model != null)
            {
                model.ChangeFrequency = viewModel.ChangeFrequency;
                model.Priority = viewModel.Priority;
                model.Title = viewModel.Title;
                model.Url = viewModel.Url;
                model.LastModified = DateTime.Now;
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task<bool> Delete(int siteMapId)
        {
            var model = await db.SiteMap.FindAsync(siteMapId);
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