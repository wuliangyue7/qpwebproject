using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using Game.Kernel;
using Game.Facade;
using Game.Utils;
using System.Text;

namespace Game.Web.Module.MatchManager
{
    public partial class MatchRankInfo : AdminPage
    {
        protected void Page_Load(object sender,EventArgs e)
        {
            BindRank();
        }

        /// <summary>
        /// 绑定排名数据
        /// </summary>
        protected void BindRank()
        {
            int matchType = GameRequest.GetQueryInt("matchtype",0);
            int serverId = GameRequest.GetQueryInt("serverid",0);
            int matchId = GameRequest.GetQueryInt("matchid",0);
            Int64 matchNo = Convert.ToInt64(GameRequest.GetQueryString("matchno"));
            StringBuilder where = new StringBuilder();
            where.AppendFormat(" WHERE MatchType={0} AND ServerID={1} AND MatchID={2} AND MatchNo={3}",matchType,serverId,matchId,matchNo);
            PagerSet pagerSet = FacadeManage.aideGameMatchFacade.GetList("StreamMatchHistory",1,1000,where.ToString(),"ORDER BY RankID ASC");
            rptData.DataSource = pagerSet.PageSet;
            rptData.DataBind();
        }
    }
}