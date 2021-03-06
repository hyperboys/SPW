﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.Model;

namespace SPW.UI.Web.MasterPage
{
    public partial class MasterPageMainAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                USER userItem = Session["user"] as USER;

                foreach (ROLE_FUNCTION item in userItem.ROLE.ROLE_FUNCTION)
                {
                    switch (item.FUNCTION_ID.ToString())
                    {
                        case "1":
                            {
                                this.MainAdmin.Visible = true;
                            } break;
                        case "2":
                            {
                                this.SystemData.Visible = true;
                            } break;
                        case "3":
                            {
                                this.StandardData.Visible = true;
                            } break;
                        case "4":
                            {
                                this.OrderData.Visible = true;
                            } break;
                    }
                }
            }
        }
    }
}