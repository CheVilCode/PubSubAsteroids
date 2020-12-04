using Scenery;
using Spaceship;
using SuperMaxim.Messaging;
using UnityEngine;

namespace Game
{
    public class GamePointsPayload
    {
        public float Points { get; set; }
    }

    public class GameEndedPayload
    {

    }

    public class Game : MonoBehaviour
    {
        private enum State {  None, Playing, Ended }

        [SerializeField]
        private GameObject        spaceship;
        [SerializeField]          
        private GameObject        asteroidFactory;
        private State             currentState = State.None;
        private float             points = 0;
        private GamePointsPayload pointsPayload;

        void Start()
        {
            spaceship.SetActive(true);
            asteroidFactory.SetActive(true);

            currentState = State.Playing;

            Messenger.Default.Subscribe<AsteroidDestructionPayload>(OnAsteroidDestruction);
            Messenger.Default.Subscribe<SpaceshipDestructionPayload>(OnSpaceshipDestruction);

            pointsPayload = new GamePointsPayload();
        }

        void Update()
        {
            if (currentState == State.Playing)
            {
                points += 10 * Time.deltaTime;
                pointsPayload.Points = points;

                Messenger.Default.Publish<GamePointsPayload>(pointsPayload);
            }
        }

        private void OnAsteroidDestruction(AsteroidDestructionPayload payload)
        {
            points += 500;
        }

        private void OnSpaceshipDestruction(SpaceshipDestructionPayload payload)
        {
            currentState = State.Ended;

            Messenger.Default.Publish<GameEndedPayload>(new GameEndedPayload());
        }
    }
}