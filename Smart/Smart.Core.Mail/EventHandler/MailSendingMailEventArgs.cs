using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Smart.Core.Mail.Base;

namespace Smart.Core.Mail.EventHandler
{
    /// <summary> 
    /// Custom Event Args for Mail event 
    /// </summary> 
    public class MailSendingMailEventArgs : CancelEventArgs
    {
        private Email _email;
        public Email Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }

}
