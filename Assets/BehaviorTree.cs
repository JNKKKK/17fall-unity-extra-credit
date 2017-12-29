using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using TreeSharpPlus;
using MyNodes;

public class BehaviorTree : MonoBehaviour
{
    private List<GameObject> AgentPool = new List<GameObject>();
    private BehaviorAgent behaviorAgent;

    public GameObject AgentPrefab;

    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    void Update()
    {

    }

    protected Node BuildTreeRoot()
    {
        Func<bool> ffunc = () => (true);
        Func<bool> ifGoPressed = () => (gameObject.GetComponent<UI>().goPressed);
        Func<bool> ifNotGoPressed = () => (!gameObject.GetComponent<UI>().goPressed);
        Func<RunStatus> invertGo = () => ((gameObject.GetComponent<UI>().goPressed = !gameObject.GetComponent<UI>().goPressed) ? RunStatus.Success : RunStatus.Success);
        Node root =
            new DecoratorLoop(
            new DecoratorForceStatus(RunStatus.Success,
            new Sequence(
                new DecoratorForceStatus(RunStatus.Success, new DecoratorLoop(new LeafAssert(ifNotGoPressed))),
                new LeafInvoke(invertGo),
                new AssignRemoveAgents(AgentPool, gameObject.GetComponent<UI>()),
                new MoveAgents(AgentPool),
                new RemoveAgents(AgentPool, gameObject.GetComponent<UI>()),
                new LeafWait(1000),
                new SpawnAgents(AgentPool, AgentPrefab, gameObject.GetComponent<UI>()),
                new LeafWait(1000),
                //new MoveAgents(AgentPool)
                new SequenceParallel(new DecoratorLoop(new LeafAssert(ifNotGoPressed)), new MoveAgents(AgentPool))
            )
            )
            );
        return root;
    }

}
