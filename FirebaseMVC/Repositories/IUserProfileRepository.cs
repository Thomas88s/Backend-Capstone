using StonkMarket.Models;
using System.Collections.Generic;

namespace StonkMarket.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        UserProfile GetById(int id);
        List<UserProfile> GetAllUserProfiles();
    }
}