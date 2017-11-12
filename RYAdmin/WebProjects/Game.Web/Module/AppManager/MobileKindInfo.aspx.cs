using Game.Entity.Enum;
using Game.Entity.Platform;
using Game.Facade;
using Game.Kernel;
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
    public partial class MobileKindInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (IntParam <= 0 && StrCmd.Equals("add"))
                    txtKindID.Text = (FacadeManage.aidePlatformFacade.GetMaxMobileKindID() + 1).ToString();

                MobileKindItemDataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProcessData();
        }
        #endregion

        #region 数据加载 

        private void MobileKindItemDataBind()
        {
            if (StrCmd == "add")
            {
                litInfo.Text = "新增";
                txtKindID.Enabled = true;
            }
            else
            {
                litInfo.Text = "更新";
                txtKindID.Enabled = false;
            }

            if (IntParam <= 0)
            {
                return;
            }

            //获取游戏信息
            MobileKindItem model = FacadeManage.aidePlatformFacade.GetMobileKindItemInfo(IntParam);
            if (model == null)
            {
                ShowError("游戏信息不存在");
                Redirect("MobileKindList.aspx");
                return;
            }

            CtrlHelper.SetText(txtKindID, model.KindID.ToString().Trim());
            CtrlHelper.SetText(txtKindName, model.KindName.Trim());
            CtrlHelper.SetText(txtModuleName, model.ModuleName.Trim());
            CtrlHelper.SetText(txtClientVersion, CalVersion(model.ClientVersion));
            CtrlHelper.SetText(txtResVersion, model.ResVersion.ToString());
            CtrlHelper.SetText(txtSortID, model.SortID.ToString().Trim());
            int kindMark = model.KindMark;
            if (cblKindMark.Items.Count > 0)
            {
                foreach (ListItem item in cblKindMark.Items)
                {
                    item.Selected = int.Parse(item.Value) == (kindMark & int.Parse(item.Value));
                }
            }
            rbtnNullity.SelectedValue = model.Nullity.ToString().Trim();            
        }
        #endregion

        #region 处理方法

        private void ProcessData()
        {
            if (!IsValid)
            {
                return;
            }
            string kindID = CtrlHelper.GetText(txtKindID);
            string sortID = CtrlHelper.GetText(txtSortID);
            if (!Utils.Validate.IsPositiveInt(kindID))
            {
                ShowError("游戏标识不规范，游戏标识只能为正整数");
                return;
            }
            if (!Utils.Validate.IsPositiveInt(sortID))
            {
                ShowError("排序输入不规范，排序只能为0或正整数");
                return;
            }
            MobileKindItem model = new MobileKindItem();
            model.KindID = Convert.ToInt32(kindID);
            model.KindName = CtrlHelper.GetText(txtKindName);
            model.ModuleName = CtrlHelper.GetText(txtModuleName);
            model.ClientVersion = CalVersion2(CtrlHelper.GetText(txtClientVersion));
            model.ResVersion = CtrlHelper.GetInt(txtResVersion, 0);

            //计算属性
            int kindMark = 0;
            if (cblKindMark.Items.Count > 0)
            {
                foreach (ListItem item in cblKindMark.Items)
                {
                    if (item.Selected)
                    {
                        kindMark |= int.Parse(item.Value);
                    }
                }
            }
            model.KindMark = kindMark;
            model.SortID = CtrlHelper.GetInt(txtSortID, 0);
            model.Nullity = Convert.ToByte(rbtnNullity.SelectedValue.Trim());

            Message msg = new Message();
            if (StrCmd == "add")
            {
                //判断权限
                AuthUserOperationPermission(Permission.Add);
                msg = FacadeManage.aidePlatformFacade.InsertMobileKindItem(model);
            }
            else
            {
                //判断权限
                AuthUserOperationPermission(Permission.Edit);
                msg = FacadeManage.aidePlatformFacade.UpdateMobileKindItem(model);
            }

            if (msg.Success)
            {                
                if (StrCmd == "add")
                {
                    ShowInfo("手游信息增加成功", "MobileKindList.aspx", 1200);
                }
                else
                {
                    ShowInfo("手游信息修改成功", "MobileKindList.aspx", 1200);
                }
            }
            else
            {
                ShowError(msg.Content);
            }
        }
        #endregion
    }
}