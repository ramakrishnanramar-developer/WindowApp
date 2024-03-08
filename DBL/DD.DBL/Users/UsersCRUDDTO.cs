using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.DBL
{
    public class UsersListDTO
    {
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
    }
    public class UserGridDTO : UsersListDTO
    {
        public long SNo { get; set; }

        public int Id { get; set; }
    }
    public class UsersCRUDDTO : UsersListDTO
    {
        public string Password { get; set; }
        public int CreatedBy { get; set; }
    }
    public class UsersCRUDUpdateDTO: UsersCRUDDTO
    {
        public int Id { get; set; }
        public bool isPasswordReset { get; set; }
    }
    public class UsersSessionDTO
    {
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public int Id { get; set; }
        public int result { get; set; }
    }
}
