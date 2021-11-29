//using Autodesk.Revit.DB;
using UnityEngine;
namespace RvtFader
{
    class Util
    {
        #region Unit Handling
        const double _inchToMm = 25.4;
        const double _footToMm = 12 * _inchToMm;
        const double _footToMetre = _footToMm * 0.001;

        /// <summary>
        /// Convert a given length in feet to metres.
        /// </summary>
        public static double FootToMetre(double length)
        {
            return length * _footToMetre;
        }
        #endregion // Unit Handling

        #region Formatting
        /// <summary>
        /// Return a string for a real number
        /// formatted to two decimal places.
        /// </summary>
        public static string RealString(double a)
        {
            return a.ToString("0.##");
        }

        /// <summary>
        /// Return a string for a Vector2 point
        /// or vector with its coordinates
        /// formatted to two decimal places.
        /// </summary>
        public static string PointString(Vector2 p)
        {
            return string.Format("({0},{1})",
              RealString(p.x),
              RealString(p.y));
        }

        /// <summary>
        /// Return a string for an Vector3 point
        /// or vector with its coordinates
        /// formatted to two decimal places.
        /// </summary>
        public static string PointString(Vector3 p)
        {
            return string.Format("({0},{1},{2})",
              RealString(p.x),
              RealString(p.y),
              RealString(p.z));
        }
        #endregion // Formatting

    }
}
