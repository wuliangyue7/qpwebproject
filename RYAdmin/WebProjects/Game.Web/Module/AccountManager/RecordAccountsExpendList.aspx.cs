using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Kernel;
using System.Text;
using Game.Utils;
using Game.Entity.Accounts;
using Game.Facade;

namespace Game.Web.Module.AccountManager
{
    public partial class RecordAccountsExpendList : AdminPage
    {
        #region Fields

        /// <summary>
        /// 页面标题
        /// </summary>
        public string StrTitle
        {
            get
            {
                return "玩家-" + GetAccounts( IntParam ) + "-修改的" + typeName + "记录";
            }
        }

        protected int type = 0;
        protected string typeName = string.Empty;
        #endregion

        #region 窗口事件
        protected void Page_Load( object sender, EventArgs e )
        {
            type = GameRequest.GetInt( "type", 0 );
            switch( type )
            {
                case 0:
                    typeName = "帐号";
                    break;
                case 1:
                    typeName = "昵称";
                    break;
            }

            if( Header != null )
                Title = StrTitle;

            if( !IsPostBack )
            {
                BindData();
            }
        }
        #endregion

        #region 数据绑定

        //绑定数据
        private void BindData()
        {
            AccountsInfo account = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID( IntParam );
            if( account == null )
                return;

            PagerSet pagerSet = FacadeManage.aideRecordFacade.GetRecordAccountsExpendList( PageIndex, anpNews.PageSize, SearchItems, Orderby );
            anpNews.RecordCount = pagerSet.RecordCount;
            anpNews.CurrentPageIndex = PageIndex;
            if( pagerSet.PageSet.Tables[0].Rows.Count > 0 )
            {
                rptDataList.DataSource = pagerSet.PageSet;
                rptDataList.DataBind();
                rptDataList.Visible = true;
                litNoData.Visible = false;
            }
            else
            {
                rptDataList.Visible = false;
                litNoData.Visible = true;
            }
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

                    condition.Append( " WHERE UserID= " + IntParam.ToString() + " AND Type=" + type );

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
                    ViewState["Orderby"] = "ORDER BY CollectDate DESC";
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
