@echo off

TITLE 网狐经典版数据库 自动安装中...请注意：安装过程中请勿关闭

md D:\数据库\经典平台

set rootPath=1.数据库脚本\
osql -E -i "%rootPath%1.数据库删除.sql"
osql -E -i "%rootPath%1_1_网站库脚本.sql"
osql -E -i "%rootPath%1_2_后台库脚本.sql"
osql -E -i "%rootPath%2_1_网站库脚本.sql"
osql -E -i "%rootPath%2_2_后台库脚本.sql"

set rootPath=2.数据脚本\
osql -E -i "%rootPath%充值服务.sql"
osql -E -i "%rootPath%后台数据.sql"
osql -E -i "%rootPath%实卡类型.sql"
osql -E -i "%rootPath%推广数据.sql"
osql -E -i "%rootPath%泡点设置.sql"
osql -E -i "%rootPath%独立页面.sql"
osql -E -i "%rootPath%站点配置.sql"
osql -E -i "%rootPath%系统广告.sql"
osql -E -i "%rootPath%网站链接.sql"
osql -E -i "%rootPath%转盘数据.sql"
osql -E -i "%rootPath%会员属性.sql"

set rootPath=3.存储过程\作业脚本\
osql -E -i "%rootPath%每日统计(作业).sql"
osql -E -i "%rootPath%统计玩家税收(作业).sql"
osql -E -i "%rootPath%统计代理充值(作业).sql"

set rootPath=3.存储过程\公共过程\
osql -d RYAccountsDB -E  -n -i "%rootPath%分页过程.sql"
osql -d RYGameMatchDB -E  -n -i "%rootPath%分页过程.sql"
osql -d RYGameScoreDB -E  -n -i "%rootPath%分页过程.sql"
osql -d RYNativeWebDB -E  -n -i "%rootPath%分页过程.sql"
osql -d RYPlatformDB -E  -n -i "%rootPath%分页过程.sql"
osql -d RYPlatformManagerDB -E  -n -i "%rootPath%分页过程.sql"
osql -d RYRecordDB -E  -n -i "%rootPath%分页过程.sql"
osql -d RYTreasureDB -E  -n -i "%rootPath%分页过程.sql"

osql -d RYAccountsDB -E  -n -i "%rootPath%切字符串.sql"
osql -d RYGameMatchDB -E  -n -i "%rootPath%切字符串.sql"
osql -d RYGameScoreDB -E  -n -i "%rootPath%切字符串.sql"
osql -d RYNativeWebDB -E  -n -i "%rootPath%切字符串.sql"
osql -d RYPlatformDB -E  -n -i "%rootPath%切字符串.sql"
osql -d RYPlatformManagerDB -E  -n -i "%rootPath%切字符串.sql"
osql -d RYRecordDB -E  -n -i "%rootPath%切字符串.sql"
osql -d RYTreasureDB -E  -n -i "%rootPath%切字符串.sql"

set rootPath=3.存储过程\函数\
osql -E -i "%rootPath%查询指定玩家的代理玩家.sql"

set rootPath=3.存储过程\前台脚本\本地数据库\
osql -E -i "%rootPath%推荐游戏.sql"
osql -E -i "%rootPath%购买奖品.sql"

set rootPath=3.存储过程\前台脚本\比赛数据库\
osql -E -i "%rootPath%比赛排行.sql"

set rootPath=3.存储过程\前台脚本\用户数据库\
osql -E -i "%rootPath%修改密码.sql"
osql -E -i "%rootPath%修改资料.sql"
osql -E -i "%rootPath%固定机器.sql"
osql -E -i "%rootPath%奖牌兑换.sql"
osql -E -i "%rootPath%每日签到.sql"
osql -E -i "%rootPath%用户全局信息.sql"
osql -E -i "%rootPath%用户名检测.sql"
osql -E -i "%rootPath%用户注册.sql"
osql -E -i "%rootPath%用户登录.sql"
osql -E -i "%rootPath%获取用户信息.sql"
osql -E -i "%rootPath%账户保护.sql"
osql -E -i "%rootPath%重置密码.sql"
osql -E -i "%rootPath%魅力兑换.sql"
osql -E -i "%rootPath%自定头像.sql"

set rootPath=3.存储过程\前台脚本\积分数据库\
osql -E -i "%rootPath%负分清零.sql"
osql -E -i "%rootPath%逃率清零.sql"

