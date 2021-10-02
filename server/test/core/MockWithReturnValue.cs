
namespace todo_app_test
{
    public abstract class MockWithReturnValue<T>
    {
        public T ReturnValue { get; protected set; }
    }
}