using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;


namespace ConnectedParties
{
    static class Utils
    {
        internal static string GenerateRandomPassword()
        {
            // password pattern:
            // 1) at least 8 characters
            // 2) must contain a number
            // 3) must contain a special character (this method uses these chars: @#$%^&+=)
            // 4) must have at least 1 upper case letter
            // 5) must have at least 1 lower case letter
            Regex reg = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^_+=]).*$");
            int passwordLength = 8;
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789@#$%^_+=";
            char[] chars = new char[passwordLength];
            Random rd = new Random();
            string password = "";

            while (!reg.IsMatch(password))
            {
                for (int i = 0; i < passwordLength; i++)
                {
                    chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                    password = new string(chars);
                }
            }

            return password;
        }

        internal static string GenerateUniqueReference()
        {
            string str = Guid.NewGuid().ToString();
            str = str.Substring(str.Length - 20, 20);
            return str;
        }
        internal static DataSet unZipDS(byte[] bdata, string schema)
        {
            DataSet _data = new DataSet();
            _data.ReadXmlSchema(new StringReader(schema));
            if (bdata != null)
            {
                MemoryStream ms2 = new MemoryStream(bdata);
                GZipStream zipStream = new GZipStream(ms2, CompressionMode.Decompress);
                _data.ReadXml(zipStream, XmlReadMode.Auto);
                _data.AcceptChanges();
                zipStream.Close();
                zipStream.Dispose();
                ms2.Close();
                ms2.Dispose();
            }
            return _data;
        }

        # region Trying To Zip
        //internal static byte[] ZipDS(DataSet bdata, string schema)
        //{
        //    DataSet _data = new DataSet();
        //    _data.ReadXmlSchema(new StringReader(schema));
        //    if (bdata != null)
        //    {
        //        MemoryStream ms2 = new MemoryStream(bdata);
        //        GZipStream zipStream = new GZipStream(ms2, CompressionMode.Compress);
        //        _data.ReadXml(zipStream, XmlReadMode.Auto);
        //        _data.AcceptChanges();
        //        zipStream.Close();
        //        zipStream.Dispose();
        //        ms2.Close();
        //        ms2.Dispose();
        //    }
        //    return _data;
        //}

        # endregion


        internal static string MD5(string input)
        {
            input = input ?? String.Empty;
            byte[] data = new byte[input.Length];
            Encoding.Default.GetBytes(input, 0, input.Length, data, 0); // set byte form of input      
            MD5CryptoServiceProvider crypto = new MD5CryptoServiceProvider();
            byte[] result = crypto.ComputeHash(data); // compute MD5 value     
            return byteArrayToHexString(result);
        }
        private static string byteArrayToHexString(byte[] byteArray)
        {
            StringBuilder output = new StringBuilder(byteArray.Length);
            foreach (byte b in byteArray)
                output.AppendFormat("{0:x2}", b);
            return output.ToString();
        }
        public static string DataSetToXMLStr(DataSet dsSrc)
        {
            DataSet ds = dsSrc.Copy();
            foreach (DataTable t in ds.Tables)
            {
                //add IUD     
                if (!t.Columns.Contains("IUD"))
                    t.Columns.Add("IUD", System.Type.GetType("System.String"));
                //remove not null   
                foreach (DataColumn c in t.Columns)
                    if (c.AllowDBNull == false)
                        c.AllowDBNull = true;
            }
            foreach (DataTable t in ds.Tables)
            {
                foreach (DataRow r in t.Rows)
                {
                    if (r.RowState == DataRowState.Unchanged)
                        continue;
                    switch (r.RowState)
                    {
                        case DataRowState.Added:
                            r["IUD"] = "I";
                            break;
                        case DataRowState.Modified:
                            r["IUD"] = "U";
                            break;
                        case DataRowState.Deleted:
                            r.RejectChanges();
                            r["IUD"] = "D";
                            break;
                        default:
                            break;
                    }
                }
            }
            //update parent records   
            foreach (DataTable t in ds.Tables)
            {
                if (t.ParentRelations.Count == 0)
                    continue;
                foreach (DataRelation rel in t.ParentRelations)
                {
                    foreach (DataColumn c in rel.ChildColumns)
                        if (!c.ExtendedProperties.ContainsKey("KEY"))
                            c.ExtendedProperties.Add("KEY", "Y");
                    foreach (DataColumn c in rel.ParentColumns)
                        if (!c.ExtendedProperties.ContainsKey("KEY"))
                            c.ExtendedProperties.Add("KEY", "Y");
                }
                foreach (DataRow r in t.Rows)
                {
                    if (r.RowState == DataRowState.Unchanged)
                        continue;
                    foreach (DataRelation rel in t.ParentRelations)
                    {
                        DataRow pr = r.GetParentRow(rel);
                        if (pr.RowState == DataRowState.Unchanged && pr["IUD"] == DBNull.Value)
                            pr["IUD"] = "N";
                    }
                }
            }
            foreach (DataTable t in ds.Tables)
            {
                foreach (DataRow r in t.Rows)
                {
                    if (r.RowState == DataRowState.Deleted)
                        continue;
                    if (r["IUD"] == DBNull.Value)
                    {
                        r.Delete();
                        continue;
                    }
                    if ((String)r["IUD"] == "D" || (String)r["IUD"] == "N")
                    {
                        foreach (DataColumn c in t.Columns)
                        {
                            if (c.ColumnName == "IUD")
                                continue;
                            if (c.ReadOnly)
                                c.ReadOnly = false;
                            if (!c.ExtendedProperties.ContainsKey("KEY"))
                                r[c] = DBNull.Value;
                        }
                        continue;
                    }
                }
            }
            //clear all readonly     
            foreach (DataTable t in ds.Tables)
            {
                foreach (DataColumn c in t.Columns)
                {
                    if (c.ReadOnly == false)
                        continue;
                    if (c.ExtendedProperties.ContainsKey("KEY"))
                        continue;
                    c.ReadOnly = false;
                    foreach (DataRow r in t.Rows)
                        if (r.RowState != DataRowState.Deleted)
                            r[c] = DBNull.Value;
                }
            }
            ds.AcceptChanges();
            return ds.GetXml();
        }
        public static bool AreAllCellsEmpty(this DataRow row)
        {
            var itemArray = row.ItemArray;
            if (itemArray == null)
                return true;
            return itemArray.All(x => string.IsNullOrEmpty(x.ToString().Trim()));
        }
        public static string ToCSV(DataTable table)
        {
            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    //switch(row[i].GetType().ToString())
                    //{
                    //    case "System.String":
                    //        continue;
                    //    case "System.Integer":
                    //        continue;
                    //}

                    //result.Append(row[i].ToString());
                    //result.Append(i == table.Columns.Count - 1 ? "\n" : ",");

                    //result.Append((row[i].ToString().IndexOfAny(new char[] { '"', ',' }) != -1) ? row[i].ToString().Replace("\"", "\"\"") : row[i].ToString());
                    //result.Append(i == table.Columns.Count - 1 ? "\n" : ",");

                    result.AppendFormat("\"{0}\"", (row[i].ToString().IndexOfAny(new char[] { '"', ',' }) != -1) ? row[i].ToString().Replace("\"", "\"\"") : row[i].ToString());
                    result.Append(i == table.Columns.Count - 1 ? "\n" : ",");

                }
            }

            return result.ToString();
        }
    }
}
