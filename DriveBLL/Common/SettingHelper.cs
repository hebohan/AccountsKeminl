using System.IO;
using System.Xml;

namespace Accounts.BLL.Common
{
    public class SettingHelper
    {
        #region 私有成员
        private string _serviceName;
        private string _displayName;
        private string _description;
        #endregion
        #region 构造函数
        /// <summary> 
        /// 初始化服务配置帮助类 
        /// </summary> 
        public SettingHelper()
        {
            InitSettings();
        }
        #endregion
        #region 属性
        /// <summary> 
        /// 系统用于标志此服务的名称 
        /// </summary> 
        public string ServiceName
        {
            get { return _serviceName; }
        }
        /// <summary> 
        /// 向用户标志服务的友好名称 
        /// </summary> 
        public string DisplayName
        {
            get { return _displayName; }
        }
        /// <summary> 
        /// 服务的说明 
        /// </summary> 
        public string Description
        {
            get { return _description; }
        }
        #endregion
        #region 私有方法
        #region 初始化服务配置信息
        /// <summary> 
        /// 初始化服务配置信息 
        /// </summary> 
        private void InitSettings()
        {
            string root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string xmlfile = root.Remove(root.LastIndexOf('\\') + 1) + "ServiceSetting.xml";
            if (File.Exists(xmlfile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlfile);
                XmlNode xn = doc.SelectSingleNode("Settings/ServiceName");
                _serviceName = xn.InnerText;
                xn = doc.SelectSingleNode("Settings/DisplayName");
                _displayName = xn.InnerText;
                xn = doc.SelectSingleNode("Settings/Description");
                _description = xn.InnerText;
                doc = null;
            }
            else
            {
                throw new FileNotFoundException("未能找到服务名称配置文件 ServiceSetting.xml！");
            }
        }
        #endregion
        #endregion
    }

}
