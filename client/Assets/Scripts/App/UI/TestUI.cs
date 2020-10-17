using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace RandB
{
    public class TestUI : MonoBehaviour
    {
        // Start is called before the first frame update
        //---------------------------------------------
        // MonoBehaviour
        //---------------------------------------------
        void Start()
        {
            // ゆるやかな拡大：
            // InOutCirc、InOutQuint
            // OutQuint、OutQuart
            // InOutCubic、OutCubic
            // OutQuad
            var parent = animationText.gameObject.transform.parent.gameObject;
            Debug.Log("--------------------");
            Debug.Log(parent);
            parent.transform.DOLocalMoveX(500.0f, 3.0f).SetRelative(true).SetEase(Ease.InOutCirc);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SlowDown();
            }

            //　スローダウンフラグがtrueの時は時間計測
            if (isSlowDown)
            {
                elapsedTime += Time.unscaledDeltaTime;
                if (elapsedTime >= slowTime)
                {
                    SetNormalTime();
                }
            }
        }

        //---------------------------------------------
        // Method
        //---------------------------------------------

        //　時間を遅らせる処理
        public void SlowDown()
        {
            elapsedTime = 0f;
            Time.timeScale = timeScale;
            isSlowDown = true;
        }
        //　時間を元に戻す処理
        public void SetNormalTime()
        {
            Time.timeScale = 1f;
            isSlowDown = false;
        }

        //---------------------------------------------
        // Const/Enum
        //---------------------------------------------

        //---------------------------------------------
        // Property
        //---------------------------------------------

        //---------------------------------------------
        // Field
        //---------------------------------------------
        [SerializeField]
        private TMP_Text animationText = null;

        //　Time.timeScaleに設定する値
        private float timeScale = 0.1f;
        //　時間を遅くしている時間
        private float slowTime = 1f;
        //　経過時間
        private float elapsedTime = 0f;
        //　時間を遅くしているかどうか
        private bool isSlowDown = false;
    }
}