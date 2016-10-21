using System;
using System.Collections.Generic;
using System.Reflection;

namespace Templater20
{
    public class ViewBag:Dictionary<String, object>
    {

        public static object GetValue(string varName, ViewBag data)
        {
            if (!varName.Contains("."))
                return data[varName];

            var parts = varName.Split('.');

            var vName = parts[0];
            var fieldName = parts[1];

            var obj = data[vName];

            FieldInfo[] fields = obj.GetType().GetFields();
            foreach (var field in fields)
            {
                if (field.Name == fieldName)
                    return field.GetValue(obj).ToString();
            }

            throw new Exception("no value found");
        }
    }
}
