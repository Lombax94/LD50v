using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingPeon : MonoBehaviour {

    public Tasks Worklist;
    public Animator myAnimator;
    public SpriteRenderer myRenderer;
    public bool PlayerControle = false;
    public NoodeMap nodemap;

    public Nodes MyNode;


    void Start() {
        Worklist = GameObject.Find("Main Camera").GetComponent<Tasks>();
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        nodemap = GameObject.Find("Main Camera").GetComponent<NoodeMap>();

    }



    //Nodes RNode;
    //Nodes Saver;
    //List<Nodes> searched = new List<Nodes>();
    //List<Nodes> open = new List<Nodes>();
    Transform target;
    //Vector3 DistanceMT = Vector3.zero;
    //Vector3 CP2 = Vector3.zero;
    //float fvalue = 0;
    //Nodes TargetRoomNode;
   
               
    //            Nodes checker;


    

    public bool doingtask = false;
    public Tasks.tasktypes TheTask = Tasks.tasktypes.Idle;

    public float droptimer = 0.2f;
    public float dropCounter = 0;
    public bool dropStart = false;
    public int playerState = 0;
    public bool PathCheck = false;
    public bool Go = false;
    public List<Nodes> myPath = new List<Nodes>();
    public float speed = 3;
    public Vector3 myVector = Vector3.zero;

    void Update() {



        if (PlayerControle == true) {


            if (Input.GetKeyDown(KeyCode.RightArrow) == true) {
                if (dropStart == true) {
                    dropStart = false;
                    dropCounter = 0;

                }

                if (playerState != 1) {
                    myAnimator.SetBool("AllowChange", true);
                    myAnimator.SetInteger("State", 1);
                    playerState = 1;

                }

                myRenderer.flipX = false;

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) == true) {
                if (dropStart == true) {
                    dropStart = false;
                    dropCounter = 0;

                }

                if (playerState != 1) {
                    myAnimator.SetBool("AllowChange", true);
                    myAnimator.SetInteger("State", 1);
                    playerState = 1;

                }

                myRenderer.flipX = true;

            }

            if (playerState == 1) {
                if (dropStart == true) {
                    dropCounter += Time.deltaTime;

                    if (dropCounter >= droptimer) {
                        dropStart = false;
                        dropCounter = 0;
                        myAnimator.SetBool("AllowChange", true);
                        myAnimator.SetInteger("State", 0);
                        playerState = 0;

                    }

                }

                if (Input.GetKeyUp(KeyCode.LeftArrow) == true) {
                    if ((Input.GetKey(KeyCode.RightArrow) == false && Input.GetKeyDown(KeyCode.RightArrow) == false) == true) {
                        dropStart = true;
                        dropCounter = 0;

                    } else {
                        myRenderer.flipX = false;

                    }

                }
                if (Input.GetKeyUp(KeyCode.RightArrow) == true) {
                    if ((Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKeyDown(KeyCode.LeftArrow) == false) == true) {
                        dropStart = true;
                        dropCounter = 0;

                    } else {
                        myRenderer.flipX = true;

                    }

                }
            }












            if (Input.GetKeyDown(KeyCode.Alpha1) == true) {
                myAnimator.SetBool("AllowChange", true);
                myAnimator.SetInteger("State", 2);
                playerState = 2;

            }
            if (Input.GetKeyDown(KeyCode.Alpha2) == true) {
                myAnimator.SetBool("AllowChange", true);
                myAnimator.SetInteger("State", 3);
                playerState = 3;

            }
            if (Input.GetKeyDown(KeyCode.Alpha3) == true && Input.GetKey(KeyCode.Alpha4) == false) {
                myAnimator.SetBool("AllowChange", true);
                myAnimator.SetInteger("State", 4);
                playerState = 4;

            }
            if (Input.GetKeyDown(KeyCode.Alpha4) == true) {
                myAnimator.SetBool("AllowChange", true);
                myAnimator.SetInteger("State", 5);
                playerState = 5;

            }

        } else {

            if (PathCheck == true) {

                PathCheck = false;
                Nodes RNode = nodemap.TheNodeMap[Mathf.FloorToInt((transform.position.x - nodemap.WorldOffset.x) / 0.25f), Mathf.FloorToInt((transform.position.y - nodemap.WorldOffset.y) / 0.25f)];
                Nodes Saver;
                List<Nodes> searched = new List<Nodes>();
                List<Nodes> open = new List<Nodes>();
                target = GameObject.Find("Main Camera").transform;
                Vector3 DistanceMT = Vector3.zero;
                Vector3 CP2 = Vector3.zero;
                float fvalue = 0;
                Nodes TargetRoomNode = nodemap.TheNodeMap[Mathf.FloorToInt((target.position.x - nodemap.WorldOffset.x) / 0.25f), Mathf.FloorToInt((target.position.y - nodemap.WorldOffset.y) / 0.25f)];
                RNode.MidpointPM = transform.position;
                Saver = RNode;
                Nodes checker;


                //RNode = nodemap.TheNodeMap[Mathf.FloorToInt((transform.position.x - nodemap.WorldOffset.x) / 0.25f), Mathf.FloorToInt((transform.position.y - nodemap.WorldOffset.y) / 0.25f)];
                //target = GameObject.Find("Main Camera").transform.position;
                //TargetRoomNode = nodemap.TheNodeMap[Mathf.FloorToInt((target.x - nodemap.WorldOffset.x) / 0.25f), Mathf.FloorToInt((target.y - nodemap.WorldOffset.y) / 0.25f)];
                //RNode.MidpointPM = transform.position;
                //Saver = RNode;

                if (TargetRoomNode == RNode) {
                    //Go To Target.
                    Debug.Log("Found End, Was At The Beginning");


                } else {
                    open.Add(Saver);

                    for (int i = 0; i < 100; i++) {
                        fvalue = 10000000000000;

                        foreach (Nodes n in open) {
                            if (n.fcost < fvalue) {
                                fvalue = n.fcost;
                                Saver = n;

                            }

                        }

                        searched.Add(Saver);
                        open.Remove(Saver);//Yikes

                        foreach (TeleportConnected n in Saver.MyPointOfInterest) {
                            checker = nodemap.TheNodeMap[Mathf.FloorToInt((n.ConnectedTo.transform.position.x - nodemap.WorldOffset.x) / 0.25f), Mathf.FloorToInt((n.ConnectedTo.transform.position.y - nodemap.WorldOffset.y) / 0.25f)];

                            if (checker.Checked == false) {//This Can Never Be True, So Ugh Not Gonna Do A Else.... Time.... 2 Hours To Go T_T
                                open.Add(checker);
                                checker.Checked = true;

                                checker.Parent = Saver;
                                checker.MidpointPM = n.transform.position;
                                checker.MidpointTeleport = n.ConnectedTo.transform.position;

                                DistanceMT = checker.MidpointTeleport - target.position;
                                checker.hcost = Mathf.Abs(DistanceMT.x) + Mathf.Abs(DistanceMT.y);

                                DistanceMT = checker.Parent.MidpointPM - checker.MidpointPM;
                                checker.gcost = Mathf.Abs(DistanceMT.x) + Mathf.Abs(DistanceMT.y);
                                checker.fcost = checker.Parent.gcost + checker.hcost;

                            }

                        }

                            foreach (NodeConnectionMidpoint n in Saver.Neighbours) {

                            if (n.Neighbour == TargetRoomNode) {
                                TargetRoomNode.Parent = Saver;
                                Debug.Log("Found End");
                                break;
                            }

                            if (n.Neighbour.Checked == false) {
                                open.Add(n.Neighbour);
                                n.Neighbour.Checked = true;

                                n.Neighbour.Parent = Saver;
                                n.Neighbour.MidpointPM = n.Midpoint;

                                DistanceMT = n.Midpoint - target.position;
                                n.Neighbour.hcost = Mathf.Abs(DistanceMT.x) + Mathf.Abs(DistanceMT.y);

                                DistanceMT = n.Neighbour.Parent.MidpointPM - n.Midpoint;
                                n.Neighbour.gcost = Mathf.Abs(DistanceMT.x) + Mathf.Abs(DistanceMT.y);
                                n.Neighbour.fcost = n.Neighbour.Parent.gcost + n.Neighbour.hcost;

                            } else {//TODO Look into A 1 Time Recursive Search, That Tests If The New Path Beats The Old One In Reaching The Same Destination. I Think That Is A Valid Path To Explore, As It Feels Correct Looking At The Map I Created. But Could Just Be Realy Close.

                                CP2 = n.Midpoint - target.position;
                                DistanceMT = Saver.MidpointPM - n.Midpoint;

                                if (n.Neighbour.fcost > Mathf.Abs(CP2.x) + Mathf.Abs(CP2.y) + Mathf.Abs(DistanceMT.x) + Mathf.Abs(DistanceMT.y)) {
                                    n.Neighbour.Parent = Saver;
                                    n.Neighbour.MidpointPM = n.Midpoint;

                                    n.Neighbour.hcost = Mathf.Abs(CP2.x) + Mathf.Abs(CP2.y);
                                    n.Neighbour.gcost = Mathf.Abs(DistanceMT.x) + Mathf.Abs(DistanceMT.y);

                                    n.Neighbour.fcost = n.Neighbour.Parent.gcost + n.Neighbour.hcost;

                                }

                            }

                        }

                    }

                }

                myPath.Clear();
                MyNode = TargetRoomNode;
                Debug.Log(MyNode.bot);
                myVector = transform.position;
                myVector.z = RNode.height - 0.5f;
                transform.position = myVector;

                if (MyNode == RNode) {
                   

                    Debug.Log("Reverse Done!");
                } else {
                    myPath.Add(MyNode);

                    for (int i = 0; i < 100; i++) {
                        MyNode = MyNode.Parent;
                Debug.Log(MyNode.bot);
                        myPath.Add(MyNode);

                        if (MyNode == RNode) {
                            myPath.Reverse();
                            Debug.Log("Reverse Done!");
                            break;
                        }

                    }

                }


            } else {

                if (Go == true) {
                    if (myPath.Count > 0) {
                        

                        if (myPath[0].myTravelType == Tasks.GoToNode.Teleport) {
                            myVector = (myPath[0].MyPointOfInterest[0].transform.position - transform.position);
                            myVector.z = 0;
                            transform.position += myVector.normalized * Time.deltaTime * speed;
                            if (Vector2.Distance(transform.position, myPath[0].MyPointOfInterest[0].transform.position) < 0.1f) {
                                transform.position = myPath[0].MyPointOfInterest[0].ConnectedTo.transform.position;
                                
                                if(myPath.Count > 1) {
                                myVector = transform.position;
                                myVector.z = myPath[1].height - 0.5f;
                                transform.position = myVector;

                                }
                                myPath.RemoveAt(0);


                            }

                        } else {

                            if (myPath.Count > 1) {
                                foreach (NodeConnectionMidpoint n in myPath[0].Neighbours) {
                                    if (n.Neighbour == myPath[1]) {
                                        myVector = (n.Midpoint - transform.position);
                                        myVector.z =0;

                                        transform.position += myVector.normalized * Time.deltaTime * speed;
                                        if (Vector2.Distance(transform.position, n.Midpoint) < 0.1f) {
                                            myPath.RemoveAt(0);
                                            myVector = transform.position;
                                            myVector.z = myPath[0].height - 0.5f;
                                            transform.position = myVector;

                                        }
                                        break;

                                    }

                                }

                            } else {
                                myVector = (target.position - transform.position);
                                myVector.z = 0;

                                transform.position += myVector.normalized * Time.deltaTime * speed;

                            }

                        }

                    } else {
                        myVector = (target.position - transform.position);
                        myVector.z = 0;
                        transform.position += myVector.normalized * Time.deltaTime * speed;

                    }
                }
            }

        }

    }

}
