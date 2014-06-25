#   Filemanager-for-UPYUN
##  项目简介
根据UPYUN提供的API,使用C#实现基于.net的简易文件管理工具

##	实现功能
1. 上传:单文件/多文件上传
2. 下载:多文件/文件夹下载,默认下载目录为当前用户目录下的Downloads/UPYUN
3. 新建文件夹:新建空目录,目录已存在时自动重命名(添加数字以区分)
4. 删除:可删除文件/目录(包括非空目录)，可多选删除		
说明:以上操作均可选择工具栏按钮和右键菜单两种方式来完成
5. 搜索:根据输入的检索条件查找需要的文件
6. 前进/后退:记录浏览历史
7. 当前路径:可通过点击路径上的目录名，快速切换到父级目录
8. 切换视图:可在列表视图和缩略图之间来回切换
9. 查看空间使用情况
10. 其他：刷新、点击列表标题排序、上传下载时显示进度(左下角)、文件显示windows默认打开图标、查询时列表右键打开所在目录等

##  未来版本展望
1.  当前UPYUN没有提供重命名和粘贴相关的API,提供以后可以添加到项目中，使功能更加完善
2.  主界面可以在左侧添加树型结构目录，以便更方便得浏览文件
3.  可以添加多种语言的Resource，根据用户选择进行语言匹配，使界面显示国际化
4.  主界面可以添加设置功能，对下载目录等进行自定义
5.  上传下载支持同时运行多个线程，用户可在设置中自定义线程数

##  开发环境
* 操作系统:windows 8.1
* 开发工具:Visual Studio 2013
* 开发语言:C#
* 项目类型:Windows Form Application
* Framework版本:.Net Framework 4

##  测试账号
* 空间名:myres
* 操作员:op1
* 密码:123456789+

## 界面展示
###	登陆界面	
![登陆界面](http://f.hiphotos.baidu.com/album/pic/item/5fdf8db1cb1349544c59849e544e9258d0094a80.jpg?psign=4c59849e544e9258d109b3de9c82d158cdbf6c81810a6eb8)
###	主界面
![主界面](http://d.hiphotos.baidu.com/album/pic/item/0b46f21fbe096b631d7f32aa0e338744eaf8ace2.jpg?psign=1d7f32aa0e338744ebf81a4c510fd9f9d62a6059242d88da)
