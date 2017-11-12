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
using Game.Entity.NativeWeb;
using System.Data;
using Game.Entity.Enum;

namespace Game.Web.Module.MallManager
{
    public partial class MallTypeInfo : AdminPage
    {
        #region 窗口事件
        protected void Page_Load( object sender, EventArgs e )
        {
            AuthUserOperationPermission( Permission.Read );
            if( !IsPostBack )
            {
                Inition();
            }
        }
        /// <summary>
        /// 点击保存数据
        /// </summary>
        protected void btnSave_Click( object sender, EventArgs e )
        {
            if( !IsValid )
            {
                return;
            }

            AwardType type = new AwardType();
            if( IntParam > 0 )
            {
                AuthUserOperationPermission( Permission.Edit );
                type = FacadeManage.aideNativeWebFacade.GetAwardTypeByID( IntParam );
            }
            else
            {
                AuthUserOperationPermission( Permission.Add );
                type.CollectDate = DateTime.Now;
            }
            type.SortID = CtrlHelper.GetInt( txtSortID, 0 );
            type.TypeName = CtrlHelper.GetText( txtTypeName );
            type.Nullity = Convert.ToByte( rbNullity.SelectedValue );

            bool isSuccess;
            if( IntParam > 0 )
            {
                isSuccess = FacadeManage.aideNativeWebFacade.UpdateAwardTypeInfo( type );
            }
            else
            {
                isSuccess = FacadeManage.aideNativeWebFacade.InsertAwardTypeInfo( type );
            }

            if( isSuccess )
            {
                ShowInfo( "保存类型信息成功", "MallTypeList.aspx" ,1000);
            }
            else
            {
                ShowError( "保存类型信息失败" );
            }
        }
        #endregion

        #region 窗口方法

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void Inition()
        {
            if( IntParam > 0 )
            {
                AwardType type = FacadeManage.aideNativeWebFacade.GetAwardTypeByID( IntParam );
                CtrlHelper.SetText( txtSortID, type.SortID.ToString() );
                CtrlHelper.SetText( txtTypeName, type.TypeName );
                rbNullity.SelectedValue = type.Nullity.ToString();
            }
        }
        #endregion
    }
}