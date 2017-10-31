namespace model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreationDate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Body = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false, defaultValueSql:"GETUTCDATE()")
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Articles");
        }
    }
}
