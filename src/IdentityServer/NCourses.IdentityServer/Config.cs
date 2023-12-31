﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;

using IdentityServer4.Models;

using System.Collections.Generic;

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
                new ApiResource("resource_basket") { Scopes = { "basket_full_permission" } },
                new ApiResource("resource_discount") { Scopes = { "discount_full_permission" } },
                new ApiResource("resource_order") { Scopes = { "order_full_permission" } },
                new ApiResource("resource_payment") {Scopes = { "payment_full_permission" }},
                new ApiResource("resource_gateway") {Scopes = { "gateway_full_permission" }},
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.Email(), new IdentityResources.OpenId(), new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource
                {
                    Name = "roles",
                    DisplayName = "Roles",
                    Description = "User roles",
                    UserClaims = new[] { "role" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_full_permission", "Full Access for Catalog API"),
                new ApiScope("photo_stock_full_permission", "Full Access for Photo Stock API"),
                new ApiScope("basket_full_permission", "Full Access for Basket API"),
                new ApiScope("discount_full_permission", "Full Access for Discount API"),
                new ApiScope("order_full_permission", "Full Access for Order API"),
                new ApiScope("payment_full_permission", "Full Access for Payment API"),
                new ApiScope("gateway_full_permission", "Full Access for Gateway API"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Asp.Net Core MVC Client",
                    ClientId = "WebMvcClient",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "catalog_full_permission",
                        "photo_stock_full_permission",
                        "gateway_full_permission",
                        IdentityServerConstants.LocalApi.ScopeName
                    }
                },
                new Client
                {
                    ClientName = "Asp.Net Core MVC Client",
                    ClientId = "WebMvcClientForUser",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        "basket_full_permission",
                        "discount_full_permission",
                        "order_full_permission",
                        "payment_full_permission",
                        "gateway_full_permission",
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "roles"
                    },
                    AccessTokenLifetime = 1 * 60 * 60,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                }
            };
    }
}