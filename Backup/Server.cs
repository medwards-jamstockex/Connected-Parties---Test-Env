using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.IO;
using System.Drawing;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Globalization;

namespace ConnectedParties
{
    class Server
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        /// <summary>         
        /// Create Middleware Object         
        /// Note that RDServer is a Service reference and is defined in the programming environment (like Visual Studio)         
        /// and points to the URL for the Depend middle layer          
        /// </summary>  
        /// 

        public static string SessionID = String.Empty;

        public static string userFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ConnectedParties";

        public static string logfile = userFilePath + "\\logfile.txt";

        //[DefaultMember("RDService")]
        public static string environ;// = "RDService";

        //public static string uploadDir;

        public Server()
        {
            environ = "RSU";
            //return environ;

        }

        public static ConnectedParties.RDService.RDServiceClient mid;// = new ConnectedParties.RDService.RDServiceClient(environ);//("RDService");
        /// <summary>         
        /// Return structure         
        /// </summary>  
        /// 
        public static ConnectedParties.RDService.ReturnInfo rt = new ConnectedParties.RDService.ReturnInfo();
        /// <summary>         
        /// Session ID        
        /// </summary> 
        /// 
        public Server(String environ)
        {
            mid = new ConnectedParties.RDService.RDServiceClient(environ);
        }

        /// <summary>     
        /// Getting the server version info       
        /// </summary> 
        /// 
        private static void GetServerVersionInfo()
        {
            try
            {
                // delete log file once it passes 200 KB
                if (File.Exists(Server.logfile))
                {
                    FileInfo f2 = new FileInfo(Server.logfile);

                    if (f2.Length > 204800)
                    {
                        File.Delete(Server.logfile);
                    }
                }

                LogDetails.writeLog(Server.logfile, String.Format("Server {0}", mid.Version(1))); //Server version                 
                LogDetails.writeLog(Server.logfile, String.Format("{0}", mid.Version(2))); //DataSource   
                LogDetails.writeLog(Server.logfile, String.Format("Product: {0}", mid.Version(3))); //Product Depend / Regard      
                LogDetails.writeLog(Server.logfile, String.Format("Compilation key(s): {0}", mid.Version(4))); //Compilation keys        
            }
            catch (Exception ex)
            {
                LogDetails.writeLog(Server.logfile, String.Format("Version exception:\r\n{0}", ex.Message));
            }
        }

        /// <summary>    
        /// Login. Create the connection and get session     
        /// </summary>    
        public static string Login(string UserName, string Password, string Participant)
        {
            string sessionID = String.Empty;
            //Console.WriteLine();
            try
            {
                GetServerVersionInfo();


                /* Call actual login. password is sent in MD5 */
                rt = mid.DependLogin(out sessionID, UserName, Utils.MD5(Password), Assembly.GetExecutingAssembly().GetName().Name + " Ver: " + Assembly.GetExecutingAssembly().GetName().Version.ToString(), Participant);
                Server.SessionID = sessionID;
                if (rt.HasError) // lets see what server thinks about that    
                    LogDetails.writeLog(Server.logfile, String.Format("Login error: {0}\r\n{1}", rt.ErrorInfo.ErrorReference, rt.ErrorInfo.ErrorText));

                else if (rt.IDInfo[6].IDType == "USER_WARN_REMAIN") //warning is returned if password needs to be changed
                {

                    MessageBox.Show("Please change the Password for user" + UserName);
                    using (ChangePassword changePassword = new ChangePassword())
                    {

                        //Server.RetrieveBrowserInformation();

                        //IntPtr handle = mainForm.Handle;
                        changePassword.Activate();

                        changePassword.ShowDialog();
                        changePassword.Dispose();
                    }

                }
                else
                    LogDetails.writeLog(Server.logfile, String.Format("Login successful.\r\nSessionID:{0}", sessionID));
            }
            catch (Exception ex) //catch unexpected stuff that is not able to set "rt" (like network failure)     
            {
                LogDetails.writeLog(Server.logfile, String.Format("Login exception:\r\n{0}", ex.Message));
            }
            finally
            {
                //always close once done.    
                //if (mid != null)
                //    mid.Close();
            }
            return sessionID;
        }

