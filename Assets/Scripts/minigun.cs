using UnityEngine;
using System.Collections;

public class minigun : MonoBehaviour {

    public Transform bulletPF;

    public void fireBullet()
    {
        Rigidbody shot = (Rigidbody)Instantiate(bulletPF.gameObject.GetComponent<Rigidbody>(), transform.position, Quaternion.identity);
        Quaternion q = transform.rotation;
        Vector3 forward = q * Vector3.forward;
        shot.AddForce( forward * 20000f, ForceMode.Force);
        
    }
}
