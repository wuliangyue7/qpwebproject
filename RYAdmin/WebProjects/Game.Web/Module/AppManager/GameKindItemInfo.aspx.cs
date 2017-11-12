using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Utils;
using Game.Entity.Platform;
using Game.Kernel;
using Game.Entity.Enum;
using Game.Entity.Accounts;
using Game.Facade;

namespace Game.Web.Module.AppManager
{
    public partial class GameKindItemInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(IntParam <= 0 && StrCmd.Equals("add"))
                    txtKindID.Text = (FacadeManage.aidePlatformFacade.GetMaxGameKindID() + 1).ToString();

                BindJoin();
                BindTypeList();
                BindGameList();
                GameKindItemDataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProcessData();
        }
        #endregion

        #region 数据加载
        //挂接
        private void BindJoin()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetGameKindItemList(1, Int32.MaxValue, "", "ORDER BY KindID ASC");
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                ddlJoin.DataSource = pagerSet.PageSet;
                ddlJoin.DataTextField = "KindName";
                ddlJoin.DataValueField = "KindID";
                ddlJoin.DataBind();
            }
            ddlJoin.Items.Insert(0, new ListItem("无挂接", "0"));
        }

        //绑定类型
        private void BindTypeList()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetGameTypeItemList(1, Int32.MaxValue, "Where Nullity=0", "ORDER BY TypeID ASC");
            if(pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                ddlTypeID.DataSource = pagerSet.PageSet;
                ddlTypeID.DataTextField = "TypeName";
                ddlTypeID.DataValueField = "TypeID";
                ddlTypeID.DataBind();
            }
        }

        //绑定模块
        private void BindGameList()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetGameGameItemList(1, Int32.MaxValue, "", "ORDER BY GameID ASC");
            if(pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                ddlGameID.DataSource = pagerSet.PageSet;
                ddlGameID.DataTextField = "GameName";
                ddlGameID.DataValueField = "GameID";
                ddlGameID.DataBind();
            }
        }

        private void GameKindItemDataBind()
        {
            if(StrCmd == "add")
            {
                litInfo.Text = "新增";
                txtKindID.Enabled = true;
            }
            else
            {
                litInfo.Text = "更新";
                txtKindID.Enabled = false;
            }

            if(IntParam <= 0)
            {
                return;
            }

            //获取游戏信息
            GameKindItem gameKindItem = FacadeManage.aidePlatformFacade.GetGameKindItemInfo(IntParam);
            if(gameKindItem == null)
            {
                ShowError("游戏信息不存在");
                Redirect("GameKindItemList.aspx");
                return;
            }

            CtrlHelper.SetText(txtKindID, gameKindItem.KindID.ToString().Trim());
            CtrlHelper.SetText(txtKindName, gameKindItem.KindName.Trim());
            ddlTypeID.SelectedValue = gameKindItem.TypeID.ToString().Trim();
            ddlJoin.SelectedValue = gameKindItem.JoinID.ToString().Trim();
            int gameFlag = gameKindItem.GameFlag;
            if (cblGameFlag.Items.Count > 0)
            {
                foreach (ListItem item in cblGameFlag.Items)
                {
                    item.Selected = int.Parse(item.Value) == (gameFlag & int.Parse(item.Value));
                }
            }
            CtrlHelper.SetText(txtSortID, gameKindItem.SortID.ToString().Trim());
            ddlGameID.SelectedValue = gameKindItem.GameID.ToString().Trim();
            CtrlHelper.SetText(txtProcessName, gameKindItem.ProcessName.Trim());
            CtrlHelper.SetText(txtGameRuleUrl, gameKindItem.GameRuleUrl.Trim());
            CtrlHelper.SetText(txtDownLoadUrl, gameKindItem.DownLoadUrl.Trim());
            rbtnNullity.SelectedValue = gameKindItem.Nullity.ToString().Trim();
            ckbRecommend.Checked = gameKindItem.Recommend == 1 ? true : false;

            GameConfig gameConfig = FacadeManage.aidePlatformFacade.GetGameConfig(IntParam);
            if(gameConfig != null)
            {
                CtrlHelper.SetText(txtNoticeChangeGolds, gameConfig.NoticeChangeGolds.ToString());
                CtrlHelper.SetText(txtWinExperience, gameConfig.WinExperience.ToString());
            }
            else
            {
                SystemStatusInfo systemStatusInfo = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(EnumerationList.SystemStatusKey.WinExperience.ToString());
                if(systemStatusInfo != null)
                {
                    CtrlHelper.SetText(txtWinExperience, systemStatusInfo.StatusValue.ToString());
                }
                else
                {
                    CtrlHelper.SetText(txtWinExperience, "10");
                }
            }
        }
        #endregion

        #region 处理方法

        private void ProcessData()
        {
            if(!IsValid)
            {
                return;
            }
            string kindID = CtrlHelper.GetText(txtKindID);
            string sortID = CtrlHelper.GetText(txtSortID);
            if(!Utils.Validate.IsPositiveInt(kindID))
            {
                ShowError("游戏标识不规范，游戏标识只能为正整数");
                return;
            }
            if(!Utils.Validate.IsPositiveInt(sortID))
            {
                ShowError("排序输入不规范，排序只能为0或正整数");
                return;
            }
            GameKindItem gameKindItem = new GameKindItem();
            gameKindItem.KindID = Convert.ToInt32(kindID);
            gameKindItem.KindName = CtrlHelper.GetText(txtKindName);
            gameKindItem.TypeID = Convert.ToInt32(ddlTypeID.SelectedValue.Trim());
            gameKindItem.JoinID = Convert.ToInt32(ddlJoin.SelectedValue.Trim());

            //计算属性
            int gameFlag = 0;
            if (cblGameFlag.Items.Count > 0)
            {
                foreach (ListItem item in cblGameFlag.Items)
                {
                    if (item.Selected)
                    {
                        gameFlag |= int.Parse(item.Value);
                    }
                }
            }
            gameKindItem.GameFlag = gameFlag;
            gameKindItem.SortID = CtrlHelper.GetInt(txtSortID, 0);
            gameKindItem.GameID = Convert.ToInt32(ddlGameID.SelectedValue.Trim());
            gameKindItem.ProcessName = CtrlHelper.GetText(txtProcessName);
            gameKindItem.GameRuleUrl = CtrlHelper.GetText(txtGameRuleUrl);
            gameKindItem.DownLoadUrl = CtrlHelper.GetText(txtDownLoadUrl);
            gameKindItem.Nullity = Convert.ToByte(rbtnNullity.SelectedValue.Trim());
            gameKindItem.Recommend = ckbRecommend.Checked ? 1 : 0;

            Message msg = new Message();
            if(StrCmd == "add")
            {
                //判断权限
                AuthUserOperationPermission(Permission.Add);
                if(FacadeManage.aidePlatformFacade.IsExistsKindID(gameKindItem.KindID))
                {
                    ShowError("游戏标识已经存在");
                    return;
                }
                msg = FacadeManage.aidePlatformFacade.InsertGameKindItem(gameKindItem);
            }
            else
            {
                //判断权限
                AuthUserOperationPermission(Permission.Edit);
                msg = FacadeManage.aidePlatformFacade.UpdateGameKindItem(gameKindItem);
            }

            if(msg.Success)
            {
                //更新游戏配置
                GameConfig gameConfig = new GameConfig();
                gameConfig.KindID = gameKindItem.KindID;
                gameConfig.NoticeChangeGolds = Convert.ToInt64(txtNoticeChangeGolds.Text);
                gameConfig.WinExperience = Convert.ToInt32(txtWinExperience.Text.Trim());
                FacadeManage.aidePlatformFacade.UpdateGameConfig(gameConfig);
                if(StrCmd == "add")
                {
                    ShowInfo("游戏信息增加成功", "GameKindItemList.aspx", 1200);
                }
                else
                {
                    ShowInfo("游戏信息修改成功", "GameKindItemList.aspx", 1200);
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
