using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homepage.Models
{
    interface ICartStrategy
    {
        double GetActPrice(double rawPrice);
    }
    class NormalStategy : ICartStrategy
    {
        public double GetActPrice(double rawPrice) => rawPrice;
    }
    public class CartItem
    {
        public SACH _sach { get; set; }
        public int _quantity { get; set; }
    }
    class Cart
    {
        public List<CartItem> items;
        private List<double> prices;
        private ICartStrategy Strategy { get; set; }
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public Cart(ICartStrategy strategy)
        {
            this.prices = new List<double>();
            this.Strategy = strategy;
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
                this.prices.Add(this.Strategy.GetActPrice(item._sach.GIA_BAN * item._quantity));
            }
            else
            {
                //item._quantity += _quan;
                this.prices.Add(this.Strategy.GetActPrice(item._sach.GIA_BAN * (item._quantity += _quan)));
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
        public void Total()
        {
            double sum = 0;
            foreach (var bookCost in this.prices)
            {
                sum += bookCost;
            }
            Console.Write($"Total : {sum}");
            this.prices.Clear();
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