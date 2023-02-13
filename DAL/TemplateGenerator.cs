using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace FGWHSEClient.DAL
{
    //public class TemplateGenerator
    //{
    //}

    public class TemplateGenerator : ITemplate // Class inheriting ITemplate
    {
        //ListItemType type;
        ButtonType type;
        string columnName;

       // public TemplateGenerator(ListItemType t, string cN)
        public TemplateGenerator(ButtonType t, string cN)
        {
            type = t;
            columnName = cN;
        }

        // Override InstantiateIn() method
        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {
            switch (type)
            {
               //case ListItemType.Item:
                case ButtonType.Button:
               //case ButtonType.Button:
                    //HyperLink hyprLnk = new HyperLink();
                    //hyprLnk.Target = "_blank"; //Optional.
                    //hyprLnk.DataBinding += new EventHandler(hyprLnk_DataBinding);
                    //container.Controls.Add(hyprLnk);
                    //break;

                    Button btnDel = new Button();
                    //btnDel.ID = "btnDel";
                    btnDel.CommandName = "Delete";
                    btnDel.Text = "Delete";
                    //btnDel.OnClientClick += "return refreshPage();";
                    //btnDel.CausesValidation = false;
                  //  btnDel.DataBinding += new EventHandler(btnDel_DataBinding);
                    btnDel.OnClientClick += "return refreshPage();";
                    container.Controls.Add(btnDel);
                    break;
            }
        }

        // The DataBinding event of your controls
        void btnDel_DataBinding(object sender, EventArgs e)
        {
            //HyperLink hyprlnk = (HyperLink)sender;
            //GridViewRow container = (GridViewRow)hyprlnk.NamingContainer;
            //object bindValue = DataBinder.Eval(container.DataItem, columnName);
            //// Adding check in case Column allows null values
            //if (bindValue != DBNull.Value)
            //{
            //    hyprlnk.Text = bindValue.ToString();
            //    hyprlnk.NavigateUrl = "http://www.google.com";
            //}

            Button btnDel = (Button)sender;
            GridViewRow container = (GridViewRow)btnDel.NamingContainer;
            //object bindValue = DataBinder.Eval(container.DataItem, columnName);
            //// Adding check in case Column allows null values
            //if (bindValue != DBNull.Value)
            //{
            //    hyprlnk.Text = bindValue.ToString();
            //    hyprlnk.NavigateUrl = "http://www.google.com";
            //}
        }
    }



}
