﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.Cache;

namespace FS.Utils.Common
{
    /// <summary>
    ///     类型转换器
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        ///     将对象转换为T类型
        /// </summary>
        /// <param name="sourceValue">要转换的源对象</param>
        /// <param name="defValue">转换失败时，代替的默认值</param>
        /// <typeparam name="T">基本类型</typeparam>
        public static T ConvertType<T>(object sourceValue, T defValue = default(T))
        {
            if (sourceValue == null) { return defValue; }
            var returnType = typeof (T);
            var sourceType = sourceValue.GetType();
            // 相同类型，则直接返回原型
            if (returnType == sourceType || Type.GetTypeCode(returnType) == Type.GetTypeCode(sourceType)) { return (T) sourceValue; }

            var val = ConvertType(sourceValue, returnType, sourceType);
            return val != null ? (T) val : defValue;
        }

        /// <summary>
        ///     将值转换成类型对象的值
        /// </summary>
        /// <param name="sourceValue">要转换的值</param>
        /// <param name="returnType">要转换的对象的类型</param>
        public static object ConvertType(object sourceValue, Type returnType)
        {
            return ConvertType(sourceValue, returnType, sourceValue.GetType());
        }

        /// <summary>
        ///     将值转换成类型对象的值
        /// </summary>
        /// <param name="sourceValue">原值</param>
        /// <param name="returnType">要转换的对象的类型</param>
        /// <param name="sourceType">原值的类型</param>
        public static object ConvertType(object sourceValue, Type returnType, Type sourceType)
        {
            if (sourceValue == null) { return null; }
            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof (Nullable<>)) { returnType = returnType.GetGenericArguments()[0]; }

            if (sourceType == returnType) { return sourceValue; }

            // 对   Nullable<> 类型处理
            if (returnType.IsGenericType)
            {
                var returnGenericType = returnType.GetGenericTypeDefinition();
                var nullableType = typeof (Nullable<>);
                // 对   List 类型处理
                if (returnGenericType != nullableType)
                {
                    var sourceValueString = sourceValue.ToString();
                    // List参数类型
                    var returnArgumType = returnType.GetGenericArguments()[0];

                    switch (Type.GetTypeCode(returnArgumType))
                    {
                        case TypeCode.Boolean:
                        {
                            return ToList(sourceValueString, false);
                        }
                        case TypeCode.DateTime:
                        {
                            return ToList(sourceValueString, DateTime.MinValue);
                        }
                        case TypeCode.Decimal:
                        case TypeCode.Double:
                        case TypeCode.Single:
                        {
                            return ToList(sourceValueString, 0m);
                        }
                        case TypeCode.Byte:
                        case TypeCode.SByte:
                        case TypeCode.UInt16:
                        {
                            return ToList<ushort>(sourceValueString, 0);
                        }
                        case TypeCode.UInt32:
                        {
                            return ToList<uint>(sourceValueString, 0);
                        }
                        case TypeCode.UInt64:
                        {
                            return ToList<ulong>(sourceValueString, 0);
                        }
                        case TypeCode.Int16:
                        {
                            return ToList<short>(sourceValueString, 0);
                        }
                        case TypeCode.Int64:
                        {
                            return ToList<long>(sourceValueString, 0);
                        }
                        case TypeCode.Int32:
                        {
                            return ToList(sourceValueString, 0);
                        }
                        case TypeCode.Empty:
                        case TypeCode.Char:
                        case TypeCode.String:
                        {
                            return ToList(sourceValueString, "");
                        }
                    }
                }
            }

            var objString = sourceValue.ToString();
            // 枚举处理
            if (returnType.IsEnum)
            {
                if (string.IsNullOrWhiteSpace(objString)) { return null; }
                return IsType<int>(sourceValue) ? Enum.ToObject(returnType, int.Parse(objString)) : Enum.Parse(returnType, objString, true);
            }

            var returnTypeCode = Type.GetTypeCode(returnType);
            var sourceTypeCode = Type.GetTypeCode(sourceType);

