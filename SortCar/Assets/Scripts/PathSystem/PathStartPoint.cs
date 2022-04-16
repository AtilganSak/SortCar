using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PathSystem
{
    public class PathStartPoint : MonoBehaviour
    {
        public Point startPoint;
        public List<Point> rightPath = new List<Point>();
        public Vector3[] positions;

        private void Start()
        {
            UpdatePath();
        }
        public void UpdatePath()
        {
            rightPath.Clear();
            rightPath.Add(startPoint);
            if (startPoint.isAvailable)
            {
                FindNextPoints(startPoint);
            }
            if (rightPath != null)
            {
                positions = new Vector3[rightPath.Count];
                for (int i = 0; i < rightPath.Count; i++)
                {
                    positions[i] = rightPath[i].transform.position;
                }
            }
        }
        private void FindNextPoints(Point point)
        {
            Point nextPoint = point.GetAvailableNextPoint();
            if (nextPoint == null)
            {
                return;
            }
            else
            {
                rightPath.Add(nextPoint);
                FindNextPoints(nextPoint);
            }
        }
    }
}
