﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.Model
{
    [Serializable]
    public partial class STOCK_PRODUCT_STOCK
    {
        public ActionEnum Action;
        public Nullable<int> STOCK_BEFORE { get; set; }
    }
}
