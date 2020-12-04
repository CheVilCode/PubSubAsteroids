using System.Collections;
using UnityEngine;

namespace Scenery
{
    public class AsteroidFactory : MonoBehaviour
    {
        const int ASTEROID_PER_BURST = 5;

        [SerializeField]
        private GameObject asteroid;

        void Start()
        {
            StartCoroutine(AsteroidGeneration());
        }

        private void CreateAsteroid()
        {
            GameObject gameObject = Instantiate(asteroid);
            
            switch(Random.Range(0,4))
            {
                case 0: gameObject.transform.position = new Vector3(-100, Random.Range(-10,10), 5); break;
                case 1: gameObject.transform.position = new Vector3(100, Random.Range(-10, 10), 5); break;
                case 2: gameObject.transform.position = new Vector3(Random.Range(-10,10),  100, 5); break;
                case 3: gameObject.transform.position = new Vector3(Random.Range(-10, 10), -100, 5); break;
            }
        }

        private IEnumerator AsteroidGeneration()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1, 4));

                for (int i = 0; i < ASTEROID_PER_BURST; i++)
                    CreateAsteroid();
            }
        }
    }
}