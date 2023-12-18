using System.Collections.Generic;

namespace Colosoft
{
    public abstract class AggregateObserver<TObserver>
    {
        private readonly Queue<TObserver> observers = new Queue<TObserver>();

        protected Queue<TObserver> Observers
        {
            get { return this.observers; }
        }

        public AggregateObserver<TObserver> Add(TObserver observer)
        {
            if (observer != null)
            {
                lock (this.observers)
                {
                    this.observers.Enqueue(observer);
                }
            }

            return this;
        }

        public AggregateObserver<TObserver> Remove(TObserver observer)
        {
            if (observer != null)
            {
                lock (this.observers)
                {
                    var aux = new Queue<TObserver>();

                    while (this.observers.Count > 0)
                    {
                        var i = this.observers.Dequeue();
                        if (!object.ReferenceEquals(i, observer))
                        {
                            aux.Enqueue(i);
                        }
                    }

                    while (aux.Count > 0)
                    {
                        this.observers.Enqueue(aux.Dequeue());
                    }
                }
            }

            return this;
        }

        public static AggregateObserver<TObserver> operator +(AggregateObserver<TObserver> aggregate, TObserver observer)
        {
            if (aggregate != null)
            {
                return aggregate.Add(observer);
            }

            return aggregate;
        }

        public static AggregateObserver<TObserver> operator -(AggregateObserver<TObserver> aggregate, TObserver observer) =>
            Subtract(aggregate, observer);

#pragma warning disable CA1000 // Do not declare static members on generic types
        public static AggregateObserver<TObserver> Subtract(AggregateObserver<TObserver> aggregate, TObserver observer)
#pragma warning restore CA1000 // Do not declare static members on generic types
        {
            if (aggregate != null)
            {
                return aggregate.Remove(observer);
            }

            return aggregate;
        }
    }
}
