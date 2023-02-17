using TestApp;

var comandService = new CommandService();

while (true)
{
    var arg = Console.ReadLine();
    if (arg != null)
        comandService.CommandStart(arg!);    
}