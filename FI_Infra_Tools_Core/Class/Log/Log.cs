using System;

namespace FI_Infra_Tools_Core
{
    public class Log
    {

        public Guid LogID { get; set; }
        public String Content { get; set; }

        public String NameAPI { get; set; }

        public DateTime DateCreated { get; set; }

        public String NameMethod { get; set; }

        public String IPSource { get; set; }

        public String DataSource { get; set; }

        public TypeLog TypeLog { get; set; }


    }

    public enum TypeLog
    {
        ERROR = 0,
        WARNING = 1,
        INFORMATION = 2
    }
}
