using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Web.UI;
using Game.Entity.Accounts;

using Game.Kernel;
using Game.Entity.Platform;
using Game.Entity.Enum;
using System.Collections;
using Game.Entity.Record;
using Game.Facade;
using System.Text;

namespace Game.Web.Module.AccountManager
{
    public partial class AccountInfo : AdminPage
    {
        #region Fields

        /// <summary>
        /// 页面标题
        /// </summary>
        public string StrTitle
        {
            get
            {
                return "玩家-" + GetAccounts( IntParam ) + "-基本信息";
            }
        }


        protected int faceId = 0;    //头像ID
        protected int faceType = 0;  //头像类型
        protected string faceUrl = string.Empty;    //头像地址
        #endregion

        #region 窗口事件
        protected void Page_Load( object sender, EventArgs e )
        {
            //判断权限
            AuthUserOperationPermission( Permission.Read );
            if( Header != null )
                Title = StrTitle;
            if( !IsPostBack )
            {
                BindRight();
                BindData();
            }
        }

        protected void btnSave_Click( object sender, EventArgs e )
        {
            #region 验证

            //判断权限
            AuthUserOperationPermission( Permission.Edit );
            string strAccount = TextFilter.FilterAll( CtrlHelper.GetText( txtAccount ) );   //帐号
            string strNickName = TextFilter.FilterAll( CtrlHelper.GetText( txtNickName ) ); //昵称
            string strLoginPassword = CtrlHelper.GetText(txtLogonPass);
            string strInsurePassword = CtrlHelper.GetText(txtInsurePass);

            // 验证帐号
            if( string.IsNullOrEmpty( strAccount ) )
            {
                MessageBox( "帐号不能为空！" );
                return;
            }
            int length = Encoding.Default.GetBytes(strAccount).Length;
            if(length > 32 || length < 6) 
            {
                MessageBox("帐号的长度为6-32个字符");
                return;
            }

            // 验证昵称
            if( string.IsNullOrEmpty( strNickName ) )
            {
                strNickName = strAccount;
            }
            length = Encoding.Default.GetBytes(strNickName).Length;
            if(length > 32 || length < 6)
            {
                MessageBox("昵称的长度为6-32个字符");
                return;
            }

            // 验证登陆密码
            if(!string.IsNullOrEmpty(strLoginPassword))
            {
                if(strLoginPassword.Length > 32 || strLoginPassword.Length < 6)
                {
                    MessageBox("登陆密码的长度为6-32个字符");
                    return;
                }
            }

            // 验证保险柜密码
            if(!string.IsNullOrEmpty(strInsurePassword))
            {
                if(strInsurePassword.Length > 32 || strInsurePassword.Length < 6)
                {
                    MessageBox("保险柜密码的长度为6-32个字符");
                    return;
                }
            }

            //计算用户权限
            int intUserRight = 0;
            if( ckbUserRight.Items.Count > 0 )
            {
                foreach( ListItem item in ckbUserRight.Items )
                {
                    if( item.Selected )
                    {
                        intUserRight |= int.Parse( item.Value );
                    }
                }
            }
            //计算管理权限         
            int intMasterRight = 0;
            if( ckbMasterRight.Items.Count > 0 )
            {
                foreach( ListItem item in ckbMasterRight.Items )
                {
                    if( item.Selected )
                        intMasterRight |= int.Parse( item.Value );
                }
            }
            #endregion

            AccountsInfo model = new AccountsInfo();
            model = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID( IntParam );
            if( model == null )
                return;
            string strOldAccounts = model.Accounts;     //原帐号
            string strOldNickName = model.NickName;     //原昵称
            string strOldLogonPass = model.LogonPass;   //原登录密码
            string strOldInsurePass = model.InsurePass; //原银行密码

            model.UserID = IntParam;
            model.Accounts = strAccount;
            model.NickName = strNickName;
            model.LogonPass = string.IsNullOrEmpty(strLoginPassword) ? model.LogonPass : Utility.MD5(CtrlHelper.GetText(txtLogonPass));
            model.InsurePass = string.IsNullOrEmpty(strInsurePassword) ? model.InsurePass : Utility.MD5(CtrlHelper.GetText(txtInsurePass));
            model.UnderWrite = CtrlHelper.GetText( txtUnderWrite );
            model.Experience = CtrlHelper.GetInt( txtExperience, 0 );
            model.Present = CtrlHelper.GetInt( txtPresent, 0 );
            model.LoveLiness = CtrlHelper.GetInt( txtLoveLiness, 0 );
            model.Gender = byte.Parse( ddlGender.SelectedValue );
            model.FaceID = (short)GameRequest.GetFormInt( "inFaceID", 0 );
            model.Nullity = (byte)( ckbNullity.Checked ? 1 : 0 );
            model.StunDown = (byte)( ckbStunDown.Checked ? 1 : 0 );
            model.MoorMachine = byte.Parse( rdoMoorMachine.SelectedValue );

            model.IsAndroid = (byte)( chkIsAndroid.Checked ? 1 : 0 );
            model.UserRight = intUserRight;
            model.MasterRight = intMasterRight;
            model.MasterOrder = Convert.ToByte( ddlMasterOrder.SelectedValue.Trim() );

            //头像
            int type = (short)GameRequest.GetFormInt( "faceType", 0 );//头像类型
            if( type == 1 )
            {
                //系统头像
                model.FaceID = (short)GameRequest.GetFormInt( "inFaceID", 0 );
                model.CustomID = 0;
            }
            else
            {
                //自定义头像
                model.CustomID = GameRequest.GetFormInt( "inFaceID", 0 );
            }

            Message msg = new Message();
            msg = FacadeManage.aideAccountsFacade.UpdateAccount( model, userExt.UserID, GameRequest.GetUserIP() );

            if( msg.Success )
            {
                MessageBox( msg.Content, Request.Url.AbsoluteUri );
            }
            else
            {
                MessageBox( msg.Content );
            }
        }
        #endregion

