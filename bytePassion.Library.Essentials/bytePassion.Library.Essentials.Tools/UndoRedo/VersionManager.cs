using System;
using System.Collections.Generic;
using System.ComponentModel;
using bytePassion.Library.Essentials.Tools.FrameworkExtensions;

namespace bytePassion.Library.Essentials.Tools.UndoRedo
{

	public class VersionManager<T> : IUndoRedo where T : class
	{
		public event EventHandler<T> CurrentVersionChanged;

		private readonly uint maximalSavedVersions;

		private readonly LinkedList<T> versions;
		private LinkedListNode<T> currentVersionPointer;

		private bool undoPossible;
		private bool redoPossible;

		public VersionManager(uint maximalSavedVersions)
		{
			this.maximalSavedVersions = maximalSavedVersions;
			versions = new LinkedList<T>();
			CurrentVersionPointer = null;

			CheckIfUndoAndRedoIsPossible();
		}

		public VersionManager(uint maximalSavedVersions, T initialVersion)
			: this(maximalSavedVersions)
		{
			AddnewVersion(initialVersion);
		}

		public void FixCurrentVersion()
		{
			lock (this)
			{
				versions.Clear();
				versions.AddFirst(CurrentVersionPointer);

				CheckIfUndoAndRedoIsPossible();
			}
		}

		public bool UndoPossible
		{
			get { return undoPossible; }
			private set { PropertyChanged.ChangeAndNotify(this, ref undoPossible, value); }
		}

		public bool RedoPossible
		{
			get { return redoPossible; }
			private set { PropertyChanged.ChangeAndNotify(this, ref redoPossible, value); }
		}

		public void AddnewVersion(T newVersion)
		{
			lock (this)
			{
				var newVersionNode = new LinkedListNode<T>(newVersion);

				RemoveAllFromEndTo(CurrentVersionPointer);
				versions.AddLast(newVersionNode);
				CurrentVersionPointer = newVersionNode;

				if (versions.Count == maximalSavedVersions + 1)
					versions.RemoveFirst();
			}
		}

		public T CurrentVersion
		{
			get
			{
				lock (this)
				{
					return CurrentVersionPointer?.Value;
				}
			}
		}

		private LinkedListNode<T> CurrentVersionPointer
		{
			get { return currentVersionPointer; }
			set
			{
				if (value != currentVersionPointer)
				{
					currentVersionPointer = value;

					CheckIfUndoAndRedoIsPossible();

					if (CurrentVersionPointer != null)											
						CurrentVersionChanged?.Invoke(this, CurrentVersion);					
				}
			}
		}

		public void Undo()
		{
			lock (this)
			{
				if (UndoPossible)
					CurrentVersionPointer = CurrentVersionPointer.Previous;
				else
					throw new InvalidOperationException("undo not possible");
			}
		}

		public void Redo()
		{
			lock (this)
			{
				if (RedoPossible)
					CurrentVersionPointer = CurrentVersionPointer.Next;
				else
					throw new InvalidOperationException("redo not possible");
			}
		}

		private void CheckIfUndoAndRedoIsPossible()
		{
			if (CurrentVersionPointer != null)
			{
				UndoPossible = CurrentVersionPointer.Previous != null;
				RedoPossible = CurrentVersionPointer.Next != null;
			}
			else
			{
				UndoPossible = false;
				RedoPossible = false;
			}
		}

		private void RemoveAllFromEndTo(LinkedListNode<T> node)
		{
			while (versions.Last != node)
			{
				versions.RemoveLast();

				if (versions.Count == 0)
					throw new InvalidOperationException();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
