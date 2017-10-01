using System;
using System.Collections.Generic;
using System.Linq;
using CarLocationPanelLib.QueryService;
using System.Windows.Forms;
using BaseMethodLib;
using System.ServiceModel;
using CarLocationPanelLib.PushService;
using LOGManagementLib;

namespace CarLocationPanelLib
{
    #region 定义委托、事件和枚举
    /// <summary>
    /// 通信模块委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CallbackCarLocationEventHandler(object sender, CCarLocationEventArgs e);

    /// <summary>
    /// OPC回调事件参数类EventArgs
    /// </summary>
    public class CCarLocationEventArgs : EventArgs
    {
        private object parentForm;
        private string strLocAddr;
        private EnmTxtCarLocationAddr enmSrcLocAddr;
        private object paramDto;
        private int nICType;

        /// <summary>
        /// 回调事件参数-父类对象
        /// </summary>
        public object ParentForm
        {
            get
            {
                return parentForm;
            }
            set
            {
                parentForm = value;
            }
        }

        /// <summary>
        /// 回调事件参数-返回的车位地址
        /// </summary>
        public string StrLocAddr
        {
            get
            {
                return strLocAddr;
            }
            set
            {
                strLocAddr = value;
            }
        }

        /// <summary>
        /// 回调事件参数-返回的车位地址枚举值
        /// </summary>
        public EnmTxtCarLocationAddr EnmSrcLocAddr
        {
            get
            {
                return enmSrcLocAddr;
            }
            set
            {
                enmSrcLocAddr = value;
            }
        }

        /// <summary>
        /// 回调事件参数-参数对象
        /// </summary>
        public object ParamDto
        {
            get
            {
                return paramDto;
            }
            set
            {
                paramDto = value;
            }
        }

        /// <summary>
        /// 回调事件参数-IC卡类型
        /// </summary>
        public int NICType
        {
            get
            {
                return nICType;
            }
            set
            {
                nICType = value;
            }
        }
    }

    /// <summary>
    /// TextBox文本框输入车位地址具体界面
    /// </summary>
    public enum EnmTxtCarLocationAddr
    {
        /// <summary>
        /// 初始: 0
        /// </summary>
        Init = 0,
        /// <summary>
        /// 弹出车位信息对话框
        /// </summary>
        FormCarLocation,
        /// <summary>
        /// 弹出ETV信息对话框
        /// </summary>
        FormETV,
        /// <summary>
        /// 弹出车厅信息对话框
        /// </summary>
        FormHall,
        /// <summary>
        /// 手动指令源地址：1
        /// </summary>
        HandOrderSrc,
        /// <summary>
        /// 手动指令目的地址：2
        /// </summary>
        HandOrderDest,
        /// <summary>
        /// 系统维护手动挪移源地址：3
        /// </summary>
        HandInJogSrc,
        /// <summary>
        /// 系统维护手动挪移目的地址：4
        /// </summary>
        HandInJogDest,
        /// <summary>
        /// 系统维护禁用车位地址：5
        /// </summary>
        Dis,
        /// <summary>
        /// 系统维护入库车位地址：6
        /// </summary>
        HandIn,
        /// <summary>
        /// 系统维护出库车位地址：7
        /// </summary>
        HandOut,
        /// <summary>
        /// 用户管理车主信息分配车位：8
        /// </summary>
        Customer
    }

    /// <summary>
    /// WCF推送服务器回调事件
    /// </summary>
    public class ServiceCallback : PushService.IPushServiceCallback
    {
        public event Action<object> PushCallbackEvent;

        public void Push(object e)
        {
            if (null != PushCallbackEvent)
            {
                PushCallbackEvent(e);
            }
        }
    }

    /// <summary>
    /// 客户端类之间用户修改信息事件通知车位状态布局改变
    /// </summary>
    public class ClientCallback
    {
        public event Action UpdateCarLocCallbackEvent;

        /// <summary>
        /// 触发车位状态布局信息改变
        /// </summary>
        public void UpdateCarLoc()
        {
            if (null != UpdateCarLocCallbackEvent)
            {
                UpdateCarLocCallbackEvent();
            }
        }
    }
    #endregion

    /// <summary>
    /// 静态类-单例模式
    /// </summary>
    public static class CStaticClass
    {
        /// <summary>
        /// 系统默认时间
        /// </summary>
        public static readonly DateTime DefDatetime = new DateTime(1800, 1, 1, 0, 0, 0);
        public static COperatorDto myOperator = new COperatorDto();
        public static List<Panel> myPanelCarLocation = new List<Panel>();
        public static ServiceCallback myCallback = new ServiceCallback();
        public static PushServiceClient myPushProxy = new PushServiceClient(new System.ServiceModel.InstanceContext(myCallback));
        private static bool m_bConnected = true;// PushServiceClient推送服务通道连接标志
        /// <summary>
        /// 客户端类之间通信事件
        /// </summary>
        public static ClientCallback myClient = new ClientCallback();

        #region 读取配置文件之常量
        public static struClientPara myPara = new QueryServiceClient().GetConfig();
        private static bool m_isBill = myPara.bBillingFlag;// 是否有计费功能
        private static bool m_isCarImage = myPara.bCarImageFlag;// 是否需要车牌号和车辆图片  
        private static List<string> m_lstCarSizes = myPara.lstCarSizes;// 本项目车位尺寸列表
        private static List<struCarPSONLayoutInfo> m_lstPanelLayoutInfo = myPara.lstPanelLayoutInfo;// 当前项目的库号和车位排列列表
        private static List<object> m_lstPLCID = myPara.lstPLCID;// 当前项目的库号列表
        private static List<object> m_lstPLCIDDescp = null;// 当前项目的库号说明列表
        private static Dictionary<int, object> m_DictPLCIDDescp = null;// 当前项目的库号说明列表
        private static List<struCustomerInfo> m_lstStruCUSTInfo = null;// 用户车主列表

        private static string m_strMainTitle = myPara.strClientTitle;//"中集天达智能车库管理系统";
        private static string m_strBillTitle = myPara.strBillingTitle;//"中集天达智能车库收费管理系统";
        private static string m_connection = myPara.strDBConnectionInfo;//@"Data Source=STAS-L0471;Initial Catalog=6113;User ID=sa;password =";//\SQLEXPRESS
        private static string m_strSqlCarLocation = myPara.strSQLQueryNOTECarPOSN;//"SELECT warehouse,carlocaddr,carloctype,carlocstatus,iccode From dbo.carlocation";
        private static string m_strSqlDeviceFault = myPara.strSQLQueryNOTEDeviceFault;//"SELECT faultdescp,warehouse,devicecode,faultaddress,color,isable From dbo.devicefault";
        private static string m_strSqlDeviceStatus = myPara.strSQLQueryNOTEDeviceStatus;//"SELECT devicecode,warehouse,deviceaddr,devicemode,devicetype,isavailable From dbo.devicestatus";

