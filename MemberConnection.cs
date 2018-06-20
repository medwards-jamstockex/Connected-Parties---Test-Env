using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectedParties
{
    public class MemberConnection
    {
        public int KeyMemberNameID { get; set; }
        public string KeyMemberName { get; set; }
        public int ConnectedPartyNameID { get; set; }
        public string ConnectedPartyName { get; set; }
        public string Relationship { get; set; }
        public string Ownership { get; set; }
        public string EnteredBy { get; set; }
        public DateTime EnteredAt { get; set; }
    }
}
