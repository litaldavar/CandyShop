﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _appDbContext;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>
                ()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId
            };
        }

        public void AddToCart(Candy candy , int amount)
        {
            var shoppingCartItem = _appDbContext.ShoppingCartItems.FirstOrDefault(s => s.Candy.CandyId == candy.CandyId &&
            s.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Candy = candy,
                    Amount = amount
                };

                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _appDbContext.SaveChanges();
           
        }

        public int RemoveFromCart(Candy candy)
        {
            var shoppingCartItem = _appDbContext.ShoppingCartItems.FirstOrDefault(s => s.Candy.CandyId == candy.CandyId &&
           s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if(shoppingCartItem != null)
            {
                shoppingCartItem.Amount--;
                localAmount = shoppingCartItem.Amount;
                if (shoppingCartItem.Amount == 0)
                {                   
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
                
                _appDbContext.SaveChanges();               
            }

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??( ShoppingCartItems = _appDbContext.ShoppingCartItems.Where
                (c => c.ShoppingCartId == ShoppingCartId).Include(s => s.Candy).ToList());
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId);

            _appDbContext.RemoveRange(cartItems);
            _appDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
           

            decimal Total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Select(
                c => c.Candy.Price * c.Amount).Sum();



            return Total;
           
        }
    }
}
