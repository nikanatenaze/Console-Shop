using Microsoft.EntityFrameworkCore;
using ShopProjectDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDatabase.Database
{
    internal static class BaseMethods
    {
        public static void LogAll<T>(this IEnumerable<T> list)
        {
            foreach (var i in list)
            {
                Console.WriteLine(i);
            }
        }
        public static void AddToBase<T>(this DbSet<T> Table, DataContext Context, T Data) where T : class
        {
            Table.Add(Data);
            Context.SaveChanges();
        }
        public static void AddToBase<T>(this DbSet<T> Table, DataContext Context, IEnumerable<T> Data) where T : class
        {
            Table.AddRange(Data);
            Context.SaveChanges();
        }
        public static T FindInBaseById<T>(this DbSet<T> Table, int Id) where T : class
        {
            var idProperty = typeof(T).GetProperty("Id");
            if(idProperty == null)
            {
                return null;
            }
            foreach(var i in Table) {
                var iId = idProperty.GetValue(i);
                if(iId is int x && x == Id)
                {
                    return i;
                }
            }
            return null;
        }
        public static T FindInBaseByProperty<T>(this DbSet<T> Table, string Property, string PropertyValue) where T : class
        {
            var idProperty = typeof(T).GetProperty(Property);
            if (idProperty == null)
            {
                return null;
            }
            
            foreach (var i in Table)
            {
                var iId = idProperty.GetValue(i);
                if (iId is string x && x == PropertyValue)
                {
                    return i;
                }
            }
            return null;
        }
        public static T FindInBaseByProperty<T>(this DbSet<T> Table, string Property, int PropertyValue) where T : class
        {
            var idProperty = typeof(T).GetProperty(Property);
            if (idProperty == null)
            {
                return null;
            }

            foreach (var i in Table)
            {
                var iId = idProperty.GetValue(i);
                if (iId is int x && x == PropertyValue)
                {
                    return i;
                }
            }
            return null;
        }
        public static bool RemoveFromBaseById<T>(this DbSet<T> Table, DataContext data,  int Id) where T : class
        {
            var obj = FindInBaseById(Table, Id);
            if(obj == null)
            {
                return false;
            } 
            data.Remove(obj);
            data.SaveChanges();
            return true;
        }
        public static bool RemoveFromBaseByProperty<T>(this DbSet<T> Table, DataContext data, string Property, string PropertyValue) where T : class
        {
            var ObjToDelete = FindInBaseByProperty(Table, Property, PropertyValue);
            if(ObjToDelete != null)
            {
                Table.Remove(ObjToDelete);
                data.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool RemoveFromBaseByProperty<T>(this DbSet<T> Table, DataContext data, string Property, int PropertyValue) where T : class
        {
            var ObjToDelete = FindInBaseByProperty(Table, Property, PropertyValue);
            if (ObjToDelete != null)
            {
                Table.Remove(ObjToDelete);
                data.SaveChanges();
                return true;
            }
            return false;
        }
        public static void RemoveByObject<T>(this DbSet<T> Table, DataContext data, T Object) where T : class
        {
            if(Object != null)
            {
                Table.Remove(Object);
                data.SaveChanges();
            }
        }
        public static void RemoveByObject<T>(this DbSet<T> Table, DataContext data, IEnumerable<T> Objects) where T : class
        {
            Table.RemoveRange(Objects.ToArray());
            data.SaveChanges();
        }
        public static T HashObjectProperty<T>(this T obj, string Property, bool PasswordHash = false) where T : class
        {
            T result = obj;
            Type typeOfObj = typeof(T);
            PropertyInfo prop = typeOfObj.GetProperty(Property);
            if(prop != null && prop.CanWrite)
            {
                if(!PasswordHash)
                {
                    prop.SetValue(result, BCrypt.Net.BCrypt.HashString(Property, Property.Length));
                }
                else
                {
                    prop.SetValue(result, BCrypt.Net.BCrypt.HashPassword(Property));
                }
            }
            return result;
        }
        public static T HashObjectProperties<T>(this T obj, IEnumerable<string> Properties) where T : class
        {
            var result = obj;
            foreach(var i in Properties)
            {
                result = HashObjectProperty(obj, i);
            }
            return result;
        }
    }
}
