namespace MindfireSolutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_Table_Changed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BlogDen.Users", "Rank", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("BlogDen.Users", "Rank", c => c.String(maxLength: 10));
        }
    }
}