        #region 数据加载
        private void BindRight()
        {
            //用户权限
            IList<EnumDescription> arrUserRight = UserRightHelper.GetUserRightList( typeof( UserRightStatus ) );
            ckbUserRight.DataSource = arrUserRight;
            ckbUserRight.DataValueField = "EnumValue";
            ckbUserRight.DataTextField = "Description";
            ckbUserRight.DataBind();
            //用户管理权限
            IList<EnumDescription> arrMasterRight = MasterRightHelper.GetMasterRightList( typeof( MasterRightStatus ) );
            ckbMasterRight.DataSource = arrMasterRight;
            ckbMasterRight.DataValueField = "EnumValue";
            ckbMasterRight.DataTextField = "Description";
            ckbMasterRight.DataBind();

        }
        private void BindData()
        {
            if( IntParam <= 0 )
                return;

            //获取信息
            AccountsInfo model = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID( IntParam );
            if( model == null )
            {
                MessageBox( "用户信息不存在" );
                return;
            }
            //CtrlHelper.SetText( ltUserID, model.UserID.ToString( ) );
            CtrlHelper.SetText( ltGameID, model.GameID.ToString() );
            CtrlHelper.SetText( ltRegAccounts, model.RegAccounts.Trim() );
            CtrlHelper.SetText( txtAccount, model.Accounts.Trim() );
            CtrlHelper.SetText( txtNickName, model.NickName.Trim() );
            CtrlHelper.SetText( litCompellation, model.Compellation );
            //CtrlHelper.SetText(litUserMedal, model.UserMedal.ToString());

            CtrlHelper.SetText( txtUnderWrite, model.UnderWrite.Trim() );
            CtrlHelper.SetCheckBoxValue( ckbNullity, model.Nullity );
            CtrlHelper.SetCheckBoxValue( ckbStunDown, model.StunDown );
            CtrlHelper.SetText( txtExperience, model.Experience.ToString().Trim() );
            CtrlHelper.SetText( txtPresent, model.Present.ToString().Trim() );
            CtrlHelper.SetText( txtLoveLiness, model.LoveLiness.ToString().Trim() );
            CtrlHelper.SetText( ltProtectID, model.ProtectID > 0 ? "<span style=\"font-weight: bold;\">已申请</span>&nbsp;<a href=\"javascript:openWindow('AccountsProtectInfo.aspx?param=" + model.ProtectID + "',580,320)\" class=\"l1\">点击查看</a>" : "未申请" );
            CtrlHelper.SetText( ltMemberInfo, GetMemberName( model.MemberOrder ) + ( model.MemberOrder == 0 ? "" : "&nbsp;&nbsp;&nbsp;&nbsp;到期时间：" + model.MemberSwitchDate.ToString( "yyyy-MM-dd mm:HH:ss" ) ) );
            if (model.SpreaderID != 0)
            {
                AccountsAgent agent = FacadeManage.aideAccountsFacade.GetAccountAgentByUserID(model.SpreaderID);
                if (agent.UserID != 0)
                {
                    litSName.Text = "代理商";
                }
                else
                {
                    litSName.Text = "推广人";
                }
                CtrlHelper.SetText(ltSpreader, GetAccounts(model.SpreaderID));
            }
            if( model.MemberOrder != 0 )
            {
                plMemberList.Visible = true;
            }

            faceUrl = FacadeManage.aideAccountsFacade.GetUserFaceUrl( model.FaceID, model.CustomID );
            if( model.CustomID != 0 && FacadeManage.aideAccountsFacade.GetAccountsFace( model.CustomID ) != null )
            {
                faceId = model.CustomID;
                faceType = 2;
            }
            else 
            {
                faceId = model.FaceID;
                faceType = 1;
            }

            ddlGender.SelectedValue = model.Gender.ToString();
            rdoMoorMachine.SelectedValue = model.MoorMachine.ToString();
            //用户权限
            int intUserRight = model.UserRight;
            if( ckbUserRight.Items.Count > 0 )
            {
                foreach( ListItem item in ckbUserRight.Items )
                {
                    item.Selected = int.Parse( item.Value ) == ( intUserRight & int.Parse( item.Value ) );
                }
            }
            //玩家身份
            ddlMasterOrder.SelectedValue = model.MasterOrder.ToString().Trim();
            //用户管理权限
            int intMasterRight = model.MasterRight;
            if( ckbMasterRight.Items.Count > 0 )
            {
                foreach( ListItem item in ckbMasterRight.Items )
                {
                    item.Selected = int.Parse( item.Value ) == ( intMasterRight & int.Parse( item.Value ) );
                }
            }
            //机器人
            CtrlHelper.SetCheckBoxValue( chkIsAndroid, model.IsAndroid );

            //登录、注册信息
            CtrlHelper.SetText( ltWebLogonTimes, model.WebLogonTimes.ToString() );
            CtrlHelper.SetText( ltGameLogonTimes, model.GameLogonTimes.ToString() );
            CtrlHelper.SetText( ltLastLogonDate, model.LastLogonDate.ToString( "yyyy-MM-dd HH:mm:ss" ) );
            CtrlHelper.SetText( ltLogonSpacingTime, Fetch.GetTimeSpan( Convert.ToDateTime( model.LastLogonDate ), DateTime.Now ) );
            CtrlHelper.SetText( ltLastLogonIP, model.LastLogonIP.ToString() );
            CtrlHelper.SetText( ltLogonIPInfo, IPQuery.GetAddressWithIP( model.LastLogonIP.ToString() ) );
            CtrlHelper.SetText( ltLastLogonMachine, model.LastLogonMachine.ToString() );
            CtrlHelper.SetText( ltRegisterDate, model.RegisterDate.ToString( "yyyy-MM-dd HH:mm:ss" ) );
            CtrlHelper.SetText( ltRegisterIP, model.RegisterIP.ToString() );
            CtrlHelper.SetText( ltRegIPInfo, IPQuery.GetAddressWithIP( model.RegisterIP.ToString() ) );
            CtrlHelper.SetText( ltRegisterMachine, model.RegisterMachine.ToString() );
            CtrlHelper.SetText(ltRegisterOrigin, GetRegisterOrigin(model.RegisterOrigin));
            CtrlHelper.SetText( ltOnLineTimeCount, Fetch.ConverTimeToDHMS( model.OnLineTimeCount ) );
            CtrlHelper.SetText( ltPlayTimeCount, Fetch.ConverTimeToDHMS( model.PlayTimeCount ) );

            //密保卡信息
            //PasswordCard pc = new PasswordCard( );
            if( model.PasswordID != 0 )
            {
                LiteralPasswordCard.Text = "<span style=\"font-weight: bold;\">已绑定</span>";
                spanPasswordCard.Visible = true;
            }
        }
        #endregion

        /// <summary>
        /// 获取注册来源
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private string GetRegisterOrigin(byte origin)
        {
            string rValue = "";
            switch (origin)
            {
                case 0x00:
                    rValue = "PC";
                    break;
                case 0x10:
                    rValue = "Android";
                    break;
                case 0x20:
                    rValue = "iTouch";
                    break;
                case 0x30:
                    rValue = "iPhone";
                    break;
                case 0x40:
                    rValue = "iPad";
                    break;
                case 0x50:
                    rValue = "WEB";
                    break;
                default:
                    rValue = "WEB";
                    break;
            }
            return rValue;
        }
    }
}
