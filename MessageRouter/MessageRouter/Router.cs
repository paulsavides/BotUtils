using System;
using System.Reflection;
using System.Configuration;
using System.Collections.Generic;
using Pisces.BotUtils.MessageRouter.Utilities;

namespace Pisces.BotUtils.MessageRouter
{
  public class Router
  {
    private Dictionary<string, Type> _routers = new Dictionary<string, Type>();
    private string _commandBegin;
    
    public Router()
    {
      _commandBegin = ConfigurationManager.AppSettings.Get("CommandBegin") ?? "!";
      var assembly = Assembly.GetCallingAssembly();
      LoadRouters(assembly);
    }

    public Router(string commandBegin)
    {
      _commandBegin = commandBegin;
      var assembly = Assembly.GetCallingAssembly();
      LoadRouters(assembly);
    }

    public void RouteMessage(string message)
    {
      var command = message.ToCommand(_commandBegin);

      if (!_routers.ContainsKey(command.Operation))
      {
        return;
      }

      var router = _routers[command.Operation];

      var processor = (ICommandProcessor) Activator.CreateInstance(router);

      processor.ProcessCommand(command);
    }
    
    public void LoadRouters(Assembly assembly)
    {
      var types = assembly.GetTypes();

      foreach (var type in types)
      {
        bool valid = false;

        var interfaces = type.GetInterfaces();

        foreach (var intface in interfaces)
        {
          if (intface == typeof(ICommandProcessor))
          {
            valid = true;
            break;
          }
        }

        if (valid)
        {
          _routers.Add(GetRoutingKey(type), type);
        }

      }
    }

    public string GetRoutingKey(Type type)
    {
      var routingKey = (RoutingKey) type.GetCustomAttribute(typeof(RoutingKey));

      if (routingKey != null)
      {
        return routingKey.Key;
      }

      return type.Name;
    }
  }
}
