using System;
using System.Collections.Generic;
using System.Text;
using Accord.IO;
using Microsoft.Office.Interop.Outlook;
using Fodler.Models;
using Newtonsoft.Json;

namespace Fodler.Helpers
{
    public static class StorageHelpers
    {
        private static readonly Folder DefaultFolder = Globals.ThisAddIn.Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox) as Folder;

        #region Conversion methods

        /// <summary>
        /// Converts bytes array to string
        /// </summary>
        private static string ConvertFromBytes(byte[] bytes)
        {
            var sBuilder = new StringBuilder();
            for (var index = 0; index < bytes.Length; index++)
            {
                if (index != 0)
                {
                    sBuilder.Append(";");
                }
                sBuilder.Append(bytes[index].ToString());
            }

            return sBuilder.ToString();
        }

        /// <summary>
        /// Converts string to bytes array
        /// </summary>
        private static byte[] ConvertToBytes(string input)
        {
            var splited = input.Split(';');
            var resultArray = new byte[splited.Length];
            for (var i = 0; i < splited.Length; i++)
            {
                resultArray[i] = byte.Parse(splited[i]);
            }

            return resultArray;
        }

        #endregion

        #region Storage methods

        /// <summary>
        /// Return UserProperty(OutlookItem) for given storage and property name
        /// </summary>
        /// <param name="storageName">Storage name where is property saved</param>
        /// <param name="propertyName">Property name</param>
        private static UserProperty GetProperty(string storageName, string propertyName)
        {
            return GetStorage(storageName).UserProperties.Find(propertyName);
        }

        /// <summary>
        /// Return StorageItem(OutlookItem) for given storage name
        /// </summary>
        /// <param name="storageName">Storage name</param>
        private static StorageItem GetStorage(string storageName)
        {
            return DefaultFolder.GetStorage(storageName, OlStorageIdentifierType.olIdentifyBySubject);
        }


        public static void RemoveStores()
        {
            RemoveStorage(Constants.DefaultStorageName);
        }

        public static void RemoveStorage(string storage)
        {
            GetStorage(storage).Delete();
        }

        #endregion

        #region Save and Load

        /// <summary>
        /// Using Accord extension method Save(out byte[]) converts item to byte[] and then saves it to given storage
        /// </summary>
        /// <param name="model">Accord model to be saved</param>
        /// <param name="storageName">Storage name will be item saved</param>
        /// <param name="propertyName">Property name</param>
        public static void SaveItem<T>(T model, string propertyName, string storageName)
        {
            if (model == null) return;
            model.Save(out byte[] outModel);
            var convertedBytes = ConvertFromBytes(outModel);
            var storage = GetStorage(storageName);
            if (GetProperty(storageName, propertyName) == null)
            {
                storage.UserProperties.Add(propertyName, OlUserPropertyType.olText);
            }

            storage.UserProperties.Find(propertyName).Value = convertedBytes;
            storage.Save();
        }

        /// <summary>
        /// Loads item T from given storage
        /// </summary>
        /// <param name="storageName">Storage name where is property saved</param>
        /// <param name="propertyName">Property name</param>
        public static T LoadItem<T>(string propertyName, string storageName)
        {
            var myProperty = GetProperty(storageName, propertyName);
            object m = null;
            if (myProperty != null && !string.IsNullOrEmpty(myProperty.Value))
            {
                Serializer.Load(ConvertToBytes(myProperty.Value), out m);
            }
            return (T)Convert.ChangeType(m, typeof(T));
        }

        /// <summary>
        /// Serializes with JsonConvert and saves item T. 
        /// </summary>
        /// <param name="propertyName">Property name</param>
        public static void SerializeAndSave<T>(T item, string propertyName)
        {
            string serialized = JsonConvert.SerializeObject(item);
            SaveItem(serialized, propertyName, Constants.DefaultStorageName);
        }

        /// <summary>
        /// Loads and deserialize item T with JsonConvert. 
        /// </summary>
        /// <param name="propertyName">Property name</param>
        public static T LoadAndDeserialize<T>(string propertyName)
        {
            var loaded = LoadItem<string>(propertyName, Constants.DefaultStorageName);
            if (loaded == null)
            {
                if (typeof(T) == typeof(string))
                {
                    return (T)Convert.ChangeType(string.Empty, typeof(T));
                }

                if (typeof(T) == typeof(int))
                {
                    return (T)Convert.ChangeType(0, typeof(T));
                }

                if (typeof(T) == typeof(bool))
                {
                    return (T)Convert.ChangeType(false, typeof(T));
                }

                return (T)Convert.ChangeType(null, typeof(T));
            }
            var deserialized = JsonConvert.DeserializeObject<T>(loaded);
            return (T)Convert.ChangeType(deserialized, typeof(T));
        }

        #endregion
    }
}
