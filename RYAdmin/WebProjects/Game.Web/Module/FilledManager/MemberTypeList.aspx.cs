using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Kernel;
using Game.Web.UI;
using Game.Facade;
using Game.Entity.Enum;
using Game.Entity.Treasure;
using System.Text;
using Game.Entity.Accounts;

namespace Game.Web.Module.FilledManager
{
    public partial class MemberTypeList : AdminPage
    {
        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                BindData();
            }
        }

        protected void anpPage_PageChanged( object sender, EventArgs e )
        {
            BindData();
        }

        //查询
        protected void btnQuery_Click( object sender, EventArgs e )
        {           
            BindData();
        }

        #endregion

        #region 数据绑定

        //绑定数据
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aideAccountsFacade.GetList(MemberProperty.Tablename, 1, 100, SearchItems, Orderby);
            if( pagerSet.PageSet.Tables[0].Rows.Count > 0 )
            {
                litNoData.Visible = false;
            }
            else
            {
                litNoData.Visible = true;
            }

            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchItems
        {
            get
            {
                if( ViewState["SearchItems"] == null )
                {
                    StringBuilder condition = new StringBuilder();
                    condition.Append( " WHERE 1=1 " );

                    ViewState["SearchItems"] = condition.ToString();
                }

                return (string)ViewState["SearchItems"];
            }
            set
            {
                ViewState["SearchItems"] = value;
            }
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        public string Orderby
        {
            get
            {
                if( ViewState["Orderby"] == null )
                {
                    ViewState["Orderby"] = "ORDER BY MemberOrder ASC";
                }

                return (string)ViewState["Orderby"];
            }

            set
            {
                ViewState["Orderby"] = value;
            }
        }

        #endregion
    }
}
