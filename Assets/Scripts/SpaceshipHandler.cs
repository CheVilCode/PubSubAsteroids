using UnityEngine;
using SuperMaxim.Messaging;
using Input;

namespace Spaceship
{
    public class SpaceshipHandler : MonoBehaviour
    {
        [SerializeField]
        private Spaceship spaceship;
        [SerializeField]
        private Weapons   weapons;

        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<InputPayload>(OnInput);
        }

        void Start()
        {
            Messenger.Default.Subscribe<InputPayload>(OnInput);
        }

        private void OnInput(InputPayload input)
        {
            switch(input.Type)
            {
                case InputPayload.Action.Up:     spaceship.SpeedUp();     break;
                case InputPayload.Action.Down:   spaceship.SpeedDown();   break;
                case InputPayload.Action.Left:   spaceship.RotateLeft();  break;
                case InputPayload.Action.Right:  spaceship.RotateRight(); break;
                case InputPayload.Action.Button: weapons.Shoot();         break;
            }
        }
    }
}