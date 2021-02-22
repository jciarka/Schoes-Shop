using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SportsStore.Domain;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Entities.Additions;
using System.Reflection;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Entities.FilterHelperClasses;
using SportsStore.WebUI.Models.ShopProducts;

namespace SportsStore.WebUI.Controllers
{
    public class ShopProductsController : Controller
    {
        ISchoesRepository repository;
        int PageSize = 4;

        public ShopProductsController(ISchoesRepository schoesRepo)
        {
            repository = schoesRepo;
        }

         // WAŻNE fromularz filtrów musi wykorzytywać metodę GET, aby można było w inych widokach nawigować za pomocą return URL-->
        public ViewResult List(SchoesFilterInfo schoesFilterInfo, int page = 1) 
        {
            IEnumerable<SchoesModel> schoesList = repository.GetFilteredSchoesEnumerable(schoesFilterInfo);

            int totalItems = schoesList.Count();

            schoesList = schoesList
                    .OrderBy(schoesModel => schoesModel.SchoesModelID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize);

            SchoesListViewModel schoesListViewModel = new SchoesListViewModel
            {
                Schoes = schoesList,
                SchoesFilterInfo = schoesFilterInfo,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = totalItems,
                }
            };
            return View(schoesListViewModel);
         }


        public FileContentResult GetImage(int modelId)
        {
            SchoesModel schoesModel = repository.SchoesModelsRepository.FirstOrDefault(p => p.SchoesModelID == modelId);
            if (schoesModel != null)
            {
                if (schoesModel.SchoesImages.FirstOrDefault() != null )
                {
                    SchoesImage imageToSend = schoesModel.SchoesImages.FirstOrDefault();
                    return File(imageToSend.SchoesImageData, imageToSend.SchoesImageMimeType);
                }
            }
            return null;

        }
    }
}