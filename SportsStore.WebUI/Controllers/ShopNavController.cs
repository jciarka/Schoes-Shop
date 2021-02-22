using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Concrete;//```````````````````````````
using SportsStore.Domain.Entities.Additions;//```
using SportsStore.WebUI.Infrastructure.IEnumerableExtension;
using SportsStore.WebUI.Models.ShopNav;
using SportsStore.Domain.Entities.FilterHelperClasses;
using SportsStore.WebUI.Infrastructure.Abstract;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsStore.WebUI.Controllers
{
    public class ShopNavController : Controller
    {
        ISchoesRepository repository;
        IAuthProvider authProvider;

        public ShopNavController(ISchoesRepository schoesRepo, IAuthProvider authenticationProvider)
        {
            repository = schoesRepo;
            authProvider = authenticationProvider;
        }


        public PartialViewResult MenuBar()
        {
            return PartialView(repository.SchoesModelUsersRepository);
        }

        public PartialViewResult Filters(SchoesFilterInfo schoesFilterInfo)
        {
            SchoesFilterList schoesFilterList = new SchoesFilterList
            {
                    //filtry wybrane wcześniej przez użytkownika
                SchoesFilterInfo = schoesFilterInfo,

                    //lista z której będzie mógł wybrać użytkownik
                schoesModelUser = repository.SchoesModelUsersRepository
                                    .Select(user => user.SchoesModelUserName)
                                    .Distinct()
                                    .ToArray(),

                subCategory = repository.SubCategoriesRepository
                                    .Select(user => user.SubCategoryName)
                                    .Distinct()
                                    .ToArray(),

                destiny = repository.SchoesDestiniesRepository
                                    .Select(user => user.SchoesDestinyName)
                                    .Distinct()
                                    .ToArray(),

                brand = repository.BrandsRepository
                                    .Select(user => user.BrandName)
                                    .Distinct()
                                    .ToArray(),

                colour = repository.SchoesModelsRepository
                                    .Select(schoesPair => schoesPair.Colour)
                                    .UnnestHierarchy()
                                    .Distinct().ToArray(),

                soleFabric = repository.SchoesModelsRepository
                                    .Select(schoesPair => schoesPair.SoleFabric)
                                    .Distinct()
                                    .ToArray(),

                shankFabric = repository.SchoesModelsRepository
                                    .Select(schoesPair => schoesPair.ShankFabric)
                                    .Distinct()
                                    .ToArray(),

                insideFabric = repository.SchoesModelsRepository
                                    .Select(schoesPair => schoesPair.InsideFabric)
                                    .Distinct()
                                    .ToArray(),

                bindingMethod = repository.SchoesModelsRepository
                                    .Select(schoesPair => schoesPair.BindingMethod)
                                    .Distinct()
                                    .ToArray()
            };

            return PartialView(schoesFilterList);
        }

        public PartialViewResult AccountIcon(SchoesFilterInfo schoesFilterInfo)
        {
            bool isSignedIn = authProvider.IsSignedIn();
            ViewBag.IsAuthenticated = isSignedIn;
            if (!isSignedIn)
            {
                return PartialView();
            }
            ViewBag.UserName = authProvider.GetName();

            return PartialView();
        }
 


            /*
                    public PartialViewResult MenuBar()
                    {
                        /*using (SchoesDbContext bdContext = new SchoesDbContext())
                        SchoesModelUser[] modelUsersArray = null;

                        using (SchoesDbContext bdContext = new SchoesDbContext())
                        {
                            SchoesModel model = new SchoesModel()
                            {
                                SchoesModelName = "Mandy",
                                Description = "Eleganckie damskie kozaki na wysokim obcasie",
                                Price = 599,
                                Brand = new Brand("Nike"),
                                SchoesModelUser = new SchoesModelUser("Chłopiec","Dla chłopc"),
                                SchoesDestiny = new HashSet<SchoesDestiny>(new SchoesDestiny[]
                                    {
                                        new SchoesDestiny("Koszykówka"),
                                    }),
                                SubCategory = new HashSet<SubCategory>(new SubCategory[]
                                    {
                                        new SubCategory("Sportowe")
                                    }),
                                Colour = new string[] { "Białe", "Czarne" },
                                SizeArray = new int[] { 39, 40, 41, 42, 44, 45, 46 },
                                OriginCountry = "Polska",
                                InsideFabric = "Skóra naturalna",
                                ShankFabric = "Skóra",
                                SoleFabric = "Guma"
                            };

                            bdContext.SaveSchoesModel(model);
                            modelUsersArray = bdContext.SchoesModelUsers.ToArray();
                        }

                        SchoesModel model = new SchoesModel()
                        {
                            SchoesModelName = "Mandy",
                            Description = "Eleganckie damskie kozaki na wysokim obcasie",
                            Price = 599,
                            Brand = new Brand("Nike"),
                            SchoesModelUser = new SchoesModelUser("Chłopiec", "Dla chłopca"),
                            SchoesDestiny = new HashSet<SchoesDestiny>(new SchoesDestiny[]
                                    {
                                        new SchoesDestiny("Koszykówka"),
                                    }),
                            SubCategory = new HashSet<SubCategory>(new SubCategory[]
                                    {
                                        new SubCategory("Sportowe")
                                    }),
                            Colour = new string[] { "Białe", "Czarne" },
                            SizeArray = new int[] { 39, 40, 41, 42, 44, 45, 46 },
                            OriginCountry = "Polska",
                            InsideFabric = "Skóra naturalna",
                            ShankFabric = "Skóra",
                            SoleFabric = "Guma"
                        };

                        repository.SaveSchoesModel(model);

                        modelUsersArray = repository.SchoesModelUsersRepository.ToArray();

                        return PartialView(modelUsersArray);

                        return PartialView(repository.SchoesModelUsersRepository.ToArray());
                    }
            */

        }
}
