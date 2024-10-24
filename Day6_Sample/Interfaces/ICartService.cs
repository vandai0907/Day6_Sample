using Day6_Sample.Models;
using System.Collections.ObjectModel;

namespace Day6_Sample.Interfaces
{
    interface ICartService
    {
        void AddToCart(Product product);
        void UpdateCart(Cart cart, int quantity);
        void DeleteCart(Cart cart);
        int GetTotalCart();
        ObservableCollection<Cart> GetCart();
        Cart GetCartById(int id);
        void Order();
    }
}
