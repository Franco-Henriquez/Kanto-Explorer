using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
        Animator oaksAnimation;
        NavMeshAgent navMeshAgent;
        NavMeshPath path;
        public float timeForNewPath;
        bool inCoRoutine;
        Vector3 target;
        bool validPath;


        // Use this for initialization
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            path = new NavMeshPath();
            oaksAnimation = gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!inCoRoutine)
                StartCoroutine(DoSomething());
        }
        Vector3 getNewRandomPosition()
        // setting these ranges is vital larger seems better 
        {


        //choose whether to range on x or z axis but never on both at a time
            int axisRandomized = Random.Range(1, 5);
            if (axisRandomized % 2 == 0) {
            float x = Random.Range(-300, 300);
            //   float y = Random.Range(-20, 20);
            //float z = Random.Range(-300, 300);
            Vector3 pos = new Vector3(x, 0, 0);
                if (x > 0)
                {
                Debug.Log("Oak RIGHT");

                oaksAnimation.SetTrigger("ProfOakRight");
                return pos;

                }
                else
                {
                Debug.Log("Oak LEFT");
                oaksAnimation.SetTrigger("ProfOakLeft");
                return pos;
                }

            }else{
                //float x = Random.Range(-300, 300);
                //   float y = Random.Range(-20, 20);
            float z = Random.Range(-300, 300);
            Vector3 pos = new Vector3(0, 0, z);
            //Vector3 pos = new Vector3(x, 0, 0);
                if (z > 0)
                {
                Debug.Log("Oak BACK");
                oaksAnimation.SetTrigger("ProfOakBack");
                return pos;
                }
                else
                {
                Debug.Log("Oak FACE");
                oaksAnimation.SetTrigger("ProfOakFace");
                return pos;
                }
            
            }
        }
        IEnumerator DoSomething()
        {
            inCoRoutine = true;
            yield return new WaitForSeconds(timeForNewPath);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
            if (!validPath) Debug.Log("found invalid path");
            //while (!validPath)
            {
                yield return new WaitForSeconds(0.01f);
                GetNewPath();
                validPath = navMeshAgent.CalculatePath(target, path);
            }

            inCoRoutine = false;
        }
        void GetNewPath()
        {
            oaksAnimation.ResetTrigger("ProfOakFace");
            oaksAnimation.ResetTrigger("ProfOakBack");
            oaksAnimation.ResetTrigger("ProfOakLeft");
            oaksAnimation.ResetTrigger("ProfOakRight");
            target = getNewRandomPosition();
            navMeshAgent.SetDestination(target);
        }
    }

