using UnityEngine;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;

namespace PathSystem
{
    public class PathFollower : MonoBehaviour
    {
        [SerializeField] float duration;
        [SerializeField] float startDuration;
        [SerializeField] Ease animation;

        [SerializeField] Point startPoint;
        [SerializeField] PathStartPoint pathStartPoint;

        public void GoStart()
        {
            transform.DOMove(startPoint.transform.position, startDuration).SetEase(animation).OnComplete(() => startPoint.onCarHere.Invoke());
        }
        public void Move()
        {
            pathStartPoint.UpdatePath();
            pathStartPoint.rightPath[pathStartPoint.positions.Length - 1].isAvailable = false;
            transform.DOPath(pathStartPoint.positions, duration, PathType.CatmullRom,PathMode.Full3D).SetEase(animation).SetLookAt(0).OnComplete(
                () => pathStartPoint.rightPath[pathStartPoint.rightPath.Count - 1].onCarHere.Invoke());
        }
    }
}
