using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectedParties
{
    public class MemberPosition
    {
        public int KeyMemberAccountID { get; set; }
        public string KeyMemberTRN { get; set; }
        public string KeyMemberName { get; set; }
        public string KeyMemberIssuer { get; set; }
        public string KeyMemberPosition { get; set; }
        public string RSUMemberPosition { get; set; }
        public string KeyMemberNatRestrict { get; set; }
        public string KeyMemberJoinReason { get; set; }
        public string KeyMemberReportType { get; set; }
        public DateTime KeyMemberJoinDate { get; set; }
        public DateTime KeyMemberResignDate { get; set; }
        public string EnteredBy { get; set; }
        public DateTime EnteredAt { get; set; }

        //MEMBER_ACCOUNT
        //,MEMBER_ISSUER
        //,MEMBER_JOINED
        //,MEMBER_RESIGNED
        //,MEMBER_DIRECTOR
        //,MEMBER_RESTRICTED
        //,MEMBER_JOIN_REASON
        //,MEMBER_REPORT_COPIES
        //,MEMBER_REPORT_TYPE
        //,MEMBER_EMPLOYEE
        //,MEMBER_TRN
        //,EnteredByUserID
        //,EnteredAt
        //,UpdatedByUserID
        //,UpdatedAt
        //,MEMBER_POSITION

    }
}
