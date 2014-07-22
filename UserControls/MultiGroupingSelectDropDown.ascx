<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultiGroupingSelectDropDown.ascx.cs"
    Inherits="UserControls_DropDownListMultiSelected" %>
<script type="text/javascript" language="javascript">

    $(document).ready(function () {
        Sys.Application.add_load(SetWidthAndArabicFormat);
        Sys.Application.add_load(MultiSelectBindEvents);

    });

    function SetWidthAndArabicFormat(sender, args) {
        var lang = document.getElementById('<%=hdn_Language.ClientID%>').value;

                if (lang == "ar") {
                    $("#MultiSelectDropDownCheckbox").multiselect({
                        checkAllText: "تحديد الكل",
                        uncheckAllText: "حذف الكل ",
                        noneSelectedText: "إختار",
                        selectedText: "إخترت #"

                    });

                    // start button styling for arabic 
                    $(".ui-multiselect-menu").css("text-align", "right");
                    $(".ui-multiselect").css("text-align", "right");

                    // start styling of header  (checkall , uncheckall , close icon )
                    $(".ui-multiselect-header").css("width", "98%").css("position", "relative").css("text-align", "right");
                    $(".ui-multiselect-header ul li").css("float", "right");
                    $(".ui-multiselect-header li.ui-multiselect-close").css("float", "left");
                }

        //set width 
        $("#MultiSelectDropDownCheckbox").multiselect({
            minWidth: document.getElementById('<%=hdn_width.ClientID%>').value
        });
    }

    function MultiSelectBindEvents() {

        $("#MultiSelectDropDownCheckbox").multiselect({ selectedList: 1 });  // set multiselect effect

        $("#MultiSelectDropDownCheckbox").bind("multiselectclick", function (event, ui) {
            /* event: the original event object ui.value: value of the checkbox ui.text: text of the checkbox ui.checked: whether or not the input was checked or unchecked (boolean) */
            var array_of_checked_values = $("select").multiselect("getChecked").map(function () {
                return this.value;
            }).get();

            var hid = document.getElementById('<%=hdn_SelectedValues.ClientID%>');
            hid.value = array_of_checked_values;
           
        });


        $('#MultiSelectDropDownCheckbox').multiselect('refresh');
        $('.temp').hide();
    }

    function MakeDropDownMultiSelect() {
        // recieve data table in hdn field and fill in Dropdown items 

        var hdnval = document.getElementById('<%=hdn_items.ClientID%>').value;
        if (hdnval != '') {
            var oServerJSON = eval("(" + hdnval + ")"); // recieve server data 

            var options = $("#MultiSelectDropDownCheckbox"); // get my select options 

            for (var i = 0; i < oServerJSON.TABLE[0].ROW.length; i++) {

                if (oServerJSON.TABLE[0].ROW[i].COL[2].DATA == "True") {
                    // item is group 
                    options.append($('<optgroup><option class="temp"></option></optgroup>').attr('label', oServerJSON.TABLE[0].ROW[i].COL[1].DATA));
                } else {// item is option 
                    if (oServerJSON.TABLE[0].ROW[i].COL[3].DATA == "True") {
                        // item is selected option 
                        options.append($('<option selected="selected"></option>').attr('value', oServerJSON.TABLE[0].ROW[i].COL[0].DATA).text(oServerJSON.TABLE[0].ROW[i].COL[1].DATA));
                    } else {
                        // item is option but not selected  
                        options.append($('<option></option>').attr('value', oServerJSON.TABLE[0].ROW[i].COL[0].DATA).text(oServerJSON.TABLE[0].ROW[i].COL[1].DATA));
                    }
                }
            }
            $('#MultiSelectDropDownCheckbox').multiselect('refresh');
            $('.temp').hide();  
        }

    } 
     
</script>
<asp:HiddenField runat="server" ID="hdn_SelectedValues" />
<asp:HiddenField runat="server" ID="hdn_items" />
<asp:HiddenField runat="server" ID="hdn_width" />
<asp:HiddenField runat="server" ID="hdn_Language" />
<select id="MultiSelectDropDownCheckbox" multiple="multiple">
</select>
