﻿using System;
using System.Security.Claims;

namespace AspNetCore.Identity.DocumentDb.Tests.Builder
{
    public class DocumentDbIdentityRoleBuilder
    {
        protected DocumentDbIdentityRole identityRole;

        public DocumentDbIdentityRoleBuilder(DocumentDbIdentityRole identityRole)
        {
            this.identityRole = identityRole;
        }

        public static implicit operator DocumentDbIdentityRole(DocumentDbIdentityRoleBuilder builder)
        {
            return builder.identityRole;
        }

        public static DocumentDbIdentityRoleBuilder Create(string roleName = null)
        {
            if (roleName == null)
            {
                roleName = Guid.NewGuid().ToString().ToUpper();
            }

            return new DocumentDbIdentityRoleBuilder(new DocumentDbIdentityRole()
            {
                Name = roleName
            });
        }

        public DocumentDbIdentityRoleBuilder WithId(string id = null)
        {
            identityRole.Id = id ?? Guid.NewGuid().ToString().ToUpper();
            identityRole.PartitionKey = identityRole.Id;
            return this;
        }

        public DocumentDbIdentityRoleBuilder WithNormalizedRoleName(string normalizedRoleName = null)
        {
            LookupNormalizer normalizer = new LookupNormalizer();

            identityRole.NormalizedName = normalizedRoleName ?? normalizer.Normalize(identityRole.Name);
            return this;
        }

        public DocumentDbIdentityRoleBuilder AddClaim(string type, string value = null)
        {
            Claim claim = new Claim(type ?? Guid.NewGuid().ToString(), value ?? Guid.NewGuid().ToString());
            identityRole.Claims.Add(claim);

            return this;
        }

        public DocumentDbIdentityRoleBuilder AddClaim(Claim claim = null)
        {
            if (claim == null)
            {
                claim = new Claim(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            }

            identityRole.Claims.Add(claim);

            return this;
        }

        public DocumentDbIdentityRole Build()
        {
            return this.identityRole;
        }
    }
}
