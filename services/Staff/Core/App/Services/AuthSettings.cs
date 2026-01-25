using System;

namespace Staff.Core.App.Services;

public class AuthSettings
{
    public TimeSpan Expires { get; set; }
    public string SecretKey { get; set; }
}

