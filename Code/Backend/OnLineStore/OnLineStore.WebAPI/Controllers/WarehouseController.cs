﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnLineStore.Core.BusinessLayer.Contracts;
using OnLineStore.WebAPI.Filters;
using OnLineStore.WebAPI.Responses;

namespace OnLineStore.WebAPI.Controllers
{
#pragma warning disable CS1591
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseController : OnLineStoreController
    {
        protected ILogger Logger;
        protected IWarehouseService Service;

        public WarehouseController(ILogger<WarehouseController> logger, IWarehouseService service)
            : base()
        {
            Logger = logger;
            Service = service;
        }
#pragma warning restore CS1591

        /// <summary>
        /// Retrieves the products
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <returns>A sequence that contains the products</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        /// <response code="500"></response>
        [HttpGet("Product")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [OnLineStoreActionFilter]
        public async Task<IActionResult> GetProductsAsync(int? pageSize = 10, int? pageNumber = 1)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetProductsAsync));

            // Get response from business logic
            var response = await Service.GetProductsAsync((int)pageSize, (int)pageNumber);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Gets the inventory for product by warehouse
        /// </summary>
        /// <param name="id">Product</param>
        /// <param name="warehouseID">Warehouse</param>
        /// <returns>A sequence of inventory transactions by product and warehouse</returns>
        /// <response code="200"></response>
        /// <response code="401"></response>
        /// <response code="500"></response>
        [HttpGet("ProductInventory/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [OnLineStoreActionFilter]
        public async Task<IActionResult> GetProductInventoryAsync(int? id, string warehouseID)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetProductInventoryAsync));

            // Get response from business logic
            var response = await Service.GetProductInventories(id, warehouseID);

            // Return as http response
            return response.ToHttpResponse();
        }
    }
}
