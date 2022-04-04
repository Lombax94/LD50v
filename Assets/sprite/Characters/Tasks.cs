using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks : MonoBehaviour {

    public enum tasktypes {Turret1Ammo, Turret1Clean, Turret1Energy, Turret1Steer, Turret2Ammo, Turret2Clean, Turret2Energy, Turret2Steer, CleanTowels, LookAtStrategy, LogCheck, FixDoor, Idle, Battery, FirstAid, PowerSoup, HealthPot, Ammo}
    public enum GoToNode {Walk, Teleport }

    public InteractAbleObject[] TheObjects = new InteractAbleObject[18];

    Queue<tasktypes> QueueTasts;

    public void AddTask(tasktypes task) {
        QueueTasts.Enqueue(task);

    }
  

    public tasktypes GetTask() {
        return QueueTasts.Dequeue();

    }

}



public class InteractAbleObject {
    public Nodes OnNode;
    public Vector3 position;

}

public class Nodes {
    public Vector3 bot;
    public Vector3 top;
    public int height;
    public Tasks.GoToNode myTravelType;
    public List<NodeConnectionMidpoint> Neighbours;
    public List<TeleportConnected> MyPointOfInterest;
    public float gcost;
    public float hcost;
    public float fcost;
    public bool Checked;
    public Nodes Parent;
    public Vector3 MidpointPM;
    public Vector3 MidpointTeleport;
    public Nodes Next;

}

public class NodeConnectionMidpoint {
    public Vector3 Midpoint;
    public Nodes Neighbour;

    public NodeConnectionMidpoint(Vector3 v, Nodes n) {
        Midpoint = v;
        Neighbour = n;

    }

}
