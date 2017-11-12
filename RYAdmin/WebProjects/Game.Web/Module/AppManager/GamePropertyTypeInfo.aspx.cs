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
    public partial class GamePropertyTypeInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GameTypeItemDataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProcessData();
        }
        #endregion

        #region 数据加载

        private void GameTypeItemDataBind()
        {
            if (StrCmd == "add")
            {
                litInfo.Text = "新增";
                txtTypeID.Enabled = true;
            }
            else
            {
                litInfo.Text = "更新";
                txtTypeID.Enabled = false;
            }

            if (IntParam <= 0)
            {
                return;
            }

            //获取类型信息
            GamePropertyType gamePropertyType = FacadeManage.aidePlatformFacade.GetGamePropertyTypeInfo(IntParam);
            if (gamePropertyType == null)
            {
                ShowError("类型信息不存在");
                Redirect("GamePropertyTypeList.aspx");
                return;
            }

            CtrlHelper.SetText(txtTypeID, gamePropertyType.TypeID.ToString().Trim());
            CtrlHelper.SetText(txtTypeName, gamePropertyType.TypeName.Trim());
            CtrlHelper.SetText(txtSortID, gamePropertyType.SortID.ToString().Trim());
            rbtnNullity.SelectedValue = gamePropertyType.Nullity.ToString().Trim();
            hdfTagID.Value = gamePropertyType.TagID.ToString();
            CtrlHelper.SetText(lblTagName, gamePropertyType.TagID == 0 ? "大厅" : "手机");
        }
        #endregion

        #region 处理方法

        private void ProcessData()
        {
            string typeID = CtrlHelper.GetText(txtTypeID);
            string sortID = CtrlHelper.GetText(txtSortID);
            if (!Utils.Validate.IsPositiveInt(typeID))
            {
                ShowError("类型标识不规范，类型标识只能为正整数");
                return;
            }
            if (!Utils.Validate.IsPositiveInt(sortID))
            {
                ShowError("排序输入不规范，排序只能为0或正整数");
                return;
            }
            GamePropertyType gamePropertyType = new GamePropertyType();
            gamePropertyType.TypeID = Convert.ToInt32(typeID);
            gamePropertyType.TypeName = CtrlHelper.GetText(txtTypeName);
            gamePropertyType.SortID = CtrlHelper.GetInt(txtSortID, 0);
            gamePropertyType.Nullity = Convert.ToByte(rbtnNullity.SelectedValue.Trim());
            gamePropertyType.TagID = Convert.ToInt32(hdfTagID.Value);

            Message msg = new Message();
            if (StrCmd == "add")
            {
                //判断权限
                AuthUserOperationPermission(Permission.Add);
                if (FacadeManage.aidePlatformFacade.IsExistsTypeID(gamePropertyType.TypeID))
                {
                    ShowError("游戏类型标识已经存在");
                    return;
                }
                msg = FacadeManage.aidePlatformFacade.InsertGamePropertyType(gamePropertyType);
            }
            else
            {
                //判断权限
                AuthUserOperationPermission(Permission.Edit);
                msg = FacadeManage.aidePlatformFacade.UpdateGamePropertyType(gamePropertyType);
            }

            if (msg.Success)
            {
                if (StrCmd == "add")
                {
                    ShowInfo("类型信息增加成功", "GamePropertyTypeList.aspx", 1200);
                }
                else
                {
                    ShowInfo("类型信息修改成功", "GamePropertyTypeList.aspx", 1200);
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