using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    public static class OnurExtensionMethods
    {
        public static void SafeInvoke(this Action source)
        {
            if (source != null) source.Invoke();
        }

        public static void SafeInvoke<T>(this Action<T> source, T value)
        {
            if (source != null) source.Invoke(value);
        }
        
        public static void SafeInvoke<T1, T2>(this Action<T1, T2> source, T1 firstValue, T2 secondValue)
        {
            if (source != null) source.Invoke(firstValue, secondValue);
        }
        
        public static void SafeInvoke<T1, T2, T3>(this Action<T1, T2, T3> source, T1 firstValue, T2 secondValue, T3 thirdValue)
        {
            if (source != null) source.Invoke(firstValue, secondValue, thirdValue);
        }

        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable  
        {  
            return listToClone.Select(item => (T)item.Clone()).ToList();  
        }

        public static T GetRandomElementFromList<T>(this List<T> list)
        {
            int random = Random.Range(0, list.Count);
            return list[random];
        }
        
        public static T GetRandomElementFromList<T>(this List<T> list, T exclude)
        {
            int random = Random.Range(0, list.Count);
            while (Equals(list[random], exclude))
            {
                random = Random.Range(0, list.Count);
            }

            return list[random];
        }

        public static void ChangeScaleY(this Transform thisTransform,float change)
        {
            var firstScale = thisTransform.transform.localScale;
            firstScale.y = change;
            thisTransform.transform.localScale = firstScale;
        }
        
        public static List<Transform> GetAllChilds(this Transform thisTransform)
        {
            return thisTransform.Cast<Transform>().ToList();
        }
        
        public static void ChangePositionWithChild(this Transform thisTransform, string childname)
        {
            var childs = thisTransform.GetAllChilds();
            var changedChild = childs.FirstOrDefault(x => x.name == childname);
            if(changedChild == null)
                return;
            
            childs.ForEach(x=>x.SetParent(null));
            var tempPos = changedChild.position;
            changedChild.position = thisTransform.position;
            thisTransform.position = tempPos;
            
            childs.ForEach(x=> x.SetParent(thisTransform));
        }
    }
}
