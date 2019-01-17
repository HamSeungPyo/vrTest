using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InstructionDataBase
{
    public string name;
    public Sprite type;
    public Sprite composition;
    public Sprite howToUse;
    string storageMethod = "잘 보이고 사용하기 편리한 위치에 둔다\n습기가 적고 서늘한 곳에 둔다\n소화기 위에 어떤 물건도 놓지 않는다";
    public string StorageMethod { get { return storageMethod; } }
    [TextArea]
    public string howToManaged;
    [TextArea]
    public string explanation;
    [TextArea]
    public string advantage;
    [TextArea]
    public string weaknesses;
}
public class UI_InstructionControl : MonoBehaviour
{
    public Text[] instructionText = new Text[3];
    public Image explanationSprite;
    public List<InstructionDataBase> dataBase;
    string[] classification = { "종류", "구성", "사용법", "보관방법", "관리방법", "설명", "장점", "단점" };
    int[] number = new int[2];
    private void Awake()
    {
        instructionText[0].text = dataBase[0].name;
        instructionText[1].text = classification[0];
        instructionText[2].gameObject.SetActive(false);
        explanationSprite.gameObject.SetActive(true);
        explanationSprite.sprite = dataBase[0].type;
    }

    void InstructionControl(int[] num)
    {
        Debug.Log(num[0] + "//" + num[1]);
        instructionText[0].text = dataBase[num[1]].name;
        instructionText[1].text = classification[num[0]];

        bool bSwitching = false;
        switch (num[0])
        {
            case 0:
                explanationSprite.sprite = dataBase[num[1]].type;
                break;
            case 1:
                explanationSprite.sprite = dataBase[num[1]].composition;
                break;
            case 2:
                explanationSprite.sprite = dataBase[num[1]].howToUse;
                break;
            case 3:
                bSwitching = true;
                instructionText[2].text = dataBase[num[1]].StorageMethod;
                break;
            case 4:
                bSwitching = true;
                instructionText[2].text = dataBase[num[1]].howToManaged;
                break;
            case 5:
                bSwitching = true;
                instructionText[2].text = dataBase[num[1]].explanation;
                break;
            case 6:
                bSwitching = true;
                instructionText[2].text = dataBase[num[1]].advantage;
                break;
            case 7:
                bSwitching = true;
                instructionText[2].text = dataBase[num[1]].weaknesses;
                break;
            default:
                break;
        }
        instructionText[2].gameObject.SetActive(bSwitching);
        explanationSprite.gameObject.SetActive(!bSwitching);
    }

    public void SetClassification(int num)
    {
        if (0 <= number[0] + num && number[0] + num < classification.Length)
        {
            number[0] += num;
            InstructionControl(number);
        }
    }
    public void SetType(int num)
    {
        if (0 <= number[1]+num && number[1]+ num < dataBase.Count)
        {
            number[1] += num;
            InstructionControl(number);
        }
    }
}

