using Microsoft.Extensions.Logging;

namespace OzonRoute.Domain.Services.Extensions;

public static class TaskExt
{
    public static async Task<IEnumerable<T>> WhenAll<T>(IEnumerable<Task<T>> tasks)
    {
        var allTasks = Task.WhenAll(tasks);

        try
        {
            return await allTasks;
        }
        catch (Exception) {}

        throw allTasks.Exception ?? throw new ArgumentNullException("Isn't even possible.");
    }

    public static async Task WhenAll(IEnumerable<Task> tasks, ILogger logger)
    {
        var allTasks = Task.WhenAll(tasks);

        try
        {
            await allTasks;
        }
        catch (Exception) {}

        if (allTasks.Exception != null)
        {   
            logger.LogError(allTasks.Exception, "Invalid input data");
            throw allTasks.Exception;
        };
    }
}