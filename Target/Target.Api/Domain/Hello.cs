namespace Target.Api;

public static class Hello
{
    public static void MapHello(this WebApplication app)
    {
        
        var groupApi = app.MapGroup("/api");
        
        groupApi.MapGet("/api/Hello", () => {
            return HandleHello();
        });

        groupApi.MapGet("/api/World", () => {
            return HandleWorld();
        });

    }

    public static string HandleHello()
    {
        return "Hello";
    }

    public static string HandleWorld()
    {
        return "World";
    }
}
