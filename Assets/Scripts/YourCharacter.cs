using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "YourCharacter", menuName = "YourCharacter", order = 0)]
public class YourCharacter : UnityEngine.ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private int initCharaNum = default;
    [NonSerialized] public int charaNum;

    public void OnAfterDeserialize()
    {
        // Editor上では再生中に変更したScriptableObject内の値が実行終了時に消えない。
        // そのため、初期値と実行時に使う変数は分けておき、初期化する必要がある。
        charaNum = initCharaNum;
    }

    public void OnBeforeSerialize() { /* do nothing */ }
}

