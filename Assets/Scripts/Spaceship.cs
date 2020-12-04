using Scenery;
using SuperMaxim.Messaging;
using UnityEngine;

namespace Spaceship
{
    public class SpaceshipDestructionPayload
    {

    }

    public class Spaceship : MonoBehaviour
    {
        const float MAX_SPEED    = 15;
        const float ACCELERATION = 5;
        const float ROTATION     = 200;

        [SerializeField]
        private GameObject             explosion;
        private BoundableObjectPayload payload;
        private float                  speed = 0;

        public void SpeedUp()
        {
            speed = Mathf.Clamp(speed + ACCELERATION * Time.deltaTime, 0, MAX_SPEED);
        }

        public void SpeedDown()
        {
            speed = Mathf.Clamp(speed - ACCELERATION * Time.deltaTime, 0, MAX_SPEED);
        }

        public void RotateLeft()
        {
            transform.Rotate(new Vector3(0, 0, ROTATION * Time.deltaTime));
        }

        public void RotateRight()
        {
            transform.Rotate(new Vector3(0, 0, -ROTATION * Time.deltaTime));
        }

        private void Start()
        {
            payload = new BoundableObjectPayload();
            payload.GameObject = gameObject;
            payload.Type = BoundableObjectPayload.BehaviourType.Repositionable;

            Messenger.Default.Subscribe<CollisionPayload>(OnCollision, CollisionPredicate);
        }

        void Update()
        {
            transform.position += transform.right * speed * Time.deltaTime;

            Messenger.Default.Publish<BoundableObjectPayload>(payload);
        }

        void OnDestroy()
        {
            Messenger.Default.Unsubscribe<CollisionPayload>(OnCollision);
        }

        private void OnCollision(CollisionPayload payload)
        {
            Instantiate(explosion).transform.position = transform.position;

            Messenger.Default.Publish<SpaceshipDestructionPayload>(new SpaceshipDestructionPayload());

            Destroy(gameObject);
        }

        private bool CollisionPredicate(CollisionPayload payload)
        {
            return payload.GameObject != null && payload.GameObject == gameObject;
        }
    }
}