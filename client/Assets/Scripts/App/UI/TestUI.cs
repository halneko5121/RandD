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
            parent.transform.DOLocalMoveX(100.0f, 2.0f).SetRelative(true).SetEase(Ease.InOutCirc);
        }

        // Update is called once per frame
        void Update()
        {
        }

        //---------------------------------------------
        // Method
        //---------------------------------------------

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

        private Text hoge = null;
    }
}