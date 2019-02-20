using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float Max_X;
    public float Max_Y;

    // Update is called once per frame
    void Update()
    {
        bool is_correct_x = target.position.x > 0 && target.position.x <= Max_X;
        bool is_correct_y = target.position.y > 0 && target.position.y <= Max_Y;

        if (is_correct_x && is_correct_y)
            transform.position = target.position;
        else if (is_correct_x)
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        else if (is_correct_y)
            transform.position = new Vector3(transform.position.x, target.position.y, target.position.z);

    }
}
