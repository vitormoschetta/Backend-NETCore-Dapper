using System.Threading.Tasks;
using Domain.Commands;
using Domain.Commands.Responses;

namespace Domain.Contracts.Commands
{
    public interface IProductCommandHandler
    {
        Task<GenericResponse> Handle(ProductCreateCommand command);
        Task<GenericResponse> Handle(ProductUpdateCommand command);
        Task<GenericResponse> Handle(ProductPromotionCommand command);
        Task<GenericResponse> Handle(ProductDeleteCommand command);
    }
}