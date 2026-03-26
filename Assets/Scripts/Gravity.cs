using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public static List<Gravity> otherObjectsList;
    private Rigidbody rb;
    const float G = 0.00667f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(otherObjectsList == null)
        {
            otherObjectsList = new List<Gravity>();
        }
        otherObjectsList.Add(this);
    }

    void FixedUpdate()
    {
        foreach (Gravity obj in otherObjectsList)
        {
            if (obj != this) //กันไม่ให้โดนแรงดึงดูดตัวเอง
            {
                Attract(obj);
            }
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position; //ทิศทางวัตถุ M -> n

        float distance = direction.magnitude; // ระยะห่าง r
        if (distance == 0f) return; //กันไม่ให้มีแรงดึงดูด เมื่อสองวัตถุอยู่ตำแหน่งเดียวกัน

        //F = G(m1 *m2) / r^2
        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(gravitationForce); //ใส่แรงดึงดูดพร้อมทิศทาง
    }
}
