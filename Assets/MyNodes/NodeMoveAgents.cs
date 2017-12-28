using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Drawing;
using UnityEngine;
using System.Collections;
using TreeSharpPlus;

namespace MyNodes
{
    /// <summary>
    /// Move agents
    /// 
    /// </summary>
    /// 
    public class MoveAgents : Parallel
    {
        protected List<GameObject> AgentPool;

        public MoveAgents(List<GameObject> AgentPool)
        {
            this.AgentPool = AgentPool;

        }
        public override void Start()
        {
            this.Children = new List<Node>();
            for (int i = 0; i < this.AgentPool.Count; i++)
            {
                Vector3 NewPosition = new Vector3(AgentPool[i].GetComponent<AgentAssignment>().assignX, 0, AgentPool[i].GetComponent<AgentAssignment>().assignY);
                Node node = AgentPool[i].GetComponent<BehaviorMecanim>().Node_GoTo(NewPosition);
                this.Children.Add(node);
            }
            foreach (Node node in Children)
                if (node != null)
                    node.Parent = this;

            if (this.childStatus == null)
                this.childStatus = new List<RunStatus>(this.Children.Count);

            foreach (Node child in this.Children)
            {
                child.Start();
                this.childStatus.Add(RunStatus.Running);
            }
            base.Start();
            this.runningChildren = this.Children.Count;
        }
        public override IEnumerable<RunStatus> Execute()
        {
            while (true)
            {
                for (int i = 0; i < this.Children.Count; i++)
                {
                    if (this.childStatus[i] == RunStatus.Running)
                    {
                        Node node = this.Children[i];
                        RunStatus tickResult = this.TickNode(node);

                        // Check to see if anything finished
                        if (tickResult != RunStatus.Running)
                        {
                            // Clean up the node
                            node.Stop();
                            this.childStatus[i] = tickResult;
                            this.runningChildren--;

                            // If the node failed
                            if (tickResult == RunStatus.Failure)
                            {
                                // We may be stopping nodes in progress, so it's best
                                // to do a clean terminate and give them time to end
                                while (this.TerminateChildren() == RunStatus.Running)
                                    yield return RunStatus.Running;
                                // TODO: Timeout? - AS
                                // TODO: What if Terminate() fails? - AS

                                // Report failure
                                this.runningChildren = 0;
                                yield return RunStatus.Failure;
                                yield break;
                            }
                        }
                    }
                }

                // If we're out of running nodes, we're done
                if (this.runningChildren == 0)
                {
                    yield return RunStatus.Success;
                    yield break;
                }

                // For forked ticking
                yield return RunStatus.Running;
            }
        }
    }
}