            // bool转int
            if (sourceTypeCode == TypeCode.Boolean)
            {
                switch (returnTypeCode)
                {
                    case TypeCode.Byte:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.SByte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        return ConvertType(sourceValue, true) ? 1 : 0;
                }
            }
            switch (returnTypeCode)
            {
                case TypeCode.Boolean:
                {
                    return (object) (!string.IsNullOrWhiteSpace(objString) && (objString.Equals("on") || objString == "1" || objString.Equals("true")));
                }
                case TypeCode.Byte:
                {
                    Byte result;
                    if (Byte.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.Char:
                {
                    Char result;
                    if (Char.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.DateTime:
                {
                    DateTime result;
                    if (DateTime.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.Decimal:
                {
                    Decimal result;
                    if (Decimal.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.Double:
                {
                    Double result;
                    if (Double.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.Int16:
                {
                    Int16 result;
                    if (Int16.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.Int32:
                {
                    Int32 result;
                    if (Int32.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.Int64:
                {
                    Int64 result;
                    if (Int64.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.SByte:
                {
                    SByte result;
                    if (SByte.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.Single:
                {
                    Single result;
                    if (Single.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.UInt16:
                {
                    UInt16 result;
                    if (UInt16.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.UInt32:
                {
                    UInt32 result;
                    if (UInt32.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.UInt64:
                {
                    UInt64 result;
                    if (UInt64.TryParse(objString, out result)) { return result; }
                    return null;
                }
                case TypeCode.Empty:
                case TypeCode.String:
                {
                    return (object) objString;
                }
                case TypeCode.Object:
                {
                    break;
                }
            }

            try { return Convert.ChangeType(sourceValue, returnType); }
            catch {
                return null;
            }
        }

        /// <summary>
        ///     判断是否T类型
        /// </summary>
        /// <param name="sourceValue">要判断的对象</param>
        /// <typeparam name="T">基本类型</typeparam>
        public static bool IsType<T>(object sourceValue)
        {
            if (sourceValue == null) { return false; }
            var returnType = typeof (T);
            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof (Nullable<>)) { returnType = returnType.GetGenericArguments()[0]; }

            var sourceType = sourceValue.GetType();

            // 相同类型，则直接返回原型
            if (returnType == sourceType || Type.GetTypeCode(sourceType) == Type.GetTypeCode(returnType)) { return true; }

            // 判断是否为泛型
            if (returnType.IsGenericType) { return sourceValue is T; }
            if (returnType.IsEnum) { return sourceValue is Enum; }

            var objString = sourceValue.ToString();
            var returnTypeCode = Type.GetTypeCode(returnType);
            switch (returnTypeCode)
            {
                case TypeCode.Boolean:
                {
                    return !string.IsNullOrWhiteSpace(objString) && (objString.Equals("on") || objString == "1" || objString.Equals("true"));
                }
                case TypeCode.Byte:
                {
                    Byte result;
                    return Byte.TryParse(objString, out result);
                }
                case TypeCode.Char:
                {
                    Char result;
                    return Char.TryParse(objString, out result);
                }
                case TypeCode.DateTime:
                {
                    DateTime result;
                    return DateTime.TryParse(objString, out result);
                }
                case TypeCode.Decimal:
                {
                    Decimal result;
                    return Decimal.TryParse(objString, out result);
                }
                case TypeCode.Double:
                {
                    Double result;
                    return Double.TryParse(objString, out result);
                }
                case TypeCode.Int16:
                {
                    Int16 result;
                    return Int16.TryParse(objString, out result);
                }
                case TypeCode.Int32:
                {
                    Int32 result;
                    return Int32.TryParse(objString, out result);
                }
                case TypeCode.Int64:
                {
                    Int64 result;
                    return Int64.TryParse(objString, out result);
                }
                case TypeCode.SByte:
                {
                    SByte result;
                    return SByte.TryParse(objString, out result);
                }
                case TypeCode.Single:
                {
                    Single result;
                    return Single.TryParse(objString, out result);
                }
                case TypeCode.UInt16:
                {
                    UInt16 result;
                    return UInt16.TryParse(objString, out result);
                }
                case TypeCode.UInt32:
                {
                    UInt32 result;
                    return UInt32.TryParse(objString, out result);
                }
                case TypeCode.UInt64:
                {
                    UInt64 result;
                    return UInt64.TryParse(objString, out result);
                }
                case TypeCode.Empty:
                case TypeCode.String:
                {
                    return true;
                }
                case TypeCode.Object:
                {
                    break;
                }
            }
            return sourceType == returnType;
        }

        /// <summary>
        ///     将字符串转换成List型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="splitString">分隔符为NullOrEmpty时，则直接拆份为Char</param>
        /// <param name="defValue">默认值(单项转换失败时，默认值为NullOrEmpty时，则不添加，否则替换为默认值)</param>
        /// <typeparam name="T">基本类型</typeparam>
        public static List<T> ToList<T>(string str, T defValue, string splitString = ",")
        {
            var lst = new List<T>();
            if (string.IsNullOrWhiteSpace(str)) { return lst; }

            //判断是否带分隔符，如果没有。则直接拆份单个Char
            if (string.IsNullOrWhiteSpace(splitString)) { for (var i = 0; i < str.Length; i++) { lst.Add(ConvertType(str.Substring(i, 1), defValue)); } }
            else
            {
                var strArray = splitString.Length == 1 ? str.Split(splitString[0]) : str.Split(new string[1] {splitString}, StringSplitOptions.None);
                lst.AddRange(strArray.Select(item => ConvertType(item, defValue)));
            }
            return lst;
        }

        /// <summary>
        ///     查找对象属性值
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="entity">当前实体类</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="defValue">默认值</param>
        public static T GetValue<TEntity, T>(TEntity entity, string propertyName, T defValue = default(T)) where TEntity : class
        {
            if (entity == null) { return defValue; }
            foreach (var property in entity.GetType().GetProperties())
            {
                if (property.Name != propertyName) { continue; }
                if (!property.CanRead) { return defValue; }
                return ConvertType(PropertyGetCacheManger.Cache(property, entity), defValue);
            }
            return defValue;
        }

        /// <summary>
        ///     比较两者是否相等，不考虑大小写,两边空格
        /// </summary>
        /// <param name="str">对比一</param>
        /// <param name="str2">对比二</param>
        /// <returns></returns>
        public static bool IsEquals(string str, string str2)
        {
            if (str == str2) { return true; }
            if (str == null || str2 == null) { return false; }
            if (str.Trim().Length != str2.Trim().Length) { return false; }
            return String.Compare(str.Trim(), str2.Trim(), StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        ///     将List转换成字符串
        /// </summary>
        /// <param name="lst">要拼接的LIST</param>
        /// <param name="sign">分隔符</param>
        public static string ToString(IEnumerable lst, string sign = ",")
        {
            if (lst == null) { return String.Empty; }
            var sb = new StringBuilder();
            foreach (var item in lst) { sb.Append(item + sign); }
            return sb.Length > 0 ? sb.Remove(sb.Length - sign.Length, sign.Length).ToString() : String.Empty;
        }
    }
}