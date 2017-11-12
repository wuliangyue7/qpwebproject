using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Utils;
using Game.Entity.Platform;
using Game.Kernel;
using Game.Entity.Enum;
using System.Data;
using Game.Facade;

namespace Game.Web.Module.AppManager
{
    public partial class SigninConfig : AdminPage
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                BindConfig();
            }
        }

        /// <summary>
        /// 更新配置事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click( object sender, EventArgs e )
        {
            //验证权限
            AuthUserOperationPermission( Permission.Edit );

            //数据验证
            if( !Page.IsValid )
            {
                return;
            }

            //更新数据
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            table.TableName = "SigninConfig";
            table.Columns.Add( "DayID" );
            table.Columns.Add( "RewardGold" );
            for( int i = 1; i <= 7; i++ )
            {
                DataRow dr = table.NewRow();
                dr["DayID"] = i;
                TextBox tb = Page.Form.FindControl( "txtGold" + i ) as TextBox;
                if( tb != null )
                {
                    dr["RewardGold"] = tb.Text;
                }
                table.Rows.Add( dr );
            }
            ds.Tables.Add( table );
            FacadeManage.aidePlatformFacade.UpdateSigninConfig( ds );
            ShowInfo( "操作成功" );
        }

        /// <summary>
        /// 绑定配置
        /// </summary>
        protected void BindConfig()
        {
            DataSet ds = FacadeManage.aidePlatformFacade.GetSigninConfig();
            if( ds.Tables[0].Rows.Count > 0 )
            {
                try
                {
                    for( int i = 1; i <= 7; i++ )
                    {
                        TextBox tb = Page.Form.FindControl( "txtGold" + i ) as TextBox;
                        if( tb != null )
                        {
                            tb.Text = ds.Tables[0].Rows[i - 1]["RewardGold"].ToString();
                        }
                    }
                }
                catch
                {

                }
            }
        }
    }
}
