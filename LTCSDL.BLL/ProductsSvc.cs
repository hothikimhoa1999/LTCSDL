using System;
using System.Collections.Generic;
using System.Text;
// thêm thư viện
using LTCSDL.Common.BLL;
using LTCSDL.Common.Rsp;

namespace LTCSDL.BLL
{
    //thêm thư viện
    using DAL;
    using DAL.Models;
    using LTCSDL.Common.Req;
    using System.Linq;

    public class ProductsSvc: GenericSvc<ProductsRep, Products>
    {
        #region -- Overrides --
        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override SimpleRsp Read(int id)
        {
            var res = new SimpleRsp();
            var m = _rep.Read(id);
            res.Data = m;
            return res;
        }
        #endregion
        public override SimpleRsp Update(Products m)
        {
            var res = new SimpleRsp();
            var m1 = m.ProductId > 0 ? _rep.Read(m.ProductId) : _rep.Read(m.ProductName);
            if (m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;
            }
            return res;
        }

        //hàm search products
        
            /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return list product</returns>
        public object SearchProduct(string keyword, int page, int size)
        {
            var pro = All.Where(x => x.ProductName.Contains(keyword)); // contains giống so sánh like trên sql

            var offset = (page - 1) * size; // offset giốgn index
            var total = pro.Count();
            int totalPages = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
            var data = pro.OrderBy(x => x.ProductName).Skip(offset).Take(size).ToList(); //skip là phân trang đầu lấy vt offsetvà lấy cái size để phân trang

            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPages = totalPages,
                Page = page,
                Size = size
            };
            return res;
        }

        // hàm create product

        public SimpleRsp CreateProduct(ProductsReq pro)
        {
            var res = new SimpleRsp();

            //gán đối tượng từ rep sang đối tượng database
            Products products = new Products();
            products.ProductId = pro.ProductId;
            products.ProductName = pro.ProductName;
            products.SupplierId = pro.SupplierId;
            products.CategoryId = pro.CategoryId;
            products.QuantityPerUnit = pro.QuantityPerUnit;
            products.UnitPrice = pro.UnitPrice;
            products.UnitsInStock = pro.UnitsInStock;
            products.ReorderLevel = pro.ReorderLevel;
            products.Discontinued = pro.Discontinued;

            // xử lý create 
            res = _rep.CreateProduct(products);

            return res;
        }

        public object UpdateProduct(ProductsReq req)
        {
            var res = new SimpleRsp();

            //gán đối tượng từ rep sang đối tượng database
            Products products = new Products();
            products.ProductId = req.ProductId;
            products.ProductName = req.ProductName;
            products.SupplierId = req.SupplierId;
            products.CategoryId = req.CategoryId;
            products.QuantityPerUnit = req.QuantityPerUnit;
            products.UnitPrice = req.UnitPrice;
            products.UnitsInStock = req.UnitsInStock;
            products.ReorderLevel = req.ReorderLevel;
            products.Discontinued = req.Discontinued;

            //xử lý update
            res = _rep.UpdateProduct(products);

            return res;
        }

        public object getCustOrdersDetail(object dateF)
        {
            throw new NotImplementedException();
        }

        //viết hàm update product
        public SimpleRsp UpdateProduct (Products pro)
        {
            var res = new SimpleRsp();

            //gán đối tượng từ rep sang đối tượng database
            Products products = new Products();
            products.ProductId = pro.ProductId;
            products.ProductName = pro.ProductName;
            products.SupplierId = pro.SupplierId;
            products.CategoryId = pro.CategoryId;
            products.QuantityPerUnit = pro.QuantityPerUnit;
            products.UnitPrice = pro.UnitPrice;
            products.UnitsInStock = pro.UnitsInStock;
            products.ReorderLevel = pro.ReorderLevel;
            products.Discontinued = pro.Discontinued;

            //xử lý update
            res = _rep.UpdateProduct(products);
            
            return res;
        }

        public object getCustOrderHist(string cusId)
        {
            return _rep.getCustOrderHist(cusId);
        }

        public object getCustOrderHist_LinQ(string cusId)
        {
            return _rep.getCustOrderHist_LinQ(cusId);
        }
        public object getCustOrdersDetail(int orderId)
        {
            return _rep.getCustOrdersDetail(orderId);
        }

        public object getCustOrderDetails_LinQ(int orderId)
        {
            return _rep.getCustOrderDetails_LinQ(orderId);
        }

        public object getEmlandRevenue(DateTime ordDate)
        {
            return _rep.getEmlandRevenue(ordDate);
        }

        public object getEmlandRevenue_LinQ(DateTime ordDate)
        {
            return _rep.getEmlandRevenue_LinQ(ordDate);
        }

        public object getEmlandRevenuetheoNgay(DateTime dateF, DateTime dateT)
        {
            return _rep.getEmlandRevenuetheoNgay(dateF, dateT);
        }

        public object getEmlandRevenuetheoNgay_LinQ(DateTime dateF, DateTime dateT)
        {
            return _rep.getEmlandRevenuetheoNgay_LinQ(dateF, dateT);
        }

        public object listOrder_Pagination(DateTime dateF, DateTime dateT, int page, int size)
        {
            return _rep.listOrder_Pagination(dateF, dateT,page, size );
        }

    }
}
