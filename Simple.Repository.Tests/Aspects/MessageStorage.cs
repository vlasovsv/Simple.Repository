using System.Collections.Generic;

namespace Simple.Repository.Tests.Aspects
{
    public class MessageStorage
    {
        public MessageStorage()
        {
            Messages = new List<string>();
        }

        public List<string> Messages
        {
            get;
            private set;
        }
    }
}
