using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ShoesStore.Domain;
using ShoesStore.Domain.Abstract;
using ShoesStore.Domain.Entities;
using ShoesStore.Domain.Entities.Additions;
using System.IO;
using System.Drawing;
using System.Reflection;
using ShoesStore.Domain.Entities.FilterHelperClasses;

namespace ShoesStore.Domain.Concrete
{
    public class ShoesDbContext : DbContext, ISchoesRepository
    {
        public ShoesDbContext() : base("SchoesDb")
        {
            Database.SetInitializer<ShoesDbContext>(new ShoesDbContextInitializer());
        }

        public DbSet<SchoesModel> SchoesModels { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<SchoesDestiny> SchoesDestinies { get; set; }

        public DbSet<SchoesModelUser> SchoesModelUsers { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<SchoesImage> SchoesImages { get; set; }

        public DbSet<Comment> Comments { get; set; }



        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SchoesModel>().HasRequired<Brand>(model => model.Brand)
                                              .WithMany(brand => brand.SchoesModel)
                                              .HasForeignKey<int>(model => model.BrandID)
                                              .WillCascadeOnDelete();

            modelBuilder.Entity<SchoesModel>().HasRequired<SchoesModelUser>(model => model.SchoesModelUser)
                                              .WithMany(user => user.SchoesModel)
                                              .HasForeignKey<int>(model => model.SchoesModelUserID)
                                              .WillCascadeOnDelete();

            modelBuilder.Entity<SchoesModel>().HasMany<SchoesDestiny>(model => model.SchoesDestiny)
                                              .WithMany(dest => dest.SchoesModel)
                                              .Map(table =>
                                              {
                                                  table.MapLeftKey("SchoesDestiny");
                                                  table.MapRightKey("SchoesModel");
                                                  table.ToTable("ModelDestinyArray");
                                              });

            modelBuilder.Entity<SchoesModel>().HasMany<SubCategory>(model => model.SubCategory)
                                              .WithMany(category => category.SchoesModel)
                                              .Map(table =>
                                              {
                                                  table.MapLeftKey("SubCategory");
                                                  table.MapRightKey("SchoesModel");
                                                  table.ToTable("ModelSubCategoryArray");
                                              });

        }      
    }
    */




        IEnumerable<SchoesModel> ISchoesRepository.SchoesModelsRepository { get { return this.SchoesModels; } }

        IEnumerable<Brand> ISchoesRepository.BrandsRepository { get { return this.Brands; } }

        IEnumerable<SchoesDestiny> ISchoesRepository.SchoesDestiniesRepository { get { return this.SchoesDestinies; } }

        IEnumerable<SchoesModelUser> ISchoesRepository.SchoesModelUsersRepository { get { return this.SchoesModelUsers; } }

        IEnumerable<SubCategory> ISchoesRepository.SubCategoriesRepository { get { return this.SubCategories; } }



