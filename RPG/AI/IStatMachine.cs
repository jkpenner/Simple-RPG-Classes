using System;
using System.Collections.Generic;

namespace RPG.AI {
    public interface IStateMachine<T> where T : class, IState {
        void Swap(T state);
        void Push(T state);
        void Pop();
        void PopAll();
        bool TryPeek(out T state);
    }
}