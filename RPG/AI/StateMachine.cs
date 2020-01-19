using System;
using System.Collections.Generic;

namespace RPG.AI {
    public class StateMachine<StateType> : IStateMachine<StateType> where StateType : class, IState {
        private Stack<StateType> _stack;
        
        public StateMachine() {
            this._stack = new Stack<StateType>();
        }
        
        public void Swap(StateType state) {
            // Remove current state and trigger on exit.
            if(this._stack.Count > 0) {
                this._stack.Pop().OnExit();
            }
            
            this._stack.Push(state);
            state.OnEnter();
        }
        
        public void Push(StateType state) {
            if(this._stack.Count > 0) {
                this._stack.Peek().OnPause();
            }
            
            this._stack.Push(state);
            state.OnEnter();
        }
        
        public void Pop() {
            if(this._stack.Count > 0)
                this._stack.Pop().OnExit();
            
            if(this._stack.Count > 0)
                this._stack.Peek().OnResume();
        }
        
        public StateType Peek() {
            return this._stack.Count > 0 ?
                this._stack.Peek() : null;
        }
        
        public bool TryPeek(out StateType state) {
            state = Peek();
            return state != null;
        }
        
        public void PopAll() {
            while(this._stack.Count > 0) {
                this._stack.Pop().OnExit();	
            }
            this._stack.Clear();
        }
    }
}