using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnswerData
{
    public string answerText;
    public bool isCorrect;

    public AnswerData(string answerText, bool isCorrect)
    {
        this.answerText = answerText;
        this.isCorrect = isCorrect;
    }
    
}
