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

namespace Game.Web.Module.FilledManager
{
    public partial class OrderAppInfo : AdminPage
    {
        #region Fields

        int id = GameRequest.GetQueryInt("id", 0);

        #endregion

        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DetailDataBind();
            }
        }
        #endregion

        #region 数据加载

        private void DetailDataBind()
        {
            if (id <= 0)
            {
                return;
            }

            //获取站点信息
            ReturnAppDetailInfo AppDetailInfo = FacadeManage.aideTreasureFacade.GetAppDetailInfo( id );
            if (AppDetailInfo == null)
            {
                ShowError("信息不存在");
                return;
            }

            CtrlHelper.SetText(litOrderID, AppDetailInfo.OrderID);
            CtrlHelper.SetText(litPayAmount, AppDetailInfo.PayAmount.ToString());
            CtrlHelper.SetText(litproduct_id, AppDetailInfo.Product_id);
            CtrlHelper.SetText(litUserID, GetAccounts(AppDetailInfo.UserID));
            CtrlHelper.SetText(litquantity, AppDetailInfo.Quantity.ToString());
            if (AppDetailInfo.Status == 0)
                CtrlHelper.SetText(litStatus, "<span class='lan'>成功</span>");
            else
                CtrlHelper.SetText(litStatus, "<span class='hong'>失败</span>");
            CtrlHelper.SetText(litCollectDate, AppDetailInfo.CollectDate.ToString("yyyy-MM-dd HH:mm:ss"));
            CtrlHelper.SetText(littransaction_id, AppDetailInfo.Transaction_id);
            CtrlHelper.SetText(litpurchase_date, AppDetailInfo.Purchase_date);
            CtrlHelper.SetText(litoriginal_transaction_id, AppDetailInfo.Original_transaction_id);
            CtrlHelper.SetText(litoriginal_purchase_date, AppDetailInfo.Original_purchase_date);
            CtrlHelper.SetText(litapp_item_id, AppDetailInfo.App_item_id);
            CtrlHelper.SetText(litversion_external_identifier, AppDetailInfo.Version_external_identifier);
            CtrlHelper.SetText(litbid, AppDetailInfo.Bid);
            CtrlHelper.SetText(litbvrs, AppDetailInfo.Bvrs);
        }
        #endregion
    }
}
