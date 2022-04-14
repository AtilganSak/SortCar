using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathSystem
{
    public class CarPathManager : MonoBehaviour
    {
        public Point[] points;

        private void OnValidate()
        {
            // Get point components quickly
            points = null;
            points = transform.GetComponentsInChildren<Point>();
        }
    }
}
