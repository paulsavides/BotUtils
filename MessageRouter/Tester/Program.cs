using System;
using Pisces.BotUtils.MessageRouter;

namespace Tester
{
  class Program
  {
    static void Main(string[] args)
    {
      var router = new Router();

      router.RouteMessage("!test how are you");

      Console.ReadKey();

    }
  }

  [RoutingKey(Key = "test")]
  class Test : ICommandProcessor
  {
    public void ProcessCommand(Command command)
    {
      Console.WriteLine($"Command: {command.Operation}");
      foreach (var arg in command.Args)
      {
        Console.WriteLine($"Arg: {arg}");
      }
    }
  }
}
