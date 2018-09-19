using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils
{
    public class Enums
    {
        public enum ProfileType
        {
            User = 1, Admin, SubAdmin,SalesMan
        }

        public enum ActionOwnerTypes
        {
            Client = 1,ServiceProvider
        }
        public enum NotificationType
        {
            RequestReply = 1, Rating, Review, ResponseAccepted
        }
        public enum RenewalTimeSpan
        {
            SixMonth = 1, OneYear
        }
    }
}
