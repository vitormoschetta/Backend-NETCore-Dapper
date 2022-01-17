using System.Threading.Tasks;
using Domain.Commands.Responses;
using Domain.Contracts.Commands;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Validations;

namespace Domain.Commands.Handlers
{
    public class ProductCommandHandler : IProductCommandHandler
    {
        private readonly IProductRepository _repository;

        public ProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<GenericResponse> Handle(ProductCreateCommand command)
        {
            if (command is null)
            {
                return new GenericResponse(false, "Product is invalid!");
            }

            var validationResult = command.Validate();

            if (!validationResult.IsValid)
            {
                return new GenericResponse(false, ErrorParser.GetErrorMessage(validationResult));
            }

            if (await _repository.Exists(command.Name))
            {
                return new GenericResponse(false, "Já existe um Produto cadastrado com esse Nome. ", command.Name);
            }

            var product = new Product(command.Name, command.Price);

            await _repository.Add(product);

            return new GenericResponse(true, "Produto cadastrado com sucesso! ", product);
        }

        public async Task<GenericResponse> Handle(ProductUpdateCommand command)
        {
            if (command is null)
            {
                return new GenericResponse(false, "Product is invalid!");
            }

            var validationResult = command.Validate();

            if (!validationResult.IsValid)
            {
                return new GenericResponse(false, ErrorParser.GetErrorMessage(validationResult));
            }

            if (await _repository.ExistsUpdate(command.Name, command.Id))
            {
                return new GenericResponse(false, "Já existe um Produto cadastrado com esse Nome. ", command.Name);
            }

            var product = await _repository.Get(command.Id);

            if (product is null)
            {
                return new GenericResponse(false, "Produto não encontrado na base de dados. ");
            }

            product.Update(command.Name, command.Price);

            await _repository.Update(product);

            return new GenericResponse(true, "Produto atualizado com sucesso!. ", product);
        }

        public async Task<GenericResponse> Handle(ProductPromotionCommand command)
        {
            if (command is null)
            {
                return new GenericResponse(false, "Product is invalid!");
            }

            var validationResult = command.Validate();

            if (!validationResult.IsValid)
            {
                return new GenericResponse(false, ErrorParser.GetErrorMessage(validationResult));
            }

            var product = await _repository.Get(command.Id);

            if (product == null)
            {
                return new GenericResponse(false, "Produto não encontrado na base de dados. ");
            }

            product.AddPromotion(command.Price);

            await _repository.Update(product);

            return new GenericResponse(true, "Preço do Produto atualizado com sucesso! ", product);
        }

        public async Task<GenericResponse> Handle(ProductDeleteCommand command)
        {
            if (command is null)
            {
                return new GenericResponse(false, "Product is invalid!");
            }

            var product = await _repository.Get(command.Id);

            if (product == null)
            {
                return new GenericResponse(false, "Produto não encontrado na base de dados. ");
            }

            await _repository.Delete(product.Id);

            return new GenericResponse(true, "Produco excluído com sucesso! ", product);
        }

    }
}