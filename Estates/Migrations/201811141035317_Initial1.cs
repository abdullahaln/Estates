namespace Estates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FAQs", "Question", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.FAQs", "Answer", c => c.String(nullable: false, maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FAQs", "Answer", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.FAQs", "Question", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
