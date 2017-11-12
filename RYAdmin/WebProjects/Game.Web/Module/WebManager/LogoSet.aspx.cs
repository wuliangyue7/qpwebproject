using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Utils;
using Game.Web.UI;
using Game.Kernel;
using System.Text;
using Game.Entity.NativeWeb;
using Game.Entity.Accounts;
using Game.Entity.Enum;
using System.IO;

namespace Game.Web.Module.WebManager
{
    public partial class LogoSet : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void btnSave_Click( object sender, EventArgs e )
        //{
        //    //判断权限
        //    AuthUserOperationPermission( Permission.Edit );

        //    //验证图片
        //    HttpPostedFile fileLogo = fuLogo.PostedFile;
        //    if( fileLogo.ContentLength != 0 )
        //    {
        //        if( fuLogo.FileName.Substring( fuLogo.FileName.LastIndexOf( "." ) + 1 ).ToLower() != "png" )
        //        {
        //            ShowError( "前台LOGO必须为PNG格式" );
        //            return;
        //        }
        //        try
        //        {
        //            // 转化图片
        //            System.Drawing.Image img = System.Drawing.Image.FromStream( fileLogo.InputStream );
        //            img.Dispose();
        //        }
        //        catch
        //        {
        //            ShowError( "不是合法的图片" );
        //            return;
        //        }
        //    }

        //    //检查目录
        //    string serverPath = Server.MapPath( "/Upload/Site" );
        //    if( !Directory.Exists( serverPath ) )
        //    {
        //        Directory.CreateDirectory( serverPath );
        //    }

        //    //保存图片
        //    fileLogo.SaveAs( Server.MapPath( "/Upload/Site/Logo.png" ) );
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Edit);

            Message msg = new Message();
            msg = SaveImage(fuLogo, "/Upload/Site", "logo.png");
            if(!msg.Success)
            {
                ShowError(msg.Content);
                return;
            }
            msg = SaveImage(fuAdminLogo, "/Upload/Site", "Adminlogo.png");
            if(!msg.Success)
            {
                ShowError(msg.Content);
                return;
            }
            msg = SaveImage(fuMobileLogo, "/Upload/Site", "MobileLogo.png");
            if(!msg.Success)
            {
                ShowError(msg.Content);
                return;
            }
            msg = SaveImage(fuMobileRegLogo, "/Upload/Site", "MobileRegLogo.png");
            if (!msg.Success)
            {
                ShowError(msg.Content);
                return;
            }
            ShowInfo("操作成功");
            Response.Redirect("LogoSet.aspx");
        }

        protected Message SaveImage(FileUpload fileControl, string path, string fileName)
        {
            Message msg = new Message();

            //验证图片
            HttpPostedFile file = fileControl.PostedFile;
            if(file.ContentLength != 0)
            {
                if(fileControl.FileName.Substring(fileControl.FileName.LastIndexOf(".") + 1).ToLower() != "png")
                {
                    msg.Content = "LOGO必须为PNG格式";
                    msg.Success = false;
                    return msg;
                }
                try
                {
                    // 转化图片
                    System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                }
                catch
                {
                    msg.Content = "不是合法的图片";
                    msg.Success = false;
                    return msg;
                }
            }
            else
            {
                return msg;
            }

            //检查目录
            string serverPath = Server.MapPath(path);
            if(!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }

            //保存图片
            file.SaveAs(Server.MapPath(path + "/" + fileName));
            return msg;
        }
    }
}
