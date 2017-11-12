using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Enum;
using Game.Facade;
using Game.Entity.NativeWeb;
using System.Data;

namespace Game.Web.Module.WebManager
{
    public partial class MobileNoticeInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                GameNewsDataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProcessData();
            GameNewsDataBind();
        }
        #endregion

        #region 数据加载

        private void GameNewsDataBind()
        {
            if(StrCmd == "add")
            {
                litInfo.Text = "新增";
            }
            else
            {
                litInfo.Text = "更新";
            }

            if(IntParam <= 0)
            {
                return;
            }

            //获取类型信息
            News news = FacadeManage.aideNativeWebFacade.GetNewsInfo(IntParam);
            if(news == null)
            {
                ShowError("新闻信息不存在");
                Redirect("NewsList.aspx");
                return;
            }
            CtrlHelper.SetText(txtSubject, news.Subject);
            chkOnTop.Checked = news.OnTop == 1;
            CtrlHelper.SetText(txtBody, news.Body);
            upImageUrl.FilePath = news.ImageUrl;
        }      
        #endregion

        #region 处理方法

        private void ProcessData()
        {
            if(!IsValid)
                return;

            News news = new News();
            news.Subject = CtrlHelper.GetText(txtSubject);
            news.OnTop = Convert.ToByte(chkOnTop.Checked);
            news.Body = txtBody.Text.Trim();                        

            // 验证新闻
            if(string.IsNullOrEmpty(news.Body))
            {
                ShowError("请输入新闻内容");
                return;
            }
            //图片上传
            try
            {
                news.ImageUrl = upImageUrl.FilePath;
                if (string.IsNullOrEmpty(news.ImageUrl))
                {
                    ShowError("请选择一个广告图片！");
                    return;
                }
            }
            catch (Exception ex)
            {
                ShowError("广告图片上传失败：" + ex.Message);
                return;
            }

            Message msg = new Message();
            if(StrCmd == "add")
            {
                //判断权限
                AuthUserOperationPermission(Permission.Add);
                news.UserID = userExt.UserID;
                news.IssueIP = Utility.UserIP;
                news.ClassID = 3;
                msg = FacadeManage.aideNativeWebFacade.InsertNews(news);
            }
            else
            {
                //判断权限
                AuthUserOperationPermission(Permission.Edit);
                news.NewsID = IntParam;
                news.LastModifyIP = Utility.UserIP;
                news.ClassID = 3;
                msg = FacadeManage.aideNativeWebFacade.UpdateNews(news);
            }

            if(msg.Success)
            {
                if(StrCmd == "add")
                {
                    ShowInfo("公告增加成功", "MobileNoticeList.aspx", 1200);
                }
                else
                {
                    ShowInfo("公告修改成功", "MobileNoticeList.aspx", 1200);
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