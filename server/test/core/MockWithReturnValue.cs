
namespace todo_app_test
{
    public abstract class MockWithReturnValue<T>
    {
        public virtual void SetValue(T value) => ReturnValue = value;
        public T ReturnValue { get; protected set; }
    }
}