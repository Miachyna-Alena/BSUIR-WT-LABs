using Miachyna.Domain.Entities;

namespace Miachyna.Domain.Models
{
    public class CartItem
    {
        public Cosmetic Item { get; set; }
        public int Qty { get; set; }
    }
}
