using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Greetings", menuName = "ScriptableObjects/Text/Greetings", order = 0)] 
public class Greetings : ScriptableObject
{
    [Serializable] public struct Dialogue
    {
        public string text;
    }

    public Dialogue[] dialogues;


}
