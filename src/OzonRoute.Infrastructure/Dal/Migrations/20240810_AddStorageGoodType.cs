using FluentMigrator;

namespace OzonRoute.Infrastructure.Dal.Migrations;

[Migration(version: 20240810, transactionBehavior: TransactionBehavior.None)]
public class AddStorageGoodType : Migration
{
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'storage_goods_v1') THEN
            CREATE TYPE storage_goods_v1 as
            (
                id          bigint,
                name        varchar(50),
                count       int,
                length      double precision,
                width       double precision,
                height      double precision,
                weight      double precision,
                price       numeric(12, 4)
            );
        END IF;
    END
$$;";   

        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS storage_goods_v1;
    END
$$;";   

        Execute.Sql(sql);
    }

}