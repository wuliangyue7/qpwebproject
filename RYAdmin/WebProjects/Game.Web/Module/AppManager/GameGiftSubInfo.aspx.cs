using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Facade;
using Game.Kernel;
using System.Collections;
using System.Text;
using Game.Entity.Enum;
using Game.Entity.Platform;
using Game.Web.UI;

namespace Game.Web.Module.AppManager
{
    public partial class GameGiftSubInfo : AdminPage
    {
        #region Fields

        protected int ownerID = GameRequest.GetQueryInt("OwnerID", 0);
        #endregion

        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGameProperty();
                BindData();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //判断权限
            GamePropertySub propertySub = new GamePropertySub();
            propertySub.ID = Convert.ToInt32(ddlGameProperty.SelectedValue.Trim());
            propertySub.OwnerID = ownerID;
            propertySub.Count = CtrlHelper.GetInt(txtCount, 0);
            propertySub.SortID = CtrlHelper.GetInt(txtSortID, 0);
            
            if (StrCmd == "add")
            {            
                try
                {
                    FacadeManage.aidePlatformFacade.InsertGamePropertySub(propertySub);
                    ShowInfo("新增成功");
                }
                catch
                {
                    ShowInfo("新增失败");
                }
            }
            else
            {
                try
                {
                    FacadeManage.aidePlatformFacade.UpdateGamePropertySub(propertySub);
                    ShowInfo("更新成功");
                }
                catch
                {
                    ShowInfo("更新失败");
                }
            }
        }
        #endregion

        #region 数据加载
        private void BindGameProperty()
        {
            //使用范围
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetGamePropertyList(1, int.MaxValue, string.Format("WHERE Kind<>{0}", (int)GamePropertyKind.KIND11), "ORDER BY ID ASC");
            ddlGameProperty.DataSource = pagerSet.PageSet;
            ddlGameProperty.DataValueField = "ID";
            ddlGameProperty.DataTextField = "Name";
            ddlGameProperty.DataBind();
            
        }
        private void BindData()
        {
            if (StrCmd == "add")
            {
                litInfo.Text = "新增";
            }
            else
            {
                litInfo.Text = "更新";
            }

            if (IntParam <= 0)
                return;

            GamePropertySub property = FacadeManage.aidePlatformFacade.GetGamePropertySubInfo(IntParam, ownerID);
            if (property == null)
                return;

            ddlGameProperty.SelectedValue = property.ID.ToString();
            CtrlHelper.SetText(txtCount, property.Count.ToString());
            CtrlHelper.SetText(txtSortID, property.SortID.ToString());
        }
        #endregion
    }
}