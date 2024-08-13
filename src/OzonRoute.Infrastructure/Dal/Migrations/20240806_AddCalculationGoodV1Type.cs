using System.Runtime.CompilerServices;
using FluentMigrator;

namespace OzonRoute.Infrastructure.Dal.Migrations;

[Migration(version: 20240806, transactionBehavior: TransactionBehavior.None)]
public class AddCalculationGoodV1Type : Migration
{
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'calculation_goods_v1') THEN
            CREATE TYPE calculation_goods_v1 as
            (
                id          bigint,
                user_id     bigint,
                width       double precision,
                height      double precision,
                length      double precision,
                weight      double precision
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
        DROP TYPE IF EXISTS calculation_goods_v1;
    END
$$;";
        Execute.Sql(sql);
    }

}