using System;

namespace MIAAPI.Utilities
{
    public class DbTable
    {
        public string Name { get; set; }        
        public string[] Columns { get; set; }
        public string[] Keys { get; set; }

        public string Select => $"SELECT * FROM {Name}";
        public string SelectSingle => $"SELECT * FROM {Name} {Keys.GetWhere()}";
        public string Insert => $"INSERT INTO {Name} ({Columns.GetColumnNames()}) VALUES({Columns.GetInsertParams()})";
        public string Update => $"UPDATE {Name} SET {Columns.GetUpdateParams()} {Keys.GetWhere()}";
        public string Delete => $"DELETE FROM {Name} {Keys.GetWhere()}";
    }
}
