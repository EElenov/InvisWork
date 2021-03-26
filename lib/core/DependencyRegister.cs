using System;
using System.Collections.Generic;
using System.Windows;

namespace InvisWork.Core
{
    public enum DependencyRegistryKey
    {
        Visibility,
        Separator,
        Command,
        ItemSelect,
        Text,
        Content
    }
    /// <summary>
    /// Sharded class which accommodates for all custom properties collections private definitions and their public accessors.
    /// <para>For simplicity's sake <b>ONLY PREDEFINED DICTIONARY ENTRIES CAN BE MODIFIED DURING RUNTIME!</b></para>
    /// </summary>
    public static partial class DependacyRegistry
    {
        /// <summary>
        /// Accessor method for public properties of the <see cref="DependacyRegistry"/> class, defined as 
        /// <see cref="Dictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="Name">Name of the property</param>
        /// <param name="Key">Key represented as a Tuple collection of <see cref="enum"/> and an <see cref="int"/> indexer.(Refer to shard definition which contains the particular private variable of the property)</param>
        /// <returns><see cref="DependencyProperty"/></returns>
        public static DependencyProperty Get(string Name, Tuple<DependencyRegistryKey, int> Key)
        {
            //safecasting the found property
            //because its a static class GetValue doesnt need an object instance
            var prop = typeof(DependacyRegistry).GetProperty(Name).GetValue(null) as Dictionary<Tuple<DependencyRegistryKey, int>, DependencyProperty>;
            if (prop.ContainsKey(Key)) return prop[Key];
            else return null;
        }
        /// <summary>
        /// Returns a list of all properties for the <see cref="DependacyRegistry"/> class. Represented as a <see cref="string"/> with new line as delimiter.
        /// </summary>
        /// <returns><see cref="string"/></returns>
        public static string PropertiesList()
        {
            string ret = string.Empty;
            foreach (var p in typeof(DependacyRegistry).GetProperties()) ret += $"{p.Name}\n";
            return ret.TrimEnd('\r', '\n');
        }

        /// <summary>
        /// Collection of extruded properties for the Backstage UserControl manipulation.
        /// </summary>
        //public static Dictionary<Tuple<DependencyRegistryKey, int>, DependencyProperty> ComponentBackstagePropertiesCollection
        //{ 
        //    private get => _ComponentBackstagePropertiesCollection;
        //    set => _ComponentBackstagePropertiesCollection=value;
        //}
    }
}


