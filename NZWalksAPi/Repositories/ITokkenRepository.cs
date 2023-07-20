using Microsoft.AspNetCore.Identity;

namespace NZWalksAPi.Repositories
{
    public interface ITokkenRepository
    {
       string CreateJWTToken(IdentityUser user,List<string> roles);
    }
}
