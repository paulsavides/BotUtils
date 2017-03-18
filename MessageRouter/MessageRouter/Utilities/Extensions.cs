using System.Collections.Generic;
using System.Linq;

namespace Pisces.BotUtils.MessageRouter.Utilities
{
  internal static class Extensions
  {
    internal static Command ToCommand(this string message, string commandBegin)
    {
      Command comm = new Command();

      string[] elems = message.Split(' ');

      int ix = 0;
      int comm_ix = -1;

      foreach (var elem in elems)
      {
        if (elem.StartsWith(commandBegin))
        {
          comm.Operation = elem.Remove(0, 1);
          comm_ix = ix;

          break;
        }

        ix++;
      }

      if (comm_ix >= 0)
      {
        List<string> subset = elems.ToList();
        subset.RemoveRange(0, comm_ix + 1);
        comm.Args = subset;
      }

      return comm;
    }
  }
}