        public IEnumerable<SchoesModel> GetFilteredSchoesEnumerable(SchoesFilterInfo schoesFilterInfo)
        {
            IEnumerable<SchoesModel> schoesList = SchoesModels;

            if (schoesFilterInfo != null)
            {
                //------Filtr osoby dla której jest but
                if (schoesFilterInfo.schoesModelUser != null)
                {
                    schoesList = schoesList.Where(schoesModel =>
                                    schoesModel.SchoesModelUser.SchoesModelUserName.Equals(schoesFilterInfo.schoesModelUser));
                }

                //------Filtr marki
                if (schoesFilterInfo.brand != null)
                {
                    schoesList = schoesList.Where(schoesModel =>
                                    schoesModel.Brand.BrandName.Equals(schoesFilterInfo.brand));
                }

                //------Filtr kategorii
                if (schoesFilterInfo.subCategory != null)
                {
                    schoesList = schoesList.Where(schoesModel =>
                    {
                        foreach (SubCategory subCategory in schoesModel.SubCategory)
                        {
                            if (subCategory.SubCategoryName.Equals(schoesFilterInfo.subCategory))
                            {
                                return true;
                            }
                        }
                        return false;
                    });
                }

                //------Filtr przeznaczenia
                if (schoesFilterInfo.destiny != null)
                {
                    schoesList = schoesList.Where(schoesModel =>
                    {
                        foreach (SchoesDestiny schoesDestiny in schoesModel.SchoesDestiny)
                        {
                            if (schoesDestiny.SchoesDestinyName.Equals(schoesFilterInfo.destiny))
                            {
                                return true;
                            }
                        }
                        return false;
                    });
                }


                //filtr rozmiarów
                if (schoesFilterInfo.size != null)
                {
                    schoesList = schoesList.Where(schoesModel =>
                                    schoesModel.SizeArray.Any(availableSize => availableSize == schoesFilterInfo.size));
                }


                //filtr kolorów
                if (schoesFilterInfo.size != null)
                {
                    schoesList = schoesList.Where(schoesModel =>
                                    schoesModel.SizeArray.Any(availableSize => availableSize == schoesFilterInfo.size));
                }


                //Pozestałe filtry (te które w klasie schoesModel są typu int lub string)
                foreach (PropertyInfo filterProperty in typeof(SchoesFilterInfo).GetProperties())
                {
                    //PropertyInfo jest obiektem którego uzaleznia jedynie klasa. Aby zacągnąć obiekt 
                    //zapisany w atrybucie w danej istancji tej klasy, 
                    //trzeba wskazać tą instację. W poniższym przypadku kalsą jest SchoesFilterInfo, 
                    //a obiektem schoesFilterInfo
                    object filterValue = filterProperty.GetValue(schoesFilterInfo);

                    //Aby ułatwić szukanie właściwości, które nie były uwzględnione wcześniej w klasie SchoesFilterInfo
                    //dodano atrybut typu filterAttribute. Ma on wskazywać, które właściwości można poddać standardowej 
                    //procedurze sortowania, a właściwość filtra allowCustomFiltering pozwala 
                    var filterAttribute = ((filterAttribute)filterProperty.GetCustomAttribute(typeof(filterAttribute)));

                    if (filterValue != null && filterAttribute != null && filterAttribute.allowCustomFiltering)
                    {
                        schoesList = schoesList.Where(schoesModel =>
                        {
                            foreach (PropertyInfo schoesModelProperty in typeof(SchoesModel).GetProperties())
                            {
                                if (schoesModelProperty.Name.Equals(filterProperty.Name))
                                {
                                    object schoesModelPropertyValue = schoesModelProperty.GetValue(schoesModel);
                                    if (schoesModelPropertyValue.Equals(filterValue))
                                    {
                                        return true;
                                    }
                                }
                            }
                            return false;
                        });
                    }
                }
            }
            return schoesList;
        }

        public SchoesModel[] GetFilteredSchoesArray(SchoesFilterInfo schoesFilterInfo)
        {
            return GetFilteredSchoesEnumerable(schoesFilterInfo).ToArray();
        }


        public void SaveSchoesModel(SchoesModel newModel)
        {
            if (newModel.SchoesModelID == 0)
            {
                this.SchoesModels.Add(newModel);
            }
            else
            {
                SchoesModel dbElement = this.SchoesModels.Find(newModel.SchoesModelID);
                if (dbElement != null)
                {
                    foreach (PropertyInfo property in typeof(SchoesModel).GetProperties())
                    {
                        if (property.Name != "SchoesModelID")
                        {
                            object value = property.GetValue(newModel);
                            property.SetValue(dbElement, value);
                        }
                    }
                }
            }
            this.SaveChanges();
        }

        public async void SaveSchoesModelAsync(SchoesModel newModel)
        {
            if (newModel.SchoesModelID == 0)
            {
                this.SchoesModels.Add(newModel);
            }
            else
            {
                SchoesModel dbElement = await this.SchoesModels.FindAsync(newModel.SchoesModelID);
                if (dbElement != null)
                {
                    foreach (PropertyInfo property in typeof(SchoesModel).GetProperties())
                    {
                        if (property.Name != "SchoesModelID")
                        {
                            object value = property.GetValue(newModel);
                            property.SetValue(dbElement, value);
                        }
                    }
                }
            }
            await this.SaveChangesAsync();
        }

        public async Task<SchoesModel> DeleteSchoesModelAsync(int modelID)
        {
            SchoesModel dbElement = await this.SchoesModels.FindAsync(modelID);
            if (dbElement != null)
            {
                this.SchoesModels.Remove(dbElement);
                this.SaveChanges();
            }
            return dbElement;
        }

        public SchoesModel DeleteSchoesModel(int modelID)
        {
            SchoesModel dbElement = this.SchoesModels.Find(modelID);
            if (dbElement != null)
            {
                this.SchoesModels.Remove(dbElement);
                this.SaveChanges();
            }
            return dbElement;
        }

    }

    public class ShoesDbContextInitializer : DropCreateDatabaseAlways<ShoesDbContext> //DropCreateDatabaseIfModelChanges<ShoesDbContext>
    {
        protected override void Seed(ShoesDbContext context)
        {
            IList<SchoesModel> schoesModels = GenerateTestModels();

            context.SchoesModels.AddRange(schoesModels);

            base.Seed(context);
        }

