﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore.CommandQuery.Tests
{
    public class Location
    {
        public Location()
        {
            Created = DateTimeOffset.UtcNow;
            Updated = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Byte[] RowVersion { get; set; }
    }

}
