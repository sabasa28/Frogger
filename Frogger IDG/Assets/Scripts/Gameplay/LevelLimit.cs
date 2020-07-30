using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLimit : MonoBehaviour
{
    public float XToTransferTo; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Car"))
        {
            Car car = collision.GetComponent<Car>();
            car.transform.position = new Vector3(XToTransferTo, car.transform.position.y, car.transform.position.z);
        }
    }
}
