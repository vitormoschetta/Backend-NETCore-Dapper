using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Commands.Responses;
using Domain.Contracts.Commands;
using Domain.Contracts.Queries;
using Domain.Queries.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<GenericResponse> Create([FromServices] IProductCommandHandler handler,
            [FromBody] ProductCreateCommand command)
        {
            return await handler.Handle(command);

        }


        [HttpPut("{id}")]
        public async Task<GenericResponse> Update([FromServices] IProductCommandHandler handler,
            Guid id, [FromBody] ProductUpdateCommand command)
        {
            if (id != command.Id)
                return new GenericResponse(false, "Id inválido. ", null);

            return await handler.Handle(command);
        }


        [HttpPut("AddPromotion/{id}")]
        public async Task<GenericResponse> AddPromotion([FromServices] IProductCommandHandler handler,
            Guid id, [FromBody] ProductPromotionCommand command)
        {
            return await handler.Handle(command);
        }


        [HttpDelete("{id}")]
        public async Task<GenericResponse> Delete([FromServices] IProductCommandHandler handler,
            [FromBody] ProductDeleteCommand command)
        {
            return await handler.Handle(command);
        }


        [HttpGet("GetPaginated/{skip:int}/{take:int}")]
        public async Task<IEnumerable<ProductResponse>> GetAll([FromServices] IProductQueryHandler handler,
            [FromRoute] int skip = 0,
            [FromRoute] int take = 5)
        {
            return await handler.Handle(skip, take);
        }


        [HttpGet("{id}")]
        public async Task<ProductResponse> GetById([FromServices] IProductQueryHandler handler,
            Guid id)
        {
            return await handler.Handle(id);
        }


        [HttpGet("Search/{filter}")]
        public async Task<IEnumerable<ProductResponse>> Search([FromServices] IProductQueryHandler handler,
            string filter)
        {
            return await handler.Handle(filter);
        }
    }
}