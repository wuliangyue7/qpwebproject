using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Kernel;
using Game.Web.UI;
using Game.Utils;
using Game.Entity.Platform;
using Game.Entity.Enum;
using Game.Facade;
using System.Data;

namespace Game.Web.Module.TaskManager
{
    public partial class TaskInfo : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindMatch();
                BindTaskType();
                BindGame();
                BindTask();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(!IsValid)
            {
                return;
            }

            //验证任务类型
            int taskType = -1;
            taskType = Convert.ToInt32(ddlTaskType.SelectedValue);
            if(taskType == -1)
            {
                ShowError("请选择任务类型");
                return;
            }

            //验证可领取任务玩家类型
            int userType = 0;
            for(int i = 1; i <= cblUserType.Items.Count; i++)
            {
                if(cblUserType.Items[i - 1].Selected)
                {
                    userType = userType | i;
                }
            }
            if(userType == 0)
            {
                ShowError("请选择可领取任务玩家类型");
                return;
            }

            //验证游戏
            int kindID = -1;
            kindID = Convert.ToInt32(ddlGameKind.SelectedValue);
            if(kindID == -1)
            {
                ShowError("请选择任务所属游戏");
                return;
            }

            //验证局数
            int innings = CtrlHelper.GetInt(txtInnings, 0);
            if(innings == 0)
            {
                ShowError("请输入比赛局数");
                return;
            }

            //验证比赛
            int matchID = 0;
            //if( taskType == (int)EnumerationList.TaskType.比赛任务 ) 
            //{
            //    matchID = Convert.ToInt32( ddlMatchID.SelectedValue );
            //    if( matchID == 0 ) 
            //    {
            //        ShowError( "请选择任务所属比赛" );
            //        return;
            //    }
            //}

            Game.Entity.Platform.TaskInfo taskInfo = new Game.Entity.Platform.TaskInfo();
            if(IntParam > 0)
            {
                AuthUserOperationPermission(Permission.Edit);
                taskInfo = FacadeManage.aidePlatformFacade.GetTaskInfoByID(IntParam);
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
                taskInfo.InputDate = DateTime.Now;
            }

            taskInfo.TaskName = CtrlHelper.GetText(txtTaskName);
            taskInfo.TaskType = taskType;
            taskInfo.UserType = Convert.ToByte(userType);
            taskInfo.KindID = kindID;
            taskInfo.StandardAwardGold = CtrlHelper.GetInt(txtStandardAwardGold, 0);
            taskInfo.StandardAwardMedal = CtrlHelper.GetInt(txtStandardAwardMedal, 0);
            taskInfo.MemberAwardGold = CtrlHelper.GetInt(txtMemberAwardGold, 0);
            taskInfo.MemberAwardMedal = CtrlHelper.GetInt(txtMemberAwardMedal, 0);
            taskInfo.TimeLimit = CtrlHelper.GetInt(txtTimeLimit, 0);
            taskInfo.TaskDescription = TextUtility.CutString(CtrlHelper.GetText(txtTaskDescription), 0, 500);
            taskInfo.Innings = innings;
            taskInfo.MatchID = matchID;

            bool isSuccess;
            if(IntParam > 0)
            {
                isSuccess = FacadeManage.aidePlatformFacade.UpdateTaskInfo(taskInfo);
            }
            else
            {
                isSuccess = FacadeManage.aidePlatformFacade.InsertTaskInfo(taskInfo);
            }

