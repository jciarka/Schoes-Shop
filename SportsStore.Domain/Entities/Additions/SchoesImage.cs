using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStore.Domain.Entities.Additions
{
    public class SchoesImage
    {
        public int SchoesImageID { get; set; }

        public byte[] SchoesImageData { get; set; }
        
        public string SchoesImageMimeType { get; set; }

        public int SchoesModelID { get; set; }

        public virtual SchoesModel SchoesModel { get; set; }
    }
}
