using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using Game.Entity.Accounts;
using Game.Kernel;
using Game.Entity.Record;
using System.Text;
using Game.Entity.Enum;
using System.Data;
using Game.Facade;
using Game.Utils;


namespace Game.Web.Module.AccountManager
{
    public partial class AccountsUpdatePass : AdminPage
    {
        protected void Page_Load( object sender, EventArgs e )
        {

        }

        protected void btnSave_Click( object sender, EventArgs e )
        {
            string loginPass = CtrlHelper.GetText( txtLoginPass );
            string insurePass = CtrlHelper.GetText( txtInsurePass );

            //查询用户
            AccountsInfo accountsInfo = new AccountsInfo();
            accountsInfo.UserID = IntParam;

            //验证登陆密码
            if( !string.IsNullOrEmpty( loginPass ) )
            {
                string reLoginPass=CtrlHelper.GetText(txtReLoginPass);
                if( loginPass != reLoginPass )
                {
                    MessageBox( "两次输入的登陆密码不一致" );
                    return;
                }
                accountsInfo.LogonPass = Utility.MD5( loginPass );
            }

            //验证保险柜密码
            if( !string.IsNullOrEmpty( insurePass ) )
            {
                string reInsurePass = CtrlHelper.GetText( txtReInsurePass );
                if( insurePass != reInsurePass )
                {
                    MessageBox( "两次输入的银行密码不一致" );
                    return;
                }
                accountsInfo.InsurePass = Utility.MD5( insurePass );
            }
            if( string.IsNullOrEmpty( loginPass ) && string.IsNullOrEmpty( insurePass ) )
            {
                MessageBox( "未修改任何数据" );
                return;
            }

            //更新数据
            if( FacadeManage.aideAccountsFacade.UpdateUserPassword( accountsInfo ) )
            {
                MessageBox( "更新成功" );
            }
            else
            {
                MessageBox( "更新失败" );
            }
        }
    }
}