using Miachyna.Domain.Entities;

namespace Miachyna.Domain.Models
{
    public class Cart
    {
        public int Id { get; set; }
        /// <summary>
        /// Список объектов в корзине
        /// key - идентификатор объекта
        /// </summary>
        public Dictionary<int, CartItem> CartItems { get; set; } = new();
        /// <summary>
        /// Добавить объект в корзину
        /// </summary>
        /// <param name="cosmetic">Добавляемый объект</param>
        public virtual void AddToCart(Cosmetic cosmetic)
        {
            if (CartItems.ContainsKey(cosmetic.Id))
            {
                CartItems[cosmetic.Id].Qty++;
            }
            else
            {
                CartItems.Add(cosmetic.Id, new CartItem
                {
                    Item = cosmetic,
                    Qty = 1
                });
            };
        }
        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="cosmetic">удаляемый объект</param>
        public virtual void RemoveItems(int id)
        {
            CartItems.Remove(id);
        }
        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            CartItems.Clear();
        }
        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count { get => CartItems.Sum(item => item.Value.Qty); }
        /// <summary>
        /// Общая стоимость
        /// </summary>
        public double TotalPrice
        {
            get => CartItems.Sum(item => item.Value.Item.Price * item.Value.Qty);
        }
    }
}
