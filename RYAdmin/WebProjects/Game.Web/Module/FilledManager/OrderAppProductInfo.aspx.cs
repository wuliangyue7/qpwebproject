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

namespace Game.Web.Module.FilledManager
{
    public partial class OrderAppProductInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (IntParam <= 0)
                {
                    txtDate.Visible = false;
                    return;
                }

                BindData();
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            GlobalAppInfo appInfo;
            if (IntParam <= 0)
            {
                appInfo = new GlobalAppInfo();

            }
            else
            {
                appInfo = FacadeManage.aideTreasureFacade.GetGlobalAppInfo( IntParam );

            }
            appInfo.ProductID = CtrlHelper.GetText(txtProductID);
            appInfo.ProductName = CtrlHelper.GetText(txtProductName);
            appInfo.Description = CtrlHelper.GetText(txtDescription);
            appInfo.Price = Convert.ToDecimal(CtrlHelper.GetText(txtPrice));
            appInfo.AttachCurrency = Convert.ToDecimal(CtrlHelper.GetText(txtAttachCurrency));
            if (IntParam <= 0)
            {
                FacadeManage.aideTreasureFacade.InsertGlobalAppInfo( appInfo );
                ShowInfo("新增成功", "OrderAppProductList.aspx", 1200);
            }
            else
            {
                FacadeManage.aideTreasureFacade.UpdateGlobalAppInfo( appInfo );
                ShowInfo("更新成功", "OrderAppProductList.aspx", 1200);
            }

        }
        #endregion

        #region 数据加载

        private void BindData()
        {
            GlobalAppInfo appInfo = FacadeManage.aideTreasureFacade.GetGlobalAppInfo( IntParam );
            if (appInfo == null)
                return;

            CtrlHelper.SetText(txtProductID, appInfo.ProductID);
            CtrlHelper.SetText(txtProductName, appInfo.ProductName);
            CtrlHelper.SetText(txtDescription, appInfo.Description);
            CtrlHelper.SetText(txtPrice, appInfo.Price.ToString());
            CtrlHelper.SetText(txtAttachCurrency, appInfo.AttachCurrency.ToString());
            CtrlHelper.SetText(lblCollectDate, appInfo.CollectDate.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        #endregion
    }
}
