using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebAPI
{
    public interface IJWTAuthenticationManager
    {
        Mesajlar<USER_LOGIN> Authenticate(Mesajlar<USER_LOGIN> User);
    }



}
