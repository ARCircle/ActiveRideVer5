using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//from http://ftvoid.com/blog/post/660
public class Pauser : MonoBehaviour {
    static List<Pauser> targets = new List<Pauser>();   // ポーズ対象のスクリプト
    Behaviour[] pauseBehavs = null; // ポーズ対象のコンポーネント

    // 初期化
    void Start()
    {
        // ポーズ対象に追加する
        targets.Add(this);
    }

    // 破棄されるとき
    void OnDestory()
    {
        // ポーズ対象から除外する
        targets.Remove(this);
    }

   
}
