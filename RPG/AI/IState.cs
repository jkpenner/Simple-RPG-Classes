using System;
using System.Collections.Generic;

namespace RPG.AI {
    public interface IState {
        void OnEnter();
        void OnExit();
        void OnPause();
        void OnResume();
    }
}