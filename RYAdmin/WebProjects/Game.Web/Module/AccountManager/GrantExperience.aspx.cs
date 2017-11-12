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
using Game.Entity.Record;
using System.Text;
using Game.Entity.Enum;
using Game.Facade;



namespace Game.Web.Module.AccountManager
{
    public partial class GrantExperience : AdminPage
    {
        #region Fields   

        /// <summary>
        /// 页面标题
        /// </summary>
        public string StrTitle
        {
            get
            {
                return "玩家-赠送经验值";
            }

        }
        #endregion

        #region 窗口事件
        protected void Page_Load( object sender, EventArgs e )
        {
            //判断权限
            AuthUserOperationPermission( Permission.GrantExperience );
            if ( Header != null )
                Title = StrTitle;
        }
        protected void btnSave_Click( object sender, EventArgs e )
        {
            string strReason = CtrlHelper.GetText( txtReason );
            int intAddExperience = CtrlHelper.GetInt( txtAddExperience, 0 );
            if ( intAddExperience <= 0 )
            {
                MessageBox( "赠送经验数必须为大于零的正整数！" );
                return;
            }

            if ( string.IsNullOrEmpty( strReason ) )
            {
                MessageBox( "赠送原因不能为空" );
                return;
            }
            RecordGrantExperience grantExperience = new RecordGrantExperience( );
            AccountsInfo modelAccountInfo = new AccountsInfo( );
            grantExperience.ClientIP = GameRequest.GetUserIP( );
            grantExperience.MasterID = userExt.UserID;
            grantExperience.AddExperience = intAddExperience;
            grantExperience.Reason = strReason;

            string[ ] arrUserIDList = StrParamsList.Split( new char[ ] { ',' } );
            foreach ( string strid in arrUserIDList )
            {
                if ( Utils.Validate.IsPositiveInt( strid ) )
                {
                    grantExperience.UserID = int.Parse( strid );
                    grantExperience.CurExperience = FacadeManage.aideAccountsFacade.GetExperienceByUserID( int.Parse( strid ) );
                    modelAccountInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID( int.Parse( strid ) );
                    if ( modelAccountInfo == null )
                        continue;
                    modelAccountInfo.Experience = grantExperience.CurExperience + intAddExperience;
                    FacadeManage.aideAccountsFacade.UpdateAccount( modelAccountInfo, userExt.UserID, GameRequest.GetUserIP() );               //更新用户经验值
                    FacadeManage.aideRecordFacade.InsertRecordGrantExperience( grantExperience );    //插入赠送经验值日志                  
                }
            }
            MessageBox( "确认成功" );
        }
        #endregion
    }
}
