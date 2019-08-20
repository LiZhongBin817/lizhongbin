using System.Collections.Generic;
using System.Security.Claims;

namespace CDWM_MR.Common.HttpContextUser
{
    public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        List<string> GetClaimValueByType(string ClaimType);
    }
}
