using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Util
{
    public class SD
    {
        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_STAFF = "Staff";

        public static string Check_role (string input)
        {
            switch (input)
            {
                case ROLE_ADMIN: return ROLE_ADMIN;
                default: return ROLE_STAFF;
            }
        }

    }
}
