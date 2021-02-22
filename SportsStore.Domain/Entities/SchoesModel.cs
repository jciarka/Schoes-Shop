using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities.Additions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace SportsStore.Domain.Entities
{
    public class SchoesModel
    {

        public SchoesModel()
        {
            this.SchoesDestiny = new HashSet<SchoesDestiny>();
            this.SubCategory = new HashSet<SubCategory>();
            this.Comments = new HashSet<Comment>();
            this.SchoesImages = new HashSet<SchoesImage>();
        }
        

        [HiddenInput(DisplayValue = false)]
        public int SchoesModelID { get; set; }

        
        //Ogólne___________________________________

        [Required(ErrorMessage = "Proszę podać nazwę modelu")]
        [Display(Name = "Nazwa Modelu")]
        public string SchoesModelName { get; set; }



        [Required(ErrorMessage = "Proszę podać opis.")]
        [DataType(DataType.MultilineText), Display(Name = "Opis")]
        public string Description { get; set; }



        [Required(ErrorMessage = "Proszę podać cenę")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Proszę podać cenę")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        /*
        [NotMapped]
        public float TotalRank
        {
            get 
            {
                return ((float)Comments.Sum(x => x.Rank)) / ((float)Comments.Count());
            }
        }
        */

        //many to one_______________________________
        
        [Display(Name = "Zdjęcia")]
        public virtual ICollection<SchoesImage> SchoesImages { get; set; }

        [Display(Name = "Komentarze")]
        public virtual ICollection<Comment> Comments { get; set; }
        

        //One to many_______________________________

        public int BrandID { get; set; }
        [Required(ErrorMessage = "Proszę podać markę butów")]
        [Display(Name = "Marka")]
        public virtual Brand Brand { get; set; }



        public int SchoesModelUserID { get; set; }
        [Required(ErrorMessage = "Proszę wybrać dla kogo przezaczone są buty")]
        [Display(Name = "Dla Kogo")]
        public virtual SchoesModelUser SchoesModelUser { get; set; }



        //Mant to many_____________________________

        [Required(ErrorMessage = "Proszę wybrać przeznaczenie obuwia")]
        [Display(Name = "Przeznaczenie")]
        public virtual ICollection<SchoesDestiny> SchoesDestiny { get; set; }



        [Required(ErrorMessage = "Proszę kategorie do których należą buty")]
        [Display(Name = "Kategorie butów")]
        public virtual ICollection<SubCategory> SubCategory { get; set; }


        //Bez jawnych związków

        public string _SizeArray { get; set; }

        [NotMapped]
        public IEnumerable<int> SizeArray 
        {
            get
            {
                IEnumerable<string> stringArray = _SizeArray.Split(';');
                IEnumerable<int> intArray = stringArray.Select(elem => Int32.Parse(elem));
                return intArray;
            }
            set 
            {
                _SizeArray = String.Join(";", value.Select(elem => elem.ToString()).ToArray());
            } 
        }

    

        //[Required(ErrorMessage = "Proszę wybrać kolor")]
        //[Display(Name = "Kolor")]
        public string _Colour { get; set; }

        [NotMapped]
        public IEnumerable<string> Colour
        {
            get
            {
                return _Colour.Split(';');
            }
            set
            {
                _Colour = String.Join(";", value.ToArray());
            }
        }




        //[Required(ErrorMessage = "Proszę wybrać materiał podeszwy")]
        [Display(Name = "Materiał podeszwy")]
        public string SoleFabric { get; set; }


        //[Required(ErrorMessage = "Proszę wybrać materiał cholewki")]
        [Display(Name = "Materiał cholewki")]
        public string ShankFabric { get; set; }


        //[Required(ErrorMessage = "Proszę wybrać materiał wnętrza")]
        [Display(Name = "Materiał wnętrza")]
        public string InsideFabric { get; set; }


        //[Required(ErrorMessage = "Proszę wybrać sposób wiązania")]
        [Display(Name = "Sposób wiązania")]
        public string BindingMethod { get; set; }

                     
        //[Required(ErrorMessage = "Proszę podać kraj produkcji")]
        [Display(Name = "Kraj Prdukcji")]
        public string OriginCountry { get; set; }
    }
}
