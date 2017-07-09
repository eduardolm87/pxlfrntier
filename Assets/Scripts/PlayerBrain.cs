using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : Brain
{
    public override void Tick()
    {
        ReadPlayerInput();
    }


    void ReadPlayerInput()
    {
        ReadMouse();
    }


    void ReadMouse()
    {
        if (Input.GetMouseButton(0))
        {
            var MousePositionInWorld = Input.mousePosition;
            MousePositionInWorld.z = 0;
            MousePositionInWorld = Camera.main.ScreenToWorldPoint(MousePositionInWorld);

            Vector3 myPos = transform.position;
            myPos.z = 0;

            Vector3 MovementDir = (MousePositionInWorld - myPos).normalized;
            MovementDir *= character.stats.Speed;

            MoveInDirection(MovementDir);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Stop();
        }

    }

}
