using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoodeMap : MonoBehaviour {

    public Transform bottomPoint;
    public Transform topPoint;

    public Vector3 WorldOffset = Vector3.zero;
    public Nodes[,] TheNodeMap;
    public List<Nodes> SuperNodes = new List<Nodes>();
    public Nodes nodesaver;
    public Nodes lastadded;


    public NodeGenerator ngen;

    void Start() {
        WorldOffset = bottomPoint.position;
        TheNodeMap = new Nodes[Mathf.RoundToInt((topPoint.position.x - bottomPoint.position.x) / 0.25f), Mathf.RoundToInt((topPoint.position.y - bottomPoint.position.y) / 0.25f)];
        ngen = GetComponent<NodeGenerator>();

        foreach (NodeSizes s in ngen.myNodes) {
            nodesaver = new Nodes();
            nodesaver.bot = s.bot;
            nodesaver.top = s.top;
            nodesaver.height = s.height;
            nodesaver.myTravelType = s.DestinationType;
            nodesaver.MyPointOfInterest = new List<TeleportConnected>();
            nodesaver.Neighbours = new List<NodeConnectionMidpoint>();
            SuperNodes.Add(nodesaver);

            for (int x = 0; x < Mathf.FloorToInt((s.top.x - s.bot.x) / 0.25f); x++) {
                for (int y = 0; y < Mathf.FloorToInt((s.top.y - s.bot.y) / 0.25f); y++) {
                    TheNodeMap[Mathf.FloorToInt((s.bot.x - WorldOffset.x) / 0.25f) + x, Mathf.FloorToInt((s.bot.y - WorldOffset.y) / 0.25f) + y] = nodesaver;
                }
            }

        }

        GameObject saver = GameObject.Find("AllInfoObjets");
        GameObject saver2;
        for (int i = 0; i < 4; i++) {
            saver2 = saver.transform.GetChild(i).gameObject;
            TheNodeMap[Mathf.FloorToInt((saver2.transform.position.x - WorldOffset.x) / 0.25f), Mathf.FloorToInt((saver2.transform.position.y - WorldOffset.y) / 0.25f)].MyPointOfInterest.Add(saver2.GetComponent<TeleportConnected>());

        }

        foreach (Nodes s in SuperNodes) {
            lastadded = null;
            for (int x = 0; x < Mathf.FloorToInt(s.top.x - s.bot.x) / 0.25f; x++) {
                nodesaver = TheNodeMap[Mathf.FloorToInt((s.bot.x - WorldOffset.x) / 0.25f) + x, Mathf.FloorToInt((s.bot.y - WorldOffset.y) / 0.25f) - 1];
                if (nodesaver != null && lastadded != nodesaver) {
                    lastadded = nodesaver;
                    s.Neighbours.Add(new NodeConnectionMidpoint(Vector3.zero, nodesaver));

                }

            }
            lastadded = null;
            for (int x = 0; x < Mathf.FloorToInt(s.top.x - s.bot.x) / 0.25f; x++) {
                nodesaver = TheNodeMap[Mathf.FloorToInt((s.bot.x - WorldOffset.x) / 0.25f) + x, Mathf.FloorToInt((s.top.y - WorldOffset.y) / 0.25f)];
                if (nodesaver != null && lastadded != nodesaver) {
                    lastadded = nodesaver;
                    s.Neighbours.Add(new NodeConnectionMidpoint(Vector3.zero, nodesaver));

                }

            }
            lastadded = null;
            for (int y = 0; y < Mathf.FloorToInt(s.top.y - s.bot.y) / 0.25f; y++) {
                nodesaver = TheNodeMap[Mathf.FloorToInt((s.bot.x - WorldOffset.x) / 0.25f) - 1, Mathf.FloorToInt((s.bot.y - WorldOffset.y) / 0.25f) + y];
                if (nodesaver != null && lastadded != nodesaver) {
                    lastadded = nodesaver;
                    s.Neighbours.Add(new NodeConnectionMidpoint(Vector3.zero, nodesaver));

                }

            }
            lastadded = null;
            for (int y = 0; y < Mathf.FloorToInt(s.top.y - s.bot.y) / 0.25f; y++) {
                nodesaver = TheNodeMap[Mathf.FloorToInt((s.top.x - WorldOffset.x) / 0.25f), Mathf.FloorToInt((s.bot.y - WorldOffset.y) / 0.25f) + y];
                if (nodesaver != null && lastadded != nodesaver) {
                    lastadded = nodesaver;
                    s.Neighbours.Add(new NodeConnectionMidpoint(Vector3.zero, nodesaver));

                }

            }

        }

        Vector3 target = GameObject.Find("Main Camera").transform.position;
        Vector3 CP1 = Vector3.zero;
        Vector3 CP2 = Vector3.zero;



        foreach (Nodes s in SuperNodes) {
            foreach (NodeConnectionMidpoint n in s.Neighbours) {

                if (n.Neighbour.top.y <= s.bot.y) {//Neighbour is below
                    if (n.Neighbour.bot.x <= s.bot.x) {
                        CP1.x = s.bot.x;

                    } else {
                        CP1.x = n.Neighbour.bot.x;

                    }

                    if (n.Neighbour.top.x >= s.top.x) {
                        CP2.x = s.top.x;

                    } else {
                        CP2.x = n.Neighbour.top.x;

                    }

                    n.Midpoint.x = (CP1.x + CP2.x) / 2f;
                    n.Midpoint.y = s.bot.y;
                }

                if (n.Neighbour.top.x <= s.bot.x) {//Neighbour is to the left

                    if (n.Neighbour.top.y >= s.top.y) {
                        CP1.y = s.top.y;

                    } else {
                        CP1.x = n.Neighbour.top.y;

                    }

                    if (n.Neighbour.bot.y <= s.bot.y) {
                        CP1.y = s.bot.y;

                    } else {
                        CP1.x = n.Neighbour.bot.y;

                    }

                    n.Midpoint.x = s.bot.x;
                    n.Midpoint.y = (CP1.y + CP2.y) / 2;
                }

                if (n.Neighbour.bot.y >= s.top.y) {//Neighbour is above

                    if (n.Neighbour.bot.x <= s.bot.x) {
                        CP1.x = s.bot.x;

                    } else {
                        CP1.x = n.Neighbour.bot.x;

                    }

                    if (n.Neighbour.top.x >= s.top.x) {
                        CP2.x = s.top.x;

                    } else {
                        CP2.x = n.Neighbour.top.x;

                    }
             
                    n.Midpoint.x = (CP1.x + CP2.x) / 2;
                    n.Midpoint.y = s.top.y;
                 }


                if (n.Neighbour.bot.x >= s.top.x) {//Neighbour is to the right

                    if (n.Neighbour.top.y >= s.top.y) {
                        CP1.y = s.top.y;

                    } else {
                        CP1.x = n.Neighbour.top.y;

                    }

                    if (n.Neighbour.bot.y <= s.bot.y) {
                        CP1.y = s.bot.y;

                    } else {
                        CP1.x = n.Neighbour.bot.y;

                    }

                    n.Midpoint.x = s.top.x;
                    n.Midpoint.y = (CP1.y + CP2.y) / 2;
                }

            }

        }

        //foreach (Nodes a in SuperNodes) {
        //    foreach (NodeConnectionMidpoint s in a.Neighbours) {
        //        Debug.Log(a.bot + " |" + s.Neighbour.bot + " | " + s.Midpoint);

        //    }
        //}



    }

    private void Update() {

        if (Input.GetKey(KeyCode.Mouse1)) {
            Debug.Log((Camera.main.ScreenToWorldPoint(Input.mousePosition) - WorldOffset) / 0.25f);
            Debug.Log(TheNodeMap[Mathf.FloorToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - WorldOffset.x) / 0.25f), Mathf.FloorToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition).y - WorldOffset.y) / 0.25f)].Neighbours.Count);

            if(TheNodeMap[Mathf.FloorToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - WorldOffset.x) / 0.25f), Mathf.FloorToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition).y - WorldOffset.y) / 0.25f)].Neighbours.Count == 2) {
                Debug.Log(TheNodeMap[Mathf.FloorToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - WorldOffset.x) / 0.25f), Mathf.FloorToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition).y - WorldOffset.y) / 0.25f)].Neighbours[0].Neighbour.bot);
                Debug.Log(TheNodeMap[Mathf.FloorToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - WorldOffset.x) / 0.25f), Mathf.FloorToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition).y - WorldOffset.y) / 0.25f)].Neighbours[1].Neighbour.bot);
            }


        }

        }

    public bool UseGizmo = false;
    private void OnDrawGizmos() {

        if (UseGizmo == true) {

            for (int i = 0; i < TheNodeMap.GetLength(0); i++) {
                for (int j = 0; j < TheNodeMap.GetLength(1); j++) {
                    if (TheNodeMap[i, j] != null) {
                        Gizmos.color = Color.green;

                    } else {
                        Gizmos.color = Color.black;

                    }


                    Gizmos.DrawCube(new Vector3(WorldOffset.x + i * 0.25f, WorldOffset.y + j * 0.25f, 0) + Vector3.one * 0.125f, Vector3.one * 0.10f);

                }
            }
        }

    }




}
