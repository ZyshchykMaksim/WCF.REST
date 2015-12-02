using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;

namespace WCF.RESTService.Cors
{
    // Based on part of the Cross-Origin Resource Sharing documentation: http://www.w3.org/TR/cors/

    /// <summary>
    /// <see cref="http://blogs.microsoft.co.il/idof/2011/07/02/cross-origin-resource-sharing-cors-and-wcf/"/>
    /// </summary>
    public class CustomOperationInvoker : IOperationInvoker
    {
        IOperationInvoker _innerInvoker = null;
        public CustomOperationInvoker(IOperationInvoker innerInvoker)
        {
            _innerInvoker = innerInvoker;
        }

        public object[] AllocateInputs()
        {
            return _innerInvoker.AllocateInputs();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            // Check if the unhandled request is due to preflight checks (OPTIONS header)
            if (OperationContext.Current.Extensions.Find<PreflightDetected>() != null)
            {
                // Override the standard error handling, so the request won't contain an error
                outputs = null;
                return null;
            }
            else
            {
                // No preflight - probably a missed call (wrong URI or method)
                return _innerInvoker.Invoke(instance, inputs, out outputs);
            }
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            // Not supported - an exception will be thrown
            return _innerInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            // Not supported - an exception will be thrown
            return _innerInvoker.InvokeEnd(instance, out outputs, result);
        }

        public bool IsSynchronous
        {
            get
            {
                return _innerInvoker.IsSynchronous;
            }
        }
    }
}
