using UnityEngine;
using System.Collections;

public class minigun : MonoBehaviour {

    enum bullets {
        single,
        twin
    }
    public Transform bulletPF;
    public Transform doubleBulletPF;
    int totalSingle = 10;
    int totalTwin = 0;
    int max = 99;

    public void fireBullet(int bullet, int number)
    {
        bullets selection = (bullets)bullet;
        Rigidbody shot;
        Quaternion q;
        Vector3 forward;

        switch (selection)
        {
            case bullets.single:
                if (totalSingle <= 0) { break; }
                shot = (Rigidbody)Instantiate(bulletPF.gameObject.GetComponent<Rigidbody>(), transform.position, Quaternion.identity);
                q = transform.rotation;
                forward = q * Vector3.forward;
                shot.AddForce(forward * 20000f, ForceMode.Force);
                shot.gameObject.tag = "bullet" + number;
                totalSingle--;
                break;
            case bullets.twin:
                if (totalTwin <= 0) { break; }
                shot = (Rigidbody)Instantiate(doubleBulletPF.gameObject.GetComponent<Rigidbody>(), transform.position, Quaternion.identity);
                q = transform.rotation;
                forward = q * Vector3.forward;
                shot.AddForce(forward * 20000f, ForceMode.Force);
                shot.gameObject.tag = "bullet" + number;
                totalTwin--;
                break;

        }
        
        
    }
    public void addBullet(int bullet, int amount)
    {
        bullets selection = (bullets)bullet;
        switch (selection)
        {
            case bullets.single:
                if (totalSingle >= max) { break; }
                totalSingle += amount;
                if (totalSingle > max)
                {
                    totalSingle = max;
                }
                break;
            case bullets.twin:
                if (totalTwin >= max) { break; }
                totalTwin += amount;
                if (totalTwin > max)
                {
                    totalTwin = max;
                }
                break;
        }
    }

    public int getAmount(int bullet)
    {
        bullets selection = (bullets)bullet;
        switch (selection)
        {
            case bullets.single:
                return totalSingle;
              
            case bullets.twin:
                return totalTwin;
            default:
                return 0;  
        }
    }
}
