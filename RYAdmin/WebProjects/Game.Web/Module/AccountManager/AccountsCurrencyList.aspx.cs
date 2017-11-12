using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Entity.NativeWeb;
using Game.Entity.Treasure;

namespace Game.Web.Module.AccountManager
{
    public partial class AccountsCurrencyList : AdminPage
    {
        #region 属性

        /// <summary>
        /// 排序条件
        /// </summary>
        public string Orderby
        {
            get
            {
                if( ViewState["Orderby"] == null )
                {
                    if( !string.IsNullOrEmpty( OrderField ) )
                    {
                        ViewState["Orderby"] = "ORDER BY " + OrderField + " " + OrderType;
                    }
                    else
                    {
                        ViewState["Orderby"] = "ORDER BY UserID DESC";
                    }
                }
                return (string)ViewState["Orderby"];
            }
            set
            {
                ViewState["Orderby"] = value;
            }
        }

        #endregion

        #region 窗口事件
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                //绑定用户
                DataBindList();
            }
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpPage_PageChanged( object src, EventArgs e )
        {
            DataBindList();
        }

        /// <summary>
        /// 点击查询
        /// </summary>
        protected void btnQuery_Click( object sender, EventArgs e )
        {
            anpPage.CurrentPageIndex = 1;
            DataBindList();
        }

        /// <summary>
        /// 点击刷新
        /// </summary>
        protected void btnRefresh_Click( object sender, EventArgs e )
        {
            Redirect( "AccountsCurrencyList.aspx" );
        }
        #endregion

        #region 窗口方法
        private void DataBindList()
        {
            //验证数据
            string queryContent = CtrlHelper.GetTextAndFilter(txtSearch);

            //查询条件
            StringBuilder condition = new StringBuilder();
            condition.Append( " WHERE (1=1) " );
            if( queryContent != "" )
            {
                int userID = GetUserIDByAccount( queryContent );
                if( userID != 0 )
                {
                    condition.AppendFormat( " AND UserID={0}", userID );
                }
                else
                {
                    rpDataList.Visible = false;
                    litNoData.Visible = true;
                    return;
                }
            }

            //查询数据
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList( UserCurrencyInfo.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, condition.ToString(), Orderby );
            anpPage.RecordCount = pagerSet.RecordCount;
            if( pagerSet.PageSet.Tables[0].Rows.Count > 0 )
            {
                rpDataList.DataSource = pagerSet.PageSet;
                rpDataList.DataBind();
                rpDataList.Visible = true;
                litNoData.Visible = false;
            }
            else
            {
                rpDataList.Visible = false;
                litNoData.Visible = true;
            }
        }
        #endregion
    }
}