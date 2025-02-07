namespace RecipeWorker.Interfaces;

public interface IOffensiveTextCheckerService
{
    Task<string> CheckOffensiveText(string text);
}