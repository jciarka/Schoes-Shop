using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoesStore.Domain.Entities;

namespace ShoesStore.Domain.Entities
{
    public class Cart
    {
        public List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(SchoesModel schoesModel, int quantity)
        {
            CartLine line = lineCollection
                       .Where(prod => prod.SchoesModel.SchoesModelID == schoesModel.SchoesModelID)
                       .FirstOrDefault(); //Zwraca pierwszy element listy
                                          //nie jest to już IEnumerable<Catrline>
                                          //tylko referencja do istniejącego obiektu cartline
                                          //lub null jeśli obiek nie istnieje (wartość doyślna do obiektu)

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    SchoesModel = schoesModel,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(SchoesModel schoesModel) //Tu inaczej niż w książce ale powinnno działać
        {
            lineCollection.RemoveAll(l => l.SchoesModel.SchoesModelID == schoesModel.SchoesModelID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(cline => cline.SchoesModel.Price * cline.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        { 
            get { return lineCollection; }
        }


    }
}
