namespace TochalTools.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Blogs",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            ShortContent = c.String(),
            //            FullContent = c.String(),
            //            Categories = c.String(),
            //            Tags = c.String(),
            //            Language = c.String(),
            //            Review = c.Long(nullable: false),
            //            OptimizationTitle = c.String(),
            //            OptimizationKeywords = c.String(),
            //            OptimizationDescription = c.String(),
            //            UserId = c.String(),
            //            IsPublicationActive = c.Boolean(nullable: false),
            //            IsAtHomeActive = c.Boolean(nullable: false),
            //            IsCommentsActive = c.Boolean(nullable: false),
            //            IsDeleted = c.Boolean(nullable: false),
            //            Date = c.DateTime(nullable: false),
            //            Update = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Boxes",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Address = c.String(),
            //            Title = c.String(),
            //            Content = c.String(),
            //            Icon = c.String(),
            //            Link = c.String(),
            //            Language = c.String(),
            //            Priority = c.Int(nullable: false),
            //            IsDeleted = c.Boolean(nullable: false),
            //            Date = c.DateTime(nullable: false),
            //            Update = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Categories",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(),
            //            Parents = c.String(),
            //            Description = c.String(),
            //            Group = c.String(),
            //            Language = c.String(),
            //            IsDeleted = c.Boolean(nullable: false),
            //            Date = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Cities",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            StateId = c.Int(nullable: false),
            //            IsDeleted = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Comments",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            Content = c.String(),
            //            CommentId = c.Int(nullable: false),
            //            Group = c.String(),
            //            RefId = c.String(),
            //            UserId = c.String(),
            //            IsConfirmed = c.Boolean(nullable: false),
            //            IsDeleted = c.Boolean(nullable: false),
            //            Date = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Countries",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            IsDeleted = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Inbox",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Subject = c.String(),
            //            Content = c.String(),
            //            Name = c.String(),
            //            Email = c.String(),
            //            Tell = c.String(),
            //            Website = c.String(),
            //            ReceiverId = c.String(),
            //            UserId = c.String(),
            //            Group = c.String(),
            //            IsRead = c.Boolean(nullable: false),
            //            IsUserDeleted = c.Boolean(nullable: false),
            //            IsReceiverDeleted = c.Boolean(nullable: false),
            //            Date = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Infos",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            WebsiteTitle = c.String(),
            //            CompanyName = c.String(),
            //            WorkingHours = c.String(),
            //            Description = c.String(),
            //            FirstTell = c.String(),
            //            SecondTell = c.String(),
            //            FirstMobile = c.String(),
            //            SecondMobile = c.String(),
            //            Fax = c.String(),
            //            Email = c.String(),
            //            WebsiteUrl = c.String(),
            //            CountryId = c.Int(nullable: false),
            //            StateId = c.Int(nullable: false),
            //            CityId = c.Int(nullable: false),
            //            PostalCode = c.String(),
            //            Address = c.String(),
            //            Telegram = c.String(),
            //            Instagram = c.String(),
            //            Twitter = c.String(),
            //            Facebook = c.String(),
            //            GooglePlus = c.String(),
            //            Linkedin = c.String(),
            //            Language = c.String(),
            //            OptimizationTitle = c.String(),
            //            OptimizationKeywords = c.String(),
            //            OptimizationDescription = c.String(),
            //            IsDeleted = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Profiles",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(),
            //            Description = c.String(),
            //            Mobile = c.String(),
            //            Tell = c.String(),
            //            Fax = c.String(),
            //            Email = c.String(),
            //            Website = c.String(),
            //            CountryId = c.Int(nullable: false),
            //            StateId = c.Int(nullable: false),
            //            CityId = c.Int(nullable: false),
            //            PostalCode = c.String(),
            //            Address = c.String(),
            //            Telegram = c.String(),
            //            Instagram = c.String(),
            //            Twitter = c.String(),
            //            Facebook = c.String(),
            //            GooglePlus = c.String(),
            //            Linkedin = c.String(),
            //            Role = c.String(),
            //            Username = c.String(),
            //            Code = c.String(),
            //            IsCompleted = c.Boolean(nullable: false),
            //            IsConfirmed = c.Boolean(nullable: false),
            //            IsBlocked = c.Boolean(nullable: false),
            //            IsDeleted = c.Boolean(nullable: false),
            //            Date = c.DateTime(nullable: false),
            //            Update = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.AspNetRoles",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(nullable: false, maxLength: 256),
            //            Discriminator = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            //CreateTable(
            //    "dbo.AspNetUserRoles",
            //    c => new
            //        {
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            RoleId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => new { t.UserId, t.RoleId })
            //    .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.RoleId);
            
            //CreateTable(
            //    "dbo.States",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            CountryId = c.Int(nullable: false),
            //            IsDeleted = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Tags",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            Description = c.String(),
            //            Group = c.String(),
            //            Language = c.String(),
            //            Review = c.Long(nullable: false),
            //            OptimizationTitle = c.String(),
            //            OptimizationKeywords = c.String(),
            //            OptimizationDescription = c.String(),
            //            UserId = c.String(),
            //            IsDeleted = c.Boolean(nullable: false),
            //            Date = c.DateTime(nullable: false),
            //            Update = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.AspNetUsers",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Email = c.String(maxLength: 256),
            //            EmailConfirmed = c.Boolean(nullable: false),
            //            PasswordHash = c.String(),
            //            SecurityStamp = c.String(),
            //            PhoneNumber = c.String(),
            //            PhoneNumberConfirmed = c.Boolean(nullable: false),
            //            TwoFactorEnabled = c.Boolean(nullable: false),
            //            LockoutEndDateUtc = c.DateTime(),
            //            LockoutEnabled = c.Boolean(nullable: false),
            //            AccessFailedCount = c.Int(nullable: false),
            //            UserName = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            //CreateTable(
            //    "dbo.AspNetUserClaims",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            ClaimType = c.String(),
            //            ClaimValue = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.AspNetUserLogins",
            //    c => new
            //        {
            //            LoginProvider = c.String(nullable: false, maxLength: 128),
            //            ProviderKey = c.String(nullable: false, maxLength: 128),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            //DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            //DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            //DropIndex("dbo.AspNetUsers", "UserNameIndex");
            //DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            //DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            //DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            //DropTable("dbo.AspNetUserLogins");
            //DropTable("dbo.AspNetUserClaims");
            //DropTable("dbo.AspNetUsers");
            //DropTable("dbo.Tags");
            //DropTable("dbo.States");
            //DropTable("dbo.AspNetUserRoles");
            //DropTable("dbo.AspNetRoles");
            //DropTable("dbo.Profiles");
            //DropTable("dbo.Infos");
            //DropTable("dbo.Inbox");
            //DropTable("dbo.Countries");
            //DropTable("dbo.Comments");
            //DropTable("dbo.Cities");
            //DropTable("dbo.Categories");
            //DropTable("dbo.Boxes");
            //DropTable("dbo.Blogs");
        }
    }
}
