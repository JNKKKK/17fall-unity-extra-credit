using UnityEngine;
using System.Collections;

public class AgentAssignment : MonoBehaviour
{
    public enum statusEnum { init, working, finished };
    public statusEnum status = statusEnum.init;
    public float assignX, assignY;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
