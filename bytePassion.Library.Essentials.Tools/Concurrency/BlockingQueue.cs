using System.Collections.Generic;
using System.Threading;

namespace bytePassion.Library.Essentials.Tools.Concurrency
{
	public class BlockingQueue<T>
    {
        private readonly Queue<T> queue;

        public BlockingQueue()
        {
            queue = new Queue<T>();
        }

        public void Put(T item)
        {
            lock (queue)
            {
                queue.Enqueue(item);

                if (queue.Count == 1)
                    Monitor.PulseAll(queue);
            }
        }

        public T Take()
        {
            lock (queue)
            {
                while (queue.Count == 0)
                    Monitor.Wait(queue);

                return queue.Dequeue();
            }
        }
    }
}
