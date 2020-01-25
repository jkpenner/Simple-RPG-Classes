using System;
using System.Collections.Generic;
using RPG.Actors;
using RPG.Actors.Resources;

namespace Game {
    public class TurnHandler {
        private List<Actor> _turnQueue;

        public TurnHandler() {
            this._turnQueue = new List<Actor>();
        }

        public Actor GetNextReady() {
            if (this._turnQueue.Count == 0)
                return null;

            var actor = this._turnQueue[0];
            if (actor.Resources.Get(ResourceKeys.Speed) > 0f)
                return null;

            this._turnQueue.RemoveAt(0);
            return actor;
        }

        public void Queue(Actor actor) {
            var actorSpeed = actor.Resources.Get(ResourceKeys.Speed);
            for(var i = 0; i < this._turnQueue.Count; i++) {
                var otherSpeed = this._turnQueue[i].Resources.Get(ResourceKeys.Speed);
                
                if (actorSpeed < otherSpeed) {
                    this._turnQueue.Insert(i, actor);
                    return;
                }
            }

            this._turnQueue.Add(actor);
        }

        public void UpdateTurns() {
            foreach(var actor in this._turnQueue) {
                float speed = actor.Resources.Get(ResourceKeys.Speed);
                speed = Math.Max(speed - 1f, 0f);
                actor.Resources.Set(ResourceKeys.Speed, speed);
            }
        }
    }
}