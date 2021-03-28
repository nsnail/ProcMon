# ProcMon
进程守护工具

## 软件介绍
监控多个指定进程，如果发现目标进程退出，或者目标进程的日志文件的最后写入时间超过指定值，则重启进程。

  
## 目录结构
* src/ProcMon.Api   读写配置文件的的服务
  * Asp.net Core
* src/ProcMon.Core  进程检测、Kill，Start
  * .NET5
* src/ProcMon.UI    管理界面
  * Vue + Element-UI
