﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class RevenueReq
    {
        public DateTime dateF { get; set; }
        public DateTime dateT { get; set; }
        public int  page { get; set; }
        public int size { get; set; }
    }
}
