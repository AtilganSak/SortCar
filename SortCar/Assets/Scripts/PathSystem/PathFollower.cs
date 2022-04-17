using UnityEngine;
using DG.Tweening;

namespace PathSystem
{
    public class PathFollower : MonoBehaviour
    {
        [SerializeField] float duration;
        [SerializeField] float startDuration;
        [SerializeField] Ease animation;

        public Team team { get; private set; }

        [SerializeField] Point startPoint;
        [SerializeField] PathStartPoint pathStartPoint;

        public void GoStart()
        {
            transform.DOMove(startPoint.transform.position, startDuration).SetEase(animation).OnComplete(() => startPoint.CarHere(this));
        }
        public void Move()
        {
            pathStartPoint.UpdatePath();
            pathStartPoint.rightPath[pathStartPoint.positions.Length - 1].isAvailable = false;
            transform.DOPath(pathStartPoint.positions, duration, PathType.CatmullRom,PathMode.Full3D).SetEase(animation).SetLookAt(0).OnComplete(
                () => pathStartPoint.rightPath[pathStartPoint.rightPath.Count - 1].CarHere(this));
        }
        public void SetTeam(Team _team)
        {
            team = _team;
            MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
            if (meshRenderer)
            {
                meshRenderer.materials[0].color = Utility.GetColorByTeam(team);
            }
        }
        public void Done()
        {
            ReferenceKeeper.Instance.GameManager.CarIsDone();
        }
    }
}
