namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public record HiLoValue(int Low, int High)
    {
        public bool HasExceeded() => Low > High;
        public HiLoValue NextValue() => new(Low + 1, High);
    }
}