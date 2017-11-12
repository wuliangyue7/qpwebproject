using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using Game.Kernel;
using Game.Facade;
using Game.Entity.NativeWeb;
using Game.Entity.Enum;
using Game.Utils;

namespace Game.Web.Module.WebManager
{
    public partial class ActivityList : AdminPage
    {
        protected void Page_Load(object sender,EventArgs e)
        {
            if(!IsPostBack)
            {
                BindActivity();
            }
        }

        protected void btnDelete_Click(object sender,EventArgs e)
        {
            AuthUserOperationPermission(Permission.Delete);
            try
            {
                FacadeManage.aideNativeWebFacade.DeleteActivity(StrCIdList);
                ShowInfo("删除成功");
            }
            catch
            {
                ShowError("删除失败");
            }
            BindActivity();
        }

        protected void anpPage_PageChanged(object sender,EventArgs e)
        {
            BindActivity();
        }

        private void BindActivity()
        {
            //控件绑定
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetList(Activity.Tablename,PageIndex,anpPage.PageSize,"","ORDER BY ActivityID ASC");
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rpData.DataSource = pagerSet.PageSet;
            rpData.DataBind();
            anpPage.RecordCount = pagerSet.RecordCount;
        }
    }
}