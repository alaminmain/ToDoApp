using Blazored.LocalStorage;
using ToDoApp.Model;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services
{
   
        public class ProfileService : IProfileService
        {
            private readonly ILocalStorageService _localStorageService;
            private const string ProfileKey = "profiles";

            public ProfileService(ILocalStorageService localStorageService)
            {
                _localStorageService = localStorageService;
            }

            public async Task<List<Profile>> GetAllProfilesAsync()
            {
                var profiles = await _localStorageService.GetItemAsync<List<Profile>>(ProfileKey);
                return profiles ?? new List<Profile>();
            }

            public async Task<Profile> GetProfileByIdAsync(int id)
            {
                var profiles = await GetAllProfilesAsync();
                return profiles.ElementAtOrDefault(id); // Return profile if found
            }

            public async Task AddProfileAsync(Profile profile)
            {
                var profiles = await GetAllProfilesAsync();
                profiles.Add(profile);
                await SaveProfilesAsync(profiles);
            }

            public async Task<Profile> UpdateProfileAsync(int id, Profile updatedProfile)
            {
                var profiles = await GetAllProfilesAsync();
                var profile = profiles.ElementAtOrDefault(id);

                if (profile != null)
                {
                    // Update profile properties
                    profile.FirstName = updatedProfile.FirstName;
                    profile.LastName = updatedProfile.LastName;
                    profile.Address = updatedProfile.Address;
                    profile.ProfilePictureUrl = updatedProfile.ProfilePictureUrl;

                    await SaveProfilesAsync(profiles); // Save changes to local storage
                }

                return profile; // Return the updated profile
            }

            public async Task<bool> DeleteProfileAsync(int id)
            {
                var profiles = await GetAllProfilesAsync();
                if (id < profiles.Count)
                {
                    profiles.RemoveAt(id);
                    await SaveProfilesAsync(profiles);
                    return true;
                }
                return false;
            }

            public async Task SaveProfilesAsync(List<Profile> profiles)
            {
                await _localStorageService.SetItemAsync(ProfileKey, profiles);
            }


        }
}
