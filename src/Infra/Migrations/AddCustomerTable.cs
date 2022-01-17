using FluentMigrator;

namespace Infra.Migrations
{
    [Migration(20220430121801)]
    public class AddCustomerTable: Migration
    {
        public override void Up()
        {
            Create.Table("Customer")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Text").AsString();
        }

        public override void Down()
        {
            Delete.Table("Customer");
        }
    }
}