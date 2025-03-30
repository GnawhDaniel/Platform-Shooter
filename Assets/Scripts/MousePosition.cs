using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MousePosition : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 relativeMousePosition;

    public float angle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 7.5f));
        Vector3 objectWorldPosition = targetObject.transform.position;
        relativeMousePosition = mouseWorldPosition - objectWorldPosition;

        Vector3 distance = relativeMousePosition - targetObject.transform.position;

        angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;




    }
}
