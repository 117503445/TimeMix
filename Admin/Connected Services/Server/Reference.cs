﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Admin.Server {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Server.INetTime")]
    public interface INetTime {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetTime/GetTime", ReplyAction="http://tempuri.org/INetTime/GetTimeResponse")]
        System.DateTime GetTime();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetTime/GetTime", ReplyAction="http://tempuri.org/INetTime/GetTimeResponse")]
        System.Threading.Tasks.Task<System.DateTime> GetTimeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetTime/SetTime", ReplyAction="http://tempuri.org/INetTime/SetTimeResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="permitted")]
        bool SetTime(System.DateTime dateTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetTime/SetTime", ReplyAction="http://tempuri.org/INetTime/SetTimeResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="permitted")]
        System.Threading.Tasks.Task<bool> SetTimeAsync(System.DateTime dateTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetTime/Login", ReplyAction="http://tempuri.org/INetTime/LoginResponse")]
        bool Login(string name, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetTime/Login", ReplyAction="http://tempuri.org/INetTime/LoginResponse")]
        System.Threading.Tasks.Task<bool> LoginAsync(string name, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface INetTimeChannel : Admin.Server.INetTime, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NetTimeClient : System.ServiceModel.ClientBase<Admin.Server.INetTime>, Admin.Server.INetTime {
        
        public NetTimeClient() {
        }
        
        public NetTimeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NetTimeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NetTimeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NetTimeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.DateTime GetTime() {
            return base.Channel.GetTime();
        }
        
        public System.Threading.Tasks.Task<System.DateTime> GetTimeAsync() {
            return base.Channel.GetTimeAsync();
        }
        
        public bool SetTime(System.DateTime dateTime) {
            return base.Channel.SetTime(dateTime);
        }
        
        public System.Threading.Tasks.Task<bool> SetTimeAsync(System.DateTime dateTime) {
            return base.Channel.SetTimeAsync(dateTime);
        }
        
        public bool Login(string name, string password) {
            return base.Channel.Login(name, password);
        }
        
        public System.Threading.Tasks.Task<bool> LoginAsync(string name, string password) {
            return base.Channel.LoginAsync(name, password);
        }
    }
}
