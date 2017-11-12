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

namespace Game.Web.Module.AppManager
{
    public partial class SiteConfig : AdminPage
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
          

            ConfigInfo config;
            if( IntParam == 0 )
            {
                return;
            }
            else 
            {
                config = FacadeManage.aideNativeWebFacade.GetConfigInfo( IntParam );
            }
            if( config == null )
                return;

            CtrlHelper.SetText( txtConfigKey, config.ConfigKey );
            CtrlHelper.SetText( txtConfigName, config.ConfigName );
            CtrlHelper.SetText( txtConfigString, config.ConfigString );
            CtrlHelper.SetText( txtField1, config.Field1 );
            CtrlHelper.SetText( txtField2, config.Field2 );
            CtrlHelper.SetText( txtField3, config.Field3 );
            CtrlHelper.SetText( txtField4, config.Field4 );
            CtrlHelper.SetText( txtField5, config.Field5 );
            CtrlHelper.SetText( txtField6, config.Field6 );
            CtrlHelper.SetText( txtField7, config.Field7 );
            CtrlHelper.SetText( txtField8, config.Field8 );
        }
        #endregion

        #region 处理方法

        private void ProcessData()
        {
            ConfigInfo config;
            if( IntParam == 0 )
            {
                return;
            }
            else
            {
                config = FacadeManage.aideNativeWebFacade.GetConfigInfo( IntParam );
            }
            if( config == null )
                return;

            config.ConfigString = CtrlHelper.GetText( txtConfigString );
            config.Field1 = CtrlHelper.GetText( txtField1);
            config.Field2 = CtrlHelper.GetText( txtField2 );
            config.Field3 = CtrlHelper.GetText( txtField3 );
            config.Field4 = CtrlHelper.GetText( txtField4 );
            config.Field5 = CtrlHelper.GetText( txtField5 );
            config.Field6 = CtrlHelper.GetText( txtField6 );
            config.Field7 = CtrlHelper.GetText( txtField7 );
            config.Field8 = CtrlHelper.GetText( txtField8 );

            FacadeManage.aideNativeWebFacade.UpdateConfigInfo( config );
            ShowInfo( "修改成功" );
        }
        #endregion
    }
}
