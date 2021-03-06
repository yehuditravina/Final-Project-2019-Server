﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.Entity;

namespace BLL
{
    public class SaleOrderService : BaseService
    {
        public List<SaleOrder> GetAllSaleOrders()
        {
            return db.SaleOrders.ToList();
        }


        public List<SaleOrder> GetAllSaleOrdersOfCustomer(int customerId)
        {
            List<SaleOrder> list = db.SaleOrders
                .Where(saleOrders => saleOrders.CustomerId == customerId)
                .Include(x => x.Customer)
                .ToList();
            return list;
        }

        public SaleOrder GetSaleOrder(int orderId)
        {
            SaleOrder saleOrder = db.SaleOrders
          .Include(x => x.Customer)
          .Include(y => y.SaleOrderProducts.Select(x => x.Product))
          .ToList()
           .FirstOrDefault(so => so.ID == orderId);

            return saleOrder;
        }
        public SaleOrder GetSaleOrderById(int id)
        {
            return db.SaleOrders.FirstOrDefault(so => so.ID == id);
            //לבדוק אם מביא גם את כל הרשימה של המוצרים לטבלת מוצרים שנרכשו
        }

        public SaleOrder AddSaleOrder(SaleOrder saleOrder)
        {
           SaleOrder s =  db.SaleOrders.Add(saleOrder);
            db.SaveChanges();
            MailViaGmailService ms = new MailViaGmailService();
            ms.sendOrderMessenge(saleOrder);
            return s;
        }

        public void DeleteSaleOrder(int id)
        {
            SaleOrder saleOrder = db.SaleOrders.FirstOrDefault(so => so.ID == id);
            db.SaleOrders.Remove(saleOrder);
            //לבדוק אם מוחק גם את כל הרשימה של המוצרים לטבלת מוצרים שנרכשו

            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                db.SaveChanges();
                System.Diagnostics.Debugger.Launch();
            }
        }

        public void EditSaleOrder(SaleOrder saleOrder)
        {

        }

        #region toCheckIfItNeeded
        public void EditSaleOrderProduct(SaleOrderProduct saleOrderProduct)
        {

        }

        public void DeleteSaleOrderProduct(int saleOrderProductId)
        {

        }
        public void EditSaleOrderProduct(int saleOrderProductId)
        {

        }
        #endregion

    }
}
