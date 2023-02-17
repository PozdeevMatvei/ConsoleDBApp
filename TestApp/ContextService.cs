using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class ContextService
    {
        public void AddHuman(Human human)
        {
            using(AppDbContext db = new AppDbContext())
            {
                db.People.Add(human);
                db.SaveChanges();
            }
        }
        public Human[] GetRangeUniqHuman()
        {
            var humanList = new List<Human>();
            using (AppDbContext db = new AppDbContext())
            {
                var getGroupedHumans = db.People.Select(human => human)
                                      .GroupBy(human => new
                                      {
                                          human.FirstName,
                                          human.LastName,
                                          human.Patronymic,
                                          human.DateBirthday
                                      });
                                                                                           
                foreach(var groupHumans in getGroupedHumans)
                {
                    humanList.Add(groupHumans.First());                    
                }                
            }
            return humanList.ToArray();
        }
    }
}
