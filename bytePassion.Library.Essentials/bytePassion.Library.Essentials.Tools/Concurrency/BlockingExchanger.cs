using System.Threading;

namespace bytePassion.Library.Essentials.Tools.Concurrency
{
	public class BlockingExchanger<T>
    {
        private T giveOver;
        private bool itemGiven;

        public BlockingExchanger()
        {
            giveOver = default(T);
            itemGiven = false;
        }

        public void Put(T item)
        {
            lock (this)
            {
                giveOver = item;
                itemGiven = true;
                Monitor.Pulse(this);
            }
        }

        public T Take()
        {
            lock (this)
            {
                while (!itemGiven)
                    Monitor.Wait(this);

                return giveOver;
            }
        }
    }

}
