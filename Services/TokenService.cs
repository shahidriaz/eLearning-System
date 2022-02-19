using eLearning_System.Interfaces;
using eLearning_System.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eLearning_System.Services
{
    /// <summary>
    /// This class is implementing IToken Service
    /// It will be used to Generate JWT Token
    /// </summary>
    public class TokenService : ITokenService
    {
        /// <summary>
        /// It will hold the reference to the configuration
        /// </summary>
        IConfiguration configuration;
        private readonly SymmetricSecurityKey _key;
        
        public TokenService(IConfiguration configuration)
        {
            // Setting the configuration from the dependancy
            this.configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
        }
        /// <summary>
        /// This method is used to create the JWT Token
        /// </summary>
        /// <param name="user">It receive the User object from which It will generate the JWT Token</param>
        /// <returns></returns>
        public string CreateToken(User user)
        {
            #region Old Code
            /*// Issue 
            string Issuer = configuration["JWT:ValidIssuer"];
            //Audiance
            string Audience = configuration["JWT:ValidAudience"];
            //Signing Key
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            //Dictionary to hold Claims
            IDictionary<string, object> claims = new Dictionary<string, object>();
            claims.Add("UserName", user.Email);
            claims.Add("Role", user.SelectedRole);
            claims.Add("Email", user.Email);
            //Generating Token based on the Information which is prepared above
            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = securityTokenHandler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Issuer = Issuer,
                Audience = Audience,
                Claims = claims,
                Expires = System.DateTime.Now.AddDays(1),
                IssuedAt = System.DateTime.Now,
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
            });
            //Return the Token
            return new JwtSecurityTokenHandler().WriteToken(securityToken);*/
            #endregion

            #region New Code
            var claims =  new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId, user.Email),
                 new Claim(ClaimTypes.Role, user.SelectedRole)
            };
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor { 
              Subject = new ClaimsIdentity(claims),
              Expires = DateTime.Now.AddDays(7),
              SigningCredentials = cred
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            #endregion

        }
    }
}
