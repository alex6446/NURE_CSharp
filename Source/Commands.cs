using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace GraphFileManager
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }

    public class CloseCommand : ICommand
    {
        public CloseCommand() {}

        public void Execute()
        {
            GraphFileManager.Open().Exit();
        }
        public void Undo() { }
    }

    public class EmptyCommand : ICommand
    {
        public void Execute() { }
        public void Undo() { }
    }

    public class RefreshCommand : ICommand
    {
        public void Execute() {
            GraphFileManager.Open().Filemanager = new FileManager(GraphFileManager.Open().Filemanager.Dir);
        }
        public void Undo() { }
    }
}
