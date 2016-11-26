namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipNames : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET MembershipName = 'Pay As You Go' WHERE Id = 1");
            Sql("UPDATE MembershipTypes SET MembershipName = 'Monthly' WHERE Id = 2");
            Sql("UPDATE MembershipTypes SET MembershipName = 'Quaterly' WHERE Id = 3");
            Sql("UPDATE MembershipTypes SET MembershipName = 'Yearly' WHERE Id = 4");
        }
        
        public override void Down()
        {
            Sql("UPDATE MembershipTypes SET MembershipName = NULL WHERE Id IN (1, 2, 3, 4)");
        }
    }
}
