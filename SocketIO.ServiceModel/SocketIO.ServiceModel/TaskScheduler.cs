using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketIO.ServiceModel
{
    public class TaskScheduler : ITaskScheduler
    {
        public Task<TResult> Start<TResult>(Func<TResult> method)
        {
            return Task.Run(() => method());
        }
    }
}