        public static void Logout()
        {
            //Console.WriteLine();
            try
            {
                rt = mid.Logout(SessionID); // call logout      
                if (rt.HasError) // lets see what server thinks about that // lets see what server thinks about that     
                    LogDetails.writeLog(Server.logfile, String.Format("Logout error: {0}\r\n{1}", rt.ErrorInfo.ErrorReference, rt.ErrorInfo.ErrorText));
                else
                {
                    SessionID = null; // termination accepted - no point to keep old value   
                    LogDetails.writeLog(Server.logfile, String.Format("Logout successful."));
                }
            }
            catch (Exception ex) //catch unexpected stuff that is not able to set "rt" (like network failure)    
            {
                LogDetails.writeLog(Server.logfile, String.Format("Logout exception:\r\n{0}", ex.Message));
            }
            finally
            {
                //always close once done.     
                if (mid != null)
                    mid.Close();
            }
        }

        public static void isLoggedIn()
        {
            byte[] bs;
            string schema;
            string sessionID = Server.SessionID.ToString();

            rt = mid.DataSetListZIP(out schema, out bs, sessionID, "P_CHANGE_PASSWORD.1", 1, null);

            //if ((Server.rt.ErrorInfo.ErrorReference == "ML001047") ||
            //    (Server.rt.ErrorInfo.ErrorReference == "ML001048"))
            while ((Server.rt.ErrorInfo.ErrorReference == "ML001047") ||
            (Server.rt.ErrorInfo.ErrorReference == "ML001048") ||
            (Server.rt.ErrorInfo.ErrorReference == "ML001051"))
            {
                //MessageBox.Show("Please change the Password for user" + UserName);
                using (ReLogin renewSession = new ReLogin())
                {

                    //Server.RetrieveBrowserInformation();

                    //IntPtr handle = mainForm.Handle;
                    renewSession.Activate();

                    renewSession.ShowDialog();
                    renewSession.Dispose();
                }

            }

        }


        //private static bool ChangePass(string uniqueReference, string oldpass, string newpass, string sessionID)
        public static bool ChangePass(string uniqueReference, string oldpass, string newpass, string sessionID)
        {
            //string sessionID = sessionID;

            byte[] bs;
            DataRow password;
            DataSet passwordDS = new DataSet("Password");
            LogDetails.writeLog(Server.logfile, "\r\nEnter Password: ");
            string schema;
            try
            {
                /* get schema */
                rt = mid.DataSetListZIP(out schema, out bs, sessionID, "P_CHANGE_PASSWORD.1", 0, null);
                if (rt.HasError) // lets see what server thinks about that                 
                {
                    LogDetails.writeLog(Server.logfile, String.Format("Password schema retrieve error: {0}\r\n{1}", rt.ErrorInfo.ErrorReference, rt.ErrorInfo.ErrorText));
                    return false;
                }
                else
                {
                    passwordDS.ReadXmlSchema(new StringReader(schema));
                    LogDetails.writeLog(Server.logfile, String.Format("Password schema retrieved."));
                }
            }
            catch (Exception ex) //catch unexpected stuff that is not able to set "rt" (like network failure)             
            {
                LogDetails.writeLog(Server.logfile, String.Format("Account Status list exception:\r\n{0}", ex.Message));
                return false;
            }
            finally
            {
                //always close once done.             
                //if (mid != null)
                //    mid.Close();
            }
            password = passwordDS.Tables["P_CHANGE_PASSWORD"].NewRow();
            password["USER_OLD_PASSWORD"] = Utils.MD5(oldpass);// "P@ssw0rd";
            password["USER_PASSWORD"] = Utils.MD5(newpass);  // "dr0wss@P1";

            passwordDS.Tables["P_CHANGE_PASSWORD"].Rows.Add(password);
            // insert password             
            int changedRows = 0;
            int auditID = 0;
            /* check if there is changed rows */
            if (passwordDS.Tables[0].GetChanges() != null)
                changedRows = passwordDS.Tables[0].GetChanges().Rows.Count;
            if (changedRows > 0)
                LogDetails.writeLog(Server.logfile, String.Format("\r\nPosting {0} changed row(s) of password back to server... ", changedRows));
            else
            {
                LogDetails.writeLog(Server.logfile, "\r\nNothing to write to server");
                return false;
            }
            try
            {
                /* call actual update with user reference GenerateUniqueReference(). 
                server reference will be in auditID. Send changes only to reduce the load and optimize performance */
                rt = mid.DataSetUpdate(ref auditID, sessionID, "P_CHANGE_PASSWORD.1", 0, Utils.DataSetToXMLStr(passwordDS), uniqueReference);
                if (rt.HasError) // lets see what server thinks about that    
                {
                    LogDetails.writeLog(Server.logfile, String.Format("Password post error: {0}\r\n{1} (audit ref:{2})", rt.ErrorInfo.ErrorReference, rt.ErrorInfo.ErrorText, uniqueReference));
                    return false;
                }
                else
                {
                    LogDetails.writeLog(Server.logfile, String.Format("Password posted with auditID: {0} (audit ref:{1})", auditID, uniqueReference));

                }
            }
            catch (Exception ex)  //catch unexpected stuff that is not able to set "rt" (like network failure) 
            {
                LogDetails.writeLog(Server.logfile, String.Format("Password table edit exception:\r\n{0}\r\n(audit ref:{1})", ex.Message, uniqueReference));
                return false;
            }
            finally
            {
                //always close once done.   
                //if (mid != null)
                //    mid.Close();
            }
            return true;
        }

