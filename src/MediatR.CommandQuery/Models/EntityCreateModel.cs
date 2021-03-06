﻿using System;
using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Models
{
    public abstract class EntityCreateModel<TKey> : IHaveIdentifier<TKey>, ITrackCreated, ITrackUpdated
    {
        public TKey Id { get; set; }

        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

        public string CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;

        public string UpdatedBy { get; set; }
    }
}