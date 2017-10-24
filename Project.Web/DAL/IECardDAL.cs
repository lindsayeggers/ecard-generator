using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Web.DAL
{
    public interface IECardDAL
    {
        List<CardsModel> GetAllCards();
        CardsModel GetATemplate(int id);
        void SaveNewCard(CardsModel c);
        
    }
}
