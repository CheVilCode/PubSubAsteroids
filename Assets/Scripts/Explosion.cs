using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Spaceship
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField]
        private float radius;
        [SerializeField]
        private float duration;

        void Start()
        {
            StartCoroutine(Explode());
        }

        private IEnumerator Explode()
        {
            transform.DOScale(radius, duration).SetEase(Ease.OutBounce);
            
            yield return new WaitForSeconds(duration);
            
            Destroy(gameObject);
        }
    }
}