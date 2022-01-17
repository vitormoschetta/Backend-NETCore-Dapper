using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Contracts.Queries;
using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Domain.Queries.Handlers
{
    /// <summary>
    /// Essa classe não necessariamente precisa existir. A UI/Controller poderia acessar o 
    /// repositório diretamente.
    /// Porém, caso os objetos (Models) retornado do banco de dados possuirem informações 
    /// que não desejamos apresentar na UI, então essa classe seria o local ideal 
    /// para o mapeamento dos dados para uma ViewModel.
    /// </summary>
    public class ProductQueryHandler : IProductQueryHandler
    {
        private readonly IUnitOfWork _uow;

        public ProductQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Product>> Handle()
        {
            return await _uow.Products.GetAll();
        }

        public async Task<IEnumerable<Product>> Handle(string filter)
        {
            return await _uow.Products.GetByFilter(filter);
        }

        public async Task<Product> Handle(Guid id)
        {
            return await _uow.Products.Get(id);
        }
    }
}