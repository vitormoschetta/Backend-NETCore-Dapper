using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts.Queries;
using Domain.Contracts.Repositories;
using Domain.Queries.Responses;

namespace Domain.Queries.Handlers
{
    public class ProductQueryHandler : IProductQueryHandler
    {
        private readonly IProductRepository _repository;
        public ProductQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductResponse>> Handle(int skip, int take)
        {
            return from item in await _repository.GetAll()
                   select new ProductResponse(item.Id, item.Name, item.Price);
        }

        public async Task<IEnumerable<ProductResponse>> Handle(string filter)
        {
            return from item in _repository.GetByFilter(filter)
                   select new ProductResponse(item.Id, item.Name, item.Price);
        }

        public async Task<ProductResponse> Handle(Guid id)
        {
            var item = await _repository.Get(id);

            if (item is null)
                return new ProductResponse();

            return new ProductResponse(item.Id, item.Name, item.Price);
        }
    }
}