﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity=TenantManagement.InfraStructure.Entities;
using TenantManagement.Processor.DbContracts;
using Domain=TenantManagement.Processor.Models;
using TenantManagement.InfraStructure.Database;
using AppBaseEntity.BaseRepository;

namespace TenantManagement.InfraStructure.Repository
{   
    public class TenantCommandRepository : BaseCommandRepository<TenantDatabase>, ITenantCommandRepository
    {
        private readonly TenantDatabase _tenantDatabase;

        public TenantCommandRepository(IMapper mapper,TenantDatabase  tenantDatabase):base(mapper, tenantDatabase)
        {
            _tenantDatabase = tenantDatabase;
        }

        public async Task<Domain.Tenant> CreateTenant(Domain.Tenant tenant)
        {
           var domainModel = await Create<Domain.Tenant, Entities.Tenant>(tenant);
            return domainModel;
        }

        public async Task<bool> DeleteTenant(IEnumerable<long> tenantIds)
        {
            IQueryable<Entities.Tenant> tenants;
            if (tenantIds !=null && tenantIds.All(id=>id > 0))
            {
                tenants = _tenantDatabase.Tenants.Where(t => tenantIds.Contains(t.Id));
               
            }
            else
            {
                tenants = _tenantDatabase.Tenants.Select(s=>s);
            }
            _tenantDatabase.Tenants.RemoveRange(tenants);
            return  await _tenantDatabase.SaveChangesAsync() > 0;
        }

        public async Task<Domain.Tenant> UpdateTenant(Domain.Tenant tenant)
        {
            var domainModel = await Update<Domain.Tenant, Entities.Tenant>(tenant);
            return domainModel;
        }

    }
}