set rootPath=3.存储过程\前台脚本\网站数据库\
osql -E -i "%rootPath%更新浏览.sql"
osql -E -i "%rootPath%比赛报名.sql"
osql -E -i "%rootPath%获取新闻.sql"
osql -E -i "%rootPath%购买奖品.sql"
osql -E -i "%rootPath%问题反馈.sql"

set rootPath=3.存储过程\前台脚本\金币数据库\
osql -E -i "%rootPath%代理结算.sql"
osql -E -i "%rootPath%在线充值.sql"
osql -E -i "%rootPath%在线订单.sql"
osql -E -i "%rootPath%实卡充值.sql"
osql -E -i "%rootPath%推广中心.sql"
osql -E -i "%rootPath%推广信息.sql"
osql -E -i "%rootPath%苹果充值.sql"
osql -E -i "%rootPath%金币取款.sql"
osql -E -i "%rootPath%金币存款.sql"
osql -E -i "%rootPath%金币转账.sql"
osql -E -i "%rootPath%手游充值.sql"
osql -E -i "%rootPath%分享赠送.sql"
osql -E -i "%rootPath%转盘抽奖.sql"

set rootPath=3.存储过程\后台脚本\帐号库\
osql -E -i "%rootPath%插入限制IP.sql"
osql -E -i "%rootPath%插入限制机器码.sql"
osql -E -i "%rootPath%更新用户.sql"
osql -E -i "%rootPath%注册IP统计.sql"
osql -E -i "%rootPath%注册机器码统计.sql"
osql -E -i "%rootPath%添加用户.sql"
osql -E -i "%rootPath%创建代理.sql"

set rootPath=3.存储过程\后台脚本\平台库\
osql -E -i "%rootPath%在线统计.sql"

set rootPath=3.存储过程\后台脚本\数据分析\
osql -E -i "%rootPath%充值统计.sql"
osql -E -i "%rootPath%其他统计.sql"
osql -E -i "%rootPath%活跃统计.sql"
osql -E -i "%rootPath%用户统计.sql"
osql -E -i "%rootPath%金币分布.sql"

set rootPath=3.存储过程\后台脚本\权限库\
osql -E -i "%rootPath%权限加载.sql"
osql -E -i "%rootPath%用户表操作.sql"
osql -E -i "%rootPath%管理员登录.sql"
osql -E -i "%rootPath%菜单加载.sql"

set rootPath=3.存储过程\后台脚本\比赛库\
osql -E -i "%rootPath%比赛排名.sql"

set rootPath=3.存储过程\后台脚本\积分库\
osql -E -i "%rootPath%清零积分.sql"
osql -E -i "%rootPath%清零逃率.sql"
osql -E -i "%rootPath%赠送积分.sql"

set rootPath=3.存储过程\后台脚本\网站库\
osql -E -i "%rootPath%删商品类.sql"

set rootPath=3.存储过程\后台脚本\记录库\
osql -E -i "%rootPath%赠送会员.sql"
osql -E -i "%rootPath%赠送经验.sql"
osql -E -i "%rootPath%赠送金币.sql"
osql -E -i "%rootPath%赠送靓号.sql"

set rootPath=3.存储过程\后台脚本\金币库\
osql -E -i "%rootPath%代理分成详情.sql"
osql -E -i "%rootPath%增删道具.sql"
osql -E -i "%rootPath%实卡入库.sql"
osql -E -i "%rootPath%实卡统计.sql"
osql -E -i "%rootPath%数据汇总.sql"
osql -E -i "%rootPath%新增实卡.sql"
osql -E -i "%rootPath%游戏记录.sql"
osql -E -i "%rootPath%统计记录.sql"
osql -E -i "%rootPath%赠送金币.sql"
osql -E -i "%rootPath%转账税收.sql"
osql -E -i "%rootPath%统计代理充值(手工执行).sql"
osql -E -i "%rootPath%统计玩家税收(手工执行).sql"

set rootPath=4.创建作业\
osql -E -i "%rootPath%创建作业.sql"
osql -E -i "%rootPath%代理充值统计.sql"
osql -E -i "%rootPath%税收统计.sql"

pause

COLOR 0A
CLS
@echo off
CLS
echo ------------------------------
echo.
echo. 数据库建立完成
echo.
echo ------------------------------

pause
