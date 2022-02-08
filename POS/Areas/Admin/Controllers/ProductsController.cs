using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.ProductModule;
using POS.Data.Models;
using POS.Data.Services.BrandModule;
using POS.Data.Services.ProductModule;
using POS.Data.Services.SupplerModule;
using POS.Data.Services.UnitOfMeasureModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IUOMService iUOMService;

        private readonly ISupplierService supplierService;

        private readonly IBrandService brandService;

        private readonly IProductService productService;

        private readonly UserManager<AppUser> userManager;
        public ProductsController(IProductService productService, UserManager<AppUser> userManager, IBrandService brandService, ISupplierService supplierService, IUOMService iUOMService)
        {
            this.iUOMService = iUOMService;

            this.supplierService = supplierService;

            this.productService = productService;

            this.brandService = brandService;

            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.UOM = await iUOMService.GetAll();

            ViewBag.Suppliers = await supplierService.GetAll();

            ViewBag.Brands = await brandService.GetAll();

            ViewBag.ProductNames = await productService.GetAllProductName();

            var product = await productService.GetAll();

            return View(product);
        }

        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var landlords = (await productService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = landlords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public IActionResult ProductName()
        {

            return View();
        }
        public async Task<IActionResult> GetAllProductName()
        {
            try
            {
                var productnames = (await productService.GetAllProductName()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = productnames });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }


        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            try
            {

                //var isExist = (await productService.GetAll()).Where(x => x.ProductNameId == productDTO.ProductNameId).Count();

                //if (isExist > 0)
                //{
                //    return Json(new { success = false, responseText = "The record already exists" });

                //}

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                productDTO.CreatedBy = user.Id;

                var result = await productService.Create(productDTO);

                if (result != null)
                {

                    return Json(new { success = true, responseText = "Product has been successfully added" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Unable to add Product" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> CreateProductName(ProductNameDTO  productNameDTO)
        {
            try
            {

                var isExist = (await productService.GetAllProductName()).Where(x => x.Name == productNameDTO.Name).Count();

                if (isExist > 0)
                {
                    return Json(new { success = false, responseText = "The record already exists" });

                }

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                productNameDTO.CreatedBy = user.Id;

                var result = await productService.CreateProductName(productNameDTO);

                if (result != null)
                {

                    return Json(new { success = true, responseText = "Product Name has been successfully added" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Unable to add Product Name" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> GetProductNamesById(Guid Id)
        {
            try
            {
                var data = await productService.GetProductNamesById(Id);

                if (data != null)
                {
                    ProductNameDTO file = new ProductNameDTO
                    {
                        Id = data.Id,

                        Name = data.Name,

                        CreateDate = data.CreateDate,

                        CreatedBy = data.CreatedBy
                    };

                    return Json(new { data = file });
                }

                return Json(new { data = false });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public async Task<IActionResult> Update(ProductDTO productDTO)
        {
            try
            {
                var results = await productService.Update(productDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "Record  has been  successfully updated!" });
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update the record  !" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }     
        
        public async Task<IActionResult> UpdateProductName(ProductNameDTO  productNameDTO)
        {
            try
            {
                var results = await productService.UpdateProductName(productNameDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "Record  has been  successfully updated!" });
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update the record  !" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
        public async Task<IActionResult> GetById(Guid Id)
        {
            try
            {
                var data = await productService.GetById(Id);

                if (data != null)
                {
                    ProductDTO file = new ProductDTO
                    {
                        Id = data.Id,

                        ProductNameId = data.ProductNameId,

                        CostPrice = data.CostPrice,

                        SellingPrice = data.SellingPrice,

                        ExpectedProfit = data.ExpectedProfit,

                        ProductCode = data.ProductCode,

                        UOMId = data.UOMId,

                        CreateDate = data.CreateDate,

                        CreatedBy = data.CreatedBy,

                        UpdateBy = data.CreatedBy,

                        UpdatedDate = data.UpdatedDate,

                        SupplierId = data.SupplierId,

                        BrandId = data.BrandId,
                    };

                    return Json(new { data = file });
                }

                return Json(new { data = false });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var results = await productService.Delete(Id);

                if (results == true)
                {
                    return Json(new { success = true, responseText = "Record  has been  successfully Deleted!" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to Delete because the file is in use with other records!" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        
        public async Task<IActionResult> DeleteProductName(Guid Id)
        {
            try
            {
                var results = await productService.DeleteProductName(Id);

                if (results == true)
                {
                    return Json(new { success = true, responseText = "Record  has been  successfully Deleted!" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to Delete because the file is in use with other records!" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

    }
}
