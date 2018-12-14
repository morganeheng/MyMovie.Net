using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNET
{
    interface IUserDao : IDao<User>
    {
        bool CheckUser(string Login, string Password);
        bool CheckUserExist(string Login);
        int SelectIdByLogin(string Login);
    }
}
