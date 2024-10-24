namespace Day6_Sample.Models;

public class Cart
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public string Image { get; set; }
    private int _totalPrice;

    public int TotalPrice
    {
        get => _totalPrice;
        set => _totalPrice = Price * Quantity;
    }
    public bool IsEnablePlus { get; set; }
}