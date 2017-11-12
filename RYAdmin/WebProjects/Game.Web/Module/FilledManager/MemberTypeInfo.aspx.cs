using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Entity.Enum;
using Game.Web.UI;
using Game.Facade;
using Game.Entity.Platform;
using Game.Entity.Accounts;

namespace Game.Web.Module.FilledManager
{
    public partial class MemberTypeInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindRight();
                BindPropertyKind();
                if(IntParam != 0)
                {
                    BindData();
                }
            }
        }

        // 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //计算用户权限
            int intUserRight = 0;
            if (ckbUserRight.Items.Count > 0)
            {
                foreach (ListItem item in ckbUserRight.Items)
                {
                    if (item.Selected)
                    {
                        intUserRight |= int.Parse(item.Value);
                    }
                }
            }
            MemberProperty memberType;
            if(IntParam <= 0)
            {
                return;
            }
            else
            {
                memberType = FacadeManage.aideAccountsFacade.GetMemberProperty(IntParam);
            }
            memberType.TaskRate = CtrlHelper.GetInt(txtTaskRate, 0);
            memberType.ShopRate = CtrlHelper.GetInt(txtShopRate, 0);
            memberType.InsureRate = CtrlHelper.GetInt(txtInsureRate, 0);
            memberType.DayPresent = CtrlHelper.GetInt(txtPresentScore, 0);
            memberType.DayGiftID = Convert.ToInt32(ddlDayGiftID.SelectedValue);
            memberType.UserRight = intUserRight;

            //判断权限
            AuthUserOperationPermission(Permission.Edit);
            FacadeManage.aideAccountsFacade.UpdateMemberType(memberType);
            ShowInfo("更新成功", "MemberTypeList.aspx", 1200);
        }

        #endregion

        #region 数据加载

        /// <summary>
        /// 绑定会员权限
        /// </summary>
        private void BindRight()
        {
            IList<EnumDescription> arrUserRight = UserRightHelper.GetUserRightList(typeof(UserRightStatus));
            List<EnumDescription> list = new List<EnumDescription>();
            foreach (EnumDescription d in arrUserRight)
            {
                if (d.EnumValue == 512)
                {
                    list.Add(d);
                }
            }
            ckbUserRight.DataSource = list;
            ckbUserRight.DataValueField = "EnumValue";
            ckbUserRight.DataTextField = "Description";
            ckbUserRight.DataBind();
        }

        /// <summary>
        /// 绑定礼包
        /// </summary>
        private void BindPropertyKind()
        {
            IList<GameProperty> property = FacadeManage.aidePlatformFacade.GetGamePropertyGift((int)GamePropertyKind.KIND11);
            ddlDayGiftID.DataSource = property;
            ddlDayGiftID.DataValueField = GameProperty._ID;
            ddlDayGiftID.DataTextField = GameProperty._Name;
            ddlDayGiftID.DataBind();

            ListItem list = new ListItem();
            list.Value = "0";
            list.Text = "无礼包";
            ddlDayGiftID.Items.Insert(0, list);
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            MemberProperty mt = FacadeManage.aideAccountsFacade.GetMemberProperty(IntParam);
            if(mt == null)
                return;

            CtrlHelper.SetText(lbCardName, mt.MemberName);
            CtrlHelper.SetText(txtTaskRate, mt.TaskRate.ToString());
            CtrlHelper.SetText(txtShopRate, mt.ShopRate.ToString());
            CtrlHelper.SetText(txtInsureRate, mt.InsureRate.ToString());
            CtrlHelper.SetText(txtPresentScore, mt.DayPresent.ToString());
            ddlDayGiftID.SelectedValue = mt.DayGiftID.ToString();

            //用户权限
            int intUserRight = mt.UserRight;
            if (ckbUserRight.Items.Count > 0)
            {
                foreach (ListItem item in ckbUserRight.Items)
                {
                    item.Selected = int.Parse(item.Value) == (intUserRight & int.Parse(item.Value));
                }
            }
        }
        #endregion
    }
}
