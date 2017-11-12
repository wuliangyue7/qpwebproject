using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Utils;
using Game.Facade;
using Game.Kernel;
using System.Collections;
using System.Text;
using Game.Entity.Enum;
using Game.Entity.Platform;

namespace Game.Web.Module.AppManager
{
    public partial class GamePropertyInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !Page.IsPostBack )
            {
                BindArea( );
                BindPropertyType();
                BindData( );
            }
        }
        protected void btnSave_Click( object sender, EventArgs e )
        {
            //判断权限
            AuthUserOperationPermission( Permission.Edit );
            string cash = CtrlHelper.GetText( txtCash );
            GameProperty property = FacadeManage.aidePlatformFacade.GetGamePropertyInfo( IntParam );
            if ( property == null )
                return;
            property.Name = CtrlHelper.GetText( txtName );
            property.PTypeID = Convert.ToInt32(ddlPTypeID.SelectedValue);
            property.MTypeID = Convert.ToInt32(ddlMTypeID.SelectedValue);
            property.Cash = decimal.Parse( string.IsNullOrEmpty( cash ) ? "0" : cash );
            property.Gold = CtrlHelper.GetInt( txtGold, 0 );
            property.UserMedal = CtrlHelper.GetInt(txtUserMedal, 0);
            property.LoveLiness = CtrlHelper.GetInt(txtLoveLiness, 0);

            //发行范围
            int intIssueArea = 0;
            if ( ckbIssueArea.Items.Count > 0 )
            {
                foreach ( ListItem item in ckbIssueArea.Items )
                {
                    if ( item.Selected )
                        intIssueArea |= int.Parse( item.Value );
                }
            }
            //使用范围
            int intServiceArea = 0;
            if ( ckbServiceArea.Items.Count > 0 )
            {
                foreach ( ListItem item in ckbServiceArea.Items )
                {
                    if ( item.Selected )
                        intServiceArea |= int.Parse( item.Value );
                }
            }

            string strRecvLoveLiness = CtrlHelper.GetText( txtRecvLoveLiness );
            if( !Game.Utils.Validate.IsNumeric( strRecvLoveLiness ) ) 
            {
                ShowError( "接受魅力值只能正或者负的整数" );
                return;
            }

            property.UseArea = ( short ) intIssueArea;
            property.ServiceArea = ( short ) intServiceArea;
            property.SendLoveLiness = CtrlHelper.GetInt( txtSendLoveLiness, 0 );
            property.RecvLoveLiness = CtrlHelper.GetInt( txtRecvLoveLiness, 0 );
            property.RegulationsInfo = CtrlHelper.GetText( txtRegulationsInfo );
            property.UseResultsGold = CtrlHelper.GetInt(txtUseResultsGold, 0);
            property.UseResultsValidTime = CtrlHelper.GetInt(txtUseResultsValidTime, 0);
            property.UseResultsValidTimeScoreMultiple = CtrlHelper.GetInt(txtUseResultsValidTimeScoreMultiple, 0);
            property.Nullity = (byte)(chkSuportMobile.Checked ? 0 : 1);
            property.Recommend = ckbRecommend.Checked ? 1 : 0;
            property.SuportMobile = (byte)(chkSuportMobile.Checked ? 1 : 0);
            try
            {
                FacadeManage.aidePlatformFacade.UpdateGameProperty( property );
                ShowInfo( "更新成功" );
            }
            catch 
            {
                ShowInfo( "更新失败" );
            }
        }
        #endregion

        #region 数据加载
        private void BindArea( )
        {
            //使用范围
            IList<EnumDescription> arrIssueArea = IssueAreaHelper.GetIssueAreaList( typeof( IssueArea ) );
            ckbIssueArea.DataSource = arrIssueArea;
            ckbIssueArea.DataValueField = "EnumValue";
            ckbIssueArea.DataTextField = "Description";
            ckbIssueArea.DataBind( );

            //作用范围
            IList<EnumDescription> arrServiceArea = ServiceAreaHelper.GetServiceAreaList( typeof( ServiceArea ) );
            ckbServiceArea.DataSource = arrServiceArea;
            ckbServiceArea.DataValueField = "EnumValue";
            ckbServiceArea.DataTextField = "Description";
            ckbServiceArea.DataBind( );
        }

        //绑定道具类型
        private void BindPropertyType()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetGamePropertyTypeList(1, Int32.MaxValue, "Where Nullity=0", "ORDER BY TypeID ASC");
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                ddlPTypeID.DataSource = pagerSet.PageSet;
                ddlPTypeID.DataTextField = "TypeName";
                ddlPTypeID.DataValueField = "TypeID";
                ddlPTypeID.DataBind();

                ddlMTypeID.DataSource = pagerSet.PageSet;
                ddlMTypeID.DataTextField = "TypeName";
                ddlMTypeID.DataValueField = "TypeID";
                ddlMTypeID.DataBind();
            }
        }

        private void BindData( )
        {
            if ( IntParam <= 0 )
                return;

            GameProperty property = FacadeManage.aidePlatformFacade.GetGamePropertyInfo( IntParam );
            if ( property == null )
                return;

            //发行范围
            int intIssueArea = property.UseArea;
            if ( ckbIssueArea.Items.Count > 0 )
            {
                foreach ( ListItem item in ckbIssueArea.Items )
                {
                    item.Selected = int.Parse( item.Value ) == ( intIssueArea & int.Parse( item.Value ) );
                }
            }
            //使用范围
            int intServiceArea = property.ServiceArea;
            if ( ckbServiceArea.Items.Count > 0 )
            {
                foreach ( ListItem item in ckbServiceArea.Items )
                {
                    item.Selected = int.Parse( item.Value ) == ( intServiceArea & int.Parse( item.Value ) );
                }
            }
            ddlPTypeID.SelectedValue = property.PTypeID.ToString();
            ddlMTypeID.SelectedValue = property.MTypeID.ToString();
            CtrlHelper.SetText( txtName, property.Name.ToString( ) );
            CtrlHelper.SetText( txtCash, property.Cash.ToString( ) );
            CtrlHelper.SetText( txtGold, property.Gold.ToString( ) );
            CtrlHelper.SetText(txtUserMedal, property.UserMedal.ToString());
            CtrlHelper.SetText(txtLoveLiness, property.LoveLiness.ToString());

            CtrlHelper.SetText( txtSendLoveLiness, property.SendLoveLiness.ToString( ) );
            CtrlHelper.SetText( txtRecvLoveLiness, property.RecvLoveLiness.ToString( ) );
            CtrlHelper.SetText( txtRegulationsInfo, property.RegulationsInfo );
            CtrlHelper.SetText(txtUseResultsGold, property.UseResultsGold.ToString());
            CtrlHelper.SetText(txtUseResultsValidTime, property.UseResultsValidTime.ToString());
            CtrlHelper.SetText(txtUseResultsValidTimeScoreMultiple, property.UseResultsValidTimeScoreMultiple.ToString());
            ckbNullity.Checked = property.Nullity == 0;
            ckbRecommend.Checked = property.Recommend == 1;
            chkSuportMobile.Checked = property.SuportMobile == 1;
        }
        #endregion
    }
}
