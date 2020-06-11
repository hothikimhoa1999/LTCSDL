using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// thêm các thư viện cần dùng
using LTCSDL.BLL;
using LTCSDL.DAL.Models;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;

namespace LTCSDL.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {
            _svc = new ProductsSvc();
        }

        //post data lên swagger vào mục get-by-id

        [HttpPost("get-by-id")]

        public IActionResult getProductById([FromBody]SimpleReq req)
        {
            var res = new SimpleRsp();
            var pro = _svc.Read(req.Id);
            res.Data = pro;
            return Ok(res);
        }

        //post data lên swagger vào mục get-all

        [HttpPost("get-all-product")]

        public IActionResult getAllProduct()
        {
            var res = new SimpleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        [HttpPost("get-cust-orderist")]

        public IActionResult getCustOrderHist([FromBody]SimpleReq req)
        {
            var res = new SimpleRsp();
            var pro = _svc.getCustOrderHist(req.Keyword);
            res.Data = pro;
            return Ok(res);
        }

        [HttpPost("get-cust-orderist_LinQ")]

        public IActionResult getCustOrderHist_LinQ([FromBody] SimpleReq req)
        {
            var res = new SimpleRsp();
            var pro = _svc.getCustOrderHist_LinQ(req.Keyword);
            res.Data = pro;
            return Ok(res);
        }

        [HttpPost("get-cust-order-details")]

        public IActionResult getCustOrdersDetail([FromBody] SimpleReq req)
        {
            var res = new SimpleRsp();
            var pro = _svc.getCustOrdersDetail(req.Id);
            res.Data = pro;
            return Ok(res);
        }

        [HttpPost("get-cust-order-details-liinQ")]

        public IActionResult getCustOrderDetails_LinQ([FromBody] SimpleReq req)
        {
            var res = new SimpleRsp();
            var pro = _svc.getCustOrderDetails_LinQ(req.Id);
            res.Data = pro;
            return Ok(res);
        }
        // controler cho việc search 
        [HttpPost("search-product")]
        public IActionResult SearchProduct([FromBody]SearchProductReq req)
        {
            var res = new SimpleRsp();
            var pros = _svc.SearchProduct(req.Keyword, req.Page, req.Size);
            res.Data = pros;
            return Ok(res);
        }

        //controler cho việc create product
        [HttpPost("create-product")]
        public IActionResult CreateProduct([FromBody]ProductsReq req)
        {
            var res = _svc.CreateProduct(req);
            return Ok(res);
        }

        //controler cho việc update product
        [HttpPost("update-product")]
        public IActionResult UpadateProduct([FromBody]ProductsReq req)
        {
            var res = _svc.UpdateProduct(req);
            return Ok(res);
        }
        private readonly ProductsSvc _svc;


        [HttpPost("get-cust-revenue")]
        public IActionResult getEmlandRevenue([FromBody]RevenueReq req)
        {
            var res = new SimpleRsp();
            var pro = _svc.getEmlandRevenue(req.dateF);
            res.Data = pro;
            return Ok(res);
        }

        [HttpPost("get-cust-revenue-linq")]
        public IActionResult getEmlandRevenue_LinQ([FromBody] RevenueReq req)
        {
            var res = new SimpleRsp();
            var pro = _svc.getEmlandRevenue(req.dateF);
            res.Data = pro;
            return Ok(res);
        }

        [HttpPost("get-cust-revenue-theongay")]
        public IActionResult getEmlandRevenuetheoNgay([FromBody] RevenueReq req)
        {
            var res = new SimpleRsp();
            var pro = _svc.getEmlandRevenuetheoNgay(req.dateF, req.dateT);
            res.Data = pro;
            return Ok(res);
        }
        [HttpPost("get-cust-revenue-theongay-linq")]
        public IActionResult getEmlandRevenuetheoNgay_LinQ([FromBody] RevenueReq req)
        {
            var res = new SimpleRsp();
            var pro = _svc.getEmlandRevenuetheoNgay_LinQ(req.dateF, req.dateT);
            res.Data = pro;
            return Ok(res);
        }
        [HttpPost("listOrder_Pagination")]

        public IActionResult listOrder_Pagination([FromBody] RevenueReq req )
        {
            var res = new SimpleRsp();
            var pro = _svc.listOrder_Pagination(req.dateF, req.dateT, req.page, req.size );
            res.Data = pro;
            return Ok(res);
        }
    }



}