        private IList<SchoesModel> GenerateTestModels()
        {
            IList<SchoesModel> schoesModels = new List<SchoesModel>();

            Brand Nike = new Brand("Nike");
            Brand Lasocki = new Brand("Lasocki");

            SchoesDestiny Koszykówka = new SchoesDestiny("Koszykówka");
            SchoesDestiny DoMiasta = new SchoesDestiny("Do miasta");
            SchoesDestiny NaCodzien = new SchoesDestiny("Na codzień");
            SchoesDestiny Wizytowe = new SchoesDestiny("Wizytowe");


            SubCategory Sportowe = new SubCategory("Sportowe");
            SubCategory Polboty = new SubCategory("Półbuty");
            SubCategory PolbotyWizytowe = new SubCategory("Półbuty Wizytowe");
            SubCategory Eleganckie = new SubCategory("Eleganckie");

            SchoesModelUser Boy = new SchoesModelUser("Chłopiec", "dla chłopca");
            SchoesModelUser Girl = new SchoesModelUser("Dziewczynka", "dla dziewczynki");
            SchoesModelUser Men = new SchoesModelUser("Mężczyzna", "dla niego");
            SchoesModelUser Woman = new SchoesModelUser("Kobieta", "dla niej");


            Image imageFromFile = Image.FromFile("C:/Users/ciark/Documents/MVC/SportsStore/SportsStore.Domain/Pictures/Hyperdunk.jpg");
            byte[] imageArray;
            using (MemoryStream stream = new MemoryStream())
            {
                imageFromFile.Save(stream, imageFromFile.RawFormat);
                imageArray = stream.ToArray();
            };
            

            schoesModels.Add(new SchoesModel()
            {
                SchoesModelName = "Hyperfuse I",
                Description = "Lekkie buty do koszykówki",
                Price = 299,
                Brand = Nike,
                SchoesModelUser = Men,
                SchoesDestiny = new HashSet<SchoesDestiny>(new SchoesDestiny[]
                {
                        Koszykówka
                }),
                SubCategory = new HashSet<SubCategory>(new SubCategory[]
                {
                        Sportowe
                }),
                Colour = new string[] { "Białe", "Czarne" },
                SizeArray = new int[] { 39, 40, 41, 42, 44, 45, 46 },

                OriginCountry = "Chiny",
                InsideFabric = "Bawełna",
                ShankFabric = "Polietylen",
                SoleFabric = "Guma",

                Comments = new HashSet<Comment>(new Comment[]
                {
                        new Comment()
                        {
                            CommentAuthorEmail = "jkkkwalski@o2.pl",
                            CommentAuthorName = "def",
                            CommentContent = "Fajne",
                            Rank = 5,
                        },
                }),

                SchoesImages = new HashSet<SchoesImage>(new SchoesImage[]
                {
                        new SchoesImage()
                        {
                            SchoesImageData = imageArray,
                            SchoesImageMimeType = "image/jpeg",
                        },
                }),
            });
            //schoesModels[0].SchoesImages.Add(image);


            schoesModels.Add(new SchoesModel()
            {
                SchoesModelName = "HyperDunk I",
                Description = "Buty do koszykówki z najwyżsej klasy amortyzacją",
                Price = 339,
                Brand = Nike,
                SchoesModelUser = Men,
                SchoesDestiny = new HashSet<SchoesDestiny>(new SchoesDestiny[]
                    {
                        Koszykówka,
                    }),
                SubCategory = new HashSet<SubCategory>(new SubCategory[]
                    {
                         Sportowe
                    }),
                Colour = new string[] { "Białe", "Czarne" },
                SizeArray = new int[] { 38, 40, 42, 44, 45, 46 },

                OriginCountry = "Chiny",
                InsideFabric = "Bawełna",
                ShankFabric = "Polietylen",
                SoleFabric = "Guma",
                
                Comments = new HashSet<Comment>(new Comment[]
                {
                        new Comment()
                        {
                            CommentAuthorEmail = "jkkkwalski@o2.pl",
                            CommentAuthorName = "def",
                            CommentContent = "Niezłe",
                            Rank = 5,
                        },
                }),

                SchoesImages = new HashSet<SchoesImage>(new SchoesImage[]
                {
                        new SchoesImage()
                        {
                            SchoesImageData = imageArray,
                            SchoesImageMimeType = "image/jpeg",
                        },
                }),

            });




            schoesModels.Add(new SchoesModel()
            {
                SchoesModelName = "CitiGo",
                Description = "Wygodne buty do miasta",
                Price = 299,
                Brand = Nike,
                SchoesModelUser = Men,
                SchoesDestiny = new HashSet<SchoesDestiny>(new SchoesDestiny[]
                    {
                        DoMiasta,
                        NaCodzien,
                    }),
                SubCategory = new HashSet<SubCategory>(new SubCategory[]
                    {
                        Polboty
                    }),
                Colour = new string[] { "Białe", "Czarne" },
                SizeArray = new int[] { 39, 40, 41, 42, 44, 45, 46 },

                OriginCountry = "Chiny",
                InsideFabric = "Bawełna",
                ShankFabric = "Skóra naturalna",
                SoleFabric = "Guma",
                Comments = new HashSet<Comment>(new Comment[]
                {
                        new Comment()
                        {
                            CommentAuthorEmail = "jkkkwalski@o2.pl",
                            CommentAuthorName = "def",
                            CommentContent = "Niezłe",
                            Rank = 5,
                        },
                }),

                SchoesImages = new HashSet<SchoesImage>(new SchoesImage[]
                {
                        new SchoesImage()
                        {
                            SchoesImageData = imageArray,
                            SchoesImageMimeType = "image/jpeg",
                        },
                }),
            });

            schoesModels.Add(new SchoesModel()
            {
                SchoesModelName = "Gent I",
                Description = "Buty wysytowe",
                Price = 339,
                Brand = Lasocki,
                SchoesModelUser = Woman,
                SchoesDestiny = new HashSet<SchoesDestiny>(new SchoesDestiny[]
                    {
                        Wizytowe,
                    }),
                SubCategory = new HashSet<SubCategory>(new SubCategory[]
                    {
                        Eleganckie,
                        PolbotyWizytowe
                    }),
                Colour = new string[] { "Czarne" },
                SizeArray = new int[] { 38, 40, 42, 44 },

                OriginCountry = "Polska",
                InsideFabric = "Skóra naturalna",
                ShankFabric = "Skóra naturalna",
                SoleFabric = "Skóra naturalna",

                Comments = new HashSet<Comment>(new Comment[]
                {
                        new Comment()
                        {
                            CommentAuthorEmail = "jkkkwalski@o2.pl",
                            CommentAuthorName = "def",
                            CommentContent = "Niezłe",
                            Rank = 5,
                        },
                }),

                SchoesImages = new HashSet<SchoesImage>(new SchoesImage[]
                {
                        new SchoesImage()
                        {
                            SchoesImageData = imageArray,
                            SchoesImageMimeType = "image/jpeg",
                        },
                }),
            });


            schoesModels.Add(new SchoesModel()
            {
                SchoesModelName = "BabyGo",
                Description = "Wygodne buty do miasta",
                Price = 299,
                Brand = Nike,
                SchoesModelUser = Boy,
                SchoesDestiny = new HashSet<SchoesDestiny>(new SchoesDestiny[]
                    {
                        DoMiasta,
                        NaCodzien,
                    }),
                SubCategory = new HashSet<SubCategory>(new SubCategory[]
                    {
                        Polboty
                    }),
                Colour = new string[] { "Białe", "Czarne" },
                SizeArray = new int[] { 39, 40, 41, 42, 44, 45, 46 },

                OriginCountry = "Chiny",
                InsideFabric = "Bawełna",
                ShankFabric = "Skóra naturalna",
                SoleFabric = "Guma",
                Comments = new HashSet<Comment>(new Comment[]
                {
                        new Comment()
                        {
                            CommentAuthorEmail = "jkkkwalski@o2.pl",
                            CommentAuthorName = "def",
                            CommentContent = "Niezłe",
                            Rank = 5,
                        },
                }),

                SchoesImages = new HashSet<SchoesImage>(new SchoesImage[]
                {
                        new SchoesImage()
                        {
                            SchoesImageData = imageArray,
                            SchoesImageMimeType = "image/jpeg",
                        },
                }),
            });

            schoesModels.Add(new SchoesModel()
            {
                SchoesModelName = "Gent for girl",
                Description = "Buty wyzytowe",
                Price = 339,
                Brand = Lasocki,
                SchoesModelUser = Girl,
                SchoesDestiny = new HashSet<SchoesDestiny>(new SchoesDestiny[]
                    {
                        Wizytowe,
                    }),
                SubCategory = new HashSet<SubCategory>(new SubCategory[]
                    {
                        Eleganckie,
                        PolbotyWizytowe
                    }),
                Colour = new string[] { "Czarne" },
                SizeArray = new int[] { 38, 40, 42, 44 },

                OriginCountry = "Polska",
                InsideFabric = "Skóra naturalna",
                ShankFabric = "Skóra naturalna",
                SoleFabric = "Skóra naturalna",

                Comments = new HashSet<Comment>(new Comment[]
                {
                        new Comment()
                        {
                            CommentAuthorEmail = "jkkkwalski@o2.pl",
                            CommentAuthorName = "def",
                            CommentContent = "Niezłe",
                            Rank = 5,
                        },
                }),

                SchoesImages = new HashSet<SchoesImage>(new SchoesImage[]
                {
                        new SchoesImage()
                        {
                            SchoesImageData = imageArray,
                            SchoesImageMimeType = "image/jpeg",
                        },
                }),
            });


            return schoesModels;
        }
    }
}