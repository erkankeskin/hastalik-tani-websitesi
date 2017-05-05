using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    List<string> symptomlist = new List<string>();
    DataTable dt = new DataTable();
    UserDataAccess uDataAccess;
    protected void Page_Load(object sender, EventArgs e)
    {
        uDataAccess = new UserDataAccess();
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        dt = uDataAccess.GetSymptoms(part.Value);
        

        dl1.DataSource = dt;
        dl1.DataBind();
        

    }
    
    protected void cb_CheckedChanged(object sender, EventArgs e)
    {
        var cb = (sender as CheckBox);
        if (cb.Checked)
        {
            symString.Value += cb.Text +"-";//+cb.Attributes["symID"]
            
        }
        else
        {
            symString.Value = symString.Value.Replace(cb.Text + "-", "");
            
        }
        Label1.Text = symString.Value;

        dt = uDataAccess.Getdisease(symString.Value);
        symtomes.DataSource = dt;
        symtomes.DataBind();

    }
    private string printsymptomes(List<string> list1)
    {
        string total="";

        total = symString.Value;

        //total = (list1.Count).ToString();

        //for (int i = 0; i < list1.Count; i++)
        //{
        //    total = total + list1[i];
        //}

            return total;
    }
}