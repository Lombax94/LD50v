using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour {
    public int height = 0;
    public Vector3 bottomleft = Vector3.zero;
    public Vector3 topright = Vector3.zero;
    public bool create = false;
    public Tasks.GoToNode myType = Tasks.GoToNode.Walk;

    [SerializeField]
    public List<NodeSizes> myNodes = new List<NodeSizes>();

    private void Start() {
        
        if(myNodes[0].bot.x == 135.5f) {
           GameObject.Destroy(GameObject.Find("Ammo"));
        }

    }


    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Alpha0)) {
            height = 0;
        }
        if (Input.GetKey(KeyCode.Alpha1)) {
            height = -1;
        }
        if (Input.GetKey(KeyCode.Alpha2)) {
            height = -2;
        }
        if (Input.GetKey(KeyCode.Alpha3)) {
            height = -3;
        }
        if (Input.GetKey(KeyCode.Alpha4)) {
            height = -4;
        }
        if (Input.GetKey(KeyCode.Alpha5)) {
            height = -5;
        }
        if (Input.GetKey(KeyCode.Alpha6)) {
            height = -6;
        }
        if (Input.GetKey(KeyCode.Alpha7)) {
            height = -7;
        }
        if (Input.GetKey(KeyCode.Alpha8)) {
            height = -8;
        }
        if (Input.GetKey(KeyCode.Alpha9)) {
            height = -9;
        }

        if (Input.GetKey(KeyCode.W)) {
            myType = Tasks.GoToNode.Walk;
        }
        if (Input.GetKey(KeyCode.T)) {
            myType = Tasks.GoToNode.Teleport;
        }


        if (Input.GetKey(KeyCode.Mouse0)) {
         bottomleft = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log(Mathf.FloorToInt(bottomleft.x));
            Debug.Log((Mathf.FloorToInt((bottomleft.x % 1) / nodesize)));

            bottomleft.x = Mathf.FloorToInt(bottomleft.x) + (Mathf.FloorToInt((bottomleft.x % 1) / nodesize) * nodesize);
            bottomleft.y = Mathf.FloorToInt(bottomleft.y) + (Mathf.FloorToInt((bottomleft.y % 1) / nodesize) * nodesize);
        }
        if (Input.GetKey(KeyCode.Mouse1)) {
            topright = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            topright.x = Mathf.FloorToInt(topright.x) + (Mathf.CeilToInt((topright.x % 1) / nodesize) * nodesize);
            topright.y = Mathf.FloorToInt(topright.y) + (Mathf.CeilToInt((topright.y % 1) / nodesize) * nodesize);
        }


        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            create = true;
            myNodes.Add(new NodeSizes(bottomleft, topright, height, myType));

        }















    }
    public float nodesize = 0.25f;
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        for(int x = 0; x < Mathf.RoundToInt((topright.x - bottomleft.x) / nodesize); x++) {
            for (int y = 0; y < Mathf.RoundToInt((topright.y - bottomleft.y) / nodesize); y++) {
                Gizmos.DrawCube(new Vector3((nodesize / 2) + bottomleft.x + (x * nodesize), (nodesize / 2) + bottomleft.y + (y * nodesize), -10),Vector3.one * 0.15f );
            }
        }



    }


}

[System.Serializable]
public class NodeSizes {
    public Vector3 bot;
    public Vector3 top;
    public int height;
    public Tasks.GoToNode DestinationType;
    public GameObject MyTeleport;
    public GameObject DestTeleport;

    public NodeSizes(Vector3 b, Vector3 t, int h, Tasks.GoToNode tnode) {
        bot = b;
        top = t;
        height = h;
        DestinationType = tnode;

    }

}