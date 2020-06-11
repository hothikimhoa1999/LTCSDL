using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{ // khởi tạo hàm lấy thông tin từ bảng product
    public class ProductsReq
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
    public class SimpleReq : BaseReq<BaseModel>
    {
        
        #region -- Overrides --

        /// <summary>
        /// Convert the request to the model
        /// </summary>
        /// <param name="createdBy">Created by</param>
        /// <returns>Return the result</returns>
        public override BaseModel ToModel(int? createdBy = null)
        {
            return new BaseModel(Id);
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public SimpleReq() : base() { }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="id">ID</param>
        public SimpleReq(int id) : base(id) { }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="keyword">Keyword</param>
        public SimpleReq(string keyword) : base(keyword) { }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="size">Size</param>
        //public SimpleReq(int size) : base(size) { }
     
        #endregion
    }
}