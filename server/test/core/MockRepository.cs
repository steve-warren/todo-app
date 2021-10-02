
using System.Collections;
using System.Collections.Generic;

namespace todo_app_test
{
    public abstract class MockRepository<TEntity> : IEnumerable<TEntity> where TEntity : class
    {
        protected MockRepository() { }
        public MockRepository(IEnumerable<TEntity> items)
        {
            Items = new List<TEntity>(items);   
        }

        public List<TEntity> Items { get; } = new List<TEntity>();

        public IEnumerator<TEntity> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }
}