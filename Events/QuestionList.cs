using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * Create a queue of question for showing to user each question
 * in case, question has pop-up in the same time 
 */
public class QuestionList : MonoBehaviour
{
    private Queue<Question> questions = new Queue<Question>();
    private Queue<SimulateParameters> parameters = new Queue<SimulateParameters>();
    public DiseaseQuestion diseaseQuestion;
    public InsectQuestion insectQuestion;

    private Question activeQuestion;
    public QuestionDisplay questionDisplay;
    private float waitTime = 1.0f;
    private Coroutine trainingCoroutine = null;

    public void addQuestion(Question question, SimulateParameters parameters)
    {
        question.isActive = false;
        questions.Enqueue(question);
        this.parameters.Enqueue(parameters);

        Debug.Log(question.topic + " Added");
        Debug.Log("Number of question: " + questions.Count);
        if (trainingCoroutine == null)
        {
            Debug.Log("Start Question Coroutine");
            trainingCoroutine = StartCoroutine(showActiveQuestion());
        }
    }
    public void addQuestion(bool isInsect, string questionName, SimulateParameters parameters)
    {
        if (isInsect)
        {
            Debug.Log("QuestionList: insect triggered");
            activeQuestion = insectQuestion.getQuestion(questionName);
            Events.InsectTrigger(activeQuestion.topic);
        }
        else
        {
            Debug.Log("QuestionList: disease triggered");
            activeQuestion = diseaseQuestion.getQuestion(questionName);
            Events.DiseaseTrigger(activeQuestion.topic);
        }
        addQuestion(activeQuestion, parameters);
    }

    IEnumerator showActiveQuestion()
    {
        WaitForSeconds time = new WaitForSeconds(waitTime);
        while (questions.Count > 0)
        {
            yield return time;
            activeQuestion = questions.Dequeue();
            SimulateParameters parameter = parameters.Dequeue();
            activeQuestion.isActive = true;
            if (activeQuestion.isActive)
            {
                Debug.Log("Question: " + activeQuestion.topic);
                questionDisplay.OpenQuestionWindow(activeQuestion, parameter);
            }
        }
        trainingCoroutine = null;
    }
}
