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
using Game.Entity.Enum;

namespace Game.Web.Module.FilledManager
{
    public partial class LivcardAssociatorInfo : AdminPage
    {
        #region Fields


        #endregion

        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !Page.IsPostBack )
            {
                if(string.IsNullOrEmpty(StrParam))              
                {
                    btnDisable.Enabled = false;
                    btnEnable.Enabled = false;
                    return;
                }
                BindData( );
            }
        }

        //禁用
        protected void btnDisable_Click( object sender, EventArgs e )
        {
            string strQuery = string.Format( "WHERE SerialID='{0}'", StrParam );
            try
            {
                FacadeManage.aideTreasureFacade.SetCardDisbale( strQuery );
                MessageBox( "禁用成功" );
            }
            catch
            {
                MessageBox( "禁用失败" );
            }
            BindData( );
        }

        //启用
        protected void btnEnable_Click( object sender, EventArgs e )
        {

            string strQuery = string.Format( "WHERE SerialID='{0}'", StrParam );
            try
            {
                FacadeManage.aideTreasureFacade.SetCardEnbale( strQuery );
                MessageBox( "启用成功" );
            }
            catch
            {
                MessageBox( "启用失败" );
            }
            BindData( );
        }
        #endregion

        #region 数据加载
        
        private void BindData( )
        {
            LivcardAssociator card = FacadeManage.aideTreasureFacade.GetLivcardAssociatorInfo( StrParam );
            if ( card == null )
                return;
            CtrlHelper.SetText( ltSerialID, card.SerialID );
            CtrlHelper.SetText( ltCardTypeName, GetCardTypeName( card.CardTypeID ) );
            CtrlHelper.SetText( ltBuildID, card.BuildID.ToString( ) );
            CtrlHelper.SetText( ltCardPrice, card.CardPrice.ToString( ) );
            CtrlHelper.SetText( ltCurrency, card.Currency.ToString() );
            CtrlHelper.SetText( ltUseRange, GetUserRange( card.UseRange ) );
            CtrlHelper.SetText( ltValidDate, card.ValidDate.ToString( "yyyy-MM-dd HH:mm:ss" ) );
            CtrlHelper.SetText( ltBuildDate, card.BuildDate.ToString( "yyyy-MM-dd HH:mm:ss" ) );
            CtrlHelper.SetText( ltSalesPerson, card.SalesPerson );
            CtrlHelper.SetText( ltNullity, GetNullityStatus( card.Nullity ) );

            if ( card.ApplyDate != null )
            {
                ShareDetailInfo detailInfo = FacadeManage.aideTreasureFacade.GetShareDetailInfo( StrParam );
                if ( detailInfo != null )
                {
                    plPyaCard.Visible = true;
                    lbPayCardMsg.Text = "充值信息";
                    CtrlHelper.SetText( ltPayDate , detailInfo.ApplyDate.ToString( ) );
                    CtrlHelper.SetText( ltPayUser , GetAccounts( detailInfo.UserID ) + "(" + GetGameID( detailInfo.UserID ) + ")" );
                    CtrlHelper.SetText( ltPayOperUser , GetAccounts( detailInfo.OperUserID ) );
                    CtrlHelper.SetText( ltPayBeforeCurrency , detailInfo.BeforeCurrency.ToString() );
                    CtrlHelper.SetText( ltPayAddress , detailInfo.IPAddress + "  " + Utils.IPQuery.GetAddressWithIP( detailInfo.IPAddress ) );
                }
            }
        }
        #endregion
    }
}
