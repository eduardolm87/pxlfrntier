using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{

    void Update()
    {
        if (GameManager.Instance.Player == null)
            return;

        transform.position = new Vector3(GameManager.Instance.Player.transform.position.x, GameManager.Instance.Player.transform.position.y, transform.position.z);
    }
}
