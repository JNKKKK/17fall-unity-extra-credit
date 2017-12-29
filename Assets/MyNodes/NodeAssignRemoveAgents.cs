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
    /// Assign Remove agents
    /// 
    /// </summary>
    /// 
    public class AssignRemoveAgents : Node
    {
        protected List<GameObject> AgentPool;
        protected Bitmap image;
        protected int[,] matrix;
        protected UI uiScript;

        public AssignRemoveAgents(List<GameObject> AgentPool, UI uiScript)
        {
            this.AgentPool = AgentPool;
            this.uiScript = uiScript;
        }

        public override IEnumerable<RunStatus> Execute()
        {
            this.image = this.uiScript.image;
            matrix = new int[image.Width, image.Height];
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            for (int i = this.AgentPool.Count - 1; i > -1; i--)
            {
                bool keep = false;
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        if (SameColor(this.image.GetPixel(x, y),
                            this.AgentPool[i].transform.Find("Daniel").gameObject.transform.Find("Mesh").GetComponent<SkinnedMeshRenderer>().materials[0].color
                            ) && (matrix[x, y] == 0))
                        {
                            matrix[x, y] = 1;
                            keep = true;
                            break;
                        }
                    }
                    if (keep) break;
                }
                if (!keep)
                {
                    UI.xy xy;
                    xy = this.uiScript.randXY();
                    this.AgentPool[i].GetComponent<AgentAssignment>().assignX = xy.x;
                    this.AgentPool[i].GetComponent<AgentAssignment>().assignY = xy.y;
                }
            }
            for (int i = this.AgentPool.Count - 1; i > -1; i--)
            {
                UnityEngine.Color color;
                color = this.AgentPool[i].transform.Find("Daniel").gameObject.transform.Find("Mesh").GetComponent<SkinnedMeshRenderer>().materials[0].color;
                if (!this.uiScript.ToggleBlack.GetComponent<UnityEngine.UI.Toggle>().isOn)
                {
                    if (color.r == 0 && color.g == 0 && color.b == 0)
                    {
                        UI.xy xy;
                        xy = this.uiScript.randXY();
                        this.AgentPool[i].GetComponent<AgentAssignment>().assignX = xy.x;
                        this.AgentPool[i].GetComponent<AgentAssignment>().assignY = xy.y;
                    }
                }
                if (!this.uiScript.ToggleWhite.GetComponent<UnityEngine.UI.Toggle>().isOn)
                {
                    if (color.r == 1 && color.g == 1 && color.b == 1)
                    {
                        UI.xy xy;
                        xy = this.uiScript.randXY();
                        this.AgentPool[i].GetComponent<AgentAssignment>().assignX = xy.x;
                        this.AgentPool[i].GetComponent<AgentAssignment>().assignY = xy.y;
                    }
                }
                if (!this.uiScript.ToggleColors.GetComponent<UnityEngine.UI.Toggle>().isOn)
                {
                    if ((color.r > 0 && color.r < 255) || (color.g > 0 && color.g < 255) || (color.b > 0 && color.b < 255))
                    {
                        UI.xy xy;
                        xy = this.uiScript.randXY();
                        this.AgentPool[i].GetComponent<AgentAssignment>().assignX = xy.x;
                        this.AgentPool[i].GetComponent<AgentAssignment>().assignY = xy.y;
                    }
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