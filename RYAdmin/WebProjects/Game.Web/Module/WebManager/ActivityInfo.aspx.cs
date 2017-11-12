using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;

namespace Game.Web.Module.WebManager
{
    public partial class ActivityInfo : AdminPage
    {
        protected void Page_Load(object sender,EventArgs e)
        {
            if(!IsPostBack)
            {
                BindData();
            }
        }

        protected void btnSave_Click(object sender,EventArgs e)
        {
            Activity model;
            if(IntParam > 0)
            {
                model = FacadeManage.aideNativeWebFacade.GetActivity(IntParam);
            }
            else
            {
                model = new Activity();
            }

            //图片验证
            try
            {
                if(string.IsNullOrEmpty(upImage.FilePath))
                {
                    ShowError("操作失败，请上传图片");
                    return;
                }
                model.ImageUrl = upImage.FilePath;
            }
            catch(Exception ex)
            {
                ShowError("操作失败，图片上传失败：" + ex.Message);
                return;
            }

            //更新数据
            model.Title = CtrlHelper.GetText(txtActivityTitle);
            model.ImageUrl = upImage.FilePath;
            model.Describe = CtrlHelper.GetText(txtBody);
            model.SortID = CtrlHelper.GetInt(txtSortID,0);
            model.IsRecommend = rblIsRecommend.SelectedValue == "1" ? true : false;
            if(IntParam > 0)
            {
                try
                {
                    FacadeManage.aideNativeWebFacade.UpdateActivity(model);
                    ShowInfo("更新成功","ActivityList.aspx",1000);
                }
                catch
                {
                    ShowError("更新失败");
                }
            }
            else
            {
                try
                {
                    FacadeManage.aideNativeWebFacade.AddActivity(model);
                    ShowInfo("新增成功","ActivityList.aspx",1000);
                }
                catch
                {
                    ShowError("新增失败");
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BindData()
        {
            if(IntParam > 0)
            {
                Activity model = FacadeManage.aideNativeWebFacade.GetActivity(IntParam);
                if(model != null)
                {
                    txtActivityTitle.Text = model.Title;
                    upImage.FilePath = model.ImageUrl;
                    CtrlHelper.SetText(txtBody, model.Describe);
                    txtSortID.Text = model.SortID.ToString();
                    rblIsRecommend.SelectedValue = model.IsRecommend ? "1" : "0";
                }
            }
        }
    }
}