using System;

namespace Pisces.BotUtils.MessageRouter
{
  public class RoutingKey : Attribute
  {
    public string Key { get; set; }
  }
}
