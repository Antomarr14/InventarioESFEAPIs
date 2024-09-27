using System;

namespace InventarioESFEAPIs.Auth;

public interface IJwtAuthenticationService
{
    string Authenticate(string userName);
}
