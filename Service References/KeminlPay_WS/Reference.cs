﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Accounts.Web.KeminlPay_WS {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="KeminlPay_WS.WsItemSoap")]
    public interface WsItemSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPayStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetPayStatus(string orderid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetUserInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetUserInfo(string username, string loginpass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UpdateCash", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool UpdateCash(string Key, string addmoney, string sign, string userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendMail", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool SendMail(string receiver, string title, string content, string sender, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WsItemSoapChannel : Accounts.Web.KeminlPay_WS.WsItemSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WsItemSoapClient : System.ServiceModel.ClientBase<Accounts.Web.KeminlPay_WS.WsItemSoap>, Accounts.Web.KeminlPay_WS.WsItemSoap {
        
        public WsItemSoapClient() {
        }
        
        public WsItemSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WsItemSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsItemSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsItemSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetPayStatus(string orderid) {
            return base.Channel.GetPayStatus(orderid);
        }
        
        public System.Data.DataSet GetUserInfo(string username, string loginpass) {
            return base.Channel.GetUserInfo(username, loginpass);
        }
        
        public bool UpdateCash(string Key, string addmoney, string sign, string userid) {
            return base.Channel.UpdateCash(Key, addmoney, sign, userid);
        }
        
        public bool SendMail(string receiver, string title, string content, string sender, string password) {
            return base.Channel.SendMail(receiver, title, content, sender, password);
        }
    }
}
