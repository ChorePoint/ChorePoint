using System.Security.Cryptography;
using System.Text;

using ChorePoint.Application.Interfaces;

namespace ChorePoint.Infrastructure.Authentication;

public class KidLoginCodeGenerator : IKidLoginCodeGenerator
{
    public string GenerateLoginCode()
    {
        var loginCode = new StringBuilder(InfrastructureConstants.LoginCodeMaxLength);
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
