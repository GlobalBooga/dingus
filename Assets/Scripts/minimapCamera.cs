using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapCamera : MonoBehaviour
{
    private void Update()
    {
        Transform player = StaticStuff.player.transform;

        Vector3 position = player.position;
        position.y = transform.position.y;
        transform.position = position;

        Quaternion rotation = Quaternion.Euler(90, player.rotation.eulerAngles.y, 0);
        transform.rotation = rotation;
    }

}
