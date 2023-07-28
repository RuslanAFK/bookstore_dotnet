namespace Domain.Models;

public class ListResponse<T>
{
    public ICollection<T> Items { get; set; }
    public int Count { get; set; }
}