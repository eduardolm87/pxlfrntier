using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{

    public CharacterStats Stats = new CharacterStats();

    public Sprite Sprite;

    [TextArea]
    public string Description = "";

}
