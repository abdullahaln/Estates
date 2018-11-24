namespace Estates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FAQs",
                c => new
                    {
                        FAQId = c.String(nullable: false, maxLength: 40),
                        Question = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 40),
                        Answer = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.FAQId);
            
            CreateTable(
                "dbo.ItemImages",
                c => new
                    {
                        ItemImageId = c.String(nullable: false, maxLength: 40),
                        Imagepath = c.String(nullable: false, maxLength: 64),
                        AddedDate = c.DateTime(nullable: false),
                        ItemId = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.ItemImageId)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.String(nullable: false, maxLength: 40),
                        Description = c.String(nullable: false, maxLength: 512),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemType = c.String(nullable: false, maxLength: 25),
                        MainImagePath = c.String(nullable: false, maxLength: 64),
                        IpAddress = c.String(nullable: false, maxLength: 40),
                        IsHidden = c.Boolean(nullable: false),
                        IsSold = c.Boolean(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        CustomerId = c.String(maxLength: 40),
                        TypeId = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.Types", t => t.TypeId)
                .Index(t => t.CustomerId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40),
                        FirstName = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        IpAddress = c.String(nullable: false, maxLength: 40),
                        Email = c.String(nullable: false, maxLength: 40),
                        IsBlocked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.String(nullable: false, maxLength: 40),
                        FromId = c.String(nullable: false, maxLength: 40),
                        ToId = c.String(nullable: false, maxLength: 40),
                        Description = c.String(nullable: false, maxLength: 512),
                        MessageDate = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        PersonId = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.People", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.String(nullable: false, maxLength: 40),
                        NickName = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 512),
                        Value = c.Double(nullable: false),
                        Titel = c.String(nullable: false, maxLength: 40),
                        IpAddress = c.String(nullable: false, maxLength: 40),
                        ReviewDate = c.DateTime(nullable: false),
                        PersonId = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.People", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.String(nullable: false, maxLength: 40),
                        TageName = c.String(nullable: false, maxLength: 40),
                        ItemId = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeId = c.String(nullable: false, maxLength: 40),
                        TypeName = c.String(nullable: false, maxLength: 25),
                        AddesDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TypeId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SliderImages",
                c => new
                    {
                        SliderImageId = c.String(nullable: false, maxLength: 40),
                        ImagePath = c.String(nullable: false, maxLength: 64),
                        URL = c.String(nullable: false, maxLength: 64),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SliderImageId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40),
                        Address = c.String(nullable: false, maxLength: 64),
                        Phone = c.String(nullable: false, maxLength: 30),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "Id", "dbo.People");
            DropForeignKey("dbo.Customer", "Id", "dbo.People");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Items", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Tags", "ItemId", "dbo.Items");
            DropForeignKey("dbo.ItemImages", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Reviews", "PersonId", "dbo.People");
            DropForeignKey("dbo.Messages", "PersonId", "dbo.People");
            DropForeignKey("dbo.Items", "CustomerId", "dbo.Customer");
            DropIndex("dbo.User", new[] { "Id" });
            DropIndex("dbo.Customer", new[] { "Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tags", new[] { "ItemId" });
            DropIndex("dbo.Reviews", new[] { "PersonId" });
            DropIndex("dbo.Messages", new[] { "PersonId" });
            DropIndex("dbo.Items", new[] { "TypeId" });
            DropIndex("dbo.Items", new[] { "CustomerId" });
            DropIndex("dbo.ItemImages", new[] { "ItemId" });
            DropTable("dbo.User");
            DropTable("dbo.Customer");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SliderImages");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Types");
            DropTable("dbo.Tags");
            DropTable("dbo.Reviews");
            DropTable("dbo.Messages");
            DropTable("dbo.People");
            DropTable("dbo.Items");
            DropTable("dbo.ItemImages");
            DropTable("dbo.FAQs");
        }
    }
}
