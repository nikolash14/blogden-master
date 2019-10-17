namespace MindfireSolutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BlogDen.Admin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 25),
                        Password = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BlogDen.BlogComments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        CreatedBy = c.String(maxLength: 40),
                        CreationTime = c.DateTime(nullable: false),
                        EditedBy = c.String(maxLength: 40),
                        LastEditTime = c.DateTime(nullable: false),
                        ParentId = c.Int(),
                        BlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("BlogDen.Blogs", t => t.BlogId, cascadeDelete: true)
                .ForeignKey("BlogDen.BlogComments", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.BlogId);
            
            CreateTable(
                "BlogDen.Blogs",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(maxLength: 40),
                        CreationTime = c.DateTime(nullable: false),
                        EditedBy = c.String(maxLength: 40),
                        LastEditTime = c.DateTime(nullable: false),
                        BlogStatus = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("BlogDen.BlogTopics", t => t.TopicId, cascadeDelete: true)
                .ForeignKey("BlogDen.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TopicId);
            
            CreateTable(
                "BlogDen.BlogTopics",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        TopicName = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.TopicId)
                .ForeignKey("BlogDen.BlogTopics", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "BlogDen.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 60),
                        Mobile = c.String(maxLength: 15),
                        CreationTime = c.DateTime(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                        Gender = c.String(maxLength: 15),
                        Description = c.String(maxLength: 80),
                        Rank = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "BlogDen.BlogImages",
                c => new
                    {
                        BlogImageId = c.Int(nullable: false, identity: true),
                        ImgageUrl = c.String(),
                        BlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogImageId)
                .ForeignKey("BlogDen.Blogs", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId);
            
            CreateTable(
                "BlogDen.BlogStatusCounts",
                c => new
                    {
                        BlogCountId = c.Int(nullable: false, identity: true),
                        LikesCount = c.Int(nullable: false),
                        DislikesCount = c.Int(nullable: false),
                        SpamCount = c.Int(nullable: false),
                        CommentsCount = c.Int(nullable: false),
                        BlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogCountId)
                .ForeignKey("BlogDen.Blogs", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId);
            
            CreateTable(
                "BlogDen.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false),
                        Comment = c.String(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId);
            
            CreateTable(
                "BlogDen.Responses",
                c => new
                    {
                        ResponseId = c.Int(nullable: false, identity: true),
                        StatusId = c.Int(nullable: false),
                        BlogId = c.Int(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ResponseId)
                .ForeignKey("BlogDen.Blogs", t => t.BlogId)
                .ForeignKey("BlogDen.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("BlogDen.Users", t => t.UserId)
                .Index(t => t.StatusId)
                .Index(t => t.BlogId)
                .Index(t => t.UserId);
            
            CreateTable(
                "BlogDen.Status",
                c => new
                    {
                        StausId = c.Int(nullable: false, identity: true),
                        StatusName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.StausId);
            
            CreateTable(
                "BlogDen.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TagTitle = c.String(),
                        BlogId = c.Int(),
                        TopicId = c.Int(),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("BlogDen.Blogs", t => t.BlogId)
                .ForeignKey("BlogDen.BlogTopics", t => t.TopicId)
                .Index(t => t.BlogId)
                .Index(t => t.TopicId);
            
            CreateTable(
                "BlogDen.UserImages",
                c => new
                    {
                        UserImageId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 60),
                        ImageURL = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserImageId)
                .ForeignKey("BlogDen.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "BlogDen.UserSkills",
                c => new
                    {
                        UserSkillId = c.Int(nullable: false, identity: true),
                        Company = c.String(maxLength: 60),
                        skill = c.String(maxLength: 90),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserSkillId)
                .ForeignKey("BlogDen.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("BlogDen.UserSkills", "UserId", "BlogDen.Users");
            DropForeignKey("BlogDen.UserImages", "UserId", "BlogDen.Users");
            DropForeignKey("BlogDen.Tags", "TopicId", "BlogDen.BlogTopics");
            DropForeignKey("BlogDen.Tags", "BlogId", "BlogDen.Blogs");
            DropForeignKey("BlogDen.Responses", "UserId", "BlogDen.Users");
            DropForeignKey("BlogDen.Responses", "StatusId", "BlogDen.Status");
            DropForeignKey("BlogDen.Responses", "BlogId", "BlogDen.Blogs");
            DropForeignKey("BlogDen.BlogStatusCounts", "BlogId", "BlogDen.Blogs");
            DropForeignKey("BlogDen.BlogImages", "BlogId", "BlogDen.Blogs");
            DropForeignKey("BlogDen.BlogComments", "ParentId", "BlogDen.BlogComments");
            DropForeignKey("BlogDen.BlogComments", "BlogId", "BlogDen.Blogs");
            DropForeignKey("BlogDen.Blogs", "UserId", "BlogDen.Users");
            DropForeignKey("BlogDen.Blogs", "TopicId", "BlogDen.BlogTopics");
            DropForeignKey("BlogDen.BlogTopics", "ParentId", "BlogDen.BlogTopics");
            DropIndex("BlogDen.UserSkills", new[] { "UserId" });
            DropIndex("BlogDen.UserImages", new[] { "UserId" });
            DropIndex("BlogDen.Tags", new[] { "TopicId" });
            DropIndex("BlogDen.Tags", new[] { "BlogId" });
            DropIndex("BlogDen.Responses", new[] { "UserId" });
            DropIndex("BlogDen.Responses", new[] { "BlogId" });
            DropIndex("BlogDen.Responses", new[] { "StatusId" });
            DropIndex("BlogDen.BlogStatusCounts", new[] { "BlogId" });
            DropIndex("BlogDen.BlogImages", new[] { "BlogId" });
            DropIndex("BlogDen.BlogTopics", new[] { "ParentId" });
            DropIndex("BlogDen.Blogs", new[] { "TopicId" });
            DropIndex("BlogDen.Blogs", new[] { "UserId" });
            DropIndex("BlogDen.BlogComments", new[] { "BlogId" });
            DropIndex("BlogDen.BlogComments", new[] { "ParentId" });
            DropTable("BlogDen.UserSkills");
            DropTable("BlogDen.UserImages");
            DropTable("BlogDen.Tags");
            DropTable("BlogDen.Status");
            DropTable("BlogDen.Responses");
            DropTable("BlogDen.Messages");
            DropTable("BlogDen.BlogStatusCounts");
            DropTable("BlogDen.BlogImages");
            DropTable("BlogDen.Users");
            DropTable("BlogDen.BlogTopics");
            DropTable("BlogDen.Blogs");
            DropTable("BlogDen.BlogComments");
            DropTable("BlogDen.Admin");
        }
    }
}
