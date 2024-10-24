using System.Collections.ObjectModel;

namespace Day6_Sample.Models;

public class ListCart
{
    public static ObservableCollection<Cart> CartList { get; set; }

    public static ObservableCollection<Cart> GetInstance()
    {
        if (CartList is null) return CartList = new ObservableCollection<Cart>();
        return CartList;
    }
}