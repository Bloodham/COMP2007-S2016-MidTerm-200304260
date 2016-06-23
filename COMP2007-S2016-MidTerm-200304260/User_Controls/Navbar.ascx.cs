using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/**
 * @author: Tom Tsiliopoulos
 * @date: June 23, 2016
 * @version: 0.0.3 - updated SetActivePage Method to include Todo List
 */

namespace COMP2007_S2016_MidTerm_200304260
{
    public partial class Navbar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // check if a user is logged in
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {

                    // show the Contoso Content area
                    TodoPlaceHolder.Visible = true;
                    PublicPlaceHolder.Visible = false;

                
                }
                else
                {
                    // only show login and register
                    TodoPlaceHolder.Visible = false;
                    PublicPlaceHolder.Visible = true;
                    
                }
                SetActivePage();
            }
        }

        /**
         * This method adds a css class of "active" to list items
         * relating to the current page
         * 
         * @private
         * @method SetActivePage
         * @return {void}
         */
        private void SetActivePage()
        {
            switch (Page.Title)
            {
                case "Home Page":
                    home.Attributes.Add("class", "active");
                    break;
                case "Todo List":
                    todo.Attributes.Add("class", "active");
                    break;
                case "Login":
                    login.Attributes.Add("class", "active");
                    break;
                case "Register":
                    register.Attributes.Add("class", "active");
                    break;
                case "Logout":
                    logout.Attributes.Add("class", "active");
                    break;
            }
        }
    }
}