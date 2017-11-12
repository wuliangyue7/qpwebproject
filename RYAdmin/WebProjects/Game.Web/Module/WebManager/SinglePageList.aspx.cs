using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Utils;
using Game.Web.UI;
using Game.Kernel;
using System.Text;
using Game.Entity.NativeWeb;
using Game.Entity.Accounts;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.WebManager
{
    public partial class SinglePageList : AdminPage
    {
        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if( !Page.IsPostBack )
            {
                BindData();
            }
        }

        protected void btnSave_Click( object sender, EventArgs e )
        {
            //判断权限
            AuthUserOperationPermission( Permission.Edit );
            ProcessData();
        }
        #endregion

        #region 数据加载

        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetList( SinglePage.Tablename, 1, 10000, "", " ORDER BY PageID ASC" );
            if( pagerSet.PageSet.Tables[0].Rows.Count > 0 )
            {
                rptDataList.DataSource = pagerSet.PageSet;
                rptDataList.DataBind();
            }

            SinglePage singlePage;
            if( IntParam == 0 )
            {
                singlePage = FacadeManage.aideNativeWebFacade.GetSinglePage( FacadeManage.aideNativeWebFacade.GetSinglePageMinID() );
            }
            else
            {
                singlePage = FacadeManage.aideNativeWebFacade.GetSinglePage( IntParam );
            }
            if( singlePage == null )
                return;

            CtrlHelper.SetText( lbPageName, singlePage.PageName );
            CtrlHelper.SetText( txtKeyWords, singlePage.KeyWords );
            CtrlHelper.SetText( txtDescription, singlePage.Description );
            CtrlHelper.SetText( txtContents, singlePage.Contents );
        }
        #endregion

        #region 处理方法

        private void ProcessData()
        {
            SinglePage singlePage;
            if( IntParam == 0 )
            {
                singlePage = FacadeManage.aideNativeWebFacade.GetSinglePage( FacadeManage.aideNativeWebFacade.GetSinglePageMinID() );
            }
            else
            {
                singlePage = FacadeManage.aideNativeWebFacade.GetSinglePage( IntParam );
            }
            if( singlePage == null )
            {
                ShowError( "修改失败，独立页不存在" );
                return;
            }
                
            singlePage.KeyWords = CtrlHelper.GetText( txtKeyWords );
            singlePage.Description = CtrlHelper.GetText( txtDescription );
            singlePage.Contents = CtrlHelper.GetText( txtContents );

            int result = FacadeManage.aideNativeWebFacade.UpdateSinglePage( singlePage );
            if( result > 0 )
            {
                ShowInfo( "修改成功" );
            }
            else 
            {
                ShowError( "修改失败" );
            }
        }
        #endregion
    }
}
