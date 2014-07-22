using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    DataTable Service_Data;
    protected void Page_Load(object sender, EventArgs e)
    {
        Service_Data = new DataTable();
        Service_Data.Columns.Add("Value", typeof(string));
        Service_Data.Columns.Add("Text", typeof(string));
        Service_Data.Columns.Add("IsGroup", typeof(bool));
        Service_Data.Columns.Add("IsChecked", typeof(bool));

        Service_Data.Rows.Add("G1", "Group1 ", true, false);
        for (int i = 0; i < 3; i++)
        {
            Service_Data.Rows.Add(i, "Text of " + i, false, false);
        }

        Service_Data.Rows.Add("G2", "Group2 ", true, false);
        for (int i = 4; i < 6; i++)
        {
            Service_Data.Rows.Add(i, "Taxt of " + i, false, false);
        }


        MS_Services.DataSource = Service_Data;
    }
}