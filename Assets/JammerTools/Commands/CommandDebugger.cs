#if UNITY_EDITOR
using JammerTools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace JammerTools.Commands
{
    public class CommandDebugger : MonoSingleton<CommandDebugger>
    {
        class CommandNode
        {
            public Command parent;
            public Command command;
            public List<CommandNode> children = new List<CommandNode>();
            public string lastToString;

            public bool IsRoot { get => parent == null; }
            public bool IsCleanedUp { get => command == null; }

            public override string ToString()
            {
                return command == null ? lastToString : command.ToString();
            }
        }

        private Dictionary<Command, CommandNode> aliveNodes = new Dictionary<Command, CommandNode>();
        private List<CommandNode> rootNodes = new List<CommandNode>();
        private bool logOnFinish;

        private void OnCommandFinished(Command cmd)
        {
            var node = aliveNodes[cmd];
            aliveNodes.Remove(node.command);

            if (node.IsRoot)
            {
                string toString = string.Format("Node Finished\n{0}", BuildComandLog(node));
            }

            foreach (var child in node.children)
            {
                Debug.Assert(child.IsCleanedUp);
            }

            node.command = null;
        }
        public void LogAliveCommands()
        {
            Debug.LogFormat("Root Commands: {0}", rootNodes.Count);
            foreach (var node in rootNodes)
            {
                Debug.Log(BuildComandLog(node));
            }
        }

        #region MenuItems


        [MenuItem("PointNSheep/Commands/Log All Commands")]
        public static void LogAllCommands()
        {
            Instance.LogAliveCommands();
        }
        [MenuItem("PointNSheep/Commands/Log All Commands", true)]
        public static bool LogAllCommandsEnabled()
        {
            return Application.isPlaying;
        }

        [MenuItem("PointNSheep/Commands/Log On Finish - Off")]
        public static void DisableLogOnFinish()
        {
            Instance.logOnFinish = false;
        }
        [MenuItem("PointNSheep/Commands/Log On Finish - Off", true)]
        public static bool CanDisableLogOnFinish()
        {
            return Application.isPlaying && Instance.logOnFinish;
        }

        [MenuItem("PointNSheep/Commands/Log On Finish - On")]
        public static void EnableLogOnFinish()
        {
            Instance.logOnFinish = true;
        }
        [MenuItem("PointNSheep/Commands/Log On Finish - On", true)]
        public static bool CanEnableLogOnFinish()
        {
            return Application.isPlaying && !Instance.logOnFinish;
        }
        #endregion

        private string BuildComandLog(CommandNode cmd)
        {
            StringBuilder bldr = new StringBuilder();
            AppendLogRecursive(cmd, bldr);
            return bldr.ToString();
        }

        private void AppendLogRecursive(CommandNode cmd, StringBuilder bldr)
        {
            bldr.Append(cmd.ToString());
            if(cmd.children.Any())
                bldr.Append("|\nChildren\n");
            foreach (var child in cmd.children)
            {
                AppendLogRecursive(child, bldr);
            }
        }


        public void DebugRegisterCommand(Command c)
        {
            var node = new CommandNode()
            {
                command = c
            };
            aliveNodes.Add(c, node);
            rootNodes.Add(node);
        }
        public void DebugRegisterParentage(Command parent, Command child)
        {
            Debug.Assert(!parent.HasStarted);
            var childNode = aliveNodes[child];
            var parentNode = aliveNodes[parent];
            parentNode.children.Add(childNode);
            childNode.parent = parent;

            rootNodes.Remove(childNode);
        }

    }
}
#endif