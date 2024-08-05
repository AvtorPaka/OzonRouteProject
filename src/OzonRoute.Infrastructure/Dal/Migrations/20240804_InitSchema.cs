using FluentMigrator;

namespace OzonRoute.Infrastructure.Dal.Migrations;

[Migration(version:20240804, TransactionBehavior.None)]
public class InitSchema : Migration
{
    public override void Up()
    {
        Create.Table("DeliveryGoods")
            .WithColumn("id").AsInt64().PrimaryKey("goods_pk").Identity()
            .WithColumn("user_id").AsInt64().NotNullable()
            .WithColumn("width").AsDouble().NotNullable()
            .WithColumn("height").AsDouble().NotNullable()
            .WithColumn("length").AsDouble().NotNullable()
            .WithColumn("weight").AsDouble().NotNullable();

        Create.Table("Calculations")
            .WithColumn("id").AsInt64().PrimaryKey("calculations_pk").Identity()
            .WithColumn("user_id").AsInt64().NotNullable()
            .WithColumn("good_ids").AsCustom("bigint[]").NotNullable() //Ugly af, NT rework
            .WithColumn("total_volume").AsDouble().NotNullable()
            .WithColumn("total_weight").AsDouble().NotNullable()
            .WithColumn("price").AsDecimal().NotNullable()
            .WithColumn("distance").AsDouble().NotNullable()
            .WithColumn("at").AsDateTimeOffset().NotNullable();

        Create.Index("delivery_goods_user_id_index")
            .OnTable("DeliveryGoods")
            .OnColumn("user_id");

        Create.Index("calculations_user_id_index")
            .OnTable("Calculations")
            .OnColumn("user_id");
    }
    public override void Down()
    {
        Delete.Table("DeliveryGoods");
        Delete.Table("Calculations");
    }

}