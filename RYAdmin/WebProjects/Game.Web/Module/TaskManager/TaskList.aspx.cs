using System;
using Game.Entity.Enum;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.UI;

namespace Game.Web.Module.TaskManager
{
    public partial class TaskList : AdminPage
    {
        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                BindTask();
            }
        }

        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            BindTask();
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        protected void btnDelete_Click( object sender, EventArgs e )
        {
            AuthUserOperationPermission( Permission.Delete );
            string strQuery = "WHERE TaskID IN (" + StrCIdList + ")";
            try
            {
                FacadeManage.aidePlatformFacade.DeleteTaskInfo( strQuery );
                ShowInfo( "删除成功" );
            }
            catch
            {
                ShowError( "删除失败" );
            }
            BindTask();
        }

        #endregion 窗口事件

        #region 窗口方法

        private void BindTask()
        {
            //控件绑定
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetList( Game.Entity.Platform.TaskInfo.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, "", "ORDER BY TaskID ASC" );
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rpData.DataSource = pagerSet.PageSet;
            rpData.DataBind();
            anpPage.RecordCount = pagerSet.RecordCount;
        }

        #endregion 窗口方法
    }
}