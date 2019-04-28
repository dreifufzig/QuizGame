using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.IO;

[System.Serializable]
public class QuestionData
{
    public string category;
    public string type;
    public string difficulty;
    public string questionText;
    public string correctAnswer;
    public string[] incorrectAnswers; 
    public AnswerData[] answers;

    public QuestionData ()
    {

    }

    public QuestionData (JsonData jsonvale, int i)
    {
        // Json wrapper objects to question objects mapping
        this.category = jsonvale["results"][i]["category"].ToString();
        this.type = jsonvale["results"][i]["type"].ToString();
        this.difficulty = jsonvale["results"][i]["difficulty"].ToString();
        this.questionText = jsonvale["results"][i]["question"].ToString();
        this.correctAnswer = jsonvale["results"][i]["correct_answer"].ToString();
        this.incorrectAnswers = new string[jsonvale["results"][i]["incorrect_answers"].Count + 1];


        for(int j = 0; j < jsonvale["results"][i]["incorrect_answers"].Count; j++)
        {
            this.incorrectAnswers[j] = jsonvale["results"][i]["incorrect_answers"][j].ToString();
            Debug.Log(this.incorrectAnswers[j]);
        }

        System.Random random = new System.Random();

        int correctIndex = random.Next(4) % 4; 
        this.answers = new AnswerData[this.incorrectAnswers.Length];
        int answerDataIterator = 0;
    

        for(int k = 0; k < this.incorrectAnswers.Length; k++)
        {
            if(correctIndex == k)
            {
                //Debug.Log(this.correctAnswer);
                //Debug.Log("Korrekte Antwort");
                AnswerData answerData = new AnswerData(this.correctAnswer, true);
                this.answers[k] = answerData;
            }
            else
            {
                //Debug.Log(this.incorrectAnswers[answerDataIterator]);
                //Debug.Log("Inkorrekte Antwort");
                AnswerData answerData = new AnswerData(this.incorrectAnswers[answerDataIterator], false);
                this.answers[k] = answerData;
                answerDataIterator++;
            }
        }
    }
 
    public static QuestionData[] readQuestionsFromWeb()
    {
        int amount = 15;
        string string_url = "https://opentdb.com/api.php?amount="+ amount +"&type=multiple";
        WWW www = new WWW(string_url);
        if (www.error != null)
        {
            Debug.Log("ERROR: " + www.error);
        }

        while(!(www.isDone))
        {

        }
        if(www.isDone)
        {
            string jsonString = www.text;
            QuestionData[] questions = new QuestionData[amount];

            // From file to json wrapper object
            JsonData jsonvale = JsonMapper.ToObject(jsonString);
            for(int i = 0; i < jsonvale["results"].Count; i++)
            {
                Debug.Log("Frage");
                
                questions[i] = new QuestionData(jsonvale, i);
            }
            
            return questions;
        }
        return null;
    }
}

