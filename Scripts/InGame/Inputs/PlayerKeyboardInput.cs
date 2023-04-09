using System;
using UniRx;
using Unity1week202303.Debugging;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202303.InGame.Inputs
{
    public class PlayerKeyboardInput : ITickable
    {
        [Inject]
        private readonly DebugSettings _debugSettings;

        private readonly ISubject<string> _inputString = new Subject<string>();

        public void Tick()
        {
            if (Input.inputString.Length > 0)
            {
                PrintLog($"[{Time.frameCount}]:{Input.inputString}");
                foreach (var c in Input.inputString)
                {
                    if (c == "\b"[0])
                    {
                        PrintLog("BackSpace");
                    }
                    else if (c == "\n"[0] || c == "\r"[0])
                    {
                        PrintLog("EnterOrReturn");
                    }
                    else if (c == "\t"[0])
                    {
                        PrintLog("Tab");
                    }
                    else if (Input.GetKeyDown(KeyCode.Space))
                    {
                        PrintLog("Space");
                    }
                    else
                    {
                        // 空じゃなければ文字を表示
                        if (c.ToString() != "")
                        {
                            _inputString.OnNext(c.ToString());
                        }
                    }
                }
            }
        }

        private void PrintLog(string str)
        {
            if (!_debugSettings.EnableInputLogging) return;
            Debug.Log($"Input:{str}");
        }

        public IObservable<string> OnInputStringAsObservable() => _inputString;
    }
}
