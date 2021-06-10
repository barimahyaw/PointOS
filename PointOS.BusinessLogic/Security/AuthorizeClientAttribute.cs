using Microsoft.AspNetCore.Mvc;
using System;

namespace PointOS.BusinessLogic.Security
{
    public class AuthorizeClientAttribute : TypeFilterAttribute
    {
        public AuthorizeClientAttribute(Type type) : base(typeof(AuthorizationFilter))
        {
            Arguments = new object[] { type };
        }
    }
}
