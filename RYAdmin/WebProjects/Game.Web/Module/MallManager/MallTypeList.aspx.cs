using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Kernel;
using Game.Utils;
using Game.Facade;
using Game.Entity.NativeWeb;
using Game.Entity.Enum;

namespace Game.Web.Module.MallManager
{
    public partial class MallTypeList : AdminPage
    {
        #region 窗口事件
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                DataMalType();
            }
        }

        /// <summary>
        /// 禁用
        /// </summary>
        protected void btnNulityFalse_Click( object sender, EventArgs e )
        {
            AuthUserOperationPermission( Permission.IsNulity );
            string Checkbox_Value = GameRequest.GetFormString( "cbxData" );
            string[] str = Checkbox_Value.Split( ',' );
            string idList = "";
            for( int i = 0; i < str.Length; i++ )
            {
                string[] cheild = str[i].Split( '/' );
                if( cheild[1] == "0" )
                {
                    string getstr = FacadeManage.aideNativeWebFacade.GetChildAwardTypeByPID( Convert.ToInt32( cheild[0] ) );
                    if( getstr != "" )
                    {
                        idList = idList + getstr + ",";
                    }
                    idList = idList + cheild[0] + ",";
                }
                else
                {
                    idList = idList + cheild[0] + ",";
                }
            }
            if( idList != "" )
            {
                string list = idList.Substring( 0, idList.Length - 1 );
                if( FacadeManage.aideNativeWebFacade.UpdateNulity( list, 1 ) > 0 )
                {
                    ShowInfo( "冻结商品类型成功" );
                }
                else
                {
                    ShowError( "冻结商品类型失败" );
                }
            }
            DataMalType();
        }
        /// <summary>
        /// 启用
        /// </summary>
        protected void btnNulityTrue_Click( object sender, EventArgs e )
        {
            AuthUserOperationPermission( Permission.IsNulity );
            string Checkbox_Value = GameRequest.GetFormString( "cbxData" );
            string[] str = Checkbox_Value.Split( ',' );
            string idList = "";
            for( int i = 0; i < str.Length; i++ )
            {
                string[] cheild = str[i].Split( '/' );
                if( cheild[1] == "0" )
                {
                    string getstr = FacadeManage.aideNativeWebFacade.GetChildAwardTypeByPID( Convert.ToInt32( cheild[0] ) );
                    if( getstr != "" )
                    {
                        idList = idList + getstr + ",";
                    }
                    idList = idList + cheild[0] + ",";
                }
                else
                {
                    idList = idList + cheild[0] + ",";
                }
            }
            if( idList != "" )
            {
                string list = idList.Substring( 0, idList.Length - 1 );
                if( FacadeManage.aideNativeWebFacade.UpdateNulity( list, 0 ) > 0 )
                {
                    ShowInfo( "解冻商品类型成功" );
                }
                else
                {
                    ShowError( "解冻商品类型失败" );
                }
            }
            DataMalType();
        }
        /// <summary>
        /// 删除类型
        /// </summary>
        protected void lbDelete_Command( object sender, CommandEventArgs e )
        {
            AuthUserOperationPermission( Permission.Delete );
            int typeId = Convert.ToInt32( e.CommandArgument );
            Message ms = FacadeManage.aideNativeWebFacade.DeleteAwardType( typeId );
            if( ms.Success )
            {
                ShowInfo( "商品类型删除成功！" );
            }
            else
            {
                ShowError( ms.Content );
            }
            DataMalType();
        }
        #endregion

        #region 窗口方法
        private void DataMalType()
        {
            //控件绑定
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetList( AwardType.Tablename, PageIndex, anpPage.PageSize, "", "ORDER BY TypeID ASC" );
            if( pagerSet.PageSet.Tables[0].Rows.Count > 0 )
            {
                litNoData.Visible = false;
                rpData.Visible = true;
                rpData.DataSource = pagerSet.PageSet;
                rpData.DataBind();
                anpPage.RecordCount = pagerSet.RecordCount;
            }
            else
            {
                litNoData.Visible = true;
                rpData.Visible = false;
            }
        }

        /// <summary>
        /// 获取父类型名称
        /// </summary>
        public string GetParentName( int parentId )
        {
            if( parentId == 0 )
            {
                return "";
            }
            return FacadeManage.aideNativeWebFacade.GetAwardTypeByPID( parentId ).TypeName;
        }
        #endregion
    }
}