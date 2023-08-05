using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TochalTools.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<StateModel> States { get; set; }
        public DbSet<CityModel> Cities { get; set; }
        public DbSet<ProfileModel> Profiles { get; set; }
        public DbSet<InfoModel> Infos { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<BoxModel> Boxes { get; set; }
        public DbSet<PageModel> Pages { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<InboxModel> Inbox { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ProductSettingsModel> ProductSettings { get; set; }
        public DbSet<SliderModel> Sliders { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<MarketerModel> Marketers { get; set; }
        public DbSet<NewsLetterModel> NewsLetters { get; set; }
        public DbSet<SmsModel> Smses { get; set; }
        public DbSet<SiteMapModel> SiteMap { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}