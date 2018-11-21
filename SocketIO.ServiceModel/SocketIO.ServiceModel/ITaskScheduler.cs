using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketIO.ServiceModel
{
    public interface ITaskScheduler
    {
        Task<TResult> Start<TResult>(Func<TResult> method);
    }
}