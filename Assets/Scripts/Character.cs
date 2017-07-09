using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour
{
    public SpriteRenderer body;
    public SpriteRenderer equipment;
    public CircleCollider2D collider;
    public Rigidbody2D rigidbody;

    public CharacterStats stats = new CharacterStats();


    [HideInInspector]
    public Brain brain;


    public bool IsPlayer = false;


    public void ApplyCharacterData(CharacterData data)
    {
        body.sprite = data.Sprite;
        name = data.name;
        stats = data.Stats;
    }

    protected virtual void Update()
    {
        if (brain != null)
            brain.Tick();
    }


}
