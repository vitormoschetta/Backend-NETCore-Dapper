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
        private readonly IUnitOfWork _uow;

        public ProductCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
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

            if (await _uow.Products.Exists(command.Name))
            {
                return new GenericResponse(false, "Já existe um Produto cadastrado com esse Nome. ", command.Name);
            }

            var product = new Product(command.Name, command.Price);

            //_uow.BeginTransaction();

            await _uow.Products.Add(product);

            //_uow.Commit();
            //_uow.Rollback();

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

            if (await _uow.Products.ExistsUpdate(command.Name, command.Id))
            {
                return new GenericResponse(false, "Já existe um Produto cadastrado com esse Nome. ", command.Name);
            }

            var product = await _uow.Products.Get(command.Id);

            if (product is null)
            {
                return new GenericResponse(false, "Produto não encontrado na base de dados. ");
            }

            product.Update(command.Name, command.Price);

            await _uow.Products.Update(product);

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

            var product = await _uow.Products.Get(command.Id);

            if (product == null)
            {
                return new GenericResponse(false, "Produto não encontrado na base de dados. ");
            }

            product.AddPromotion(command.Price);

            await _uow.Products.Update(product);

            return new GenericResponse(true, "Preço do Produto atualizado com sucesso! ", product);
        }

        public async Task<GenericResponse> Handle(ProductDeleteCommand command)
        {
            if (command is null)
            {
                return new GenericResponse(false, "Product is invalid!");
            }

            var product = await _uow.Products.Get(command.Id);

            if (product == null)
            {
                return new GenericResponse(false, "Produto não encontrado na base de dados. ");
            }

            await _uow.Products.Delete(product.Id);

            return new GenericResponse(true, "Produco excluído com sucesso! ", product);
        }

    }
}