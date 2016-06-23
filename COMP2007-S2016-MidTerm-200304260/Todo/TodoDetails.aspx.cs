using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP2007_S2016_MidTerm_200304260.Models;
using System.Web.ModelBinding;



namespace COMP2007_S2016_MidTerm_200304260
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTodo();
            }
        }


        /*
        * <summary>
        * Updates the Todo database 
        * </summary>
        * @method GetTodo
        * @return {void}
        */
        protected void GetTodo()
        {
            // populate the form with existing data from the database
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            // connect to the EF DB
            using (TodoConnection db = new TodoConnection())
            {
                // populate a object with the todo ID
                Todo UpdatedTodo = (from todo in db.Todos
                                    where todo.TodoID == TodoID
                                    select todo).FirstOrDefault();

                // map the todo properties to the form controls
                if (UpdatedTodo != null)
                {
                    TodoNameTextBox.Text = UpdatedTodo.TodoName;
                    TodoNotesTextBox.Text = UpdatedTodo.TodoNotes;
                    if (UpdatedTodo.Completed == true)
                    {
                        CompletedCheckBox.Checked = true;
                    }
                    else
                    {
                        CompletedCheckBox.Checked = false;
                    }
                }
            }
        }

             protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Todo/TodoList.aspx");
        }

        /*
         * <summary>
         * Saves a Todo into the database
         * </summary>
         * @method SaveButton_Click
         * @return {void}
         */
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0) 
                {
                    // get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    // get the current todo from EF DB
                    newTodo = (from todo in db.Todos
                               where todo.TodoID == TodoID
                               select todo).FirstOrDefault();
                }

                // add new form data to the record
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;
                if (CompletedCheckBox.Checked == true)
                {
                    newTodo.Completed = true;
                }
                else
                {
                    newTodo.Completed = false;
                }

                // use LINQ to ADO.NET to add 
                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }
                // save our changes - also updates and inserts
                db.SaveChanges();
                // Redirect back to the updated TodoList page
                Response.Redirect("~/Todo/TodoList.aspx");
            }
        }


    }
  }