        private static string m_strOptPermission = myPara.strAllOptPermission;//"系统维护,系统配置,用户管理,缴费管理,操作员管理,查询统计,临时取物,手动指令";

        // 界面参数 
        private static int m_minWidth = 1000;// 主界面最小宽度
        private static int m_minHeight = 500;// 主界面最小高度
        private static int m_mainGap = 12;// 主界面间隔
        private static int m_controlSize = 30;
        private static int m_minGap = 5;
        private static int m_faultLabelWidth = 190;
        private static int m_faultLabelHeight = 20;
        private static string m_strOf = "of ";
        private static string m_strTo = " to ";
        private static string m_strPages = " Pages";
        #endregion

        #region 常用函数
        /// <summary>
        /// 获取当前日期和时间
        /// </summary>
        /// <returns></returns>
        public static DateTime CurruntDateTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 显示当前车库车位状态对话框
        /// </summary>
        /// <param name="strWareHouse"></param>
        /// <param name="enmType"></param>
        public static void showFormCarLocationStatus(object sender, Form form, string strWareHouse, EnmTxtCarLocationAddr enmType)
        {
            if (null == myPanelCarLocation)
            {
                return;
            }

            // 获取对应车库车位状态布局p.Name
            CWareHousePanel panel = (CWareHousePanel)myPanelCarLocation.Find(p => p.Text == strWareHouse);// myPanelCarLocation[nIndex];
            if (null == panel)
            {
                return;
            }           

            form.Controls.Clear();
            panel.Tag = sender;
            panel.EnmSrcLocAddr = enmType;

            form.Text = panel.Text;
            panel.Size = form.ClientSize;
            panel.UpdateCarLocationStatus();
            panel.UpdateDeviceStatus();            
            form.Controls.Add(panel);           
            form.ShowDialog();
        }

        /// <summary>
        /// 设置用户车主列表信息
        /// </summary>
        /// <param name="lstStruCUSTInfo"></param>
        public static void SetLstStruCUSTInfo(List<struCustomerInfo> lstStruCUSTInfo)
        {
            m_lstStruCUSTInfo = lstStruCUSTInfo;
        }
        
        /// <summary>
        /// 增加、修改、删除用户车主列表信息(nType:0-增加 1-修改 2-删除)
        /// </summary>
        /// <param name="lstStruCUSTInfo"></param>
        public static void SaveStruCUSTInfo(struCustomerInfo customerInfo, int nType)
        {
            if (null == m_lstStruCUSTInfo)
            {
                return;
            }

            if (2 == nType)
            {
                m_lstStruCUSTInfo.RemoveAll(s => s.strICCardID == customerInfo.strICCardID && s.strName == customerInfo.strName);
            }
            else
            {
                int index = m_lstStruCUSTInfo.FindIndex(s => s.strICCardID == customerInfo.strICCardID);
                if (-1 == index)
                {
                    m_lstStruCUSTInfo.Add(customerInfo);
                }
                else
                {
                    m_lstStruCUSTInfo.RemoveAt(index);
                    m_lstStruCUSTInfo.Insert(index, customerInfo);
                }
            }
        }

        /// <summary>
        /// 获取车主姓名根据IC卡号
        /// </summary>
        /// <param name="strICCardID"></param>
        /// <returns></returns>
        public static string GetCUSTNameByICCardID(string strICCardID)
        {
            switch(myPara.ShowInfoFlag)
            {
                case 2:
                    {
                        if (null == m_lstStruCUSTInfo)// || string.IsNullOrWhiteSpace(strICCardID))
                        {
                            return strICCardID;
                        }

                        struCustomerInfo customer = m_lstStruCUSTInfo.Find(s => s.strICCardID == strICCardID);

                        if (customer.strICCardID != strICCardID)
                        {
                            return strICCardID;
                        }

                        return customer.strName;
                    }
                default:
                    {
                        break;
                    }
            }

            return strICCardID;
        }

        /// <summary>
        /// 获取抛出异常友好文本
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string GetExceptionInfo(Exception exception)
        {
            string strResult = "系统异常，请联系管理员！";
            if(null == exception)
            {
                CLOGException.Trace("GetExceptionInfo:请查看服务端日志");
                return strResult;
            }

            if (typeof(FaultException) == exception.GetType())
            {
                strResult = "请检查服务端是否打开，并打开服务！";
            }
            else if (typeof(Exception) == exception.GetType())
            {
                strResult = "请检查联系维护人员！";
            }
            else if (exception.ToString().Contains("ThrowMaxReceivedMessageSizeExceeded"))
            {
                strResult = "查询范围过大，请缩短查询范围！";
            }
            CLOGException.Trace("GetExceptionInfo:" + exception.ToString());
            return strResult;
        }

        /// <summary>
        /// 设置单独计费系统时计费标志
        /// </summary>
        /// <param name="bFlag"></param>
        public static void SetBillingFlag(bool bFlag)
        {
            m_isBill = bFlag;
        }
        #endregion

