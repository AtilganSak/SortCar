using UnityEngine;
using UnityEngine.Events;

namespace PathSystem
{
    public class Point : MonoBehaviour
    {
        public Team team;
        public bool isAvailable = true;
        public bool isLinkPoint;
        public DOScale tick;
        public UnityEvent onCarHere;
        public Point previousPoint;
        public Point[] nextPoints;

        private void OnEnable()
        {
            tick = GetComponentInChildren<DOScale>();
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = ReferenceKeeper.Instance.LevelSettings.GetColorByTeam(team);
            }
        }
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
        public void CarHere(PathFollower pathFollower)
        {
            onCarHere.Invoke();
            if (pathFollower.team == this.team)
            {
                tick.DO();
                pathFollower.Done();
            }
            else if(team != Team.None)
            {
                ReferenceKeeper.Instance.GameManager.GameOver();
            }
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
