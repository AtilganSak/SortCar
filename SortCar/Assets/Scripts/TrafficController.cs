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
}
