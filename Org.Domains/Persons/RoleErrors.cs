using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Domains.Shared;

namespace Org.Domains.Persons
{
    public static class RoleErrors
    {
        public static ErrorCode RoleAlreadyExists =>
            new ErrorCode("RoleError.AlreadyExists", "Role Already Exists");
    }
}