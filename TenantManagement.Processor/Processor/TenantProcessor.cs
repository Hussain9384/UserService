﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantManagement.Processor.DbContracts;
using TenantManagement.Processor.Models;

namespace TenantManagement.Processor.Processor
{
    public class TenantProcessor : ITenantProcessor
    {
        private readonly ITenantCommandRepository _tenantCommandRepository;
        private readonly ITenantQueryRepository _tenantQueryRepository;

        public TenantProcessor(ITenantCommandRepository tenantCommandRepository, 
                                ITenantQueryRepository tenantQueryRepository)
        {
            _tenantCommandRepository = tenantCommandRepository;
            _tenantQueryRepository = tenantQueryRepository;
        }
        public async Task<Tenant> CreateTenant(Tenant tenant)
        {
            var res= await _tenantCommandRepository.CreateTenant(tenant);
            return res;
        }


        public  async Task<bool> DeleteTenant(IEnumerable<long> tenantIds)
        {
            return await  _tenantCommandRepository.DeleteTenant(tenantIds);
        }

        public async Task<IEnumerable<Tenant>> GetTenants()
        {
            var tenants= await _tenantQueryRepository.GetTenants();
            return tenants;
        }

        public async Task<Summary> GetTenantSummary()
        {
            var summary = await _tenantQueryRepository.GetTenantSummary();
            return summary;
        }

        public async Task<Tenant> UpdateTenant(Tenant tenant)
        {
            return await _tenantCommandRepository.UpdateTenant(tenant);
        }
    }
}
