﻿using System.Collections;
using UnityEngine;

public class BackgroundHandler : MonoBehaviour
{

    #region 이벤트

    public delegate void ScrollEvent();

    public event ScrollEvent OnScrollStartEvent;
    public event ScrollEvent OnScrollCompletedEvent;

    #endregion

    #region 메테리얼

    private Material mat;

    #endregion

    #region 수치

    public float scrollSpeed = 5.0f; // 배경이 스크롤 되는 속도
    private float offset; // 이동하는 파워
    private float time; // 이동하고 있는 시간

    [SerializeField] private float scrollTime; // 이동할 수 있는 시간

    #endregion

    #region 상태

    public bool isScrolling; // 배경이 스크롤 되고 있는지를 확인하는 상태
    private bool isScrollStarted;

    #endregion

        
    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
        
    public void OnScrolling()
    {
        time = 0;
        isScrolling = true;
        isScrollStarted = false;
        offset = mat.GetTextureOffset("_MainTex").x; // Initial offset will be current texture offset
    }

    private void Update()
    {
        if (isScrolling)
        {
            time += Time.deltaTime;

            if (time < 0.5f)
            {
                return;
            }

            if (isScrollStarted == false)
            {
                OnScrollStartEvent?.Invoke();
                isScrollStarted = true;
            }
            
            offset += (Time.deltaTime * scrollSpeed) / 10f;
            mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
                
            if (time > scrollTime)
            {
                OnScrollCompletedEvent?.Invoke();
                isScrolling = false;
                isScrollStarted = false;
            }
        }
    }
}