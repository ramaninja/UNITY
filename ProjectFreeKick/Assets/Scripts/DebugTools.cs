using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools
{

    public static class DebugTools
    {

        public static void Log(string message, GameObject go = null, bool displayFrameCount = false, bool displayTime = false)
        {
            string str = (go ? go.name + " - " : "") +
                (displayFrameCount ? Time.frameCount + " - " : "") +
                (displayTime ? Time.time + " - " : "") +
                message;

            Debug.Log(str);
        }

    }
}
