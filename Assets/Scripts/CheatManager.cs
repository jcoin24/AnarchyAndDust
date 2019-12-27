using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {

    private bool cheats = true;

    public bool get()
    {
        return cheats;
    }

    public void setfalse()
    {
        cheats = false;
    }

    public void setTrue()
    {
        cheats = true;
    }

}
