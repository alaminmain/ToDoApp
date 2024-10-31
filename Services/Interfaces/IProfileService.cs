using ToDoApp.Model;

namespace ToDoApp.Services.Interfaces
{
    public interface IProfileService
    {
        Task<List<Profile>> GetAllProfilesAsync();
        Task<Profile> GetProfileByIdAsync(int id);
        Task AddProfileAsync(Profile profile);
        Task<bool> DeleteProfileAsync(int id);
        Task<Profile> UpdateProfileAsync(int id, Profile updatedProfile); // Added update method
        Task SaveProfilesAsync(List<Profile> profiles);
    }
}
