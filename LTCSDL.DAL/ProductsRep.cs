using System;
using System.Collections.Generic;
using System.Text;
// khai báo các thư viện cần dùng
using LTCSDL.DAL;

namespace LTCSDL.DAL
{
    using System.Linq;
    using LTCSDL.Common.DAL;
    using LTCSDL.DAL.Models;
    using LTCSDL.Common.Rsp;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using System.Data;
    using System.Linq.Expressions;

    //khai báo thư việc cần dùng

    // mình cần dùng public nên để public hàm
    public class ProductsRep : GenericRep<NorthwindContext, Products> //đối tượng generic đc match zs northwind và products
    {
        #region -- Overrides --
        public override Products Read(int id)
        {
            var res = All.FirstOrDefault(p => p.ProductId == id); // lấy tất cả thông tin từ bảng products từ id products
            return res;
        }
        public int Remove(int id)
        {
            var m = base.All.FirstOrDefault(i => i.ProductId == id);
            m = base.Delete(m);
            return m.ProductId;
        }
        #endregion

        // viết hàm create product
        #region --Methods--
        /// <summary>
        /// Intialize
        /// </summary>

        public SimpleRsp CreateProduct(Products pro)
        {
            var res = new SimpleRsp();
            using (var context = new NorthwindContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Products.Add(pro);
                        context.SaveChanges();
                        tran.Commit(); //  có j thay đổi sẽ tự động update
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        // viết hàm Update
        public SimpleRsp UpdateProduct(Products pro)
        {
            var res = new SimpleRsp();
            using (var context = new NorthwindContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Products.Update(pro);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public object getCustOrderHist(string cusId)
        {
            List<object> res = new List<object>();
            var cmn = (SqlConnection)(Context.Database.GetDbConnection());
            if (cmn.State == ConnectionState.Closed)
                cmn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "CustOrderHist";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", cusId);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if(ds.Tables.Count >0 && ds.Tables[0].Rows.Count>0 )
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            ProductsName = row["ProductName"],
                            Total = row["Total"]

                        };
                        res.Add(x);
                    }
                }    
            }
             catch (Exception e)
            {
                res = null;
            }
            return res;  
             
        }

        public object getCustOrderHist_LinQ(string cusId)
        {
            var res = Context.Products
                .Join(Context.OrderDetails, a => a.ProductId, b => b.ProductId, (a, b) => new
                {
                    a.ProductId,
                    a.ProductName,
                    b.Quantity,
                    b.OrderId
                })
                .Join(Context.Orders, a => a.OrderId, b => b.OrderId, (a, b) => new
                {
                    a.ProductId,
                    a.ProductName,
                    a.Quantity,
                    a.OrderId,
                    b.CustomerId
                })
                .Where(x => x.CustomerId == cusId)
                .ToList();

            var data = res.GroupBy(x => x.ProductName)
                .Select(x => new
                {
                    ProductName = x.First().ProductName,
                    Total = x.Sum(p => p.Quantity)
                });
            return data;   
        }

