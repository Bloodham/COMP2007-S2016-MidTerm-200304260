using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP2007_S2016_MidTerm_200304260.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;


namespace COMP2007_S2016_MidTerm_200304260
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //if loading page for the first time, populate the grid
            if (!IsPostBack)
            {
                //get the todo list data
                this.GetTodo();
            }
        }
        /*
         * <summary>
         * Grabs the todo from database and puts into a gridview
         * </summary>
         * @method GetTodo
         * @return {void}
         */
        protected void GetTodo()
        {
            // connect to EF
            //connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                //query the Games table using EF and LINQ
                var Todo = (from allTodo in db.Todos
                             select allTodo);

                //bind results to gridview
                TodoGridView.DataSource = Todo.AsQueryable().ToList();
                TodoGridView.DataBind();
            }
        }

        /*
         * <summary>
         * This allows the list to be paginated
         * </summary>
         * @method TodosGridView_PageIndexChanging
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
         */

        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store which row was clicked
            int selectedRow = e.RowIndex;

            //get the selected todoid using the grids datakey collection
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoID"]);

            //use ef to find the selected todo and delete it
            using (TodoConnection db = new TodoConnection())
            {
                //create object of the todo class and store the query string inside of it
                Todo deletedTodo = (from todoRecords in db.Todos
                                    where todoRecords.TodoID == TodoID
                                    select todoRecords).FirstOrDefault();

                //remove the selected todo from the db
                db.Todos.Remove(deletedTodo);

                //save db changes
                db.SaveChanges();

                //refresh gridview
                this.GetTodo();

            }
        }

        /*
        * <summary>
        * Allows the user to change the page
        * </summary>
        * @method TodosGridView_PageIndexChanging
        * @param {object} sender
        * @param {GridViewPageEventArgs} e
        * @returns {void}
        */

        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Set the new page number
            TodoGridView.PageIndex = e.NewPageIndex;

            //refresh the grid
            this.GetTodo();
        }

        /*
        * <summary>
        * Allows the user to change the amount of different items displayed at once
        * </summary>
        * @method PageSizeDropDownList_SelectedIndexChanged
        * @param {object} sender
        * @param {GridViewPageEventArgs} e
        * @returns {void}
        */

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the new page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            //refresh
            this.GetTodo();
        }

    }
}