﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.GZipEncoder
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Microsoft.Samples.GZipEncoder.ISampleServer")]
    public interface ISampleServer
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleServer/Echo", ReplyAction="http://tempuri.org/ISampleServer/EchoResponse")]
        string Echo(string input);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleServer/Echo", ReplyAction="http://tempuri.org/ISampleServer/EchoResponse")]
        System.Threading.Tasks.Task<string> EchoAsync(string input);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleServer/BigEcho", ReplyAction="http://tempuri.org/ISampleServer/BigEchoResponse")]
        string[] BigEcho(string[] input);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleServer/BigEcho", ReplyAction="http://tempuri.org/ISampleServer/BigEchoResponse")]
        System.Threading.Tasks.Task<string[]> BigEchoAsync(string[] input);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public interface ISampleServerChannel : Microsoft.Samples.GZipEncoder.ISampleServer, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public partial class SampleServerClient : System.ServiceModel.ClientBase<Microsoft.Samples.GZipEncoder.ISampleServer>, Microsoft.Samples.GZipEncoder.ISampleServer
    {
        
        public SampleServerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public string Echo(string input)
        {
            return base.Channel.Echo(input);
        }
        
        public System.Threading.Tasks.Task<string> EchoAsync(string input)
        {
            return base.Channel.EchoAsync(input);
        }
        
        public string[] BigEcho(string[] input)
        {
            return base.Channel.BigEcho(input);
        }
        
        public System.Threading.Tasks.Task<string[]> BigEchoAsync(string[] input)
        {
            return base.Channel.BigEchoAsync(input);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
    }
}