        /// <summary>        
        /// Retrieving browser information (use LIST_HOLDER as an example)    
        /// </summary>      
        public static DataTable getTable(string operation, string param, int noRecords, string version)
        {
            byte[] bs;
            string schema;
            string function;
            string parameter;
            DataTable configTable = new DataTable();

            //function = "LIST_BATCH.1";
            //parameter = "BATCHCONTROL_ID = " + batchID;
            //function = operation + version;
            function = operation;
            parameter = param;

            //File.AppendAllText(Server.logfile,"\r\nBrowsing: ");
            try
            {
                isLoggedIn();
                /* call operation and fill dataset. output stream is ziped! */
                Server.rt = Server.mid.DataSetListZIP(out schema, out bs, Server.SessionID, function, noRecords, parameter);
                //Server.rt = Server.mid.DataSetListZIP(out schema, out bs, Server.SessionID, function, -1, "BATCHCONTROL_ID = 31");
                if (Server.rt.HasError) // lets see what server thinks about that        
                    LogDetails.writeLog(Server.logfile, String.Format("Browser error: {0}\r\n{1}", Server.rt.ErrorInfo.ErrorReference, Server.rt.ErrorInfo.ErrorText));
                else
                {
                    /* unzip output from server */
                    DataSet browserDS = Utils.unZipDS(bs, schema);
                    /* print out all tables in dataset */

                    configTable = browserDS.Tables[operation];
                }
            }
            catch (Exception ex) //catch unexpected stuff that is not able to set "rt" (like network failure)     
            {
                LogDetails.writeLog(Server.logfile, String.Format("Account Status list exception:\r\n{0}", ex.Message));
            }
            finally
            {
                //always close once done.     
                //if (mid != null)
                //    mid.Close();
            }
            return configTable;
        }

        /// <summary>        
        /// Retrieving browser information (use LIST_HOLDER as an example)    
        /// </summary>      
        public static DataSet getDataSet(string operation, string param, string version)
        {
            byte[] bs;
            string schema;
            string function;
            string parameter;
            DataSet dependDS = new DataSet();

            //function = "LIST_BATCH.1";
            //parameter = "BATCHCONTROL_ID = " + batchID;
            function = operation + version;
            parameter = param;

            //File.AppendAllText(Server.logfile,"\r\nBrowsing: ");
            try
            {
                isLoggedIn();
                /* call operation and fill dataset. output stream is ziped! */
                Server.rt = Server.mid.DataSetListZIP(out schema, out bs, Server.SessionID, function, -1, parameter);
                //Server.rt = Server.mid.DataSetListZIP(out schema, out bs, Server.SessionID, function, -1, "BATCHCONTROL_ID = 31");
                if (Server.rt.HasError) // lets see what server thinks about that        
                    LogDetails.writeLog(Server.logfile, String.Format("Browser error: {0}\r\n{1}", Server.rt.ErrorInfo.ErrorReference, Server.rt.ErrorInfo.ErrorText));
                else
                {
                    /* unzip output from server */
                    DataSet browserDS = Utils.unZipDS(bs, schema);
                    /* print out all tables in dataset */

                    dependDS = browserDS;
                }
            }
            catch (Exception ex) //catch unexpected stuff that is not able to set "rt" (like network failure)     
            {
                LogDetails.writeLog(Server.logfile, String.Format("Account Status list exception:\r\n{0}", ex.Message));
            }
            finally
            {
                //always close once done.     
                //if (mid != null)
                //    mid.Close();
            }
            return dependDS;
        }
    }
}
