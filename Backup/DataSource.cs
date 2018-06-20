using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace ConnectedParties
{
    public class DataSource
    {
        private static readonly string ConnStrConfig =
            CurrentUser.Instance.Environment == WorkingEnvironment.RSU ?
                "DependTrain" : "DependProducton";

        private static readonly string ConnStr = ConfigurationManager
            .ConnectionStrings[ConnStrConfig]
            .ConnectionString;

        public static List<Member> GetMembers()
        {
            List<Member> members = new List<Member>();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                // get the members
                string membersQuery = @"select
	                NAME.NAME_ID,
	                NAME.NAME_SURNAME,
	                NAME.NAME_FORENAMES,
	                ACCOUNT.ACCOUNT_REFERENCE,
                    ACCOUNT.ACCOUNT_ID,
                    NAMEID_VALUE,
	                NAME.NAME_STATUS
                from
	                NAME
	                left join ACCOUNT on NAME.NAME_ACCOUNT = ACCOUNT.ACCOUNT_ID
                    left join NAMEID on (NAME_ID = NAMEID_NAME 
                                          and NAMEID_TYPE = 'TRN')		
                where
	                NAME.NAME_STATUS = 'A'
                order by NAME.NAME_SURNAME, NAME.NAME_FORENAMES";

                conn.Open();
                SqlCommand cmd = new SqlCommand(membersQuery, conn);
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
            }

            return members;
        }

        public static List<Member> GetKeyMembers()
        {
            List<Member> members = new List<Member>();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                // get the members
                string membersQuery = @"select
	                NAME.NAME_ID,
	                NAME.NAME_SURNAME,
	                NAME.NAME_FORENAMES,
	                ACCOUNT.ACCOUNT_REFERENCE,
                    ACCOUNT.ACCOUNT_ID,
                    NAMEID_VALUE,
	                NAME.NAME_STATUS
                from
	                NAME
	                left join ACCOUNT on NAME.NAME_ACCOUNT = ACCOUNT.ACCOUNT_ID
                    left join NAMEID on (NAME_ID = NAMEID_NAME 
                                          and NAMEID_TYPE = 'TRN')
                where
	                NAME.NAME_STATUS = 'A' and NAME_SEQUENCE = 1
                order by NAME.NAME_SURNAME, NAME.NAME_FORENAMES";

                conn.Open();
                SqlCommand cmd = new SqlCommand(membersQuery, conn);
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
            }

            return members;
        }

        public static List<Relationship> GetRelationships()
        {
            List<Relationship> relationships = new List<Relationship>();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                string relationsQuery = @"select * from
                        RSUDepend.dbo.CP_Relationships order by Relationship";

                conn.Open();
                SqlCommand cmd = new SqlCommand(relationsQuery, conn);
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
            }

            return relationships;
        }

        public static void SaveMember(int keyMemAccountId, string keyMemTRN, int issuerId, string memTypeId, string memJoinReason, string memReportType, DateTime memJoinDate, string memPosition)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                var cmdStr = @"insert into RSUDepend.dbo.CP_Member
                    ([MEMBER_ACCOUNT]
                      ,[MEMBER_ISSUER]
                      ,[MEMBER_JOINED]
                      ,[MEMBER_RESIGNED]
                      ,[MEMBER_DIRECTOR]
                      ,[MEMBER_RESTRICTED]
                      ,[MEMBER_JOIN_REASON]
                      ,[MEMBER_REPORT_COPIES]
                      ,[MEMBER_REPORT_TYPE]
                      ,[MEMBER_EMPLOYEE]
                      ,[MEMBER_TRN]
                      ,[MEMBER_POSITION]
                      ,EnteredByUserID
                      ,EnteredAt)
                    values(@keyMemAccountId,
                           @issuerId, 
                           @memJoinDate, 
                           @memResignDate, 
                           @memTypeId,
                           @memRestricted,
                           @memJoinReason,
                           @memReportCopies,
                           @memReportType,
                           @memEmployee,
                           @memTRN,
                           @memPosition,
                           @userID,
                           @today)";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
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
        }

        public static void SaveConnection(int keyMemNameId, int conPartyNameId, int relationshipId)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                var cmdStr = @"insert into RSUDepend.dbo.CP_Connections
                    (KeyMemberNameID, ConnectedPartyNameID, RelationshipID,
                    EnteredByUserID, EnteredAt) values(@keyMemNameId,
                    @conPartyNameId, @relationshipId, @userID, @today)";

                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@keyMemNameId", keyMemNameId);
                cmd.Parameters.AddWithValue("@conPartyNameId", conPartyNameId);
                cmd.Parameters.AddWithValue("@relationshipId", relationshipId);
                cmd.Parameters.AddWithValue("@userID", CurrentUser.Instance.UserID);
                cmd.Parameters.AddWithValue("@today", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteConnection(int keyMemNameId, int conPartyNameId)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                string cmdStr = @"delete from RSUDepend.dbo.CP_Connections
                    where KeyMemberNameID = @keyMemNameId
                        and ConnectedPartyNameID = @conPartyNameId";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@keyMemNameId", keyMemNameId);
                cmd.Parameters.AddWithValue("@conPartyNameId", conPartyNameId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void EditKeyMember(string KeyMemberAccountID, string memNewPosition, string memIssuer, DateTime MemberJoin, DateTime MemberResign,
                                         string memJoinReason, string memReportType, string memTRN, string RSUPosition, string memPosition)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                string cmdStr = @"update	RSUDepend..CP_Member								 
	                                set		MEMBER_RESIGNED = @MemberResign,
                                            MEMBER_DIRECTOR = @MemNewPosition,
											MEMBER_ISSUER = @MemberIssuer,
											MEMBER_JOIN_REASON = @MemberJoinReason,
											MEMBER_REPORT_TYPE = @MemberReportType,
											MEMBER_TRN = @MemberTRN,
											MEMBER_POSITION = @RSUPosition,					
									        UpdatedByUserID = @userID,
                                            UpdatedAt = @today
	                                from	RSUDepend..CP_Member m join ISSUER i on m.MEMBER_ISSUER = i.ISSUER_ID
								                                   join CONFIG_DIRECTOR_STATUS d on  m.MEMBER_DIRECTOR = d.DIRECTOR_STATUS_CODE 
	                                where 	MEMBER_ACCOUNT = @KeyMemberAccountID	
			                                and DIRECTOR_STATUS_CODE = @MemPosition
			                                and ISSUER_ID = @MemberIssuer
			                                and MEMBER_JOINED = @MemberJoin";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
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
        }


        public static void DeleteKeyMember(string KeyMemberAccountID, string memPosition, string memIssuer, DateTime MemberJoin, DateTime MemberResign,
                                 string memJoinReason, string memReportType, string memTRN, string RSUPosition)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                string cmdStr = @"insert into RSUDepend..CP_Member_Deleted (
				                                MEMBER_ACCOUNT
				                                ,MEMBER_ISSUER
				                                ,MEMBER_JOINED
				                                ,MEMBER_RESIGNED
				                                ,MEMBER_DIRECTOR
				                                ,MEMBER_RESTRICTED
				                                ,MEMBER_JOIN_REASON
				                                ,MEMBER_REPORT_COPIES
				                                ,MEMBER_REPORT_TYPE
				                                ,MEMBER_EMPLOYEE
				                                ,MEMBER_POSITION
				                                ,MEMBER_TRN
				                                ,EnteredByUserID
				                                ,EnteredAt
				                                ,UpdatedByUserID
				                                ,UpdatedAt
				                                ,DeletedByUserID
				                                ,DeletedAt
                                )

                                select		MEMBER_ACCOUNT
			                                ,MEMBER_ISSUER
			                                ,MEMBER_JOINED
			                                ,MEMBER_RESIGNED
			                                ,MEMBER_DIRECTOR
			                                ,MEMBER_RESTRICTED
			                                ,MEMBER_JOIN_REASON
			                                ,MEMBER_REPORT_COPIES
			                                ,MEMBER_REPORT_TYPE
			                                ,MEMBER_EMPLOYEE
			                                ,MEMBER_POSITION
			                                ,MEMBER_TRN
			                                ,EnteredByUserID
			                                ,EnteredAt
			                                ,UpdatedByUserID
			                                ,UpdatedAt
			                                ,@userID
			                                ,@today
	                                from	RSUDepend..CP_Member
	                                where 	MEMBER_ACCOUNT = @KeyMemberAccountID	
                                            and MEMBER_DIRECTOR = @memPosition
                                            and MEMBER_ISSUER = @MemberIssuer
                                            and MEMBER_JOINED = @MemberJoin
                                            
                                delete 	
                                    from	RSUDepend..CP_Member
	                                where 	MEMBER_ACCOUNT = @KeyMemberAccountID	
                                            and MEMBER_DIRECTOR = @memPosition
                                            and MEMBER_ISSUER = @MemberIssuer
                                            and MEMBER_JOINED = @MemberJoin;";   
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@KeyMemberAccountID", KeyMemberAccountID);
                cmd.Parameters.AddWithValue("@MemberIssuer", memIssuer);
                cmd.Parameters.AddWithValue("@memPosition", memPosition);
                cmd.Parameters.AddWithValue("@MemberJoin", MemberJoin);
                cmd.Parameters.AddWithValue("@userID", CurrentUser.Instance.UserID);
                cmd.Parameters.AddWithValue("@today", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void ResignPosition(string KeyMemberAccountID, string txtPosition, string txtIssuer, DateTime MemberJoin, DateTime MemberResign)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                string cmdStr = @"update	RSUDepend..CP_Member								 
	                                set		MEMBER_RESIGNED = @MemberResign,
                                            UpdatedByUserID = @userID,
                                            UpdatedAt = @today
	                                from	RSUDepend..CP_Member m join ISSUER i on m.MEMBER_ISSUER = i.ISSUER_ID
								                                   join CONFIG_DIRECTOR_STATUS d on  m.MEMBER_DIRECTOR = d.DIRECTOR_STATUS_CODE 
	                                where 	MEMBER_ACCOUNT = @KeyMemberAccountID	
			                                and DIRECTOR_STATUS_DESCRIPTION = @txtPosition
			                                and ISSUER_SHORT_NAME = @txtIssuer
			                                and MEMBER_JOINED = @MemberJoin";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
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
        }

        public static List<MemberConnection> GetConnections(int keyMemNameId)
        {
            List<MemberConnection> connections = new List<MemberConnection>();

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                string query = @"select USERSHADOW.USER_NAME,
	                    RSUDepend.dbo.CP_Connections.*,
	                    RSUDepend.dbo.CP_Relationships.Relationship	
                    from
	                    RSUDepend.dbo.CP_Connections
	                    left join RSUDepend.dbo.CP_Relationships on
		                    RSUDepend.dbo.CP_Relationships.RelationshipID = RSUDepend.dbo.CP_Connections.RelationshipID
	                    left join USERSHADOW on USERSHADOW.USER_ID = RSUDepend.dbo.CP_Connections.EnteredByUserID
	                    left join DEPOUSER on USERSHADOW.USER_ID = DEPOUSER.DEPOUSER_USER
	                    left join DEPOPART on DEPOPART.DEPOPART_ID = DEPOUSER.DEPOUSER_PART_ID
	                    where KeyMemberNameID = " + keyMemNameId.ToString();

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
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
            }

            return connections;
        }

        public static List<MemberPosition> GetPositions(int keyMemAccountId)
        {
            List<MemberPosition> positions = new List<MemberPosition>();

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                string query = @"select USERSHADOW.USER_NAME,
	                    RSUDepend.dbo.CP_Member.*,
	                    ISSUER_NAME,
	                    DIRECTOR_STATUS_DESCRIPTION" //,
//                        ACCOUNT_REFERENCE,
//                        isnull(NAME_SURNAME,'') + ', ' + isnull(NAME_FORENAMES,'') as MemberName

                   + @" from
	                    RSUDepend.dbo.CP_Member
	                    left join ISSUER on ISSUER_ID = RSUDepend.dbo.CP_Member.MEMBER_ISSUER
						left join CONFIG_DIRECTOR_STATUS on RSUDepend.dbo.CP_Member.MEMBER_DIRECTOR	= DIRECTOR_STATUS_CODE
		                left join USERSHADOW on USERSHADOW.USER_ID = RSUDepend.dbo.CP_Member.EnteredByUserID
	                    left join DEPOUSER on USERSHADOW.USER_ID = DEPOUSER.DEPOUSER_USER
	                    left join DEPOPART on DEPOPART.DEPOPART_ID = DEPOUSER.DEPOUSER_PART_ID "
                        //left join ACCOUNT on ACCOUNT.ACCOUNT_ID = RSUDepend.dbo.CP_Member.MEMBER_ACCOUNT
                        //left join NAME on NAME.NAME_ACCOUNT = ACCOUNT.ACCOUNT_ID and NAME_SEQUENCE = 1
                        +@"where   MEMBER_ACCOUNT = " + keyMemAccountId.ToString();

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
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
            }

            return positions;
        }

        public static List<MemberPosition> GetPositions(int keyMemAccountId, int issuerId)
        {
            List<MemberPosition> positions = new List<MemberPosition>();

            DateTime resignOn = new DateTime(9998, 12, 31);

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                string query = @"select USERSHADOW.USER_NAME,
	                    RSUDepend.dbo.CP_Member.*,
	                    ISSUER_NAME,
	                    DIRECTOR_STATUS_DESCRIPTION
                        
                    from
	                    RSUDepend.dbo.CP_Member
	                    left join ISSUER on ISSUER_ID = RSUDepend.dbo.CP_Member.MEMBER_ISSUER
						left join CONFIG_DIRECTOR_STATUS on RSUDepend.dbo.CP_Member.MEMBER_DIRECTOR	= DIRECTOR_STATUS_CODE
		                left join USERSHADOW on USERSHADOW.USER_ID = RSUDepend.dbo.CP_Member.EnteredByUserID
	                    left join DEPOUSER on USERSHADOW.USER_ID = DEPOUSER.DEPOUSER_USER
	                    left join DEPOPART on DEPOPART.DEPOPART_ID = DEPOUSER.DEPOUSER_PART_ID
	                    where MEMBER_ACCOUNT = " + keyMemAccountId.ToString() +
                        "and ISSUER_ID = " + issuerId +
                        "and cast(MEMBER_RESIGNED as date) = '" + resignOn +"'";

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
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
                        KeyMemberReportType= reader["MEMBER_REPORT_TYPE"].ToString(),
                        EnteredBy = reader["USER_NAME"].ToString(),
                        EnteredAt = (DateTime)reader["EnteredAt"]

                    };

                    positions.Add(memPos);
                }
            }

            return positions;
        }

        public static List<MemberPosition> GetPositions(int keyMemAccountId, int issuerId, string memberPosition)
        {
            List<MemberPosition> positions = new List<MemberPosition>();

            DateTime resignOn = new DateTime(9998, 12, 31);

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                string query = @"select USERSHADOW.USER_NAME,
	                    RSUDepend.dbo.CP_Member.*,
	                    ISSUER_NAME,
	                    DIRECTOR_STATUS_DESCRIPTION
                        
                    from
	                    RSUDepend.dbo.CP_Member
	                    left join ISSUER on ISSUER_ID = RSUDepend.dbo.CP_Member.MEMBER_ISSUER
						left join CONFIG_DIRECTOR_STATUS on RSUDepend.dbo.CP_Member.MEMBER_DIRECTOR	= DIRECTOR_STATUS_CODE
		                left join USERSHADOW on USERSHADOW.USER_ID = RSUDepend.dbo.CP_Member.EnteredByUserID
	                    left join DEPOUSER on USERSHADOW.USER_ID = DEPOUSER.DEPOUSER_USER
	                    left join DEPOPART on DEPOPART.DEPOPART_ID = DEPOUSER.DEPOUSER_PART_ID
	                    where MEMBER_ACCOUNT = " + keyMemAccountId.ToString() +
                        "and ISSUER_ID = " + issuerId + 
                        "and MEMBER_DIRECTOR = '" + memberPosition + "'" +
                        "and cast(MEMBER_RESIGNED as date) = '" + resignOn + "'";

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
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
            }

            return positions;
        }

        public static string[] Getuser(string username, string participantCode)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                string query = @"select USERSHADOW.*
                        from
	                        USERSHADOW
	                        left join DEPOUSER on USERSHADOW.USER_ID = DEPOUSER.DEPOUSER_USER
	                        left join DEPOPART on DEPOPART.DEPOPART_ID = DEPOUSER.DEPOUSER_PART_ID
                        where
	                        USER_LOGIN_NAME = @username
                        and DEPOPART.DEPOPART_CODE = @participantCode";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@participantCode", participantCode);

                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new string[] { reader["USER_ID"].ToString(), reader["USER_NAME"].ToString() };
            }

            return new string[0];
        }

        public static List<InstrumentNote> GetInstrumentNotes()
        {
            List<InstrumentNote> instruments = new List<InstrumentNote>();

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = @"select ISIN_CODE, ISIN_SHORT_NAME, Note
                    from ISIN left join RSUDepend.dbo.CP_SymbolNotes on SymbolIsin = ISIN_CODE
	                order by ISIN_SHORT_NAME";

                SqlCommand cmd = new SqlCommand(query, conn);
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
            }

            return instruments;
        }

        public static void SaveInstrumentNote(string isin, string note)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string cmdStr = @"IF EXISTS (SELECT * FROM RSUDepend.dbo.CP_SymbolNotes WHERE SymbolIsin = @SymbolIsin)
                        UPDATE RSUDepend.dbo.CP_SymbolNotes SET Note = @Note where SymbolIsin = @SymbolIsin
                    ELSE
                        INSERT INTO RSUDepend.dbo.CP_SymbolNotes values (@SymbolIsin, @Note)";

                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@SymbolIsin", isin);
                cmd.Parameters.AddWithValue("@Note", note);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
