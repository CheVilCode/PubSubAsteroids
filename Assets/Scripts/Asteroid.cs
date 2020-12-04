using SuperMaxim.Messaging;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenery
{
    public class CollisionPayload
    {
        public GameObject GameObject { get; set; }
    }

    public class AsteroidDestructionPayload
    {

    }

    public class Asteroid : MonoBehaviour
    {
        const float MIN_SPEED = 3;
        const float MAX_SPEED = 15;

        [SerializeField]
        private List<GameObject>       pieces;
        [SerializeField]
        private GameObject             core;
        private float                  speed;
        private BoundableObjectPayload boundsPayload;
        private CollisionPayload       collisionPayload;

        void Start()
        {
            speed = Random.Range(MIN_SPEED, MAX_SPEED);

            transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));

            boundsPayload = new BoundableObjectPayload();
            boundsPayload.GameObject = gameObject;
            boundsPayload.Type = BoundableObjectPayload.BehaviourType.Repositionable;

            collisionPayload = new CollisionPayload();
        }

        void Update()
        {
            transform.position += transform.right * speed * Time.deltaTime;

            Messenger.Default.Publish<BoundableObjectPayload>(boundsPayload);
        }

        private void OnTriggerEnter(Collider other)
        {
            transform.right = Vector3.Reflect(transform.right, other.transform.right);

            collisionPayload.GameObject = other.transform.parent.gameObject;
            Messenger.Default.Publish<CollisionPayload>(collisionPayload);

            if (pieces.Count > 0)
            {
                GameObject piece = pieces.ElementAt(Random.Range(0, pieces.Count));
                pieces.Remove(piece);
                Destroy(piece);
            }
            else
            {
                Messenger.Default.Publish<AsteroidDestructionPayload>(new AsteroidDestructionPayload());
                Destroy(gameObject);
            }
        }
    }
}