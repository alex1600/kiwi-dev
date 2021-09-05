using System.Collections;
using System.Text;

namespace Nostrum.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Calls <see cref="object.ToString"/> on each element of the list and concatenates them while separating them using commas.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToCSV(this IList list)
        {
            var sb = new StringBuilder();
            foreach (var val in list)
            {
                sb.Append(val);
                if (list.IndexOf(val) < list.Count - 1) sb.Append(',');
            }
            return sb.ToString();
        }
    }
}