using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Game.Facade;
using Game.Web.UI;
using Game.Kernel;
using Game.Utils;

namespace Game.Web.Module.MatchManager
{
    public partial class MatchRank : AdminPage
    {
        public byte matchType = 0;
        protected void Page_Load(object sender,EventArgs e)
        {
            if(!IsPostBack)
            {
                BindMatch();
                BindRankRecord();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender,EventArgs e)
        {
            BindRankRecord();
        }

        protected void anpPage_PageChanged(object sender,EventArgs e)
        {
            BindRankRecord();
        }

        /// <summary>
        /// 绑定比赛
        /// </summary>
        protected void BindMatch()
        {
            matchType = Convert.ToByte(ddlMatchType.SelectedValue);
            DataSet ds;
            ds = FacadeManage.aideGameMatchFacade.GetMatchListByMatchType(matchType);

            ddlMatchName.DataSource = ds;
            ddlMatchName.DataTextField = "MatchName";
            ddlMatchName.DataValueField = "MatchName";
            ddlMatchName.DataBind();
            ddlMatchName.Items.Insert(0,new ListItem("全部","0"));
        }

        /// <summary>
        /// 比赛类型选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMatchTypeSelect(object sender,EventArgs e)
        {
            BindMatch();
        }

        /// <summary>
        /// 绑定排名记录
        /// </summary>
        protected void BindRankRecord()
        {
            matchType = Convert.ToByte(ddlMatchType.SelectedValue);
            string where = string.Empty;
            if(ddlMatchName.SelectedValue != "0")
                where = " WHERE MatchName='" + TextFilter.FilterScript(ddlMatchName.SelectedValue) + "'";
            PagerSet pagerSet = FacadeManage.aideGameMatchFacade.GetTimingMatchHistoryGroup(matchType, PageIndex, anpPage.PageSize, where, "ORDER BY MatchStartTime DESC");
            rpData.DataSource = pagerSet.PageSet;
            rpData.DataBind();
            anpPage.RecordCount = pagerSet.RecordCount;
        }
    }
}