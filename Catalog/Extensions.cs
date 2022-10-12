using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.DTOs;
using Catalog.Models;

namespace Catalog
{
    public static class Extensions
    {
        public static ItemDTO AsDto(this Item itemObject)
        {
            return new ItemDTO
            {
                Id=itemObject.Id,
                Name=itemObject.Name,
                Price=itemObject.Price,
                CreatedTime=itemObject.CreatedTime,
            };
        }
    }
}