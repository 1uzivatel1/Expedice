namespace PocetKsPaleta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adress20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        City = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ContaktPerson = c.String(),
                        TelNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Adresses");
        }
    }
}
