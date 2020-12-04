using Game;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField]
        private Text       points;
        [SerializeField]
        private GameObject gameover;

        private void Start()
        {
            Messenger.Default.Subscribe<GamePointsPayload>(OnGamePoints);
            Messenger.Default.Subscribe<GameEndedPayload>(OnGameEnded);
        }

        private void OnGamePoints(GamePointsPayload payload)
        {
            points.text = payload.Points.ToString("00000000");
        }

        private void OnGameEnded(GameEndedPayload payload)
        {
            gameover.SetActive(true);
        }
    }
}