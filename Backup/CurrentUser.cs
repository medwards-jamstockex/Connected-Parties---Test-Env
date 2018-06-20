using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectedParties
{
    public class CurrentUser
    {
        private static CurrentUser instance = null;
        private CurrentUser() { }

        public int UserID { get; set; }
        public string LoginName { get; set; }
        public string Name { get; set; }
        public string SessionID { get; set; }
        public WorkingEnvironment Environment { get; set; }

        public static CurrentUser Instance
        {
            get
            {
                if (instance == null)
                    instance = new CurrentUser();

                return instance;
            }
        }
    }

    public enum WorkingEnvironment { RSU , Production  }
}
