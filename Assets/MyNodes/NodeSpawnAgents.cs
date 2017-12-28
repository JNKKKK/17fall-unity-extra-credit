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
    /// Spawn agents
    /// 
    /// </summary>
    /// 
    public class SpawnAgents : Node
    {
        protected List<GameObject> AgentPool;
        protected GameObject AgentPrefab;
        protected GameObject agent;
        protected Bitmap image;
        protected float GroundWidth, GroundHeight;
        protected int[] arr;
        protected int[,] matrix;
        protected UI uiScript;

        public SpawnAgents(List<GameObject> AgentPool, GameObject AgentPrefab, UI uiScript)
        {
            this.AgentPool = AgentPool;
            this.AgentPrefab = AgentPrefab;
            this.uiScript = uiScript;
            this.GroundWidth = uiScript.GroundWidth;
            this.GroundHeight = uiScript.GroundHeight;
        }

        public override IEnumerable<RunStatus> Execute()
        {
            this.image = this.uiScript.image;
            Debug.Log(this.image.Width);
            Debug.Log(this.image.Height);
            Material[] mats;
            System.Drawing.Color sysColor;
            UnityEngine.Color unityColor;
            int AssignX = 0, AssignY = 0;
            System.Random rnd = new System.Random();
            arr = new int[this.AgentPool.Count];
            for (int i = 0; i < this.AgentPool.Count; i++)
            {
                arr[i] = 0;
            }
            matrix = new int[image.Width, image.Height];
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    bool ifskip = false;
                    for (int i = 0; i < this.AgentPool.Count; i++)
                    {
                        if ((SameColor(image.GetPixel(x, y),
                            this.AgentPool[i].transform.Find("Daniel").gameObject.transform.Find("Mesh").GetComponent<SkinnedMeshRenderer>().materials[0].color)
                            ) && (arr[i] == 0))
                        {
                            arr[i] = 1;
                            if (image.Width == this.GroundWidth)
                            {
                                AssignX = x - (int)GroundWidth / 2;
                                AssignY = -(y - (int)(image.Height) / 2);
                            }
                            if (image.Height == this.GroundHeight)
                            {
                                AssignX = x - (int)(image.Width) / 2;
                                AssignY = -(y - (int)GroundHeight / 2);
                            }
                            this.AgentPool[i].GetComponent<AgentAssignment>().assignX = AssignX;
                            this.AgentPool[i].GetComponent<AgentAssignment>().assignY = AssignY;
                            ifskip = true;
                            break;
                        }
                    }
                    if (ifskip)
                    {
                        matrix[x, y] = 1;
                        continue;
                    }
                }
            }
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    if (matrix[x, y] == 1) continue;
                    this.agent = (GameObject)UnityEngine.Object.Instantiate(this.AgentPrefab,
                        new Vector3(rnd.Next((int)-GroundWidth / 2, (int)GroundWidth / 2), 0, rnd.Next((int)-GroundHeight / 2, (int)GroundHeight / 2)),
                        Quaternion.identity);
                    //Assign new position
                    if (image.Width == this.GroundWidth)
                    {
                        AssignX = x - (int)GroundWidth / 2;
                        AssignY = -(y - (int)(image.Height) / 2);
                    }
                    if (image.Height == this.GroundHeight)
                    {
                        AssignX = x - (int)(image.Width) / 2;
                        AssignY = -(y - (int)GroundHeight / 2);
                    }
                    this.agent.GetComponent<AgentAssignment>().assignX = AssignX;
                    this.agent.GetComponent<AgentAssignment>().assignY = AssignY;
                    //Get Required Color
                    sysColor = image.GetPixel(x, y);
                    unityColor = new UnityEngine.Color(((float)sysColor.R) / 255, ((float)sysColor.G) / 255, ((float)sysColor.B) / 255);
                    //Change cloth color
                    mats = this.agent.transform.Find("Daniel").gameObject.transform.Find("Mesh").GetComponent<SkinnedMeshRenderer>().materials;
                    mats[0].color = unityColor;
                    //Change hair color
                    mats = this.agent.transform.Find("Daniel")
                        .gameObject.transform.Find("UMA_Male_Rig")
                        .gameObject.transform.Find("Global")
                        .gameObject.transform.Find("Position")
                        .gameObject.transform.Find("Hips")
                        .gameObject.transform.Find("LowerBack")
                        .gameObject.transform.Find("Spine")
                        .gameObject.transform.Find("Spine1")
                        .gameObject.transform.Find("Neck")
                        .gameObject.transform.Find("Head")
                        .gameObject.transform.Find("Hair_1")
                        .GetComponent<MeshRenderer>().materials;
                    mats[0].color = unityColor;
                    this.AgentPool.Add(this.agent);
                }
            }

            yield return RunStatus.Success;
            yield break;
        }
        private bool SameColor(System.Drawing.Color sysColor, UnityEngine.Color unityColor)
        {
            if ((sysColor.R == Math.Round(unityColor.r * 255))
                && (sysColor.G == Math.Round(unityColor.g * 255))
                    && (sysColor.B == Math.Round(unityColor.b * 255)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}