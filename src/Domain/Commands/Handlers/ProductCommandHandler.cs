using System.Threading.Tasks;
using Domain.Commands.Responses;
using Domain.Contracts.Commands;
using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Domain.Commands.Handlers
{
    public class ProductCommandHandler : Notifiable, IProductCommandHandler
    {
        private readonly IProductRepository _repository;
        public ProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<GenericResponse> Handle(ProductCreateCommand command)
        {
            var exist = await _repository.Exists(command.Name);
            if (exist)
                return new GenericResponse(false, "Já existe um Produto cadastrado com esse Nome. ", command);

            var product = new Product(command.Name, command.Price);

            AddNotifications(product);

            if (Invalid)
                return new GenericResponse(false, string.Join(". ", Notifications));

            await _repository.Add(product);

            return new GenericResponse(true, "Produto cadastrado com sucesso! ", product);
        }

        public async Task<GenericResponse> Handle(ProductUpdateCommand command)
        {
            var product = await _repository.Get(command.Id);
            if (product == null)
                return new GenericResponse(false, "Produto não encontrado na base de dados. ", null);

            var existOtherProduct = await _repository.ExistsUpdate(command.Name, command.Id);
            if (existOtherProduct)
                return new GenericResponse(false, "Já existe outro Produto cadatrado com esse Nome. ", null);

            product.Update(command.Name, command.Price);

            AddNotifications(product);

            if (Invalid)
                return new GenericResponse(false, string.Join(". ", Notifications));

            await _repository.Update(product);

            return new GenericResponse(true, "Produto atualizado com sucesso!. ", product);
        }

        public async Task<GenericResponse> Handle(ProductPromotionCommand command)
        {
            var product = await _repository.Get(command.Id);
            if (product == null)
                return new GenericResponse(false, "Produto não encontrado na base de dados. ", null);

            product.AddPromotion(command.Price);

            AddNotifications(product);

            if (Invalid)
                return new GenericResponse(false, string.Join(". ", Notifications));

            await _repository.Update(product);

            return new GenericResponse(true, "Preço do Produto atualizado com sucesso! ", product);
        }

        public async Task<GenericResponse> Handle(ProductDeleteCommand command)
        {
            var product = await _repository.Get(command.Id);
            if (product == null)
                return new GenericResponse(false, "Produto não encontrado na base de dados. ", null);

            await _repository.Delete(product.Id);

            return new GenericResponse(true, "Produco excluído com sucesso! ", product);
        }

    }
}