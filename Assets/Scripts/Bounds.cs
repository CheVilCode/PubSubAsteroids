using SuperMaxim.Messaging;
using UnityEngine;

namespace Scenery
{
    public class BoundableObjectPayload
    {
        public enum BehaviourType { Repositionable, Destroyable}
        public BehaviourType Type { get; set; }
        public GameObject GameObject { get; set; }
    }

    public class Bounds : MonoBehaviour
    {
        const float MARGIN = 1;

        [SerializeField]
        private GameObject top;
        [SerializeField]
        private GameObject bot;
        [SerializeField]
        private GameObject left;
        [SerializeField]
        private GameObject right;

        void Start()
        {
            Messenger.Default.Subscribe<BoundableObjectPayload>(OnSpaceshipPosition, SpacePositionPredicate);
        }

        private void OnSpaceshipPosition(BoundableObjectPayload boundable)
        {
            if (boundable.Type == BoundableObjectPayload.BehaviourType.Repositionable)
            {
                Reposition(boundable.GameObject);
            }
            else if (boundable.Type == BoundableObjectPayload.BehaviourType.Destroyable)
            {
                Destroy(boundable.GameObject);
            }
        }

        private bool SpacePositionPredicate(BoundableObjectPayload boundable)
        {
            Vector3 position = boundable.GameObject.transform.position;

            if (position.x > right.transform.position.x)
            {
                return true;
            }
            else if (position.x < left.transform.position.x)
            {
                return true;
            }
            else if (position.y > top.transform.position.y)
            {
                return true;
            }
            else if (position.y < bot.transform.position.y)
            {
                return true;
            }

            return false;
        }

        private void Reposition(GameObject boundable)
        {
            Vector3 position = boundable.transform.position;

            if (position.x > right.transform.position.x)
            {
                boundable.transform.position = new Vector3(left.transform.position.x + MARGIN, position.y, position.z);
            }
            else if (position.x < left.transform.position.x)
            {
                boundable.transform.position = new Vector3(right.transform.position.x - MARGIN, position.y, position.z);
            }
            else if (position.y > top.transform.position.y)
            {
                boundable.transform.position = new Vector3(position.x, bot.transform.position.y + MARGIN, position.z);
            }
            else if (position.y < bot.transform.position.y)
            {
                boundable.transform.position = new Vector3(position.x, top.transform.position.y - MARGIN, position.z);
            }
        }
    }
}