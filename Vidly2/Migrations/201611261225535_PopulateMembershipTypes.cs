namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes VALUES (1, 0, 0, 10)");
            Sql("INSERT INTO MembershipTypes VALUES (2, 30, 0, 15)");
            Sql("INSERT INTO MembershipTypes VALUES (3, 90, 0, 20)");
            Sql("INSERT INTO MembershipTypes VALUES (4, 300, 0, 30)");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM MembershipTypes WHERE Id IN (1, 2, 3, 4)");
        }
    }
}
