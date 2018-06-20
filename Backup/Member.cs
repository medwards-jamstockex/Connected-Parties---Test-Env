using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectedParties
{
    public class Member
    {
        public string AccountReference { get; set; }
        public string AccountName { get; set; }
        public int AccountID { get; set; }
        public int NameID { get; set; }
        public string TRN { get; set; }
        public string UIName
        {
            get
            {
                return string.Format("{0} - {1} - {2} - {3} - {4}", AccountReference, AccountID, AccountName, TRN, NameID);
            }
        }
    }
}
