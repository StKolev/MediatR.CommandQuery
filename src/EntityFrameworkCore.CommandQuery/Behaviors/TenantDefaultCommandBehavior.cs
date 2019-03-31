﻿using System;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Definitions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Behaviors
{
    public class TenantDefaultCommandBehavior<TKey, TEntityModel, TResponse>
        : PipelineBehaviorBase<EntityModelCommand<TEntityModel, TResponse>, TResponse>
        where TEntityModel : class
    {
        private readonly ITenantResolver<TKey> _tenantResolver;


        public TenantDefaultCommandBehavior(ILoggerFactory loggerFactory, ITenantResolver<TKey> tenantResolver) : base(loggerFactory)
        {
            _tenantResolver = tenantResolver;
        }

        protected override async Task<TResponse> Process(EntityModelCommand<TEntityModel, TResponse> request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            SetTenantId(request);

            // continue pipeline
            return await next().ConfigureAwait(false);
        }

        private void SetTenantId(EntityModelCommand<TEntityModel, TResponse> request)
        {
            if (!(request.Model is IHaveTenant<TKey> tenantModel))
                return;

            if (!Equals(tenantModel.TenantId, default(TKey)))
                return;

            var tenantId = _tenantResolver.GetTenantId(request.Principal);
            tenantModel.TenantId = tenantId;
        }
    }
}