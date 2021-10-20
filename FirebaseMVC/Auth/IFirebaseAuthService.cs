using System.Threading.Tasks;
using StonkMarket.Auth.Models;

namespace StonkMarket.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}