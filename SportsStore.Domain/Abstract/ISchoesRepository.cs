using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ShoesStore.Domain.Entities;
using ShoesStore.Domain.Entities.Additions;
using ShoesStore.Domain.Entities.FilterHelperClasses;

namespace ShoesStore.Domain.Abstract
{
    public interface ISchoesRepository
    {

        IEnumerable<SchoesModel> SchoesModelsRepository { get; }

        IEnumerable<Brand> BrandsRepository { get; }

        IEnumerable<SchoesDestiny> SchoesDestiniesRepository { get; }

        IEnumerable<SchoesModelUser> SchoesModelUsersRepository { get; }

        IEnumerable<SubCategory> SubCategoriesRepository { get; }

        IEnumerable<SchoesModel> GetFilteredSchoesEnumerable(SchoesFilterInfo schoesFilterInfo);

        SchoesModel[] GetFilteredSchoesArray(SchoesFilterInfo schoesFilterInfo);

        void SaveSchoesModel(SchoesModel newModel);

        void SaveSchoesModelAsync(SchoesModel newModel);

        SchoesModel DeleteSchoesModel(int modelID);

        Task<SchoesModel> DeleteSchoesModelAsync(int modelID);

    }
}
