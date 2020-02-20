using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public enum UserAuthenticationResults
    {
        Success,
        UserDoesntExist,
        PasswordIncorrect,
        InvalidUsername
    }
}
