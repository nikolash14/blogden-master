namespace MindfireSolutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("BlogDen.BlogStatusCounts", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("BlogDen.BlogStatusCounts", "UserId");
        }
    }
}
