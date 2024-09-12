using BatcheAPI.Abstractions;
using BatcheAPI.DB.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BatcheAPI.Controllers
{
    [ApiController]
    [Route("Batch/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierController( ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        [HttpPost (template: "add_suplier")]
        public ActionResult AddSuplier(PostSupplierDTO supplierDTO)
        {
            var result = _supplierRepository.AddSupplier(supplierDTO);
            return Ok (result);
        }
        
        
        [HttpGet (template: "get_supplier")]
        public ActionResult<IEnumerable<SupplierDTO>> GetSuppliers()
        {
            var result = _supplierRepository.GetSuppliers();
            return Ok(result);
        }

        [HttpDelete(template:"delete_supplier")]
        public ActionResult DelSupplier(int supplierId)
        {
            var result = _supplierRepository.DelSupplier(supplierId);
            return Ok(result);
        }
    }
}

