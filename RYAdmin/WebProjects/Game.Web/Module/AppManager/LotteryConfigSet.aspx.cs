using Game.Entity.Enum;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using Game.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Module.AppManager
{
    public partial class LotteryConfigSet : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Edit);
            LotteryConfig model = FacadeManage.aideTreasureFacade.GetLotteryConfig(1);
            model.FreeCount = CtrlHelper.GetInt(txtFreeCount, 0);
            model.ChargeFee = CtrlHelper.GetInt(txtChargeFee, 0);
            model.IsCharge = (byte)(cbIsCharge.Checked ? 1 : 0);      

            try
            {
                FacadeManage.aideTreasureFacade.UpdateLotteryConfig(model);
                ShowInfo("更新成功");
            }
            catch
            {
                ShowError("更新失败");
            }
        }
        #endregion

        #region 数据加载

        private void BindData()
        {
            LotteryConfig model = FacadeManage.aideTreasureFacade.GetLotteryConfig(1);
            if (model == null)
                return;
            CtrlHelper.SetText(txtFreeCount, model.FreeCount.ToString());
            CtrlHelper.SetText(txtChargeFee, model.ChargeFee.ToString());
            cbIsCharge.Checked = model.IsCharge == 0 ? false : true;
        }
        #endregion
    }
}