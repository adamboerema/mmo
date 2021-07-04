using System;
using System.Collections.Generic;
using Common.Bus;

namespace Server.Bus.Game
{
    public class GameLoopBus: IGameLoopBus
    {
        private readonly IList<IEventBusListener<GameLoopEvent>> listeners
           = new List<IEventBusListener<GameLoopEvent>>();

        public void Publish(GameLoopEvent eventObject)
        {
            foreach (var listener in listeners)
            {
                listener.Handle(eventObject);
            }
        }

        public void Publish(double elapsedTime)
        {
            Publish(new GameLoopEvent
            {
                ElapsedTime = elapsedTime
            });
        }

        public void Subscribe(IEventBusListener<GameLoopEvent> listener)
        {
            listeners.Add(listener);
        }

        public void Unsubscribe(IEventBusListener<GameLoopEvent> listener)
        {
            listeners.Remove(listener);
        }
    }
}
