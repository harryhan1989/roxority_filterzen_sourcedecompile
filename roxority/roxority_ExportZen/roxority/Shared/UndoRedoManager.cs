namespace roxority.Shared
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    internal class UndoRedoManager
    {
        internal readonly string Original;
        private readonly Stack<Trio<Modification, int, string>[]> redoStack = new Stack<Trio<Modification, int, string>[]>();
        private readonly Stack<Trio<Modification, int, string>[]> undoStack = new Stack<Trio<Modification, int, string>[]>();

        internal UndoRedoManager(string original)
        {
            this.Original = (original == null) ? string.Empty : original;
        }

        internal void AddChange(string previousVersion, string currentVersion)
        {
            if (previousVersion == null)
            {
                previousVersion = string.Empty;
            }
            if (currentVersion == null)
            {
                currentVersion = string.Empty;
            }
            if (!previousVersion.Equals(currentVersion))
            {
                int length = previousVersion.Length;
                int num2 = currentVersion.Length;
                this.redoStack.Clear();
            }
        }

        internal string Modify(string currentVersion, bool revert, params Trio<Modification, int, string>[] operations)
        {
            for (int i = revert ? (operations.Length - 1) : 0; revert ? (i >= 0) : (i < operations.Length); i = revert ? (i - 1) : (i + 1))
            {
                currentVersion = this.Modify(currentVersion, revert ? ((((Modification) operations[i].Value1) == Modification.Insert) ? Modification.Remove : Modification.Insert) : operations[i].Value1, operations[i].Value2, operations[i].Value3);
            }
            return currentVersion;
        }

        internal string Modify(string currentVersion, Modification modification, int position, string value)
        {
            StringBuilder builder = new StringBuilder(currentVersion);
            if (modification == Modification.Insert)
            {
                builder.Insert(position, value);
            }
            else
            {
                builder.Remove(position, value.Length);
            }
            return builder.ToString();
        }

        internal string Redo(string currentVersion)
        {
            Trio<Modification, int, string>[] trioArray;
            if ((this.undoStack.Count != 0) && !SharedUtil.IsEmpty((ICollection) (trioArray = this.undoStack.Pop())))
            {
                return this.Modify(currentVersion, false, trioArray);
            }
            return currentVersion;
        }

        internal string Undo(string currentVersion)
        {
            Trio<Modification, int, string>[] trioArray;
            if ((this.undoStack.Count != 0) && !SharedUtil.IsEmpty((ICollection) (trioArray = this.undoStack.Pop())))
            {
                return this.Modify(currentVersion, true, trioArray);
            }
            return currentVersion;
        }

        internal bool CanRedo
        {
            get
            {
                return (this.redoStack.Count > 0);
            }
        }

        internal bool CanUndo
        {
            get
            {
                return (this.undoStack.Count > 0);
            }
        }

        internal enum Modification
        {
            Insert,
            Remove
        }
    }
}

