using AkhabarnaAPI.Models;
using AkhabarnaAPI.Reposatories;

public class PreferenceRepository : IPreferenceRepository
{
    private readonly AppDbContext context;

    public PreferenceRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task AddPreference(UserPreference preference)
    {
        await context.UserPreferences.AddAsync(preference);
    }

    public async Task AddUserCategories(List<UserCategory> categories)
    {
        await context.UserCategories.AddRangeAsync(categories);
    }

    public async Task AddUserSources(List<UserSource> sources)
    {
        await context.UserSources.AddRangeAsync(sources);
    }
}