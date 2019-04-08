using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class QuestionData
{
    public string category;
    public string type;
    public string difficulty;
    public string questionText;
    public string correctAnswer;
    public string[] incorrectAnswers; 
    public AnswerData[] answers;
}


public class JSONparser
{
    public QuestionData[] readQuestionsFromWeb()
    {
        string_url = "https://opentdb.com/api.php?amount=15";
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("ERROR: " + www.error);
            return null;
        }

        const string jsonString = www.data;
        QuestionData[] questions;

        // From file to json wrapper object
        JsonData jsonvale = JsonMapper.ToObject(jsonString);
        for(int i = 0; i < jsonvale["results"].Count; i++)
        {
            // Json wrapper objects to question objects mapping
            QuestionData question = new QuestionData();
            question.category = jsonvale[i]["category"].toString();
            question.type = jsonvale[i]["type"].toString();
            question.difficulty = jsonvale[i]["difficulty"].toString();
            question.questionText = jsonvale[i]["question"].toString();
            question.correctAnswer = jsonvale[i]["correct_answer"].toString();
            
            for(int j = 0; j < jsonvale[i]["incorrect_answers"].Length; j++)
            {
                question.incorrectAnswers.Add(jsonvale[i][""][j][""].toString());
            }

            int correctIndex = random.Next(4) % 4; 

            for(int k = 0; k < question.incorrectAnswers.Length; k++)
            {
                if(correctIndex == k)
                {
                    question.answers.add(correctAnswer, isCorrect = true); 
                }

                question.answers.add(incorrectAnswers[k], isCorrect = false);

                if(k == question.incorrectAnswers.Count)
                {
                    question.answers.add(correctAnswer, isCorrect = true);
                }
            }

            questions.Add(question);
        }
        return questions;
    }
}
