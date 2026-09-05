using System.Security.Cryptography;
using System.Text;

using ChorePoint.Application.Interfaces;
using ChorePoint.Infrastructure.Options;

using Microsoft.Extensions.Options;

namespace ChorePoint.Infrastructure.Authentication;

public class KidLoginCodeGenerator(IOptions<AuthenticationOptions> authOptions) : IKidLoginCodeGenerator
{
    private readonly AuthenticationOptions _authOptions = authOptions.Value;

    public string GenerateLoginCode()
    {
        var loginCode = new StringBuilder(_authOptions.KidLoginCodeLength);
        while (loginCode.Length < loginCode.Capacity)
        {
            if (loginCode.Length > 0)
            {
                loginCode.Append('-');
            }

            var loginCodePart = RandomNumberGenerator.GetInt32(1, 100);
            loginCode.Append(loginCodePart.ToString("D2"));
        }

        return loginCode.ToString();
    }
}
