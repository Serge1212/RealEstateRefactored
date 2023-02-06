namespace RealEstateRefactored;

public static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public static void Main()
    {
        try
        {
            var startup = new Startup();
            startup.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}