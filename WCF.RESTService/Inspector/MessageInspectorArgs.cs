using System;

namespace WCF.RESTService.Inspector
{
    /// <summary>
    /// Class to pass inspection event arguments.
    /// </summary>
    public class MessageInspectorArgs : EventArgs
    {
        ///<summary>
        /// Type of the message inpected.
        /// </summary>
        public MessageInspectionType MessageInspectionType { get; internal set; }

        ///<summary> 
        /// Inspected raw message.
        /// </summary>
        public MessageInfo Message { get; internal set; }
    }
}
