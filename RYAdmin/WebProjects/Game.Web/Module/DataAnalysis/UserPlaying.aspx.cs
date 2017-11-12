using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Game.Web.UI;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using Game.Entity.Accounts;
using Game.Entity.Treasure;
using Game.Entity.Platform;

namespace Game.Web.Module.DataAnalysis
{
    public partial class UserPlaying : AdminPage
    {
        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindServerList();
                BindingData();
            }
        }

        protected void btnQueryOrder_Click(object sender, EventArgs e)
        {
            BindingData();
        }

        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            BindingData();
        }
        #endregion

        #region 数据方法
        protected void BindingData()
        {
            string type = ddlSearchType.SelectedValue;
            string search = CtrlHelper.GetText(txtSearch);
            string where = string.Empty;

            if(!string.IsNullOrEmpty(search))
            {
                int userID = 0;
                AccountsInfo model = null;
                switch(type)
                {
                    case "1":
                        model = FacadeManage.aideAccountsFacade.GetAccountInfoByAccount(search);
                        break;
                    case "2":
                        model = FacadeManage.aideAccountsFacade.GetAccountInfoByNickname(search);
                        break;
                    case "3":
                        if(!Utils.Validate.IsPositiveInt(search))
                        {
                            ShowError("操作失败！游戏ID只能为整数");
                            return;
                        }
                        model = FacadeManage.aideAccountsFacade.GetAccountInfoByGameID(Convert.ToInt32(search));
                        break;
                    case "4":
                        if(!Utils.Validate.IsPositiveInt(search))
                        {
                            ShowError("操作失败！用户ID只能为数字");
                            return;
                        }
                        userID = Convert.ToInt32(search);
                        break;
                    default:
                        break;
                }
                userID = model == null ? 0 : model.UserID;
                if(userID == 0)
                {
                    litNoData.Visible = true;
                    rptData.Visible = false;
                    return;
                }
                where = " WHERE UserID=" + userID;
            }

            int serverID = Convert.ToInt32(ddlServerID.SelectedValue);
            if(serverID != 0)
            {
                where = string.IsNullOrEmpty(where) ? string.Format(" WHERE ServerID={0}", serverID) : where + string.Format(" AND ServerID={0}", serverID);
            }

            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(GameScoreLocker.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, where, " ORDER BY CollectDate ASC");
            anpPage.RecordCount = pagerSet.RecordCount;

            if(pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                rptData.DataSource = pagerSet.PageSet;
                rptData.DataBind();
                litNoData.Visible = false;
                rptData.Visible = true;
            }
            else
            {
                litNoData.Visible = true;
                rptData.Visible = false;
            }
        }

        /// <summary>
        /// 获取游戏名称
        /// </summary>
        /// <param name="serverID"></param>
        /// <returns></returns>
        protected string GameGameNameByServerID(int serverID)
        {
            GameRoomInfo gameRoomInfo = FacadeManage.aidePlatformFacade.GetGameRoomInfoInfo(serverID);
            if(gameRoomInfo != null)
            {
                GameKindItem gameKindItem = FacadeManage.aidePlatformFacade.GetGameKindItemInfo(gameRoomInfo.KindID);
                if(gameKindItem != null)
                {
                    return gameKindItem.KindName;
                }
            }
            return "";
        }

        //绑定房间
        private void BindServerList()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetGameRoomInfoList(1, Int32.MaxValue, "WHERE ServerType=1", "ORDER BY GameID ASC");

            if(pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                ddlServerID.DataSource = pagerSet.PageSet;
                ddlServerID.DataTextField = "ServerName";
                ddlServerID.DataValueField = "ServerID";
                ddlServerID.DataBind();
            }

            ddlServerID.Items.Insert(0, new ListItem("全部金币房间", "0"));
        }
        #endregion
    }
}