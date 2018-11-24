namespace Estates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "TypeId", "dbo.Types");
            DropIndex("dbo.Items", new[] { "TypeId" });
            CreateTable(
                "dbo.EstatesTypes",
                c => new
                    {
                        EstatesTypeId = c.String(nullable: false, maxLength: 40),
                        EstatesTypeName = c.String(nullable: false, maxLength: 25),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EstatesTypeId);
            
            AddColumn("dbo.Items", "EstatesTypeId", c => c.String(maxLength: 40));
            CreateIndex("dbo.Items", "EstatesTypeId");
            AddForeignKey("dbo.Items", "EstatesTypeId", "dbo.EstatesTypes", "EstatesTypeId");
            DropColumn("dbo.Items", "TypeId");
            DropTable("dbo.Types");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeId = c.String(nullable: false, maxLength: 40),
                        TypeName = c.String(nullable: false, maxLength: 25),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TypeId);
            
            AddColumn("dbo.Items", "TypeId", c => c.String(maxLength: 40));
            DropForeignKey("dbo.Items", "EstatesTypeId", "dbo.EstatesTypes");
            DropIndex("dbo.Items", new[] { "EstatesTypeId" });
            DropColumn("dbo.Items", "EstatesTypeId");
            DropTable("dbo.EstatesTypes");
            CreateIndex("dbo.Items", "TypeId");
            AddForeignKey("dbo.Items", "TypeId", "dbo.Types", "TypeId");
        }
    }
}
