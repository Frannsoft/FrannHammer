using System.Threading.Tasks;
using KuroganeHammer.DataSynchro.Models;

namespace KuroganeHammer.DataSynchro.ViewModels
{
    public class LoginVm : BaseVm
    {
        #region public method

        public async Task<UserModel> LoginAs(string username, string password, string baseUrl)
        {
            var loggedInUser = await UserModel.LoginAs(username, password, baseUrl);

            return loggedInUser;
        }
        #endregion
    }
}
