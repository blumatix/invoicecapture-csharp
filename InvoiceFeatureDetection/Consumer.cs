using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceFeatureDetection
{
  public class Consumer<T>
  {
    private readonly BlockingCollection<T> _consumables;
    private readonly int _nrTasks;

    public Consumer(BlockingCollection<T> consumables, int numberOfTasks)
    {
      if (consumables == null)
      {
        throw new ArgumentNullException(nameof(consumables));
      }

      _consumables = consumables;
      _nrTasks = numberOfTasks;
    }

    public Task<IEnumerable<TR>> Process<TR>(Func<T, TR> func)
    {
      return Task.Run(() =>
      {
        var taskList = new List<Task<List<TR>>>();

        for (var i = 0; i < _nrTasks; i++)
        {
          var task = Task.Run(() =>
          {
            var results = new List<TR>();

            // Take items until blocking collection is empty
            T item;

            while (_consumables.TryTake(out item))
            {
              results.Add(func(item));
            }

            return results;
          });

          taskList.Add(task);
        }

        Task.WaitAll(taskList.ToArray<Task>());

        return taskList.SelectMany(t => t.Result);
      });
    }
  }
}
