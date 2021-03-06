﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Card.aspx.cs" Inherits="Game.Web.Pay.JFT.Card" %>
<%@ Register TagPrefix="qp" TagName="Header" Src="~/Themes/Standard/Common_Header.ascx" %>
<%@ Register TagPrefix="qp" TagName="Footer" Src="~/Themes/Standard/Common_Footer.ascx" %>
<%@ Register TagPrefix="qp" TagName="PaySidebar" Src="~/Themes/Standard/Pay_Sidebar.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="/css/base.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/pay/pay-bank.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.min.js"></script>
    <script src="/js/Check.js" type="text/javascript"></script>
    <script src="/js/utils.js" type="text/javascript"></script>    
</head>
<body>
    <!--头部开始-->
    <qp:Header ID="sHeader" runat="server" PageID="5"/>
    <!--头部结束-->

    <div class="ui-banner">
      <div class="ui-banner-bg-1"></div>
      <div class="ui-banner-bg-2"></div>
      <div class="ui-carousel-right">
        <div class="ui-carousel-left">
          <div class="ui-banner-img">
            <a href="javascript:;"><img src="/images/banner_2.png"></a>
          </div>
        </div>
      </div>
    </div>

    <div class="ui-content">
      <div class="ui-main">
        <div class="ui-page-title fn-clear">
          <a href="/index.aspx"><i class="ui-page-title-home"></i>首页</a>
          <i class="ui-page-title-current"></i>
          <a href="/Pay/index.aspx">充值中心</a>
          <i class="ui-page-title-current"></i>
          <span>账户充值</span>
          <div class="ui-page-title-right"><span>PAY&nbsp;CENTER</span><strong>账户充值</strong></div>
        </div>
        <div class="fn-clear">
          <!--左边开始-->
          <qp:PaySidebar ID="sPaySidebar" runat="server"/>
          <!--左边结束-->
          <div class="ui-main-details fn-right">
            <div class="ui-pay-step">
              <h2 class="ui-title-solid">充值流程</h2>
              <img src="/images/pay_step.png" />
            </div>
            <div class="ui-pay-way">
              <h2 class="ui-title-solid">您选择了&nbsp;<span><%= cardName %></span>&nbsp;方式</h2>
              <form name="fmStep1" runat="server" id="fmStep1">
                <script type="text/javascript">
                    $(document).ready(function () {
                        $("#txtPayAccounts").blur(function () { checkAccounts(); });
                        $("#txtPayReAccounts").blur(function () { checkReAccounts(); });
                        //$("#txtPayAmount").blur(function () { checkAmount(); });
                        $("#txtCardNumber").blur(function () { checkCardNumber(); });
                        $("#txtCardPassword").blur(function () { checkCardPassword(); });

                        $("#btnPay").click(function () {
                            return checkInput();
                        });
                    });

                    function checkAccounts() {
                        if ($.trim($("#txtPayAccounts").val()) == "") {
                            $("#txtPayAccountsTips").html("请输入您的游戏帐号");
                            return false;
                        }
                        $("#txtPayAccountsTips").html("");
                        return true;
                    }

                    function checkReAccounts() {
                        if ($.trim($("#txtPayReAccounts").val()) == "") {
                            $("#txtPayReAccountsTips").html("请再次输入游戏帐号");
                            return false;
                        }
                        if ($("#txtPayReAccounts").val() != $("#txtPayAccounts").val()) {
                            $("#txtPayReAccountsTips").html("两次输入的帐号不一致");
                            return false;
                        }
                        $("#txtPayReAccountsTips").html("");
                        return true;
                    }

                    function checkCardNumber() {
                        if ($.trim($("#txtCardNumber").val()) == "") {
                            $("#txtCardNumberTips").html("请输入充值卡号");
                            return false;
                        }
                        $("#txtCardNumberTips").html("");
                        return true;
                    }

                    function checkCardPassword() {
                        if ($.trim($("#txtCardPassword").val()) == "") {
                            $("#txtCardPasswordTips").html("请输入充值卡密码");
                            return false;
                        }
                        $("#txtCardPasswordTips").html("");
                        return true;
                    }

                    function checkAmount()
                    {
                        if ($.trim($("#ddlAmount").val()) == "0") {
                            $("#ddlAmountTips").html("请选择充值卡面值");
                            return false;
                        }
                        $("#ddlAmountTips").html("");
                        return true;
                    }


                    function checkInput() {
                        if (!checkAccounts()) { $("#txtPayAccounts").focus(); return false; }
                        if (!checkReAccounts()) { $("#txtPayReAccounts").focus(); return false; }
                        if (!checkAmount()) { return false; }
                        if (!checkCardNumber()) { $("#txtCardNumber").focus(); return false; }
                        if (!checkCardPassword()) { $("#txtCardPassword").focus(); return false; }
                    }
                </script>
                <ul>
                  <li>
                    <label>游戏帐号：</label>
                    <asp:TextBox ID="txtPayAccounts" runat="server" CssClass="ui-text-1"></asp:TextBox>
                    <span id="txtPayAccountsTips" style=" color:Red"></span>
                  </li>
                  <li>
                    <label>确认帐号：</label>
                    <asp:TextBox ID="txtPayReAccounts" runat="server" CssClass="ui-text-1"></asp:TextBox>
                    <span id="txtPayReAccountsTips" style="color:Red;"></span>
                  </li>
                  <li>
                    <label>选择面值：</label>
                    <asp:DropDownList ID="ddlAmount" runat="server" CssClass="text">
                        <asp:ListItem Value="0" Text="---请选择卡面值---"></asp:ListItem>
                    </asp:DropDownList>
                    <span id="ddlAmountTips" style="color:Red;"></span>
                  </li>
                  <li>
                    <label>充值卡号：</label>
                    <asp:TextBox ID="txtCardNumber" runat="server" CssClass="ui-text-1"></asp:TextBox>
                    <span id="txtCardNumberTips" style="color:Red;"></span>
                  </li>
                  <li>
                    <label>卡 密 码：</label>
                    <asp:TextBox ID="txtCardPassword" TextMode="Password" runat="server" CssClass="ui-text-1"></asp:TextBox>
                    <span id="txtCardPasswordTips" style="color:Red;"></span>
                  </li>
                  <li>
                    <p id="lblPayInfo">10 元= <%= 10 * rateGameBean%>游戏豆</p>
                    <span id="ePayInfo" style="color:Red;"></span>
                    <input type="hidden" name="hdfSalePrice" id="hdfSalePrice" runat="server" value="10" />
                  </li>
                  <li>
                    <asp:Button ID="btnPay" runat="server" CssClass="ui-btn-1" Text="确定" onclick="btnPay_Click" />
                  </li>
                </ul>
              </form>

              <form id="fmStep2" runat="server" action="http://pay.jtpay.com/form/pay" method="post">
        	     <div class="ui-result">
                    <p>
                        <asp:Label ID="lblAlertIcon" runat="server"></asp:Label>
                        <asp:Label ID="lblAlertInfo" runat="server" Text="操作提示"></asp:Label>
                        <%= formData%>
                    </p>
                    <p id="pnlContinue" runat="server">
                        <input id="btnReset1" type="button" value="继续充值" onclick="goURL('/Pay/JFT/Card.aspx');" class="ui-btn-1" />
                    </p>
                </div>
             </form>
             <%= js %>
            </div>
            <div class="ui-pay-alert">
              <h2 class="ui-title-solid">温馨提示</h2>
              <p>1、请确保您填写的帐号正确无误。</p>
              <p>2、充值过程中，浏览器会跳转至银行页面，支付成功后，会自动返回网站，如果没有跳转或是弹出充值成功的页面，请
                您不要关闭充值窗口。</p>
              <p>3、遇到任何充值问题，请您联系客服中心。</p>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!--尾部开始-->
    <qp:Footer ID="sFooter" runat="server" />
    <!--尾部结束-->
</body>
</html>
