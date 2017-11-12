using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Entity.Enum;
using Game.Entity.Treasure;
using Game.Web.UI;
using Game.Entity.NativeWeb;
using Game.Facade;


namespace Game.Web.Module.WebManager
{
    public partial class AdsGameInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                if( IntParam != 0 )
                {
                    BindData();
                }
            }
        }

        //保存
        protected void btnSave_Click( object sender, EventArgs e )
        {
            Ads ads;
            ads = FacadeManage.aideNativeWebFacade.GetAds( IntParam );

            ads.LinkURL = CtrlHelper.GetText( txtLinkURL );
            ads.Type = 1;

            #region 图片验证
            //缩率图上传
            try
            {
                ads.ResourceURL = upImage.FilePath;
            }
            catch( Exception ex )
            {
                ShowError( "缩率图上传失败：" + ex.Message );
                return;
            }
            #endregion

            //判断权限
            AuthUserOperationPermission( Permission.Edit );
            FacadeManage.aideNativeWebFacade.UpdateAds( ads );
            ShowInfo( "更新成功", "AdsGameList.aspx", 1200 );
        }

        #endregion

        #region 数据加载

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            Ads ads = FacadeManage.aideNativeWebFacade.GetAds( IntParam );
            if( ads == null )
                return;

            CtrlHelper.SetText( txtLinkURL, ads.LinkURL );
            CtrlHelper.SetText( lbRemark, ads.Remark );
            if( !string.IsNullOrEmpty( ads.ResourceURL ) )
            {
                upImage.FilePath = ads.ResourceURL;
            }
        }
        #endregion
    }
}
