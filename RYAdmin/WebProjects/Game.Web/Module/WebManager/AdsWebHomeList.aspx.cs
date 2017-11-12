using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Web.UI;
using Game.Kernel;
using Game.Facade;
using System.Data;
using Game.Entity.Enum;
using Game.Entity.NativeWeb;

namespace Game.Web.Module.WebManager
{
    public partial class AdsWebHomeList : AdminPage
    {
        #region 窗口事件
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                BindAds();
            }
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        protected void btnDelete_Click( object sender, EventArgs e )
        {
            AuthUserOperationPermission( Permission.Delete );
            string strQuery = "WHERE ID IN (" + StrCIdList + ")";
            try
            {
                FacadeManage.aideNativeWebFacade.DeleteAds( strQuery );
                ShowInfo( "删除成功" );
            }
            catch
            {
                ShowError( "删除失败" );
            }
            BindAds();
        }
        #endregion

        #region 窗口方法
        private void BindAds()
        {
            //控件绑定
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetList( "Ads", 1, 1000, " WHERE Type=0", "ORDER BY ID ASC" );
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rpData.DataSource = pagerSet.PageSet;
            rpData.DataBind();
        }
        #endregion
    }
}
