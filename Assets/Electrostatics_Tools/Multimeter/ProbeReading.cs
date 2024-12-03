//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ProbeReading : MonoBehaviour
//{

//    private float ChargeValue = 0.0f;

//    void OnCollisionStay(Collision collision)
//    {
//        try
//        {
//            Debug.Log("Probed objID: " + collision.gameObject.GetInstanceID());
//            ChargeValue = collision.gameObject.GetComponent<ChargedObject>().GetCharge();
//            Debug.Log("Charge read: " + ChargeValue);
//        }
//        catch(NullReferenceException e)
//        {
//            Debug.Log(collision.gameObject.GetInstanceID() + " is not a ChargedObject");
//            ChargeValue = 0.0f;
//        }
//    }

//    private void OnCollisionExit(Collision collision)
//    {
//        ChargeValue = 0.0f;
//    }

//    public float GetReading()
//    {
//        return ChargeValue;
//    }

//}
