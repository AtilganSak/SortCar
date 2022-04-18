using PathSystem;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrafficController : MonoBehaviour
{
    public Team team;

    public PathFollower[] cars { get; private set; }
    private int currentIndex;

    private void OnEnable()
    {
        cars = GetComponentsInChildren<PathFollower>();

        SetTeams();
        MixCars();

        ActionManager.Instance.AddListener(Actions.GameStart, () => NextCar());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            NextCar();
        }
    }
    public void GO()
    {
        cars[currentIndex].Move();
        currentIndex++;
    }
    public void NextCar()
    {
        if (currentIndex < cars.Length)
        {
            cars[currentIndex].GoStart();
        }
    }
    private void SetTeams()
    {
        if (cars != null)
        {
            foreach (PathFollower item in cars)
            {
                item.SetTeam(team);
            }
        }
    }
    private void MixCars()
    {
        if (ReferenceKeeper.Instance.LevelSettings.mixOnStart)
        {
            PathFollower[] pathFollowers = new PathFollower[cars.Length];
            cars.CopyTo(pathFollowers, 0);
            for (int i = 0; i < cars.Length; i++)
            {
                int rnd = Random.Range(0, cars.Length);
                Vector3 pos = cars[i].transform.localPosition;
                cars[i].transform.localPosition = cars[rnd].transform.localPosition;
                cars[rnd].transform.localPosition = pos;
                PathFollower cache = cars[i];
                cars[i] = cars[rnd];
                cars[rnd] = cache;
            }
            Vector3 doorPosition = team == Team.Team1 ? ReferenceKeeper.Instance.purpleDoor.transform.position : ReferenceKeeper.Instance.yellowDoor.transform.position;
            cars = cars.OrderBy(x => Vector3.Distance(x.transform.position, doorPosition)).ToArray();
        }
    }
}
