﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;

using System.Collections.Generic;
using System.Linq;

using IdentityServer4;

namespace NCourses.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("resource_catalog") { Scopes = { "catalog_full_permission" } },
                new ApiResource("resource_photo_stock") { Scopes = { "photo_stock_full_permission" } },
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[] { };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_full_permission", "Full Access for Catalog API"),
                new ApiScope("photo_stock_full_permission", "Full Access for Photo Stock API"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientName = "Asp.Net Core MVC Client",
                    ClientId = "WebMvcClient",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "catalog_full_permission",
                        "photo_stock_full_permission",
                        IdentityServerConstants.LocalApi.ScopeName
                    }
                }
            };
    }
}