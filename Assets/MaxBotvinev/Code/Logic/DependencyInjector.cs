using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic
{
    static class DependencyInjector
    {
        #region VARIABLES

        static Dictionary<Type, MonoBehaviour> instances = new Dictionary<Type, MonoBehaviour>();

        #endregion

        #region INTERFACE

        public static void Add<T>(T component) where T : MonoBehaviour
        {
            instances.Add(typeof(T), component);
        }

        public static T Get<T>() where T : MonoBehaviour
        {
            return (T)instances[typeof(T)];
        }

        public static void Reload()
        {
            instances.Clear();
        }

        #endregion
    }
}