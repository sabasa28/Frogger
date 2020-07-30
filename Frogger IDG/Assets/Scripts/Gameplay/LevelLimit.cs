using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLimit : MonoBehaviour
{
    public float XToTransferTo; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Car")|| collision.CompareTag("Log"))
        {
            Transform colTrans = collision.GetComponent<Transform>();
            colTrans.transform.position = new Vector3(XToTransferTo, colTrans.transform.position.y, colTrans.transform.position.z);
        }
    }
}
