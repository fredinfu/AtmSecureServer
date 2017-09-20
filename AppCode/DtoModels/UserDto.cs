using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.DtoModels
{
    public class UserDto
    {
        public decimal IdUser{ get; set; }
        public string Username{ get; set; }
        public string Name{ get; set; }
        public string Password{ get; set; }
        public bool IsDeleted{ get; set; }
    }
}
