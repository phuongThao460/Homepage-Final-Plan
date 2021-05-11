using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homepage.Models
{
    public class CartItem
    {
        public SACH _sach { get; set; }
        public int _quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add_Product_Cart(SACH _sa, int _quan = 1)
        {
            var item = Items.FirstOrDefault(s => s._sach.ID_SACH == _sa.ID_SACH);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _sach = _sa,
                    _quantity = _quan
                });
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
                item._quantity = _new_quan;
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