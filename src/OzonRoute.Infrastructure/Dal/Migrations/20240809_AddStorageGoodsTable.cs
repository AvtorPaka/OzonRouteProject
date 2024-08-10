using FluentMigrator;

namespace OzonRoute.Infrastructure.Dal.Migrations;

[Migration(version: 20240809, transactionBehavior: TransactionBehavior.None)]
public class AddStorageGoodsTable : Migration
{
    public override void Up()
    {
        Create.Table("storage_goods")
            .WithColumn("id").AsInt64().PrimaryKey("storage_goods_pk").Identity()
            .WithColumn("name").AsFixedLengthString(50).NotNullable()
            .WithColumn("count").AsInt32().NotNullable()
            .WithColumn("length").AsDouble().NotNullable()
            .WithColumn("width").AsDouble().NotNullable()
            .WithColumn("height").AsDouble().NotNullable()
            .WithColumn("weight").AsDouble().NotNullable()
            .WithColumn("price").AsDecimal(12, 4).NotNullable();
    }
    public override void Down()
    {
        Delete.Table("storage_goods");
    }

}