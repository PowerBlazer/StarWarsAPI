﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StarWarsAPI.Common;

public class JwtOptions
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }

    public string? Secret { get; set; }
    public int ExpirationDays { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret!));
    }
}