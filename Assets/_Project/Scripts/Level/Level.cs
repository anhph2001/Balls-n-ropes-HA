using System;
using Pancake;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Level : MonoBehaviour
{
    [ReadOnly] public int bonusMoney;
    
    private bool _isFingerDown;
    private bool _isFingerDrag;
    public Transform SpawnBallPos;
    private Camera _camera;
    public Camera Camera => _camera ??= GetComponentInChildren<Camera>(true);
    public int currentPoint;
    public int pointWhenHit;
    public int pointToWin;
    public TextMeshProUGUI Point;
    #if UNITY_EDITOR
    [Button]
    private void StartLevel()
    {
        Data.CurrentLevel = Utility.GetNumberInAString(gameObject.name);
        EditorApplication.isPlaying = true;
        currentPoint = 0;
    }
    #endif
    private void Update()
    {
        Point.SetText(currentPoint+" / "+pointToWin );
    }

    void OnEnable()
    {
        Lean.Touch.LeanTouch.OnFingerDown += HandleFingerDown;
        Lean.Touch.LeanTouch.OnFingerUp += HandleFingerUp;
        Lean.Touch.LeanTouch.OnFingerUpdate += HandleFingerUpdate;
        
    }

    void OnDisable()
    {
        Lean.Touch.LeanTouch.OnFingerDown -= HandleFingerDown;
        Lean.Touch.LeanTouch.OnFingerUp -= HandleFingerUp;
        Lean.Touch.LeanTouch.OnFingerUpdate -= HandleFingerUpdate;
    }
    
    void HandleFingerDown(Lean.Touch.LeanFinger finger)
    {
        if (!finger.IsOverGui)
        {
            _isFingerDown = true;
            
            //Get Object raycast hit
            var ray = finger.GetRay(Camera);
            var hit = default(RaycastHit);
        
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity)) { //ADDED LAYER SELECTION
                Debug.Log(hit.collider.gameObject);
            }
        }
    }
    
    void HandleFingerUp(Lean.Touch.LeanFinger finger)
    {
        _isFingerDown = false;
    }
    
    void HandleFingerUpdate(Lean.Touch.LeanFinger finger)
    {
        if (_isFingerDown)
        {
            _isFingerDrag = true;
        }
    }
    
    private void Start()
    {
        Observer.WinLevel += OnWin;
        Observer.LoseLevel += OnLose;
    }

    private void OnDestroy()
    {
        Observer.WinLevel -= OnWin;
        Observer.LoseLevel -= OnLose;
    }

    public void OnWin(Level level)
    {
        
    }

    public void OnLose(Level level)
    {
         
    }
}
