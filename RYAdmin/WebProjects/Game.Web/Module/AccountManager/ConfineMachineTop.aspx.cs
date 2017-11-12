using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Kernel;
using System.Text;
using Game.Utils;
using System.Data;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.AccountManager
{
    public partial class ConfineMachineTop : AdminPage
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                BindData();
            }
        }

        protected void DisableMachine( object sender, EventArgs e )
        {
            AuthUserOperationPermission( Permission.Add );
            StringBuilder sbValue = new StringBuilder();
            string machineList = GameRequest.GetFormString( "cid" );
            FacadeManage.aideAccountsFacade.BatchInsertConfineMachine( machineList );
            MessageBox( "操作成功", "ConfineMachineTop.aspx" );
        }

        //绑定数据
        private void BindData()
        {
            DataSet ds = FacadeManage.aideAccountsFacade.GetMachineRegisterTop100();
            if( ds.Tables[0].Rows.Count > 0 )
            {
                rptDataList.Visible = true;
                litNoData.Visible = false;
                rptDataList.DataSource = ds;
                rptDataList.DataBind();
            }
            else
            {
                rptDataList.Visible = true;
                litNoData.Visible = false;
            }
        }
    }
}
