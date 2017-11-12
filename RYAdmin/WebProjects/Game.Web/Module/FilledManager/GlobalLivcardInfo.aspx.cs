using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Utils;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using Game.Entity.Enum;
using System.Drawing;

namespace Game.Web.Module.FilledManager
{
    public partial class GlobalLivcardInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if( !Page.IsPostBack )
            {
                if( IntParam <= 0 )
                    return;

                BindData();
            }
        }

        //保存
        protected void btnSave_Click( object sender, EventArgs e )
        {
            if( !IsValid )
                return;

            GlobalLivcard cardType;
            if( IntParam <= 0 )
            {
                cardType = new GlobalLivcard();

            }
            else
            {
                cardType = FacadeManage.aideTreasureFacade.GetGlobalLivcardInfo( IntParam );

            }
            cardType.CardName = CtrlHelper.GetText( txtCardTypeName );
            cardType.CardPrice = Utils.Validate.IsDouble( txtCardPrice.Text.Trim() ) ? decimal.Parse( txtCardPrice.Text.Trim() ) : 0;
            cardType.Currency = Convert.ToDecimal( txtCurrency.Text.Trim() );

            if( IntParam <= 0 )
            {
                //判断权限
                AuthUserOperationPermission( Permission.Add );
                FacadeManage.aideTreasureFacade.InsertGlobalLivcard( cardType );
                ShowInfo( "新增成功", "GlobalLivcardList.aspx", 1200 );
            }
            else
            {
                //判断权限
                AuthUserOperationPermission( Permission.Edit );
                FacadeManage.aideTreasureFacade.UpdateGlobalLivcard( cardType );
                ShowInfo( "更新成功", "GlobalLivcardList.aspx", 1200 );
            }
        }
        #endregion

        #region 数据加载

        private void BindData()
        {
            GlobalLivcard cardType = FacadeManage.aideTreasureFacade.GetGlobalLivcardInfo( IntParam );
            if( cardType == null )
                return;

            CtrlHelper.SetText( txtCardTypeName, cardType.CardName );
            CtrlHelper.SetText( txtCardPrice, cardType.CardPrice.ToString() );
            CtrlHelper.SetText( txtCurrency, cardType.Currency.ToString() );
        }

        #endregion
    }
}