        #region 转换枚举类
        /// <summary>
        /// 转换操作员类型枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static string ConvertOperatorType(int? enmtype)
        {
            string str = string.Empty;

            if (null == enmtype)
            {
                return str;
            }

            switch ((EnmOperatorType)enmtype)
            {
                case EnmOperatorType.Operator:
                    {
                        str = "操作员";
                        break;
                    }
                case EnmOperatorType.Manager:
                    {
                        str = "管理员";
                        break;
                    }
                case EnmOperatorType.CIMCWorker:
                    {
                        str = "天达维护人员";
                        break;
                    }
                case EnmOperatorType.TollKeeper:
                    {
                        str = "计费人员";
                        break;
                    }
                case EnmOperatorType.Other:
                    {
                        str = "其他";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return str;
        }

        /// <summary>
        /// 转换操作员类型枚举类enum
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static EnmOperatorType ConvertOperatorType(string str)
        {
            EnmOperatorType type = 0;

            if (string.IsNullOrEmpty(str))
            {
                return type;
            }

            switch (str)
            {
                case "操作员":
                    {
                        type = EnmOperatorType.Operator;
                        break;
                    }
                case "管理员":
                    {
                        type = EnmOperatorType.Manager;
                        break;
                    }
                case "天达维护人员":
                    {
                        type = EnmOperatorType.CIMCWorker;
                        break;
                    }
                case "计费人员":
                    {
                        type = EnmOperatorType.TollKeeper;
                        break;
                    }
                case "其他":
                    {
                        type = EnmOperatorType.Other;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return type;
        }

        /// <summary>
        /// 转换车位状态枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static string ConvertCarLocStatus(int? enmtype)
        {
            string str = string.Empty;

            if (null == enmtype)
            {
                return str;
            }

            switch ((PushService.EnmLocationStatus)enmtype)
            {
                case PushService.EnmLocationStatus.Space:
                    {
                        str = "空闲";
                        break;
                    }
                case PushService.EnmLocationStatus.Occupy:
                    {
                        str = "占用";
                        break;
                    }
                case PushService.EnmLocationStatus.Entering:
                    {
                        str = "入库";
                        break;
                    }
                case PushService.EnmLocationStatus.Outing:
                    {
                        str = "出库";
                        break;
                    }
                case PushService.EnmLocationStatus.MovingVEH:
                    {
                        str = "库内挪移";
                        break;
                    }
                case PushService.EnmLocationStatus.TmpFetch:
                    {
                        str = "临时取物";
                        break;
                    }
                case PushService.EnmLocationStatus.VehRotation:
                    {
                        str = "车辆旋转";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return str;
        }
       
        /// <summary>
        /// 转换车位状态枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static PushService.EnmLocationStatus ConvertCarLocStatus(string str)
        {
            PushService.EnmLocationStatus type = PushService.EnmLocationStatus.Space;

            if (string.IsNullOrEmpty(str))
            {
                return type;
            }

            switch (str)
            {
                case "空闲":
                    {
                        type = PushService.EnmLocationStatus.Space;
                        break;
                    }
                case "占用":
                    {
                        type = PushService.EnmLocationStatus.Occupy;
                        break;
                    }
                case "入库":
                    {
                        type = PushService.EnmLocationStatus.Entering;
                        break;
                    }
                case "出库":
                    {
                        type = PushService.EnmLocationStatus.Outing;
                        break;
                    }
                case "库内挪移":
                    {
                        type = PushService.EnmLocationStatus.MovingVEH;
                        break;
                    }
                case "临时取物":
                    {
                        type = PushService.EnmLocationStatus.TmpFetch;
                        break;
                    }
                case "车辆旋转":
                    {
                        type = PushService.EnmLocationStatus.VehRotation;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return type;
        }

        /// <summary>
        /// 转换IC卡类型枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static string ConvertICCardType(int? enmtype)
        {
            string str = null;// string.Empty;

            if (null == enmtype)
            {
                return str;
            }

            switch ((EnmICCardType)enmtype)
            {
                case EnmICCardType.Temp:
                    {
                        str = "临时卡";
                        break;
                    }
                case EnmICCardType.Fixed:
                    {
                        str = "定期卡";
                        break;
                    }
                case EnmICCardType.FixedLocation:
                    {
                        str = "固定车位卡";
                        break;
                    }
                default:
                    {
                        str = enmtype.ToString();
                        break;
                    }
            }

            return str;
        }

        /// <summary>
        /// 转换IC卡类型枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static EnmICCardType ConvertICCardType(string str)
        {
            EnmICCardType type = 0;

            switch (str)
            {
                case "临时卡":
                    {
                        type = EnmICCardType.Temp;
                        break;
                    }
                case "定期卡":
                    {
                        type = EnmICCardType.Fixed;
                        break;
                    }
                case "固定车位卡":
                    {
                        type = EnmICCardType.FixedLocation;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return type;
        }

        /// <summary>
        /// 转换IC卡状态枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static string ConvertICCardStatus(int? enmtype)
        {
            string str = null;// string.Empty;

            if (null == enmtype)
            {
                return str;
            }

            switch ((EnmICCardStatus)enmtype)
            {
                case EnmICCardStatus.Disposed:
                    {
                        str = "注销";
                        break;
                    }
                case EnmICCardStatus.Lost:
                    {
                        str = "挂失";
                        break;
                    }
                case EnmICCardStatus.Normal:
                    {
                        str = "正常";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return str;
        }

        /// <summary>
        /// 转换IC卡状态枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static EnmICCardStatus ConvertICCardStatus(string str)
        {
            EnmICCardStatus type = 0;

            switch (str)
            {
                case "注销":
                    {
                        type = EnmICCardStatus.Disposed;
                        break;
                    }
                case "挂失":
                    {
                        type = EnmICCardStatus.Lost;
                        break;
                    }
                case "正常":
                    {
                        type = EnmICCardStatus.Normal;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return type;
        }

        /// <summary>
        /// 转换设备模式枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static string ConvertDeviceMode(int? enmtype)
        {
            string str = "初始";

            if (null == enmtype)
            {
                return str;
            }

            switch ((EnmModel)enmtype)
            {
                case EnmModel.Maintenance:
                    {
                        str = "维修模式";
                        break;
                    }
                case EnmModel.Manual:
                    {
                        str = "手动模式";
                        break;
                    }
                case EnmModel.StandAlone:
                    {
                        str = "单机自动";
                        break;
                    }
                case EnmModel.Automatic:
                    {
                        str = "全自动";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return str;
        }

        /// <summary>
        /// 转换设备类型枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static string ConvertDeviceType(int? enmtype)
        {
            string str = string.Empty;

            if (null == enmtype)
            {
                return str;
            }

            switch ((EnmSMGType)enmtype)
            {
                case EnmSMGType.Hall:
                    {
                        str = "车厅";
                        break;
                    }
                case EnmSMGType.ETV:
                    {
                        str = "ETV";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return str;
        }

        /// <summary>
        /// 转换车厅类型枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static string ConvertHallType(int? enmtype)
        {
            string str = string.Empty;

            if (null == enmtype)
            {
                return str;
            }

            switch ((EnmHallType)enmtype)
            {
                case EnmHallType.Entance:
                    {
                        str = "进车厅";
                        break;
                    }
                case EnmHallType.Exit:
                    {
                        str = "出车厅";
                        break;
                    }
                case EnmHallType.EnterOrExit:
                    {
                        str = "进出两用车厅";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return str;
        }

        /// <summary>
        /// 转换车厅类型枚举类enum
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static EnmHallType ConvertHallType(string str)
        {
            EnmHallType type = 0;

            if (string.IsNullOrEmpty(str))
            {
                return type;
            }

            switch (str)
            {
                case "进车厅":
                    {
                        type = EnmHallType.Entance;
                        break;
                    }
                case "出车厅":
                    {
                        type = EnmHallType.Exit;
                        break;
                    }
                case "进出两用车厅":
                    {
                        type = EnmHallType.EnterOrExit;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return type;
        }

        /// <summary>
        /// 转换当前作业状态类型枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static string ConvertTaskType(int? enmtype)
        {
            string str = "待命";

            if (null == enmtype)
            {
                return str;
            }

            switch ((EnmTaskType)enmtype)
            {
                case EnmTaskType.Init:
                    {
                        str = "待命";
                        break;
                    }
                case EnmTaskType.EntryTask:
                    {
                        str = "存车";
                        break;
                    }
                case EnmTaskType.ExitTask:
                    {
                        str = "取车";
                        break;
                    }
                case EnmTaskType.TmpFetch:
                    {
                        str = "取物";
                        break;
                    }
                case EnmTaskType.MoveCarTask:
                    {
                        str = "挪移";
                        break;
                    }
                case EnmTaskType.MoveEquipTask:
                    {
                        str = "移动";
                        break;
                    }
                case EnmTaskType.AvoidMove:
                    {
                        str = "避让";
                        break;
                    }     
                default:
                    {
                        break;
                    }
            }

            return str;
        }

        /// <summary>
        /// 转换计费类型枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static string ConvertFeeType(int? enmtype)
        {
            string str = null;// string.Empty;

            if (null == enmtype)
            {
                return str;
            }

            switch ((CarLocationPanelLib.QueryService.EnmFeeType)enmtype)
            {
                case CarLocationPanelLib.QueryService.EnmFeeType.Hour:
                    {
                        str = "小时卡";
                        break;
                    }
                case CarLocationPanelLib.QueryService.EnmFeeType.Month:
                    {
                        str = "月卡";
                        break;
                    }
                case CarLocationPanelLib.QueryService.EnmFeeType.Season:
                    {
                        str = "季卡";
                        break;
                    }
                case CarLocationPanelLib.QueryService.EnmFeeType.Year:
                    {
                        str = "年卡";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return str;
        }

        /// <summary>
        /// 转换计费类型枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static int ConvertFeeType(string str)
        {
            int type = 0;

            switch (str)
            {
                case "小时卡":
                    {
                        type = (int)EnmFeeType.Hour;
                        break;
                    }
                case "月卡":
                    {
                        type = (int)EnmFeeType.Month;
                        break;
                    }
                case "季卡":
                    {
                        type = (int)EnmFeeType.Season;
                        break;
                    }
                case "年卡":
                    {
                        type = (int)EnmFeeType.Year;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return type;
        }

        ///// <summary>
        ///// 转换计费类型枚举类enum
        ///// </summary>
        ///// <param name="enmtype"></param>
        ///// <returns></returns>
        //public static CarLocationPanelLib.PushService.EnmFeeType ConvertFeeType(string str)
        //{
        //    CarLocationPanelLib.PushService.EnmFeeType type = 0;

        //    switch (str)
        //    {
        //        case "小时卡":
        //            {
        //                type = CarLocationPanelLib.PushService.EnmFeeType.Hour;
        //                break;
        //            }
        //        case "月卡":
        //            {
        //                type = CarLocationPanelLib.PushService.EnmFeeType.Month;
        //                break;
        //            }
        //        case "季卡":
        //            {
        //                type = CarLocationPanelLib.PushService.EnmFeeType.Season;
        //                break;
        //            }
        //        case "年卡":
        //            {
        //                type = CarLocationPanelLib.PushService.EnmFeeType.Year;
        //                break;
        //            }
        //        default:
        //            {
        //                break;
        //            }
        //    }

        //    return type;
        //}

        /// <summary>
        /// 转换bool枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static string ConvertBoolType(int? enmtype)
        {
            string str = string.Empty;

            if (null == enmtype)
            {
                return str;
            }

            switch (enmtype)
            {
                case 0:
                    {
                        str = "否";
                        break;
                    }
                case 1:
                    {
                        str = "是";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return str;
        }

        /// <summary>
        /// 转换operator表格属性permission值对应权限列表（object[]）
        /// 有低位到高位次序如下：
        /// 系统维护,系统配置,用户管理,缴费管理,操作员管理,查询统计,临时取物,手动指令
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static object[] ConvertOptPermission(EnmOperatorType opttype)
        {
            int permission = 0;

            switch (opttype)
            {
                case EnmOperatorType.CIMCWorker:// 天达维护人员: 1
                    {
                        // 16383转换成二进制：11 1111 1111 1111
                        permission = myPara.CIMCWorkerTopPRMSN;
                        break;
                    }
                case EnmOperatorType.Manager:// 管理者: 2
                    {
                        // 15871转换成二进制：11 1101 1111 1111
                        permission = myPara.ManagerTopPRMSN;
                        break;
                    }
                case EnmOperatorType.Operator:// 操作员： 3
                    {
                        // 9699转换成二进制：10 0101 1110 0011
                        permission = myPara.OperatorTopPRMSN;
                        break;
                    }
                case EnmOperatorType.TollKeeper:// 计费人员: 4
                    {
                        // 9707转换成二进制：10 0101 1110 1011
                        permission = myPara.TollKeeperTopPRMSN;
                        break;
                    }
                case EnmOperatorType.Other:// 其他：5
                    {
                        // 9699转换成二进制：10 0101 1110 0011
                        permission = myPara.OtherTopPRMSN;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return ConvertOptPermission(permission);
        }

        /// <summary>
        /// 转换operator表格属性permission值对应权限列表（object[]）
        /// 有低位到高位次序如下：
        /// 系统维护,系统配置,用户管理,缴费管理,操作员管理,查询统计,临时取物,手动指令
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static object[] ConvertOptPermission(int? permission)
        {
            List<object> lstObject = new List<object>();
            if (string.IsNullOrEmpty(m_strOptPermission))
            {
                return lstObject.ToArray();
            }

            List<string> lstStr = m_strOptPermission.Split(',').ToList();

            for (int i = 0; i < lstStr.Count; i++)
            {
                int nValue = (int)Math.Pow(2, i);

                if (nValue == (permission & nValue))
                {
                    lstObject.Add(lstStr[i]);
                }
            }

            return lstObject.ToArray();
        }

        /// <summary>
        /// 转换界面权限列表为operator表格属性permission值
        /// 有低位到高位次序如下：
        /// 系统维护,系统配置,用户管理,缴费管理,操作员管理,查询统计,临时取物,手动指令
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static int ConvertOptPermission(object[] lstObject)
        {
            int permission = 0;
            if (string.IsNullOrEmpty(m_strOptPermission))
            {
                return permission;
            }
            List<string> lstStr = m_strOptPermission.Split(',').ToList();

            for (int i = 0; i < lstStr.Count; i++)
            {
                if (lstObject.Contains(lstStr[i]))
                {
                    permission += (int)Math.Pow(2, i);
                }
            }

            return permission;
        }

        /// <summary>
        /// 转换刷卡队列作业类型枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static string ConvertSwipeCountType(int? enmtype)
        {
            string str = string.Empty;

            if (null == enmtype)
            {
                return str;
            }

            switch ((EnmSwipeCount)enmtype)
            {
                case EnmSwipeCount.GetCar:
                    {
                        str = "取车";
                        break;
                    }
                case EnmSwipeCount.Oversize:
                    {
                        str = "车体超限";
                        break;
                    }
                case EnmSwipeCount.SaveCarFirst:
                    {
                        str = "刷第一次存车卡";
                        break;
                    }
                case EnmSwipeCount.SaveCarSecond:
                    {
                        str = "刷第二次存车卡";
                        break;
                    }
                case EnmSwipeCount.TmpFetch:
                    {
                        str = "临时取物";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return str;
        }

        /// <summary>
        /// 转换刷卡队列作业类型枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        /// <returns></returns>
        public static EnmSwipeCount ConvertSwipeCountType(string str)
        {
            EnmSwipeCount type = 0;

            switch (str)
            {
                case "取车":
                    {
                        type = EnmSwipeCount.GetCar;
                        break;
                    }
                case "车体超限":
                    {
                        type = EnmSwipeCount.Oversize;
                        break;
                    }
                case "刷第一次存车卡":
                    {
                        type = EnmSwipeCount.SaveCarFirst;
                        break;
                    }
                case "刷第二次存车卡":
                    {
                        type = EnmSwipeCount.SaveCarSecond;
                        break;
                    }
                case "临时取物":
                    {
                        type = EnmSwipeCount.TmpFetch;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return type;
        }

        /// <summary>
        /// 转换EnmFlowNodeDescp车厅流程图节点描述枚举类enum
        /// </summary>
        /// <param name="enmtype"></param>
        public static string ConvertFlowNodeDescpType(int? enmtype)
        {
            string str = string.Empty;

            if (null == enmtype)
            {
                return str;
            }

            switch ((EnmFlowNodeDescp)enmtype)
            {
                case EnmFlowNodeDescp.End:
                    {
                        str = "结束";
                        break;
                    }
                case EnmFlowNodeDescp.Init:
                    {
                        str = "待命";
                        break;
                    }
                case EnmFlowNodeDescp.EnterHasCar:
                    {
                        str = "等待刷第一次卡";// "有车入库";
                        break;
                    }
                case EnmFlowNodeDescp.EnterSwipeOne:
                    {
                        str = "等待刷第二次卡";// "第一次刷卡";
                        break;
                    }
                case EnmFlowNodeDescp.EnterSwipeTwo:
                    {
                        str = "等待车厅对中"; // "第二次刷卡";
                        break;
                    }
                case EnmFlowNodeDescp.EnterCarCheck:
                    {
                        str = "等待车厅确认有车入库"; // "车厅对中完毕";
                        break;
                    }
                case EnmFlowNodeDescp.EnterCommandAccept:
                    {
                        str = "等待ETV装载"; // "车厅确认有车入库";
                        break;
                    }
                case EnmFlowNodeDescp.DealLoad:
                    {
                        str = "等待ETV卸载"; // "ETV装载完成";
                        break;
                    }
                case EnmFlowNodeDescp.DealUnload:
                    {
                        str = "ETV卸载完成";
                        break;
                    }
                case EnmFlowNodeDescp.EnterCarOverrun:
                    {
                        str = "车体超限";
                        break;
                    }
                case EnmFlowNodeDescp.EnterCarLeave:
                    {
                        str = "车辆离开车厅";
                        break;
                    }
                case EnmFlowNodeDescp.ExitSwipe:
                    {
                        str = "等待车厅确认出车"; // "刷取车卡";
                        break;
                    }
                case EnmFlowNodeDescp.ExitCommandAccept:
                    {
                        str = "等待车厅允许取车"; // "车厅确认出车";
                        break;
                    }
                case EnmFlowNodeDescp.ExitHasCar:
                    {
                        str = "等待车辆离开车厅"; // "允许取车";
                        break;
                    }
                case EnmFlowNodeDescp.ExitCarLeave:
                    {
                        str = "车辆离开车厅";
                        break;
                    }
                case EnmFlowNodeDescp.MoveCarOk:
                    {
                        str = "确定挪移";
                        break;
                    }
                case EnmFlowNodeDescp.MoveEquipOk:
                    {
                        str = "确定移动";
                        break;
                    }
                case EnmFlowNodeDescp.MoveEquipLoad:
                    {
                        str = "ETV移动完成";
                        break;
                    }
                case EnmFlowNodeDescp.TmpFetchOk:
                    {
                        str = "等待车厅确认临时取物"; // "临时取物确认";
                        break;
                    }
                case EnmFlowNodeDescp.TmpFetchCommandAccept:
                    {
                        str = "等待车厅允许临时取物"; // "车厅确认临时取物";
                        break;
                    }
                case EnmFlowNodeDescp.TmpFetchHasCar:
                    {
                        str = "等待车辆离开车厅"; // "允许临时取物";
                        break;
                    }
                case EnmFlowNodeDescp.TmpFetchCarLeave:
                    {
                        str = "临时取物车辆离开车厅";
                        break;
                    }
                case EnmFlowNodeDescp.Load:
                    {
                        str = "装载中";
                        break;
                    }
                case EnmFlowNodeDescp.LoadFinish:
                    {
                        str = "装载完成";
                        break;
                    }
                case EnmFlowNodeDescp.WillUnLoad:
                    {
                        str = "等待卸载";
                        break;
                    }
                case EnmFlowNodeDescp.UnLoad:
                    {
                        str = "卸载中";
                        break;
                    }
                case EnmFlowNodeDescp.UnLoadFinish:
                    {
                        str = "卸载完成";
                        break;
                    }
                case EnmFlowNodeDescp.Move:
                    {
                        str = "移动中";
                        break;
                    }
                case EnmFlowNodeDescp.TMURO: 
                    {
                        str = "故障中";
                        break;
                    }
                case EnmFlowNodeDescp.TMURORecoryNoCar:
                    {
                        str = "故障恢复装载中";
                        break;
                    }
                case EnmFlowNodeDescp.TMURORecoryHasCar:
                    {
                        str = "故障恢复卸载中";
                        break;
                    }
                default:
                    {
                        str = ((EnmFlowNodeDescp)enmtype).ToString();
                        break;
                    }
            }

            return str;
        }

        /// <summary>
        /// 转换库号为对应文本说明
        /// </summary>
        /// <param name="nWareHouse"></param>
        /// <returns></returns>
        public static string ConvertWareHouse(int? nWareHouse)
        {
            string str = string.Empty;

            if (null == m_DictPLCIDDescp)
            {
                m_lstPLCIDDescp = new List<object>();
                m_DictPLCIDDescp = new Dictionary<int, object>();
                foreach (struCarPSONLayoutInfo obj in m_lstPanelLayoutInfo)
                {
                    string[] strs = obj.strPanelName.Split('_');
                    string strText = string.Empty;
                    if (1 < strs.Count())
                    {
                        strText = strs[1];
                    }

                    m_lstPLCIDDescp.Add(strText);
                    m_DictPLCIDDescp.Add(obj.rectInfo.X, strText);
                }
            }

            if (null == m_DictPLCIDDescp || CBaseMethods.MyBase.IsEmpty(nWareHouse))
            {
                return str;
            }

            if (m_DictPLCIDDescp.ContainsKey((int)nWareHouse))
            {
                str = m_DictPLCIDDescp[(int)nWareHouse].ToString();
            }

            return str;
        }

        /// <summary>
        /// 转换库号文本说明为库号
        /// </summary>
        /// <param name="nWareHouse"></param>
        /// <returns></returns>
        public static int ConvertWareHouse(string strWareHouse)
        {
            int nWareHouse = 0;

            if (null == m_DictPLCIDDescp || string.IsNullOrEmpty(strWareHouse))
            {
                return nWareHouse;
            }

            foreach (int nKey in m_DictPLCIDDescp.Keys)
            {
                if (strWareHouse == m_DictPLCIDDescp[nKey].ToString())
                {
                    nWareHouse = nKey;
                    break;
                }
            }

            return nWareHouse;
        }

        /// <summary>
        /// 转换车厅设备号为对应文本说明
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertHallDescp(object obj)
        {
            string str = string.Empty;

            //modify by suhan
			if (null == obj || 0 == (int)obj)
            {
                return str;
            }

            str = ((int)obj - 10) + "号车厅";
            return str;
        }

        /// <summary>
        /// 转换车厅设备号文本说明为设备号
        /// </summary>
        /// <param name="nWareHouse"></param>
        /// <returns></returns>
        public static int ConvertHallDescp(string strHallID)
        {
            int nHallID = 0;

            if (string.IsNullOrEmpty(strHallID))
            {
                return nHallID;
            }

            strHallID = strHallID.Replace("号车厅", "").Trim();
            CBaseMethods.MyBase.StringToUInt32(strHallID, out nHallID);
            return nHallID + 10;
        }

        /// <summary>
        /// 转换车厅设备号为对应文本说明
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertHallDescp(int nWareHouse, object obj)
        {
            string str = string.Empty;

            //modify by suhan
            if (null == obj || 0 == (int)obj)
            {
                return str;
            }
            if (!myPara.DicHallIDDictionary.ContainsKey(nWareHouse) || !myPara.DicHallIDDictionary[nWareHouse].ContainsKey((int)obj))
            {
                return str;
            }

            if (string.IsNullOrEmpty(myPara.DicHallIDDictionary[nWareHouse][(int)obj].strHallName))
            {
                str = ((int)obj - 10) + "号车厅";
            }
            else
            { 
                str = myPara.DicHallIDDictionary[nWareHouse][(int)obj].strHallName;
            }
            return str;
        }

        /// <summary>
        /// 转换车厅设备号文本说明为设备号
        /// </summary>
        /// <param name="nWareHouse"></param>
        /// <returns></returns>
        public static int ConvertHallDescp(int nWareHouse, string strHallID)
        {
            int nHallID = 0;
            if (string.IsNullOrEmpty(strHallID))
            {
                return nHallID;
            }
            if (!myPara.DicHallIDDictionary.ContainsKey(nWareHouse))
            {
                return nHallID;
            }

            foreach (struHallEquips struHall in myPara.DicHallIDDictionary[nWareHouse].Values)
            {
                if (strHallID.Equals(struHall.strHallName))
                {
                    nHallID = struHall.nHallID;
                    break;
                }
            }

            if (0 == nHallID)
            {
                strHallID = strHallID.Replace("号车厅", "").Trim();
                CBaseMethods.MyBase.StringToUInt32(strHallID, out nHallID);
                nHallID = nHallID + 10;
            }
            return nHallID;
        }

        /// <summary>
        /// 转换ETV设备号为对应文本说明
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertETVDescp(object obj)
        {
            string str = string.Empty;

            if (null == obj)
            {
                return str;
            }

            str =" "+ (int)obj + "号 TV";
            return str;
        }

        /// <summary>
        /// 转换ETV设备号文本说明为设备号
        /// </summary>
        /// <param name="nWareHouse"></param>
        /// <returns></returns>
        public static int ConvertETVDescp(string strHallID)
        {
            int nHallID = 0;

            if (string.IsNullOrEmpty(strHallID))
            {
                return nHallID;
            }

            strHallID = strHallID.Replace("号TV", "").Trim();
            CBaseMethods.MyBase.StringToUInt32(strHallID, out nHallID);
            return nHallID;
        }

        /// <summary>
        /// 转换计费标准中费用的文字说明 
        /// </summary>
        /// <param name="nFee"></param>
        /// <param name="nFeeType"></param>
        /// <returns></returns>
        public static string ConvertPayableFee(float? nFee, int? nFeeType)
        {
            string strTariffFee = ((float)nFee).ToString("￥0.00") + "元";
            return strTariffFee;
        }

        /// <summary>
        /// 转换计费标准中费用的文字说明 
        /// </summary>
        /// <param name="nFee"></param>
        /// <param name="nFeeType"></param>
        /// <returns></returns>
        public static string ConvertTariffFee(float? nFee, int? nFeeType)
        {
            string strUnit = ConvertFeeType(nFeeType);
            strUnit = "/" + strUnit.Replace("小时", "天").Substring(0, 1);

            string strTariffFee = ((float)nFee).ToString("￥0.00") + "元" + strUnit;
            return strTariffFee;
        }

        /// <summary>
        /// 转换计费标准中费用的文字说明 
        /// </summary>
        /// <param name="nFee"></param>
        /// <param name="nFeeType"></param>
        /// <returns></returns>
        public static float ConvertTariffFee(string strTariffFee)
        {
            float fPayableFee;
            string strPayableFee = strTariffFee.Replace('￥', ' ').Trim();

            int nIndex = strPayableFee.IndexOf('元');
            if (-1 != nIndex)
            {
                strPayableFee = strPayableFee.Substring(0, nIndex);
            }

            float.TryParse(strPayableFee.Trim(), out fPayableFee);
            return fPayableFee;
        }

        /// <summary>
        /// 去除字符串前面所有‘0’
        /// </summary>
        /// <param name="strICCardID"></param>
        /// <returns></returns>
        public static string ConvertICCardID(string strICCardID)
        {
            return strICCardID;

            //if (string.IsNullOrEmpty(strICCardID))
            //{
            //    return strICCardID;
            //}

            //int nStartIndex = 0;

            //for (int i = 0; i < strICCardID.Length; i++)
            //{
            //    if ('0' != strICCardID[i])
            //    {
            //        break;
            //    }

            //    nStartIndex = i;
            //}

            //return strICCardID.Substring(nStartIndex);
        }

        /// <summary>
        /// 转换优先级为对应文本说明
        /// </summary>
        /// <param name="nPriorityID"></param>
        /// <returns></returns>
        public static string ConvertPriorityID(int? nPriorityID)
        {
            string str = string.Empty;

            if (null == nPriorityID)
            {
                return str;
            }

            return nPriorityID.ToString();
        }

        /// <summary>
        /// 转换优先级文本说明
        /// </summary>
        /// <param name="nWareHouse"></param>
        /// <returns></returns>
        public static int ConvertPriorityID(string strPriorityID)
        {
            int nPriorityID = 0;

            if (string.IsNullOrEmpty(strPriorityID))
            {
                return nPriorityID;
            }

            CBaseMethods.MyBase.StringToUInt32(strPriorityID, out nPriorityID);
            return nPriorityID;
        }
        #endregion

        #region 读取配置文件之函数
        /// <summary>
        /// 获取当前项目的库号和车位排列列表
        /// 在各个PLC下配置：(1, 102, 4, 12), "CVerticalPanel")当前项目的库号和车位排列列表
        /// </summary>
        /// <returns></returns>
        public static List<struCarPSONLayoutInfo> ConfigLstRectProject()
        {
            if (null == m_lstPanelLayoutInfo)
            {
                return new List<struCarPSONLayoutInfo>();
            }
            return m_lstPanelLayoutInfo;
        }

        /// <summary>
        /// 获取当前项目的库号说明列表
        /// </summary>
        /// <returns></returns>
        public static List<object> ConfigLstWareHouseDescp()
        {
            if (null == m_lstPLCIDDescp)
            {
                m_lstPLCIDDescp = new List<object>();
                m_DictPLCIDDescp = new Dictionary<int, object>();
                foreach (struCarPSONLayoutInfo obj in m_lstPanelLayoutInfo)
                {
                    if (null == obj.strPanelName)
                    {
                        continue;
                    }
                    string[] strs = obj.strPanelName.Split('_');
                    string strText = string.Empty;
                    if (1 < strs.Count())
                    {
                        strText = strs[1];
                    }

                    m_lstPLCIDDescp.Add(strText);
                    m_DictPLCIDDescp.Add(obj.rectInfo.X, strText);
                }
            }
            return m_lstPLCIDDescp;// m_lstPLCID;
        }

        /// <summary>
        /// 获取当前项目的库号列表
        /// </summary>
        /// <returns></returns>
        public static List<object> ConfigLstWareHouse()
        {
            if (null == m_lstPLCID)
            {
                return new List<object>();
            }
            return m_lstPLCID;
        }

        /// <summary>
        /// 获取计费标志
        /// </summary>
        /// <returns></returns>
        public static bool ConfigBillingFlag()
        {
            return m_isBill;
        }

        /// <summary>
        /// 是否需要车牌号和车辆图片
        /// </summary>
        /// <returns></returns>
        public static bool ConfigCarImageFlag()
        {
            return m_isCarImage;
        }

        /// <summary>
        /// 获取本项目客户端系统标题
        /// </summary>
        /// <returns></returns>
        public static string ConfigMainTitle()
        {
            return m_strMainTitle;
        }

        /// <summary>
        /// 获取本项目单独计费系统标题
        /// </summary>
        /// <returns></returns>
        public static string ConfigBillTitle()
        {
            return m_strBillTitle;
        }

        /// <summary>
        /// 获取SQL数据库连接信息
        /// </summary>
        /// <returns></returns>
        public static string ConfigDBConnectionInfo()
        {
            return m_connection;
        }

        /// <summary>
        /// 获取数据库车位表格查询通知事件sql语句
        /// </summary>
        /// <returns></returns>
        public static string ConfigSQLCarLocation()
        {
            return m_strSqlCarLocation;
        }

        /// <summary>
        /// 获取数据库设备故障表格查询通知事件sql语句
        /// </summary>
        /// <returns></returns>
        public static string ConfigSQLDeviceFault()
        {
            return m_strSqlDeviceFault;
        }

        /// <summary>
        /// 获取数据库设备状态表格查询通知事件sql语句
        /// </summary>
        /// <returns></returns>
        public static string ConfigSQLDeviceStatus()
        {
            return m_strSqlDeviceStatus;
        }

        /// <summary>
        /// 输入的预留尺寸是否是本项目的车位尺寸
        /// </summary>
        /// <param name="strCarReservedSize"></param>
        /// <returns></returns>
        public static bool ConfigIsProjectSize(string strCarReservedSize)
        {
            return m_lstCarSizes.Exists(s => s.StartsWith(strCarReservedSize));
        }

        /// <summary>
        /// 获取本项目车位尺寸列表
        /// </summary>
        /// <param name="strCarReservedSize"></param>
        /// <returns></returns>
        public static List<string> ConfigLstProjectSize()
        {
            if (null == m_lstCarSizes)
            {
                return new List<string>();
            }
            return m_lstCarSizes;
        }

        /// <summary>
        /// 根据库号获取对应所有ETV设备号
        /// </summary>
        /// <param name="nWareHouse"></param>
        /// <returns></returns>
        public static List<object> ConfigLstETVOrTVDeviceID(int nWareHouse)
        {
            if (!myPara.dictETVOrTVDeviceID.ContainsKey(nWareHouse))
            {
                return new List<object>();
            }

            return myPara.dictETVOrTVDeviceID[nWareHouse].Cast<object>().ToList();
        }

        /// <summary>
        /// 根据库号获取对应所有车厅Hall设备号
        /// </summary>
        /// <param name="nWareHouse"></param>
        /// <returns></returns>
        public static List<object> ConfigLstHallDeviceID(int nWareHouse)
        {
            if (!myPara.dictHallDeviceID.ContainsKey(nWareHouse))
            {
                return new List<object>();
            }

            return myPara.dictHallDeviceID[nWareHouse].Cast<object>().ToList();
        }

        /// <summary>
        /// 获取本项目最大车辆尺寸
        /// </summary>
        /// <returns></returns>
        public static string ConfigCarMaxSize()
        {
            return myPara.strCarMaxSize;
        }

        /// <summary>
        /// 根据库号获取对应所有ETV设备号
        /// </summary>
        /// <param name="nWareHouse"></param>
        /// <returns></returns>
        public static List<object> ConfigLstETVOrTVDeviceIDDescp(int nWareHouse)
        {
            if (!myPara.dictETVOrTVDeviceID.ContainsKey(nWareHouse))
            {
                return new List<object>();
            }

            List<object> lstObj = new List<object>();
            foreach (int nID in myPara.dictETVOrTVDeviceID[nWareHouse])
            {
                lstObj.Add(nID + "号TV");
            }
            return lstObj;
        }

        /// <summary>
        /// 根据库号获取对应所有车厅Hall设备号
        /// </summary>
        /// <param name="nWareHouse"></param>
        /// <returns></returns>
        public static List<object> ConfigLstHallDeviceIDDescp(int nWareHouse)
        {
            if (!myPara.dictHallDeviceID.ContainsKey(nWareHouse))
            {
                return new List<object>();
            }

            List<object> lstObj = new List<object>();
            Dictionary<int, struHallEquips> dicHall = myPara.DicHallIDDictionary[nWareHouse];
            foreach (struHallEquips struHall in dicHall.Values)
            {
                if (string.IsNullOrEmpty(struHall.strHallName))
                {
                    lstObj.Add((struHall.nHallID - 10) + "号车厅");
                }
                else
                { 
                    lstObj.Add(struHall.strHallName);
                }
            }
            return lstObj;
        }
        #endregion

        #region 读取配置文件之界面参数
        /// <summary>
        /// 主界面间隔12
        /// </summary>
        /// <returns></returns>
        public static int ConfigMainGap()
        {
            return m_mainGap;
        }

        /// <summary>
        /// 主界面最小宽度1000
        /// </summary>
        /// <returns></returns>
        public static int ConfigMinWidth()
        {
            return m_minWidth;
        }

        /// <summary>
        /// 主界面最小高度500
        /// </summary>
        /// <returns></returns>
        public static int ConfigMinHeight()
        {
            return m_minHeight;
        }

        /// <summary>
        /// 一搬控件大小30
        /// </summary>
        /// <returns></returns>
        public static int ConfigControlSize()
        {
            return m_controlSize;
        }

        /// <summary>
        /// 重列边间隔5
        /// </summary>
        /// <returns></returns>
        public static int ConfigMinGap()
        {
            return m_minGap;
        }

        /// <summary>
        /// "of "字符串
        /// </summary>
        /// <returns></returns>
        public static string ConfigStringOf()
        {
            return m_strOf;
        }

        /// <summary>
        /// " to "字符串
        /// </summary>
        /// <returns></returns>
        public static string ConfigStringTo()
        {
            return m_strTo;
        }

        /// <summary>
        /// " Pages"字符串
        /// </summary>
        /// <returns></returns>
        public static string ConfigStringPages()
        {
            return m_strPages;
        }

        /// <summary>
        /// 设备故障单个标签宽度140
        /// </summary>
        /// <returns></returns>
        public static int ConfigFaultLabelWidth()
        {
            return m_faultLabelWidth;
        }

        /// <summary>
        /// 设备故障单个标签高度20
        /// </summary>
        /// <returns></returns>
        public static int ConfigFaultLabelHeight()
        {
            return m_faultLabelHeight;
        }
        #endregion

        #region WCF推送服务器处理
        /// <summary>
        /// 打开推送服务器
        /// </summary>
        public static void OpenPushService()
        {
            m_bConnected = false;
            myPushProxy.Abort();
            myPushProxy.Close();
            myPushProxy = new PushServiceClient(new System.ServiceModel.InstanceContext(myCallback));
            myPushProxy.Register(Environment.MachineName);
            m_bConnected = true;
        }

        /// <summary>
        /// 获取推送服务器通道连接标志
        /// </summary>
        /// <returns></returns>
        public static bool GetPushServiceConnectFlag()
        {
            return m_bConnected;
        }

        /// <summary>
        /// 检查服务
        /// </summary>
        /// <returns></returns>
        public static bool CheckPushService()
        {
            if (!GetPushServiceConnectFlag())
            {// 服务器通道断开时
                try
                {// 服务器断开之后，尝试重新打开
                    OpenPushService();
                }
                catch (Exception)
                {
                    MessageBox.Show("尝试连接服务器失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 转换QueryService类到PushService中类
        /// </summary>
        /// <param name="deviceStatusDto"></param>
        /// <returns></returns>
        public static PushService.CDeviceStatusDto ConvertDeviceStatus(QueryService.CDeviceStatusDto deviceStatusDto)
        {
            PushService.CDeviceStatusDto dtoResult = new PushService.CDeviceStatusDto
            {//修改设备状态的属性 
                id = deviceStatusDto.id,
                devicecode = deviceStatusDto.devicecode,
                warehouse = deviceStatusDto.warehouse,
                iccode = deviceStatusDto.iccode,
                tasktype = deviceStatusDto.tasktype,
                isable = deviceStatusDto.isable,
                isavailable = deviceStatusDto.isavailable,
                devicemode = deviceStatusDto.devicemode,
                deviceaddr = deviceStatusDto.deviceaddr,
                devicetype = deviceStatusDto.devicetype,
                halltype = deviceStatusDto.halltype,
                instep = deviceStatusDto.instep,
                outstep = deviceStatusDto.outstep,
                runstep = deviceStatusDto.runstep,
                warehousechange = deviceStatusDto.warehousechange,
                devicelayer = deviceStatusDto.devicelayer,
                region = deviceStatusDto.region,
                prevnode = deviceStatusDto.prevnode,
                currentnode = deviceStatusDto.currentnode,
                queueprevnode = deviceStatusDto.queueprevnode,
                queuecurrentnode = deviceStatusDto.queuecurrentnode
            };

            return dtoResult;
        }

        /// <summary>
        /// 转换QueryService类到PushService中类
        /// </summary>
        /// <param name="deviceStatusDto"></param>
        /// <returns></returns>
        public static QueryService.CDeviceStatusDto ConvertDeviceStatus(PushService.CDeviceStatusDto deviceStatusDto)
        {
            QueryService.CDeviceStatusDto dtoResult = new QueryService.CDeviceStatusDto
            {//修改设备状态的属性 
                id = deviceStatusDto.id,
                devicecode = deviceStatusDto.devicecode,
                warehouse = deviceStatusDto.warehouse,
                iccode = deviceStatusDto.iccode,
                tasktype = deviceStatusDto.tasktype,
                isable = deviceStatusDto.isable,
                isavailable = deviceStatusDto.isavailable,
                devicemode = deviceStatusDto.devicemode,
                deviceaddr = deviceStatusDto.deviceaddr,
                devicetype = deviceStatusDto.devicetype,
                halltype = deviceStatusDto.halltype,
                instep = deviceStatusDto.instep,
                outstep = deviceStatusDto.outstep,
                runstep = deviceStatusDto.runstep,
                warehousechange = deviceStatusDto.warehousechange,
                devicelayer = deviceStatusDto.devicelayer,
                region = deviceStatusDto.region,
                prevnode = deviceStatusDto.prevnode,
                currentnode = deviceStatusDto.currentnode,
                queueprevnode = deviceStatusDto.queueprevnode,
                queuecurrentnode = deviceStatusDto.queuecurrentnode
            };

            return dtoResult;
        }
        #endregion
    }
}
