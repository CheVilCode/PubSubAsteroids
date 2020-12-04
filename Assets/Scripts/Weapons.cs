using System.Collections;
using UnityEngine;

namespace Spaceship
{
    public class Weapons : MonoBehaviour
    {
        const float COLDOWN_DURATION = 0.15f;

        [SerializeField]
        GameObject[] cannons;
        [SerializeField]
        GameObject   projectile;
        
        private bool coldown = false;

        public void Shoot()
        {
            if (!coldown)
            {
                for (int i = 0; i < cannons.Length; i++)
                {
                    GameObject shot = Instantiate(projectile);
                    shot.transform.position = cannons[i].transform.position;
                    shot.transform.rotation = cannons[i].transform.rotation;
                }

                StartCoroutine(ColdownApplication());
            }
        }

        private IEnumerator ColdownApplication()
        {
            coldown = true;
            yield return new WaitForSeconds(COLDOWN_DURATION);
            coldown = false;
        }
    }
}