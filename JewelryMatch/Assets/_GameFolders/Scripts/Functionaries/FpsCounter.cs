using UnityEngine;

namespace _GameFolders.Scripts.Functionaries
{
    public class FpsCounter : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("How often (in seconds) the displayed FPS updates.")]
        [Range(0.1f, 1.0f)]
        public float updateInterval = 0.25f;

        [Tooltip("Anchor position on screen.")]
        public TextAnchor anchor = TextAnchor.UpperLeft;

        [Tooltip("Pixel offset from chosen anchor.")]
        public Vector2 offset = new Vector2(10, 10);

        [Tooltip("Font size of label.")]
        public int fontSize = 18;

        [Tooltip("Show milliseconds per frame too.")]
        public bool showMs = true;

        [Tooltip("Toggle visibility with F1.")]
        public bool allowToggleKey = true;

        [Tooltip("Draw background box.")]
        public bool drawBackground = true;

        [Tooltip("Background color (with alpha).")]
        public Color backgroundColor = new Color(0, 0, 0, 0.35f);

        [Tooltip("Text color.")]
        public Color textColor = Color.white;

        private float _accum;
        private int _frames;
        private float _timeLeft;
        private float _currentFps;
        private GUIStyle _style;
        private bool _visible = true;
        private Rect _rect;

        private void Awake()
        {
            _timeLeft = updateInterval;
            _style = new GUIStyle
            {
                alignment = TextAnchor.UpperLeft,
                fontSize = fontSize,
                normal = { textColor = textColor }
            };
            RecalculateRect();
        }

        private void Update()
        {
            if (allowToggleKey && Input.GetKeyDown(KeyCode.F1))
                _visible = !_visible;

            _timeLeft -= Time.unscaledDeltaTime;
            _accum += Time.unscaledDeltaTime;
            _frames++;

            if (_timeLeft <= 0f)
            {
                _currentFps = _frames / _accum;
                _timeLeft = updateInterval;
                _accum = 0f;
                _frames = 0;
            }

            if (_style.fontSize != fontSize)
            {
                _style.fontSize = fontSize;
                RecalculateRect();
            }
        }

        private void OnGUI()
        {
            if (!_visible) return;

            _style.normal.textColor = textColor;

            string label = $"FPS: {Mathf.RoundToInt(_currentFps)}";
            if (showMs && _currentFps > 0.01f)
                label += $" ({(1000f / _currentFps):F1} ms)";

            if (drawBackground)
            {
                var prev = GUI.color;
                GUI.color = backgroundColor;
                GUI.Box(_rect, GUIContent.none);
                GUI.color = prev;
            }

            GUI.Label(_rect, label, _style);
        }

        private void OnValidate()
        {
            if (_style != null)
            {
                _style.fontSize = fontSize;
                _style.normal.textColor = textColor;
            }
            RecalculateRect();
        }

        private void RecalculateRect()
        {
            float width = 210f;
            float height = fontSize + 10f;
            float x = offset.x;
            float y = offset.y;

            switch (anchor)
            {
                case TextAnchor.UpperCenter:
                    x = (Screen.width - width) * 0.5f + offset.x;
                    break;
                case TextAnchor.UpperRight:
                    x = Screen.width - width - offset.x;
                    break;
                case TextAnchor.MiddleLeft:
                    y = (Screen.height - height) * 0.5f + offset.y;
                    break;
                case TextAnchor.MiddleCenter:
                    x = (Screen.width - width) * 0.5f + offset.x;
                    y = (Screen.height - height) * 0.5f + offset.y;
                    break;
                case TextAnchor.MiddleRight:
                    x = Screen.width - width - offset.x;
                    y = (Screen.height - height) * 0.5f + offset.y;
                    break;
                case TextAnchor.LowerLeft:
                    y = Screen.height - height - offset.y;
                    break;
                case TextAnchor.LowerCenter:
                    x = (Screen.width - width) * 0.5f + offset.x;
                    y = Screen.height - height - offset.y;
                    break;
                case TextAnchor.LowerRight:
                    x = Screen.width - width - offset.x;
                    y = Screen.height - height - offset.y;
                    break;
            }

            _rect = new Rect(x, y, width, height);
        }
    }
}