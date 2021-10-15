using ModestTree;

namespace Zenject.Asteroids
{
    public enum ShipStates // TODO FactoryState: 3) Перечесление состояний.
    {
        Moving,
        Dead,
        WaitingToStart,
        Count
    }

    public class ShipStateFactory // TODO FactoryState: 1) Делаем класс фабрик состояний. Все фабрики наследуют класс ShipState.
                                  // ShipState это абстракный класс реализующий интерфейс IDisposable. У класса есть виртуальные методы
                                  // Start, Dispose, OnTriggerEnter, и абстрактный Update;
                                  
    {
        readonly ShipStateWaitingToStart.Factory _waitingFactory;
        readonly ShipStateMoving.Factory _movingFactory;
        readonly ShipStateDead.Factory _deadFactory;

        public ShipStateFactory(
            ShipStateDead.Factory deadFactory,
            ShipStateMoving.Factory movingFactory,
            ShipStateWaitingToStart.Factory  waitingFactory)
        {
            _waitingFactory = waitingFactory;
            _movingFactory = movingFactory;
            _deadFactory = deadFactory;
        }

        public ShipState CreateState(ShipStates state)  // TODO FactoryState: 2) Делаем метод принимающий состояния.
        {
            switch (state)
            {
                case ShipStates.Dead:
                {
                    return _deadFactory.Create();
                }
                case ShipStates.WaitingToStart:
                {
                    return _waitingFactory.Create();
                }
                case ShipStates.Moving:
                {
                    return _movingFactory.Create();
                }
            }

            throw Assert.CreateException(); //TODO ??
        }
    }
}
