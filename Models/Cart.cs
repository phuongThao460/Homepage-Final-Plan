using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homepage.Models
{
    public interface ICartStrategy
    {
        CartItem Items(SACH _sa, int quan);
    }
    class NormalStategy : ICartStrategy
    {
        public CartItem Items(SACH _sa, int quan)
        {
            return (new CartItem
            {
                _sach = _sa,
                _quantity = quan
            });
        }
    }
    class UpdatePriceStrategy : ICartStrategy
    {
        public CartItem Items(SACH _sa, int quan)
        {
            return (new CartItem
            {
                _sach = _sa,
                _quantity = quan
            });
        }
    }
    public class CartItem
    {
        public SACH _sach { get; set; }
        public int _quantity { get; set; }
    }
    public class Cart
    {
        public List<CartItem> items;
        public ICartStrategy Strategy { get; set; }
        public Cart(ICartStrategy strategy)
        {
            this.items = new List<CartItem>();
            this.Strategy = strategy;
        }
        public void Add_Product_Cart(SACH _sa, int _quan = 1)
        {
            var normalStrategy = new NormalStategy();
            var item = items.FirstOrDefault(s => s._sach.ID_SACH == _sa.ID_SACH);
            if (item == null)
            {
                this.items.Add(this.Strategy.Items( _sa, _quan));
            }
            else
            {
                item._quantity += _quan;
            }
            
        }
        public int Total_quantity()
        {
            return items.Sum(s => s._quantity);
        }
        public decimal Total_money()
        {
            var total = items.Sum(s => s._quantity * s._sach.GIA_BAN);
            return (decimal)total;
        }
        public void Update_quantity(int id, int _new_quan)
        {
            var item = items.Find(s => s._sach.ID_SACH == id);
            if (item != null)
            {
                if (items.Find(s => s._sach.SOLUONG_TON > _new_quan) != null)
                {
                    item._quantity = _new_quan;
                }
                else 
                { 
                    item._quantity = (int)item._sach.SOLUONG_TON; 
                }
            }
        }
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._sach.ID_SACH == id);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}