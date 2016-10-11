using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class TestXInput : MonoBehaviour
{
    private bool bPlayIndexSet = false;
    private PlayerIndex ePlayerIndex;
    private GamePadState currentState;
    private GamePadState preState;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!bPlayIndexSet || !preState.IsConnected)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerIndex tmpPlayerIndex =   (PlayerIndex)i;
                GamePadState state = GamePad.GetState(tmpPlayerIndex);
                if (state.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", tmpPlayerIndex));
                    ePlayerIndex = tmpPlayerIndex;
                    bPlayIndexSet = true;
                }
            }
        }

        preState = currentState;
        currentState = GamePad.GetState(ePlayerIndex);
        if (preState.Buttons.A == ButtonState.Released && currentState.Buttons.A == ButtonState.Pressed)
        {
            GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        }
        if (preState.Buttons.A == ButtonState.Pressed && currentState.Buttons.A == ButtonState.Released)
        {
            GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        GamePad.SetVibration(ePlayerIndex, currentState.Triggers.Left, currentState.Triggers.Right);
        transform.localRotation *= Quaternion.Euler(0.0f, 0f,currentState.ThumbSticks.Left.X * 25.0f * Time.deltaTime);
    }

    void OnGUI()
    {
        string text = "Use left stick to turn the cube, hold A to change color\n";
        text += string.Format("IsConnected {0} Packet #{1}\n", currentState.IsConnected, currentState.PacketNumber);
        text += string.Format("\tTriggers {0} {1}\n", currentState.Triggers.Left, currentState.Triggers.Right);
        text += string.Format("\tD-Pad {0} {1} {2} {3}\n", currentState.DPad.Up, currentState.DPad.Right, currentState.DPad.Down, currentState.DPad.Left);
        text += string.Format("\tButtons Start {0} Back {1} Guide {2}\n", currentState.Buttons.Start, currentState.Buttons.Back, currentState.Buttons.Guide);
        text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", currentState.Buttons.LeftStick, currentState.Buttons.RightStick, currentState.Buttons.LeftShoulder, currentState.Buttons.RightShoulder);
        text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", currentState.Buttons.A, currentState.Buttons.B, currentState.Buttons.X, currentState.Buttons.Y);
        text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", currentState.ThumbSticks.Left.X, currentState.ThumbSticks.Left.Y, currentState.ThumbSticks.Right.X, currentState.ThumbSticks.Right.Y);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
    }
}
