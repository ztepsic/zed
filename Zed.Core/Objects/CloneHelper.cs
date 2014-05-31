using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Zed.Core.Objects {
    /// <summary>
    /// Helper class that allows various methods of copying/cloning the object
    /// </summary>
    public static class CloneHelper {

        /// <summary>
        /// Creates a deep copy of the object
        /// </summary>
        /// <see href="http://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-an-object-in-net-c-specifically"/>
        /// <typeparam name="T">Object must be decorated with [Seriliziable] attribute.</typeparam>
        /// <param name="source">the object that is being copied</param>
        /// <returns>new copy (instance) of an object</returns>
        public static T DeepClone<T>(this T source) where T : class {
            using (var memoryStream = new MemoryStream()) {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, source);
                memoryStream.Position = 0;

                return (T)formatter.Deserialize(memoryStream);
            }
        }


        /// <summary>
        /// Creates a collection of deep copy elements from provided collection.
        /// Change over the elements from returned collection does not affect the elements from original collection 
        /// because of deep copy.
        /// </summary>
        /// <typeparam name="T">Object in collection must be decorated with [Seriliziable] attribute.</typeparam>
        /// <param name="collection">collection for which is need to make deep copy of its elements</param>
        /// <returns>a collection of deep copy elements from provided collection</returns>
        public static IEnumerable<T> ToArrayWithDeepClone<T>(this IEnumerable<T> collection) where T : class {
            var count = collection.Count();
            T[] array = new T[count];

            int i = 0;
            foreach (var element in collection) {
                array[i++] = element.DeepClone();
            }

            return array;
        }
    }
}
