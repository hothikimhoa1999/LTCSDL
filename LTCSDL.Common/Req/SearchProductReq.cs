using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class SearchProductReq
    {
        public int Size {get; set;}
        public int Page { get; set; }
        public string Keyword { get; set; }
        public int Id { get; set; }
        public string Type { get; set; } 
    }
}
