using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayerPosition 
{
    private static DeadPlayerPosition deadPlayer= new DeadPlayerPosition();

    public static DeadPlayerPosition deadPlayerPosition => deadPlayer;

    public List<GameObject> deadPeople = new List<GameObject>();
}
