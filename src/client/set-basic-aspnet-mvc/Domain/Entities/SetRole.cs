using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace set_basic_aspnet_mvc.Domain.Entities
{
    public class SetRole
    {
        public static readonly SetRole Admin = new SetRole(1);
        public static readonly SetRole User = new SetRole(2);
        public static readonly SetRole Developer = new SetRole(3);

        public readonly int Value;

        public SetRole(int v)
        {
            Value = v;
        }

        public override string ToString()
        {
            return GetString(Value);
        }

        public static Dictionary<int, string> GetItemList()
        {
            var list = new Dictionary<int, string> { { 1, "Admin" }, { 2, "User" }, { 3, "Developer" } };
            return list;
        }

        public static bool IsValid(int id)
        {
            return id > 0 && id <= GetItemList().Count;
        }

        public static string GetString(int id)
        {
            var list = GetItemList();

            if (!list.ContainsKey(id))
            {
                throw new Exception(string.Format("Unknown SetRoleId > {0}", id));
            }

            return list[id];
        }
    }
}