using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;

public partial class UserControls_DropDownListMultiSelected : System.Web.UI.UserControl
{
    // Created by M.RAGAB
    #region PrivateFields
    string source { get; set; }
    #endregion

    #region PublicProperties
    public List<string> SelectedValues { get { return hdn_SelectedValues.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList(); } }

    public DataTable DataSource
    {
        set
        {
            source = JSON_DataTable(value);
            hdn_items.Value = source;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "multiselectscript", ";Sys.Application.add_load(MakeDropDownMultiSelect);", true);
        }
    }
    public int Width { get; set; }

    public string Language { get; set; }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.ClientScript.RegisterStartupScript(this.GetType(), "multiselectscript", ";Sys.Application.add_load(MakeDropDownMultiSelect);", true);
        hdn_width.Value = Width.ToString();
        
        if (Language != null)
            hdn_Language.Value = Language;
        else hdn_Language.Value = "En";
    }

    #region DataTableToJsonObject
    public string JSON_DataTable(DataTable dt)
    {

        /****************************************************************************
        * Without goingin to the depth of the functioning
        * of this method, i will try to give an overview
        * As soon as this method gets a DataTable
        * it starts to convert it into JSON String,
        * it takes each row and ineach row it creates
        * an array of cells and in each cell is having its data
        * on the client side it is very usefull for direct binding of object to  TABLE.
        * Values Can be Access on clien in this way. OBJ.TABLE[0].ROW[0].CELL[0].DATA 
        * NOTE: One negative point. by this method user
        * will not be able to call any cell by its name.
        * *************************************************************************/

        StringBuilder JsonString = new StringBuilder();

        JsonString.Append("{ ");
        JsonString.Append("\"TABLE\":[{ ");
        JsonString.Append("\"ROW\":[ ");

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            JsonString.Append("{ ");
            JsonString.Append("\"COL\":[ ");

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (j < dt.Columns.Count - 1)
                {
                    JsonString.Append("{" + "\"DATA\":\"" +
                                      dt.Rows[i][j].ToString() + "\"},");
                }
                else if (j == dt.Columns.Count - 1)
                {
                    JsonString.Append("{" + "\"DATA\":\"" +
                                      dt.Rows[i][j].ToString() + "\"}");
                }
            }
            /*end Of String*/
            if (i == dt.Rows.Count - 1)
            {
                JsonString.Append("]} ");
            }
            else
            {
                JsonString.Append("]}, ");
            }
        }
        JsonString.Append("]}]}");
        return JsonString.ToString();
    }
    public string CreateJsonParameters(DataTable dt)
    {
        /* /****************************************************************************
         * Without goingin to the depth of the functioning
         * of this method, i will try to give an overview
         * As soon as this method gets a DataTable it starts to convert it into JSON String,
         * it takes each row and in each row it grabs the cell name and its data.
         * This kind of JSON is very usefull when developer have to have Column name of the .
         * Values Can be Access on clien in this way. OBJ.HEAD[0].<ColumnName>
         * NOTE: One negative point. by this method user
         * will not be able to call any cell by its index.
         * *************************************************************************/

        StringBuilder JsonString = new StringBuilder();

        //Exception Handling
        if (dt != null && dt.Rows.Count > 0)
        {
            JsonString.Append("{ ");
            JsonString.Append("\"Head\":[ ");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JsonString.Append("{ ");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j < dt.Columns.Count - 1)
                    {
                        JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() +
                              "\":" + "\"" +
                              dt.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == dt.Columns.Count - 1)
                    {
                        JsonString.Append("\"" +
                           dt.Columns[j].ColumnName.ToString() + "\":" +
                           "\"" + dt.Rows[i][j].ToString() + "\"");
                    }
                }

                /*end Of String*/
                if (i == dt.Rows.Count - 1)
                {
                    JsonString.Append("} ");
                }
                else
                {
                    JsonString.Append("}, ");
                }
            }

            JsonString.Append("]}");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }
    #endregion
}