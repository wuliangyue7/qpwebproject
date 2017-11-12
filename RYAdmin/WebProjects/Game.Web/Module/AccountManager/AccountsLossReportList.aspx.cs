using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Kernel;
using Game.Facade;
using Game.Entity.NativeWeb;
using System.Text;
using Game.Utils;
using Game.Web.UI;

namespace Game.Web.Module.AccountManager
{
    public partial class AccountsLossReportList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-") + "01";
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindData();
            }
        }

        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BindData()
        {
            //搜索条件
            string startDate = CtrlHelper.GetTextAndFilter(txtStartDate);
            string endDate = CtrlHelper.GetTextAndFilter(txtEndDate);
            StringBuilder where = new StringBuilder();
            if(string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                ShowError("请输入开始及结束日期");
                return;
            }
            where.AppendFormat(" WHERE ReportDate>='{0}' AND ReportDate<'{1}'", Convert.ToDateTime(startDate), Convert.ToDateTime(endDate).AddDays(1));

            //查询数据并绑定
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetList(LossReport.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, where.ToString(), "ORDER BY ReportDate DESC");
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rpData.DataSource = pagerSet.PageSet;
            rpData.DataBind();
            anpPage.RecordCount = pagerSet.RecordCount;
        }

        /// <summary>
        /// 获取申诉进度
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        protected string GetProcess(string pro)
        {
            if(pro == "" || pro == null)
            {
                return "";
            }
            else
            {
                string title = pro == "1" ? "发送邮件成功" : pro == "2"
                ? "发送邮件失败" : pro == "3" ? "更新密码成功" : "";
                return pro == "0" ? "<font class=\"hong\">未处理</font>" : string.Format("<label title=\"{0}\" class=\"lanse\">已处理</label>", title);
            }
        }
    }
}