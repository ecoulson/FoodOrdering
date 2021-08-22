namespace Menu
{
    public interface IMenuItem
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
        int Cost { get; }
    }
}
