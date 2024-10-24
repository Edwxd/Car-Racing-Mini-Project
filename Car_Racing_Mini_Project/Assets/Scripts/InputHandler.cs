using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    CarConstroller carConstroller;

    private void Awake()
    {
        carConstroller = GetComponent<CarConstroller>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;


        inputVector.x = Input.GetAxis("Horizontal");

        inputVector.y= Input.GetAxis("Vertical");

        carConstroller.SetInputVector(inputVector);
    }
}
