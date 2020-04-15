using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    public static Player PLAYER;    

    public enum SpawnStates { CountingDown, Spawning}

    public enum PlantStates { Growing, DoneGrowing}

    public enum EnemyStates { Seek, Flee, Pursue, Evade, Wander, Hide}
}
