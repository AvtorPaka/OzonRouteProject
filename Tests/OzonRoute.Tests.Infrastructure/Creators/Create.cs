namespace OzonRoute.Tests.Infrastructure.Creators;

public sealed class Create
{
    private static long _counter = DateTime.UtcNow.Ticks;
    private static readonly Random StaticRandom = new Random();

    public static long RandomId() => Interlocked.Increment(ref _counter);
    public static double RandomDouble() => StaticRandom.NextDouble();
    public static decimal RandomDecimal() => (decimal)StaticRandom.NextDouble();
}