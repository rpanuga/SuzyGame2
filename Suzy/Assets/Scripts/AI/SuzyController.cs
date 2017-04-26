using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class SuzyController : MonoBehaviour
{
    public GameObject player;
    public GameObject Suzy;
    public Transform[] path;
    private FSMSystem fsm;

    public void SetTransition(Transition t) { fsm.PerformTransition(t); }

    public void Start()
    {
        MakeFSM();
    }

    public void FixedUpdate()
    {
        fsm.CurrentState.Reason(player, gameObject);
        fsm.CurrentState.Act(player, gameObject);
    }

    // Finite State Machine w 2 paths: FollowPath + ChasePlayer
    private void MakeFSM()
    {

        fsm = new FSMSystem();

        FollowPathState follow = new FollowPathState(path);
        follow.AddTransition(Transition.SawPlayer, StateID.ChasingPlayer);

        ChasePlayerState chase = new ChasePlayerState();
        chase.AddTransition(Transition.LostPlayer, StateID.FollowingPath);

        fsm.AddState(follow);
        fsm.AddState(chase);
    }


}

public class FollowPathState : FSMState
{
    private int currentWayPoint;
    private Transform[] waypoints;

    public FollowPathState(Transform[] wp)
    {
        waypoints = wp;
        currentWayPoint = 0;
        stateID = StateID.FollowingPath;
    }
    
    public override void Reason(GameObject player, GameObject Suzy)
    {
        // if player gets close enough

        RaycastHit hit;
        if (Physics.Raycast(Suzy.transform.position, Suzy.transform.forward, out hit, 5F))
        {
            if (hit.transform.gameObject.tag == "Player")
                Suzy.GetComponent<SuzyController>().SetTransition(Transition.SawPlayer);
        }
    }

    public override void Act(GameObject player, GameObject Suzy)
    {
        NavMeshAgent agent;
        agent = Suzy.GetComponent<NavMeshAgent>();

        agent.destination = waypoints[currentWayPoint].position;

        // follow set path of waypoints
        Vector3 vel = Suzy.GetComponent<Rigidbody>().velocity;
        Vector3 moveDir = waypoints[currentWayPoint].position - Suzy.transform.position;

        if (moveDir.magnitude < 1)
        {
            currentWayPoint++;
            if (currentWayPoint >= waypoints.Length)
            {
                currentWayPoint = 0;
            }
        }
        else
        {
           // vel = moveDir.normalized * 2;
            Suzy.transform.rotation = Quaternion.Slerp(Suzy.transform.rotation,
                                                      Quaternion.LookRotation(moveDir),
                                                      5 * Time.deltaTime);
            Suzy.transform.eulerAngles = new Vector3(0, Suzy.transform.eulerAngles.y, 0);

        }

        // apply velocity
        //Suzy.GetComponent<Rigidbody>().velocity = vel;
    }

} // FollowPathState

public class ChasePlayerState : FSMState
{
    public ChasePlayerState()
    {
        stateID = StateID.ChasingPlayer;
    }

    public override void Reason(GameObject player, GameObject Suzy)
    {
        // loses player if they get far enough away
        if (Vector3.Distance(Suzy.transform.position, player.transform.position) >= 10)
            Suzy.GetComponent<SuzyController>().SetTransition(Transition.LostPlayer);
    }

    public override void Act(GameObject player, GameObject Suzy)
    {
        // follow path
        Vector3 vel = Suzy.GetComponent<Rigidbody>().velocity;
        Vector3 moveDir = player.transform.position - Suzy.transform.position;
        Suzy.transform.rotation = Quaternion.Slerp(Suzy.transform.rotation,
                                                  Quaternion.LookRotation(moveDir),
                                                  5 * Time.deltaTime);
        Suzy.transform.eulerAngles = new Vector3(0, Suzy.transform.eulerAngles.y, 0);

        vel = moveDir.normalized * 4;

        // apply velocity
        Suzy.GetComponent<Rigidbody>().velocity = vel;

    }
}
// ChasePlayerState


