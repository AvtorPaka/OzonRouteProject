namespace OzonRoute.Infrastructure.Dal.Infrastructure;

public enum CachePolicyType
{   
    V1StorageGoodsGet,
    V1StorageGoodsPrice,
}

public enum TagType
{
    V1StorageGoods,
}

public static class CachePolicyTypeExtensions
{
    public static string Use(this CachePolicyType redisPolicyType)
    {
        string policyString = redisPolicyType switch
        {
            CachePolicyType.V1StorageGoodsGet => "storage-goods-get",
            CachePolicyType.V1StorageGoodsPrice => "storage-goods-price",
            _ => throw new ArgumentException("Unallowed type")
        };

        return policyString;
    }
}

public static class TagTypeExtensions
{
    public static string Use(this TagType redisTagType)
    {
        string tagString = redisTagType switch
        {
            TagType.V1StorageGoods => "tag-storage",
            _ => throw new ArgumentException("Unallowed type")
        };

        return tagString;
    }
}
