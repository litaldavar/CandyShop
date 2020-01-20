using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models
{
    public class CandyRepository : ICandyRepository
    {
        public IEnumerable<Candy> GetAllCandy => throw new NotImplementedException();

        public IEnumerable<Candy> GetCandyOnSale => throw new NotImplementedException();

        public Candy GetCandyById(int candyId)
        {
            throw new NotImplementedException();
        }
    }
}
