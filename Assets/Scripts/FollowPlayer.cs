using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public void FollowTheFuckingPlayer(GameObject player)
    {
        transform.position = player.transform.position + new Vector3(0,0,-5f);
    }
}
