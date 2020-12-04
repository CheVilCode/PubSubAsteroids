using SuperMaxim.Messaging;
using UnityEngine;

namespace Scenery
{
    public class Projectile : MonoBehaviour
    {
        const float SPEED = 30;

        [SerializeField]
        private GameObject             explosion;
        private BoundableObjectPayload payload;

        void Start()
        {
            payload = new BoundableObjectPayload();
            payload.GameObject = gameObject;
            payload.Type = BoundableObjectPayload.BehaviourType.Destroyable;

            Messenger.Default.Subscribe<CollisionPayload>(OnCollision, CollisionPredicate);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<CollisionPayload>(OnCollision);
        }

        void Update()
        {
            transform.position += transform.right * SPEED * Time.deltaTime;

            Messenger.Default.Publish<BoundableObjectPayload>(payload);
        }

        private void OnCollision(CollisionPayload payload)
        {
            Instantiate(explosion).transform.position = transform.position;
            Destroy(gameObject);
        }

        private bool CollisionPredicate(CollisionPayload payload)
        {
            return payload.GameObject != null && payload.GameObject == gameObject;
        }
    }
}