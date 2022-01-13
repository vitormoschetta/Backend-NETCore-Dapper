using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Commands.Responses;
using Domain.Queries.Responses;

namespace Domain.Contracts.Queries
{
    public interface IProductQueryHandler
    {
        Task<IEnumerable<ProductResponse>> Handle(int skip, int take);
        Task<IEnumerable<ProductResponse>> Handle(string filter);
        Task<ProductResponse> Handle(Guid id);
    }
}