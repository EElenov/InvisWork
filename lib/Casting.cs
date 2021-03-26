using System;

namespace InvisWork.Tools
{
    public static class Casting
    {
        /// <summary>
        /// Casting Method - <see cref="{T}"/> is suggested <see cref="Type"/> and it outs a result regardless of the outcome. 
        /// If unsuccessful result is defaulted to the <see cref="object"/> original <see cref="Type"/>
        /// </summary>
        /// <param name="objType"></param>
        /// <returns><see cref="bool"/> <c>true/false</c></returns>
        public static bool TryCast<T>(this object obj, out T result)
        {
            if (obj is T t)//pattern matching
            {
                result = t;
                return true;
            }
            result = default;
            return false;
        }
    }
}


