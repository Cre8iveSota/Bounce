using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataTransfer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private YourCharacter yourCharacter = default;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void UpdateChoseCharacterNum(int num)
    {
        Utilities.altCharaNum = num;
        Debug.Log("called: " + num);
        yourCharacter.charaNum = num;
    }
}
