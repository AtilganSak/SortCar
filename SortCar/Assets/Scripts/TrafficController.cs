using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathSystem;
using UnityEngine.Events;

public class TrafficController : MonoBehaviour
{
    private PathFollower[] cars;

    public int currentIndex;

    private void OnEnable()
    {
        cars = GetComponentsInChildren<PathFollower>();
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
}
