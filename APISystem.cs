using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using com.appidea.MiniGamePlatform.CommunicationAPI;

namespace com.appidea.MiniGamePlatform.Match_Color_Frogs.MiniGames.Match_The_Colors.Runtime.Scripts.Managers
{
    public class APISystem : MonoBehaviour,IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private Slider exitSlider;
        private MatchColorFrogsEntryPoint entryPoint;
        private bool isDraggingSlider;
        
        private IGameOverScreen gameOverScreen;
        [SerializeField] private Canvas mainCanvas;
        public Canvas MainCanvas => mainCanvas;

        private void OnEnable() => levelManager.OnGameFinish += ShowGameOverScreenIfExist;
        private void OnDisable() => levelManager.OnGameFinish -= ShowGameOverScreenIfExist;
        public void SetEntryPoint(MatchColorFrogsEntryPoint matchColorFrogsEntryPoint) =>
            entryPoint = matchColorFrogsEntryPoint;
        
        public void SetGameOverScreen(IGameOverScreen screen) => gameOverScreen = screen;
        private void ShowGameOverScreenIfExist() => gameOverScreen?.ShowGameOverScreen();

        private void Update()
        {
            var isInputReleased = Input.GetMouseButtonUp(0) ||
                                  Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;

            if (isDraggingSlider || !isInputReleased) return;
            if (exitSlider.value <= 0.3f)
            {
                entryPoint.SendGameFinished();
            }
            else
            {
                StartCoroutine(ResetSliderSmoothly());
            }
        }

        public void OnBeginDrag(PointerEventData eventData) => isDraggingSlider = true;
        public void OnEndDrag(PointerEventData eventData) => isDraggingSlider = false;

        private IEnumerator ResetSliderSmoothly()
        {
            const float duration = 0.1f;
            var elapsedTime = 0f;
            var startValue = exitSlider.value;
        
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                exitSlider.value = Mathf.Lerp(startValue, 1, elapsedTime / duration);
                yield return null;
            }

            exitSlider.value = 1;
        }
    }
}