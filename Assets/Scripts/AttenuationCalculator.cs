using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

#region Namespaces
using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using Autodesk.Revit.DB;
using System.Linq;
using UnityEngine.Reflect;
#endregion


namespace RvtFader
{
    public class AttenuationCalculator
    {
        Document _doc;
        Settings _settings;

#if DEBUG_GRAPHICAL
    /// <summary>
    /// Draw model lines for graphical geometrical debugging.
    /// </summary>
    SketchPlane _sketch;
#endif // DEBUG_GRAPHICAL

        public AttenuationCalculator(
          Document doc,
          Settings settings) {
            _doc = doc;
            _settings = settings;
        }

        /// <summary>
        /// Return the number of walls encountered 
        /// between the two given points.
        /// </summary>
        int GetWallCount(Vector3 psource, Vector3 ptarget) {
            float d = Vector3.Distance(ptarget, psource);

            // cast a ray
            RaycastHit[] hits;
            hits = Physics.RaycastAll(psource, (ptarget-psource).normalized, d);
            List<GameObject> wallIds = new List<GameObject>();

            foreach (RaycastHit hit in
              hits) {
                //string _name = hit.collider.gameObject.name;
                //if (_name.Contains("Wall") || _name.Contains("Floor")) {
                //    if (!wallIds.Contains(hit.collider.gameObject)) {
                //        wallIds.Add(hit.collider.gameObject);
                //    }
                //}
                Metadata metadata;
                hit.collider.gameObject.TryGetComponent<Metadata>(out metadata);
                if (metadata) {
                    if (metadata.GetParameter("Category") == "Walls" || metadata.GetParameter("Category") == "Floors") {
                        Debug.DrawLine(psource, ptarget, Color.green);
                        if (!wallIds.Contains(hit.collider.gameObject)) {
                            wallIds.Add(hit.collider.gameObject);
                        }
                    }
                }
            }
            //Debug.Log(
            //  string.Format("{0} -> {1}: {2} walls",
            //    Util.PointString(psource),
            //    Util.PointString(ptarget),
            //    wallIds.Count));
            //Debug.DrawLine(psource, ptarget, Color.green);

            return wallIds.Count;
        }

        /// <summary>
        /// Calculate the signal attenuation between the 
        /// given source and target points using ray tracing.
        /// Walls and distance through air cause losses.
        /// </summary>
        public double Attenuation(Vector3 psource, Vector3 ptarget) {
#if DEBUG_GRAPHICAL
      Debug.Print( string.Format( "{0} -> {1}",
        Util.PointString( psource ),
        Util.PointString( ptarget ) ) );

      if( null == _sketch || 0.0001
        < _sketch.GetPlane().Origin.Z - psource.Z )
      {
        Plane plane = Plane.CreateByNormalAndOrigin(
          Vector3.BasisZ, psource );

        _sketch = SketchPlane.Create( _doc, plane );

      }
      Line line = Line.CreateBound( psource, ptarget );

      _sketch.Document.Create.NewModelCurve( line, _sketch );
#endif // DEBUG_GRAPHICAL

            float d = Vector3.Distance(ptarget, psource);

            float a = 20 + 10 * Mathf.Log(d, 10) * (float)_settings.AttenuationAirPerMetreInDb;

            int wallCount = GetWallCount(psource, ptarget);

            a += wallCount * (float)_settings.AttenuationWallInDb;

            return a;
        }
    }
}