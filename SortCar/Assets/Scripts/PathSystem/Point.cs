using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PathSystem
{
    public class Point : MonoBehaviour
    {
        public bool isAvailable = true;
        public bool isLinkPoint;

        public UnityEvent onCarHere;
        public Point previousPoint;
        public Point[] nextPoints;

        public Point GetAvailableNextPoint()
        {
            if (nextPoints != null && nextPoints.Length > 0)
            {
                for (int i = 0; i < nextPoints.Length; i++)
                {
                    if (nextPoints[i].isAvailable)
                    {
                        if (nextPoints[i].isLinkPoint)
                        {
                            if (nextPoints[i].GetAvailableNextPoint() != null)
                            {
                                return nextPoints[i];
                            }
                        }
                        else
                        {
                            return nextPoints[i];
                        }
                    }
                }
            }
            return null;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.2F);
            if (nextPoints != null)
            {
                for (int i = 0; i < nextPoints.Length; i++)
                {
                    if (nextPoints[i].isAvailable)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(transform.position, nextPoints[i].transform.position);
                    }
                }
            }
        }
    }
}
