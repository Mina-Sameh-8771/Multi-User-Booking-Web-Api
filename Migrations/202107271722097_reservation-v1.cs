namespace Reservation_Task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservationv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        reservedBy = c.String(),
                        customerName = c.String(),
                        reservationDate = c.DateTime(nullable: false),
                        notes = c.String(),
                        creationDate = c.DateTime(nullable: false),
                        trip_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Trips", t => t.trip_ID)
                .Index(t => t.trip_ID);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 64),
                        cityName = c.String(),
                        price = c.Double(nullable: false),
                        imageUrl = c.String(),
                        creationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "trip_ID", "dbo.Trips");
            DropIndex("dbo.Reservations", new[] { "trip_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Trips");
            DropTable("dbo.Reservations");
        }
    }
}
