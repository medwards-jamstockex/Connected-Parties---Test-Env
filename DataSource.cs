using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace ConnectedParties
{
    public class DataSource
    {
        private static readonly string ConnStrConfig =
            CurrentUser.Instance.Environment == WorkingEnvironment.RSU ?
                "DependTrain" : "DependProducton";

        //private static readonly string ConnStrConfigTest = "DependTest";

        //private static readonly string ConnStrTest = ConfigurationManager
        //    .ConnectionStrings[ConnStrConfigTest]
        //    .ConnectionString;

        private static readonly string ConnStr = ConfigurationManager
            .ConnectionStrings[ConnStrConfig]
            .ConnectionString;

        public static List<Member> GetMembers()
        {
            List<Member> members = new List<Member>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);

            // get the members

            conn.Open();
            SqlCommand cmd = new SqlCommand("getMembers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string memName = string.Format("{0}, {1}",
                    reader["NAME_SURNAME"], reader["NAME_FORENAMES"]);

                Member member = new Member()
                {
                    NameID = reader.GetInt32(reader.GetOrdinal("NAME_ID")),
                    AccountName = memName,
                    AccountReference = reader["ACCOUNT_REFERENCE"].ToString(),
                    AccountID = reader.GetInt32(reader.GetOrdinal("ACCOUNT_ID")),
                    TRN = reader["NAMEID_VALUE"].ToString()
                };

                members.Add(member);
            }//END WHILE


            return members;
        }

        public static List<Member> GetKeyMembers()
        {
            List<Member> members = new List<Member>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);

            // get the members
            conn.Open();
            SqlCommand cmd = new SqlCommand("getKeyMembers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string memName = string.Format("{0}, {1}",
                    reader["NAME_SURNAME"], reader["NAME_FORENAMES"]);

                Member member = new Member()
                {
                    NameID = reader.GetInt32(reader.GetOrdinal("NAME_ID")),
                    AccountName = memName,
                    AccountReference = reader["ACCOUNT_REFERENCE"].ToString(),
                    AccountID = reader.GetInt32(reader.GetOrdinal("ACCOUNT_ID")),
                    TRN = reader["NAMEID_VALUE"].ToString()
                };

                members.Add(member);
            }

            return members;
        }

        public static List<Relationship> GetRelationships()
        {
            List<Relationship> relationships = new List<Relationship>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand("getRelationships", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Relationship rel = new Relationship()
                {
                    ID = reader.GetInt32(reader.GetOrdinal("RelationshipID")),
                    Description = reader["Relationship"].ToString()
                };

                relationships.Add(rel);
            }


            return relationships;
        }

        public static void SaveMember(int keyMemAccountId, string keyMemTRN, int issuerId, string memTypeId, string memJoinReason, string memReportType, DateTime memJoinDate, string memPosition)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);

            SqlCommand cmd = new SqlCommand("saveMember", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@keyMemAccountId", keyMemAccountId);
            cmd.Parameters.AddWithValue("@memTRN", keyMemTRN);
            cmd.Parameters.AddWithValue("@issuerId", issuerId);
            cmd.Parameters.AddWithValue("@memJoinDate", memJoinDate);

            DateTime resignOn = new DateTime(9998, 12, 31);
            cmd.Parameters.AddWithValue("@memResignDate", resignOn);

            cmd.Parameters.AddWithValue("@memTypeId", memTypeId);
            cmd.Parameters.AddWithValue("@memRestricted", memJoinDate);
            cmd.Parameters.AddWithValue("@memJoinReason", memJoinReason);
            cmd.Parameters.AddWithValue("@memReportCopies", 1);
            cmd.Parameters.AddWithValue("@memReportType", memReportType);
            cmd.Parameters.AddWithValue("@memEmployee", memJoinDate);
            cmd.Parameters.AddWithValue("@memPosition", memPosition);
            cmd.Parameters.AddWithValue("@userID", CurrentUser.Instance.UserID);
            cmd.Parameters.AddWithValue("@today", DateTime.Now);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public static void SaveConnection(int keyMemNameId, int conPartyNameId, int relationshipId, int Ownership)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);

            SqlCommand cmd = new SqlCommand("saveConnection", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@keyMemNameId", keyMemNameId);
            cmd.Parameters.AddWithValue("@conPartyNameId", conPartyNameId);
            cmd.Parameters.AddWithValue("@relationshipId", relationshipId);
            cmd.Parameters.AddWithValue("@ownership", Ownership);
            cmd.Parameters.AddWithValue("@userID", CurrentUser.Instance.UserID);
            cmd.Parameters.AddWithValue("@today", DateTime.Now);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public static void DeleteConnection(int keyMemNameId, int conPartyNameId)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);


            SqlCommand cmd = new SqlCommand("deleteConnection", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@keyMemNameId", keyMemNameId);
            cmd.Parameters.AddWithValue("@conPartyNameId", conPartyNameId);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public static void EditKeyMember(string KeyMemberAccountID, string memNewPosition, string memIssuer, DateTime MemberJoin, DateTime MemberResign,
                                         string memJoinReason, string memReportType, string memTRN, string RSUPosition, string memPosition)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);

                SqlCommand cmd = new SqlCommand("editKeyMember", conn);
            cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@KeyMemberAccountID", KeyMemberAccountID);
                cmd.Parameters.AddWithValue("@MemberIssuer", memIssuer);
                cmd.Parameters.AddWithValue("@MemPosition", memPosition);
                cmd.Parameters.AddWithValue("@MemNewPosition", memNewPosition);
                cmd.Parameters.AddWithValue("@MemberJoinReason", memJoinReason);
                cmd.Parameters.AddWithValue("@MemberReportType", memReportType);
                cmd.Parameters.AddWithValue("@MemberTRN", memTRN);
                cmd.Parameters.AddWithValue("@RSUPosition", RSUPosition);
                cmd.Parameters.AddWithValue("@MemberJoin", MemberJoin);
                cmd.Parameters.AddWithValue("@MemberResign", MemberResign);
                cmd.Parameters.AddWithValue("@userID", CurrentUser.Instance.UserID);
                cmd.Parameters.AddWithValue("@today", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
           
        }


        public static void DeleteKeyMember(string KeyMemberAccountID, string memPosition, string memIssuer, DateTime MemberJoin, DateTime MemberResign,
                                 string memJoinReason, string memReportType, string memTRN, string RSUPosition)
        {

            //String conn2 = ConfigurationManager.ConnectionStrings["DependTest"];
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);
            SqlCommand cmd = new SqlCommand("deleteKeyMember", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@KeyMemberAccountID", KeyMemberAccountID);
            cmd.Parameters.AddWithValue("@memPosition", RSUPosition);
            cmd.Parameters.AddWithValue("@MemberJoin", MemberJoin);
            cmd.Parameters.AddWithValue("@userID", CurrentUser.Instance.UserID);
            cmd.Parameters.AddWithValue("@today", DateTime.Now);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public static void ResignPosition(string KeyMemberAccountID, string txtPosition, string txtIssuer, DateTime MemberJoin, DateTime MemberResign)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);
            SqlCommand cmd = new SqlCommand("resignPosition", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@KeyMemberAccountID", KeyMemberAccountID);
            cmd.Parameters.AddWithValue("@txtPosition", txtPosition);
            cmd.Parameters.AddWithValue("@txtIssuer", txtIssuer);
            cmd.Parameters.AddWithValue("@MemberJoin", MemberJoin);
            cmd.Parameters.AddWithValue("@MemberResign", MemberResign);
            cmd.Parameters.AddWithValue("@userID", CurrentUser.Instance.UserID);
            cmd.Parameters.AddWithValue("@today", DateTime.Now);

            conn.Open();
            cmd.ExecuteNonQuery();
           
        }

        public static List<MemberConnection> GetConnections(int keyMemNameId)
        {
            List<MemberConnection> connections = new List<MemberConnection>();


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);
            SqlCommand cmd = new SqlCommand("getConnections", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@keyMemNameID", keyMemNameId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MemberConnection memCon = new MemberConnection()
                    {
                        KeyMemberNameID = (int)reader["KeyMemberNameID"],
                        ConnectedPartyNameID = (int)reader["ConnectedPartyNameID"],
                        Relationship = reader["Relationship"].ToString(),
                        EnteredBy = reader["USER_NAME"].ToString(),
                        EnteredAt = (DateTime)reader["EnteredAt"]
                    };

                    connections.Add(memCon);
                }
            

            return connections;
        }

        public static List<MemberPosition> GetPositions(int keyMemAccountId)
        {
            List<MemberPosition> positions = new List<MemberPosition>();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);

                conn.Open();
                SqlCommand cmd = new SqlCommand("getPositionsPrimary", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@keyMemAccountID", keyMemAccountId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MemberPosition memPos = new MemberPosition()
                    {
                        KeyMemberAccountID = (int)reader["MEMBER_ACCOUNT"],
                        //KeyMemberAccountRef = reader["ACCOUNT_REFERENCE"].ToString(),
                        RSUMemberPosition = reader["MEMBER_POSITION"].ToString(),
                        KeyMemberJoinReason = reader["MEMBER_JOIN_REASON"].ToString(),
                        KeyMemberReportType = reader["MEMBER_REPORT_TYPE"].ToString(),
                        KeyMemberTRN = reader["MEMBER_TRN"].ToString(),
                        //KeyMemberName = reader["MemberName"].ToString(),
                        KeyMemberIssuer = reader["ISSUER_NAME"].ToString(),
                        KeyMemberPosition = reader["DIRECTOR_STATUS_DESCRIPTION"].ToString(),
                        KeyMemberJoinDate = (DateTime)reader["MEMBER_JOINED"],
                        KeyMemberResignDate = (DateTime)reader["MEMBER_RESIGNED"],
                        EnteredBy = reader["USER_NAME"].ToString(),
                        EnteredAt = (DateTime)reader["EnteredAt"]
                    };

                    positions.Add(memPos);
                }

            return positions;
        }

        public static List<MemberPosition> GetPositions(int keyMemAccountId, int issuerId)
        {
            List<MemberPosition> positions = new List<MemberPosition>();

            DateTime resignOn = new DateTime(9998, 12, 31);
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);
            SqlCommand cmd = new SqlCommand("getPositionsSecondary", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@keyMemAccountID", keyMemAccountId);
            cmd.Parameters.AddWithValue("@issuerID", issuerId);
            cmd.Parameters.AddWithValue("@resignOn", resignOn);
            
            conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MemberPosition memPos = new MemberPosition()
                    {
                        KeyMemberAccountID = (int)reader["MEMBER_ACCOUNT"],
                        KeyMemberIssuer = reader["ISSUER_NAME"].ToString(),
                        KeyMemberPosition = reader["DIRECTOR_STATUS_DESCRIPTION"].ToString(),
                        KeyMemberTRN = reader["MEMBER_TRN"].ToString(),
                        KeyMemberJoinDate = (DateTime)reader["MEMBER_JOINED"],
                        KeyMemberResignDate = (DateTime)reader["MEMBER_RESIGNED"],
                        RSUMemberPosition = reader["MEMBER_POSITION"].ToString(),
                        KeyMemberJoinReason = reader["MEMBER_JOIN_REASON"].ToString(),
                        KeyMemberReportType = reader["MEMBER_REPORT_TYPE"].ToString(),
                        EnteredBy = reader["USER_NAME"].ToString(),
                        EnteredAt = (DateTime)reader["EnteredAt"]

                    };

                    positions.Add(memPos);
                }

            return positions;
        }

        public static List<MemberPosition> GetPositions(int keyMemAccountId, int issuerId, string memberPosition)
        {
            List<MemberPosition> positions = new List<MemberPosition>();

            DateTime resignOn = new DateTime(9998, 12, 31);


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("getPositionsTertiary", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@keyMemAccountID", keyMemAccountId);
            cmd.Parameters.AddWithValue("@issuerID", issuerId);
            cmd.Parameters.AddWithValue("@resignOn", resignOn);
            cmd.Parameters.AddWithValue("@memberPosition", memberPosition);
            SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MemberPosition memPos = new MemberPosition()
                    {
                        KeyMemberAccountID = (int)reader["MEMBER_ACCOUNT"],
                        KeyMemberIssuer = reader["ISSUER_NAME"].ToString(),
                        KeyMemberPosition = reader["DIRECTOR_STATUS_DESCRIPTION"].ToString(),
                        KeyMemberTRN = reader["MEMBER_TRN"].ToString(),
                        KeyMemberJoinDate = (DateTime)reader["MEMBER_JOINED"],
                        KeyMemberResignDate = (DateTime)reader["MEMBER_RESIGNED"],
                        RSUMemberPosition = reader["MEMBER_POSITION"].ToString(),
                        KeyMemberJoinReason = reader["MEMBER_JOIN_REASON"].ToString(),
                        KeyMemberReportType = reader["MEMBER_REPORT_TYPE"].ToString(),
                        EnteredBy = reader["USER_NAME"].ToString(),
                        EnteredAt = (DateTime)reader["EnteredAt"]

                    };

                    positions.Add(memPos);
                }
            

            return positions;
        }

        public static string[] Getuser(string username, string participantCode)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);
            SqlCommand cmd = new SqlCommand("getUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@participantCode", participantCode);

                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new string[] { reader["USER_ID"].ToString(), reader["USER_NAME"].ToString() };

            return new string[0];
        }

        public static List<InstrumentNote> GetInstrumentNotes()
        {
            List<InstrumentNote> instruments = new List<InstrumentNote>();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand("getInstrumentNotes", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                instruments.Add(new InstrumentNote()
                {
                    Isin = reader["ISIN_CODE"].ToString(),
                    ShortName = reader["ISIN_SHORT_NAME"].ToString(),
                    Note = reader["Note"] as string ?? string.Empty
                });
            }


            return instruments;
        }

        public static void SaveInstrumentNote(string isin, string note)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand("saveInstrumentNote", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SymbolIsin", isin);
            cmd.Parameters.AddWithValue("@Note", note);

            cmd.ExecuteNonQuery();
        }

        //Calls Stored Procedure to update and sync database with Depend
        public static void UpdateRecords(int status)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DependTest"].ConnectionString);
            SqlCommand cmd = new SqlCommand("updateRecords_CP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Status", status);
            //cmd.Parameters.Add("@result", SqlDbType.Int);
            cmd.Parameters["@status"].Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            if (!cmd.Parameters["@status"].Value.Equals(0))
            {
                MessageBox.Show("Records were not synced properly", "Error", MessageBoxButtons.OKCancel);
            }

        }//END UDPATE RECORDS
    }
}


