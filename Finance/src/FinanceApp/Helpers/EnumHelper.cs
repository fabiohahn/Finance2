﻿// using System.ComponentModel;
// 
// namespace App.Helpers
// {
//     static class EnumHelper
//     {
//         public static string DescriptionAttr<T>(this T source)
//         {
//             var fi = source.GetType().GetField(source.ToString());
// 
//             var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
//                 typeof(DescriptionAttribute), false);
// 
//             return attributes.Length > 0 ? attributes[0].Description : source.ToString();
//         }
//     }
// }
