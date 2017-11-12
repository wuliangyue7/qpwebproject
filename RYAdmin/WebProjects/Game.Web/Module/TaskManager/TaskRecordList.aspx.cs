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
using Game.Entity.Record;
using System.Text;

namespace Game.Web.Module.TaskManager
{
    public partial class TaskRecordList : AdminPage
    {
        #region 窗口事件
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                //txtDate.Text = DateTime.Now.ToString( "yyyy-MM-dd" );
                BindTaskRecord( "" );
            }
        }

        protected void anpPage_PageChanged( object sender, EventArgs e )
        {
            BindTaskRecord( "" );
        }

        /// <summary>
        /// 用户搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUseSearch_Click( object sender, EventArgs e )
        {
            StringBuilder where = new StringBuilder( "" );

            //用户条件
            string user = CtrlHelper.GetTextAndFilter( txtUser );
            if( string.IsNullOrEmpty( user ) )
            {
                ShowError( "请输入用户信息" );
                return;
            }

            int userID = 0;
            switch( CtrlHelper.GetSelectValue( ddlType ) )
            {
                case "0":
                    userID = GetUserIDByAccount( user );
                    break;
                case "1":
                    if( Utils.Validate.IsNumeric( user ) )
                        userID = GetUserIDByGameID( Convert.ToInt32( user ) );
                    break;
                case "2":
                    if( Utils.Validate.IsNumeric( user ) )
                        userID = Convert.ToInt32( user );
                    break;
                default:
                    break;
            }
            if( userID == 0 )
            {
                litNoData.Visible = true;
                rpData.Visible = false;
                return;
            }
            where.AppendFormat( " WHERE UserID={0}", userID );

            //日期条件
            string date = CtrlHelper.GetText( txtDate );
            if( !string.IsNullOrEmpty( date ) )
            {
                string dateID = Fetch.GetDateID( Convert.ToDateTime( date ) );
                where.AppendFormat( " AND DateID={0}", dateID );
            }

            //绑定数据
            BindTaskRecord( where.ToString() );
        }

        /// <summary>
        /// 删除任务记录
        /// </summary>
        protected void btnDelete_Click( object sender, EventArgs e )
        {
            AuthUserOperationPermission( Permission.Delete );
            string strQuery = "WHERE RecordID IN (" + StrCIdList + ")";
            try
            {
                FacadeManage.aideRecordFacade.DeleteTaskRecord( strQuery );
                ShowInfo( "删除成功" );
            }
            catch
            {
                ShowError( "删除失败" );
            }
            BindTaskRecord( "" );
        }

        /// <summary>
        /// 清除一个月前的记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click( object sender, EventArgs e )
        {
            AuthUserOperationPermission( Permission.Delete );
            string strQuery = "WHERE InputDate <= '" + Fetch.GetEndTime( DateTime.Now.AddMonths( -1 ) ) + "'";
            try
            {
                FacadeManage.aideRecordFacade.DeleteTaskRecord( strQuery );
                ShowInfo( "清除成功" );
            }
            catch
            {
                ShowError( "清除失败" );
            }
            BindTaskRecord( "" );
        }
        #endregion

        #region 窗口方法
        private void BindTaskRecord( string where )
        {
            //控件绑定
            PagerSet pagerSet = FacadeManage.aideRecordFacade.GetList(RecordTask.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, where, "ORDER BY InputDate DESC");
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
        /// 获取任务名称
        /// </summary>
        /// <param name="taskID">任务标识</param>
        /// <returns>任务名称</returns>
        protected string GetTaskName( int taskID )
        {
            Game.Entity.Platform.TaskInfo taskInfo = FacadeManage.aidePlatformFacade.GetTaskInfoByID( taskID );
            if( taskInfo != null )
                return taskInfo.TaskName;
            return "";
        }

        #endregion
    }
}
