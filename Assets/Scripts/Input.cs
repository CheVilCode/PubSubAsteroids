using SuperMaxim.Messaging;
using UnityEngine;

namespace Input
{
    public class InputPayload
    {
        public enum Action { Up, Down, Left, Right, Button }
        public Action Type { get; set; }

        public override string ToString()
        {
            return $"Type: {Type}";
        }
    }

    public class Input : MonoBehaviour
    {
        private InputPayload payload;

        private void Start()
        {
            payload = new InputPayload();
        }

        void Update()
        {
            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                payload.Type = InputPayload.Action.Up;
                Messenger.Default.Publish<InputPayload>(payload);
            }

            if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                payload.Type = InputPayload.Action.Down;
                Messenger.Default.Publish<InputPayload>(payload);
            }

            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                payload.Type = InputPayload.Action.Left;
                Messenger.Default.Publish<InputPayload>(payload);
            }

            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                payload.Type = InputPayload.Action.Right;
                Messenger.Default.Publish<InputPayload>(payload);
            }

            if (UnityEngine.Input.GetKey(KeyCode.Space))
            {
                payload.Type = InputPayload.Action.Button;
                Messenger.Default.Publish<InputPayload>(payload);
            }
        }
    }
}