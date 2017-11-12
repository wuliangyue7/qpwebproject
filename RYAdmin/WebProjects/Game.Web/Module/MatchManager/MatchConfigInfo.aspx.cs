using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Game.Facade;
using Game.Utils;
using Game.Entity.Enum;
using Game.Web.UI;
using Game.Entity.GameMatch;
using System.Data;
using Game.Kernel;
using System.Text;
using Game.Entity.Platform;

namespace Game.Web.Module.MatchManager
{
    public partial class MatchConfigInfo : AdminPage
    {
        public string strReward = string.Empty;                         // 比赛奖励HTML

        protected void Page_Load(object sender, EventArgs e)
        {
            BindMatchReward();

            if(!IsPostBack)
            {
                BindMatchInfo();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MatchInfo model = new MatchInfo();
            model.MatchID = IntParam;
            model.MatchSummary = CtrlHelper.GetText(txtMatchSummary);
            model.MatchImage = upSmallImage.FilePath;
            model.MatchContent = CtrlHelper.GetText(txtContent);
            model.SortID = Convert.ToInt32(CtrlHelper.GetText(txtSortID));
            if (string.IsNullOrEmpty(upSmallImage.FilePath))
            {
                ShowError("操作失败，请上传展示小图");
                return;
            }
            if (string.IsNullOrEmpty(model.MatchContent))
            {
                ShowError("操作失败，请输入比赛描述");
                return;
            }

            Message msg = FacadeManage.aideGameMatchFacade.UpdateMatchInfo(model);
            if (msg.Success)
            {
                ShowInfo("修改成功", "MatchConfigList.aspx", 1000);
            }
            else
            {
                ShowError("修改失败");
            }
        }

        /// <summary>
        /// 绑定比赛信息
        /// </summary>
        protected void BindMatchInfo()
        {
            if (IntParam <= 0)
            {
                return;
            }

            MatchInfo matchInfo = FacadeManage.aideGameMatchFacade.GetMatchInfo(IntParam);
            MatchPublic matchPublic = FacadeManage.aideGameMatchFacade.GetMatchPublicInfo(IntParam);

            // 绑定基础配置
            if (matchPublic != null)
            {                
                lblMatchName.Text = matchPublic.MatchName;
                lblMatchTypeName.Text = GetMatchTypeName(matchPublic.MatchType);
                lblMatchStatusName.Text = GetMatchStatusName(matchPublic.MatchStatus);
                lblKindName.Text = GetGameKindName(matchPublic.KindID);
            }
            // 绑定网站展示
            if (matchInfo != null)
            {
                lblMatchDate.Text = matchInfo.MatchDate;
                txtMatchSummary.Text = matchInfo.MatchSummary;
                upSmallImage.FilePath = matchInfo.MatchImage;
                txtSortID.Text = matchInfo.SortID.ToString();
                CtrlHelper.SetText(txtContent, matchInfo.MatchContent);
            }
            
        }

        /// <summary>
        /// 绑定奖品
        /// </summary>
        protected void BindMatchReward()
        {
            // 绑定奖励配置
            string template = string.Empty;
            template += "<div class=\"ui-reward-item\">";
            template += "第<span class=\"ui-item-serial\">{0}</span>名：";
            template += "游戏币：<input type=\"text\" class=\"text wd2\" name=\"gold\" value=\"{1}\" readonly> ";
            template += "元宝：<input type=\"text\" class=\"text wd2\" name=\"medal\" value=\"{2}\"/ readonly>  ";
            template += "经验：<input type=\"text\" class=\"text wd2\" name=\"experience\" value=\"{3}\" readonly/> ";
            template += "</div>";
            DataSet dsReward = FacadeManage.aideGameMatchFacade.GetMatchRewardList(IntParam);
            if (dsReward.Tables[0].Rows.Count > 0)
            {
                strReward = string.Empty;
                DataRow dr;
                for (int i = 0; i < dsReward.Tables[0].Rows.Count; i++)
                {
                    dr = dsReward.Tables[0].Rows[i];
                    strReward += string.Format(template, i + 1, dr["RewardGold"], dr["RewardIngot"], dr["RewardExperience"]);
                }
            }
        }

        #region 公共方法

        /// <summary>
        /// 获取游戏属性
        /// </summary>
        /// <param name="joinID"></param>
        /// <returns></returns>
        protected string GetMatchTypeName(byte matchType)
        {
            if (matchType == 0)
            {
                return "定时赛";
            }
            else if (matchType == 1)
            {
                return "即时赛";
            }
            else
            {
                return "未知";
            }
        }

        /// <summary>
        /// 推荐名称
        /// </summary>
        /// <param name="recommend"></param>
        /// <returns></returns>
        protected string GetMatchStatusName(byte matchStatus)
        {
            string rValue = "";
            switch (matchStatus)
            {
                case 0:
                    rValue = "<span class='hong'>未开始</span>";
                    break;
                case 2:
                    rValue = "<span class='lan'>进行中</span>";
                    break;
                case 8:
                    rValue = "<span class='green'>已结束</span>";
                    break;
                default:
                    rValue = "未知";
                    break;
            }
            return rValue;
        }
        #endregion       
    }
}