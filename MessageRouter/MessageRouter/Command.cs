using System.Collections.Generic;

namespace Pisces.BotUtils.MessageRouter
{
  public class Command
  {
    public string Operation { get; set; }
    public List<string> Args { get; set; }
  }
}
