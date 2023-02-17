using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestApp
{
    public class Human
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public DateOnly DateBirthday { get; set; }
        public string Gender { get; set; } = null!;

        public static class Factory
        {
            public static Human Create(string[] human) 
            {
                if (!IsValidHuman(human))
                    throw new ArgumentException();

                var humen = new Human
                {
                    FirstName = human[0],
                    LastName = human[1],
                    Patronymic = human[2],
                    DateBirthday = GetDate(human[3]),
                    Gender = human[4]
                };
                return humen;
            }
            private static bool IsValidHuman(string[] human)
            {
                if(human.Length != 5)
                    return false;
                if (!IsDate(human[3]))
                    return false;

                return true;
            }
            private static DateOnly GetDate(string query)
            {
                var (_, date) = TryGetDate(query);

                return date;
            }
            private static (bool isDate, DateOnly date) TryGetDate(string date)
            {
                if (DateOnly.TryParse(date, out var result))
                    return (true, result);

                throw new ArgumentException();            
            }
            private static bool IsDate(string date)
            {
                var (isDate,_) = TryGetDate(date);
                return isDate;
            }
        }
    }
}
