using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpServerFramework.Util
{
    public class EventDispatcherUtil
    {
        public static void DispatcherEvent(EventHandler Handler, object Sender, EventArgs Args)
        {
            if (Handler == null)
            {
                return;
            }
            Handler.Invoke(Sender, Args);
        }

        public static void DispatcherEvent<TEventArgs>(EventHandler<TEventArgs> Handler, object Sender, TEventArgs Args) where TEventArgs : EventArgs
        {
            if (Handler == null)
            {
                return;
            }
            Handler.Invoke(Sender, Args);
        }

        public static void AsyncDispatcherEvent(EventHandler Handler, object Sender, EventArgs Args)
        {
            if (Handler == null)
            {
                return;
            }
            Task.Run(() =>
            {
                Handler.Invoke(Sender, Args);
            });
        }

        public static void AsyncDispatcherEvent<TEventArgs>(EventHandler<TEventArgs> Handler, object Sender, TEventArgs Args) where TEventArgs : EventArgs
        {
            if (Handler == null)
            {
                return;
            }
            var deles = Handler.GetInvocationList();
            foreach (EventHandler<TEventArgs> handler in deles)
            {
                Task.Run(() =>
                {
                    handler.Invoke(Sender, Args);
                });
            }
        }

        public static void DispatcherEvent(Delegate Handler, object Sender, EventArgs Args)
        {
            if (Handler == null)
            {
                return;
            }
            var list = Handler.GetInvocationList();
            foreach (var item in list)
            {
                item.DynamicInvoke(null, new object[] { Sender, Args });
            }
        }
    }
}
