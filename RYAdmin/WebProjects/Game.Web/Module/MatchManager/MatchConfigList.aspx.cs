using System;
using Game.Entity.Enum;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.UI;
using Game.Entity.GameMatch;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;

namespace Game.Web.Module.MatchManager
{
    public partial class MatchConfigList : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GameDataBind();
            }
        }

        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            GameDataBind();
        }

        /// <summary>
        /// 批量冻结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNoEnable_Click(object sender, EventArgs e)
        {
            string strQuery = "Update " + MatchInfo.Tablename + " Set Nullity = 1 WHERE MatchID in (" + StrCIdList + ") and Nullity=0";
            int result = FacadeManage.aideGameMatchFacade.ExecuteSql(strQuery);
            if (result > 0)
            {
                ShowInfo("冻结成功");
            }
            else
            {
                ShowError("冻结失败，没有需要冻结的消息！");
            }

            GameDataBind();
        }

        /// <summary>
        /// 批量解冻
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnable_Click(object sender, EventArgs e)
        {
            string strQuery = "Update " + MatchInfo.Tablename + " Set Nullity = 0 WHERE MatchID in (" + StrCIdList + ") and Nullity = 1";
            int result = FacadeManage.aideGameMatchFacade.ExecuteSql(strQuery);
            if (result > 0)
            {
                ShowInfo("解冻成功");
            }
            else
            {
                ShowError("解冻失败，没有需要解冻的消息！");
            }

            GameDataBind();
        }

        #endregion

        #region 数据绑定

        //绑定数据
        private void GameDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideGameMatchFacade.GetList(MatchInfo.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                litNoData.Visible = false;
            }
            else
            {
                litNoData.Visible = true;
            }

            rptGameKindItem.DataSource = pagerSet.PageSet;
            rptGameKindItem.DataBind();
            anpNews.RecordCount = pagerSet.RecordCount;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchItems
        {
            get
            {
                if (ViewState["SearchItems"] == null)
                {
                    StringBuilder condition = new StringBuilder();
                    condition.Append(" WHERE 1=1 ");

                    ViewState["SearchItems"] = condition.ToString();
                }

                return (string)ViewState["SearchItems"];
            }

            set { ViewState["SearchItems"] = value; }
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        public string Orderby
        {
            get
            {
                if (ViewState["Orderby"] == null)
                {
                    ViewState["Orderby"] = "ORDER BY MatchID DESC";
                }

                return (string)ViewState["Orderby"];
            }

            set { ViewState["Orderby"] = value; }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取游戏属性
        /// </summary>
        /// <param name="joinID"></param>
        /// <returns></returns>
        protected string GetMatchTypeName(int matchID)
        {
            MatchPublic item = FacadeManage.aideGameMatchFacade.GetMatchPublicInfo(matchID);
            if (item != null)
            {
                if (item.MatchType == 0)
                {
                    return "定时赛";
                }
                else if (item.MatchType == 1)
                {
                    return "即时赛";
                }
                else
                {
                    return "未知";
                }
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
        protected string GetMatchStatusName(int matchID)
        {
            MatchPublic item = FacadeManage.aideGameMatchFacade.GetMatchPublicInfo(matchID);
            string rValue = "";
            if (item != null)
            {
                switch (item.MatchStatus)
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
                        rValue = "<span>未知</span>";
                        break;
                }
            }
            else
            {
                rValue = "<span>未知</span>";
            }            
            return rValue;
        }
        #endregion       
    }
}