            if(isSuccess)
            {
                ShowInfo("保存任务信息成功", "TaskList.aspx", 1000);
            }
            else
            {
                ShowError("保存任务信息失败");
            }
        }

        /// <summary>
        /// 可领取任务玩家类型选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cblUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cblUserType.Items[1].Selected)
            {
                trMemberGold.Visible = true;
                trMemberMedal.Visible = true;
            }
            else
            {
                trMemberGold.Visible = false;
                trMemberMedal.Visible = false;
                txtMemberAwardGold.Text = "0";
                txtMemberAwardMedal.Text = "0";
            }
        }

        /// <summary>
        /// 选择任务类型触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(Convert.ToInt32(ddlTaskType.SelectedValue) == (int)EnumerationList.TaskType.比赛任务)
            //{
            //    trMatchID.Visible = true;
            //}
            //else
            //{
            //    trMatchID.Visible = false;
            //}
            if(Convert.ToInt32(ddlTaskType.SelectedValue) == (int)EnumerationList.TaskType.首胜)
            {
                txtInnings.Text = "1";
                txtInnings.ReadOnly = true;
            }
            else 
            {
                txtInnings.ReadOnly = false;
            }
        }

        /// <summary>
        /// 绑定任务类型
        /// </summary>
        protected void BindTaskType()
        {
            ddlTaskType.DataSource = Utility.EnumToList(typeof(EnumerationList.TaskType));
            ddlTaskType.DataValueField = "value";
            ddlTaskType.DataTextField = "text";
            ddlTaskType.DataBind();
            ddlTaskType.Items.Insert(0, new ListItem("==请选择任务类型==", "-1"));
        }

        /// <summary>
        /// 绑定游戏
        /// </summary>
        protected void BindGame()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetGameKindItemList(1, Int32.MaxValue, "", "ORDER BY KindID ASC");
            if(pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                ddlGameKind.DataSource = pagerSet.PageSet;
                ddlGameKind.DataTextField = "KindName";
                ddlGameKind.DataValueField = "KindID";
                ddlGameKind.DataBind();
                ddlGameKind.Items.Insert(0, new ListItem("==请选择所属游戏==", "-1"));
            }
        }

        /// <summary>
        /// 绑定比赛信息
        /// </summary>
        protected void BindMatch()
        {
            DataSet ds = FacadeManage.aideGameMatchFacade.GetAllMatch();
            if(ds.Tables[0].Rows.Count > 0)
            {
                ddlMatchID.DataSource = ds;
                ddlMatchID.DataTextField = "MatchName";
                ddlMatchID.DataValueField = "MatchID";
                ddlMatchID.DataBind();
                ddlMatchID.Items.Insert(0, new ListItem("==请选择所属比赛==", "0"));
            }
        }

        /// <summary>
        /// 绑定任务信息
        /// </summary>
        protected void BindTask()
        {
            Game.Entity.Platform.TaskInfo taskInfo = FacadeManage.aidePlatformFacade.GetTaskInfoByID(IntParam);
            if(taskInfo != null)
            {
                txtTaskName.Text = taskInfo.TaskName;
                ddlTaskType.SelectedValue = taskInfo.TaskType.ToString();
                ddlGameKind.SelectedValue = taskInfo.KindID.ToString();
                txtStandardAwardGold.Text = taskInfo.StandardAwardGold.ToString();
                txtStandardAwardMedal.Text = taskInfo.StandardAwardMedal.ToString();
                txtMemberAwardGold.Text = taskInfo.MemberAwardGold.ToString();
                txtMemberAwardMedal.Text = taskInfo.MemberAwardMedal.ToString();
                txtTimeLimit.Text = taskInfo.TimeLimit.ToString();
                txtTaskDescription.Text = taskInfo.TaskDescription;
                for(int i = 1; i <= cblUserType.Items.Count; i++)
                {
                    if((i & taskInfo.UserType) == i)
                        cblUserType.Items[i - 1].Selected = true;
                    else
                    {
                        cblUserType.Items[i - 1].Selected = false;
                        if(i == 2)
                        {
                            trMemberGold.Visible = false;
                            trMemberMedal.Visible = false;
                            txtMemberAwardGold.Text = "0";
                            txtMemberAwardMedal.Text = "0";
                        }
                    }
                }
                //if( taskInfo.TaskType == (int)EnumerationList.TaskType.比赛任务 )
                //{
                //    trMatchID.Visible = true;
                //    try
                //    {
                //        ddlMatchID.SelectedValue = taskInfo.MatchID.ToString();
                //    }
                //    catch { }
                //}
                //else
                //{
                //    trMatchID.Visible = false;
                //}
                txtInnings.Text = taskInfo.Innings.ToString();
            }
        }
    }
}