        public object getCustOrdersDetail(int orderId)
        {
            List<object> res = new List<object>();
            var cmn = (SqlConnection)(Context.Database.GetDbConnection());
            if (cmn.State == ConnectionState.Closed)
                cmn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "CustOrdersDetail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                           
                            ProductName = row["ProductName"],
                            UnitPrice = row["UnitPrice"],
                            Quantity = row["Quantity"],
                            Discount = row["Discount"],
                            ExtendedPrice = row["ExtendedPrice"]
                            


                        };
                        res.Add(x);
                    }
                }
            }
            catch (Exception e)
            {
                res = null;
            }
            return res;

        }

        public object getCustOrderDetails(int orderId)
        {
            List<object> res = new List<object>();
            var cmn = (SqlConnection)(Context.Database.GetDbConnection());
            if (cmn.State == ConnectionState.Closed)
                cmn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "CustOrderDetails";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            ProductsName = row["ProductName"],
                            UnitPrice = row["UnitPrice"],
                            Quantity = row["Quantity"],
                            Discount = row["Discount"],
                            ExtendedPrice = row["ExtendedPrice"]

                        };
                        res.Add(x);
                    }
                }
            }
            catch (Exception e)
            {
                res = null;
            }
            return res;

        }

        public object getCustOrderDetails_LinQ(int orderId)
        {
            var res = Context.Products
               .Join(Context.OrderDetails, a => a.ProductId, b => b.ProductId, (a, b) => new
               {
                   a.ProductId,
                   a.ProductName,
                   b.Quantity,
                   b.OrderId,
                   b.UnitPrice,
                   b.Discount,
                   ExtendedPrice = b.Quantity * (1 - (decimal)b.Discount) * b.UnitPrice
               })
               
               .Where(x => x.OrderId == orderId )
               .ToList();


           
            return res;
        }

        public object getEmlandRevenue(DateTime ordDate)
        {
            List<object> res = new List<object>();
            var cmn = (SqlConnection)(Context.Database.GetDbConnection());
            if (cmn.State == ConnectionState.Closed)
                cmn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "getEmlandRevenue";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", ordDate);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                           
                            EmployeeID = row["EmployeeID"],
                            FirstName = row["FirstName"],
                            LastName = row["LastName"],
                            Revenue = row["Revenue"]

                        };
                        res.Add(x);
                    }
                }
            }
            catch (Exception e)
            {
                res = null;
            }
            return res;

        }

        public object getEmlandRevenue_LinQ(DateTime ordDate)
        {
            var res = Context.Orders
               .Join(Context.OrderDetails, a => a.OrderId, b => b.OrderId, (a, b) => new
               {
                   a.OrderDate,
                   a.EmployeeId,
                   Revenue = b.Quantity * (1 - (decimal)b.Discount) * b.UnitPrice
               })
               .Join(Context.Employees, a => a.EmployeeId, b => b.EmployeeId, (a, b) => new
               {
                   a.OrderDate,
                   a.EmployeeId,
                   a.Revenue ,
                   b.FirstName,
                   b.LastName
               })
               .Where(x => x.OrderDate.Value.Date == ordDate.Date)
               .ToList();

            var data = res.GroupBy(x => x.EmployeeId)
                .Select(x => new
                {
                   x.First().EmployeeId,
                    x.First().LastName,
                    x.First().FirstName,
                   
                    Revenue = x.Sum(p => p.Revenue)
                });


            return data;
        }

        public object getEmlandRevenuetheoNgay(DateTime dateF, DateTime dateT)
        {
            List<object> res = new List<object>();
            var cmn = (SqlConnection)(Context.Database.GetDbConnection());
            if (cmn.State == ConnectionState.Closed)
                cmn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "getEmlandRevenuetheoNgay";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dateStart",dateF);
                cmd.Parameters.AddWithValue("@dateEnd", dateT);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {

                            EmployeeID = row["EmployeeID"],
                            FirstName = row["FirstName"],
                            LastName = row["LastName"],
                            Revenue = row["Revenue"]

                        };
                        res.Add(x);
                    }
                }
            }
            catch (Exception e)
            {
                res = null;
            }
            return res;

        }

        public object getEmlandRevenuetheoNgay_LinQ(DateTime dateF, DateTime dateT)
        {
            var res = Context.Orders
               .Join(Context.OrderDetails, a => a.OrderId, b => b.OrderId, (a, b) => new
               {
                   a.OrderDate,
                   a.EmployeeId,
                   Revenue = b.Quantity * (1 - (decimal)b.Discount) * b.UnitPrice
               })
               .Join(Context.Employees, a => a.EmployeeId, b => b.EmployeeId, (a, b) => new
               {
                   a.OrderDate,
                   a.EmployeeId,
                   a.Revenue,
                   b.FirstName,
                   b.LastName
               })
               .Where(x => x.OrderDate >= dateF && x.OrderDate <= dateT)
               .ToList();

            var data = res.GroupBy(x => x.EmployeeId)
                .Select(x => new
                {
                    x.First().EmployeeId,
                    x.First().LastName,
                    x.First().FirstName,
                    Revenue = x.Sum(p => p.Revenue)
                });


            return data;
        }

        public object listOrder_Pagination(DateTime dateF, DateTime dateT, int page, int size)
        {
            List<object> res = new List<object>();
            var cmn = (SqlConnection)(Context.Database.GetDbConnection());
            if (cmn.State == ConnectionState.Closed)
                cmn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "listOrder_Paginations";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dateF", dateF);
                cmd.Parameters.AddWithValue("@dateT", dateT);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@size", size);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            OrderID = row["OrderID"],
                            CustomerID = row["CustomerID"],
                            OrderDate = row["OrderDate"],
                            ShipCountry = row["ShipCountry"]
                        };
                        res.Add(x);
                    }
                }
            }
            catch (Exception e)
            {
                res = null;
            }
            return res;

        }


        #endregion
    }
}