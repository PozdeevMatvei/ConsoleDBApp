using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestApp
{
    public class CommandService
    {
        private readonly ContextService _contextService = new ContextService();
        public void CommandStart(string arg)
        {
            var command = GetCommandNumber(arg);
            switch (command)
            {
                case 2:
                    MyApp2(CommandTrim(arg));
                    break;

                case 3:
                    MyApp3();
                    break;

                case 4:
                    break;

                case 5:
                    break;

            }
        }
        private int GetCommandNumber(string arg)
        {
            var regex = new Regex(@"^MyApp\s\d{1}");
            Match matchCommand = regex.Match(arg);
            if (matchCommand == null)
                throw new ArgumentException();

            var regexNumber = new Regex(@"\d{1}");
            Match matchCommandNumber = regexNumber.Match(matchCommand.Value);
            if(matchCommandNumber == null)
                throw new ArgumentException();

            var commandNumber = int.Parse(matchCommandNumber.Value);
            return commandNumber;
        }
        private string CommandTrim(string arg)
        {
            int commandLength = "MyApp *".Length;
            arg = arg.Remove(0, commandLength).Trim();
            return arg;
        }
        private void MyApp2(string arg)
        {
            var humanParam = arg.Split(' ');
            var human = Human.Factory.Create(humanParam);
            _contextService.AddHuman(human);
        }
        private void MyApp3()
        {
            var humans = _contextService.GetRangeUniqHuman();
            foreach(var human in humans)
            {
                Console.WriteLine($"name: {human.FirstName} {human.LastName} {human.Patronymic} " +
                                  $"age: {Calculate(human.DateBirthday)} " +
                                  $"gender {human.Gender}");
            }
            int Calculate(DateOnly dateBirthday)
            {
                DateTime dateTimeToDay = DateTime.Today;
                var toDay = DateOnly.FromDateTime(dateTimeToDay);

                int age = toDay.Year - dateBirthday.Year;
                if (dateBirthday.AddYears(age) > toDay)
                    age -= 1;
                return age;
            }
        }
    }
}
