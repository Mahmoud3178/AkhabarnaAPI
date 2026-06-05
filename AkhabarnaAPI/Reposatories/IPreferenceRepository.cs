using AkhabarnaAPI.Models;

namespace AkhabarnaAPI.Reposatories
{
    public interface IPreferenceRepository
    {
        Task AddPreference(UserPreference preference);

        Task AddUserCategories(List<UserCategory> categories);

        Task AddUserSources(List<UserSource> sources);
    }
}
