using eLearning_System.Models;

namespace eLearning_System.Interfaces
{
    /// <summary>
    /// This interface is implemented by the TokenService to Generate the JWT token
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Signature to Create Token
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        public string CreateToken(User user);
    }
}
