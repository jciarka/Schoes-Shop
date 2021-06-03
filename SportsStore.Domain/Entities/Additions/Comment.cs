using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesStore.Domain.Entities.Additions
{
    public class Comment
    {
        public int CommentID { get; set; }

        public string CommentAuthorName { get; set; }

        public string CommentAuthorEmail { get; set; }

        public string CommentContent { get; set; }

        public int Rank { get; set; }

        public int SchoesModelID { get; set; }

        public virtual SchoesModel SchoesModel { get; set; }
    }
}
