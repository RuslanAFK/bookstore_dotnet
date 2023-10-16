namespace Domain.Models;

public class ListResponse<T>
{
    public ICollection<T> Items { get; set; } = new List<T>();
    public int Count { get; set; }
}