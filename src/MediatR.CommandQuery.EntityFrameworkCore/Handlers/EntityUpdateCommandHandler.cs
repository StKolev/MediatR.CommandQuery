﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers
{
    public class EntityUpdateCommandHandler<TContext, TEntity, TKey, TUpdateModel, TReadModel>
        : EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, EntityUpdateCommand<TKey, TUpdateModel, TReadModel>, TReadModel>
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        public EntityUpdateCommandHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {

        }

        protected override async Task<TReadModel> Process(EntityUpdateCommand<TKey, TUpdateModel, TReadModel> request, CancellationToken cancellationToken)
        {
            var dbSet = DataContext
                .Set<TEntity>();

            var keyValue = new object[] { request.Id };

            // find entity to update by message id, not model id
            var entity = await dbSet
                .FindAsync(keyValue, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return default(TReadModel);

            // copy updates from model to entity
            Mapper.Map(request.Model, entity);

            // save updates
            await DataContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            // return read model
            var readModel = await Read(entity.Id, cancellationToken)
                .ConfigureAwait(false);

            return readModel;
        }
    }
}