using System.Collections.Generic;
using UnityEngine;

namespace Playground.Services.Command
{
    public class CommandService : MonoBehaviour
    {
        #region Variables

        private readonly List<BaseCommand> _commands = new();
        private readonly List<BaseCommand> _executedCommands = new();

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.A))
            {
                AddCommand(new HelloCommand());
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.S))
            {
                AddCommand(new SaySmthCommand("Ololol"));
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.D))
            {
                AddCommand(new SumCommand(Random.Range(0, 100), Random.Range(0, 100)));
            }
            else if(UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteAllCommandsAndClear();
            }
        }

        #endregion

        #region Private methods

        private void AddCommand(BaseCommand command)
        {
            _commands.Add(command);
        }

        private void ExecuteAllCommandsAndClear()
        {
            foreach (BaseCommand command in _commands)
            {
                command.Execute();
            }

            _executedCommands.AddRange(_commands);
            _commands.Clear();
        }

        #endregion
    }
}