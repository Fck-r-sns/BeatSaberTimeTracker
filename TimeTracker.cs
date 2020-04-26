using System;
using BS_Utils.Utilities;
using TMPro;
using UnityEngine;

namespace BeatSaberTimeTracker
{
    public class TimeTracker : MonoBehaviour
    {
        private const float TEXT_UPDATE_PERIOD = 1f;

        private Canvas _canvas;
        private TextMeshProUGUI _currentTimeText;
        private TextMeshProUGUI _totalTimeText;
        private TextMeshProUGUI _activeTimeText;

        private float _nextTextUpdate;

        private bool _trackActiveTime;
        private float _activeTime;

        private void Awake()
        {
            Plugin.logger.Debug("TimeTracker.Awake()");

            GameObject canvasGo = new GameObject("Canvas");
            canvasGo.transform.parent = transform;
            _canvas = canvasGo.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.WorldSpace;

            var canvasTransform = _canvas.transform;
            canvasTransform.position = new Vector3(-1f, 3.05f, 2.5f);
            canvasTransform.localScale = Vector3.one;

            _currentTimeText = CreateText(_canvas, new Vector2(0f, 0f), "");
            _totalTimeText = CreateText(_canvas, new Vector2(0f, -0.15f), "");
            _activeTimeText = CreateText(_canvas, new Vector2(0f, -0.3f), "");

            BSEvents.gameSceneActive += EnableTrackingMode;
            BSEvents.menuSceneActive += DisableTrackingMode;
            BSEvents.songPaused += DisableTrackingMode;
            BSEvents.songUnpaused += EnableTrackingMode;
        }

        private void OnDestroy()
        {
            Plugin.logger.Debug("TimeTracker.OnDestroy()");

            BSEvents.gameSceneActive -= EnableTrackingMode;
            BSEvents.menuSceneActive -= DisableTrackingMode;
            BSEvents.songPaused -= DisableTrackingMode;
            BSEvents.songUnpaused -= EnableTrackingMode;
        }

        private void Update()
        {
            if (_trackActiveTime)
            {
                _activeTime += Time.deltaTime;
            }

            if (Time.time >= _nextTextUpdate)
            {
                _currentTimeText.text = DateTime.Now.ToString("HH:mm");
                _totalTimeText.text = $"Total: {Mathf.FloorToInt(Time.time / 60f):00}:{Mathf.FloorToInt(Time.time % 60f):00}";
                _activeTimeText.text = $"Active: {Mathf.FloorToInt(_activeTime / 60f):00}:{Mathf.FloorToInt(_activeTime % 60f):00}";
                _nextTextUpdate += TEXT_UPDATE_PERIOD;
            }
        }

        private void SetTrackingMode(bool isTracking)
        {
            _trackActiveTime = isTracking;
            _canvas.gameObject.SetActive(!isTracking);
        }

        private void EnableTrackingMode()
        {
            SetTrackingMode(true);
        }

        private void DisableTrackingMode()
        {
            SetTrackingMode(false);
        }

        private static TextMeshProUGUI CreateText(Canvas canvas, Vector2 position, string text)
        {
            GameObject gameObject = new GameObject("CustomUIText");
            gameObject.SetActive(false);
            TextMeshProUGUI textMeshProUgui = gameObject.AddComponent<TextMeshProUGUI>();

            textMeshProUgui.rectTransform.SetParent(canvas.transform, false);
            textMeshProUgui.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            textMeshProUgui.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            textMeshProUgui.rectTransform.sizeDelta = new Vector2(1f, 1f);
            textMeshProUgui.rectTransform.transform.localPosition = Vector3.zero;
            textMeshProUgui.rectTransform.anchoredPosition = position;

            textMeshProUgui.text = text;
            textMeshProUgui.fontSize = 0.15f;
            textMeshProUgui.color = Color.white;
            textMeshProUgui.alignment = TextAlignmentOptions.Left;
            gameObject.SetActive(true);

            return textMeshProUgui;
        }
    }
}