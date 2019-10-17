using System.Data.Entity.Migrations;
using System;

namespace MindfireSolutions.Migrations
{
    
    
    public partial class DescriptionColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BlogDen.Users", "Description", c => c.String(maxLength: 160));
            AlterColumn("BlogDen.Users", "Rank", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("BlogDen.Users", "Rank", c => c.String());
            AlterColumn("BlogDen.Users", "Description", c => c.String(maxLength: 80));
        }
    }
}
