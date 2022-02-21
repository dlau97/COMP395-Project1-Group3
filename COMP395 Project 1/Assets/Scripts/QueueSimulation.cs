using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class QueueSimulation : MonoBehaviour
{
    public float timeMultiplier = 60;
    public Slider timeSlider;
    public Text numOfCustomersText;
    public Text serviceTimeText;
    public Text interarrivalText;
    public Text timeText;


    private float GLOBAL_X = 0f;
    private float time = 0f;
    private string path = "Assets/Resources/GeneratedData.txt";
    private Queue<Customer> customerQueue = new Queue<Customer>();
    private int numOfCustomers;

    // Start is called before the first frame update
    void Start()
    {
        Customer [] customersArray = new Customer[500];
        try{
            TextAsset test = Resources.Load<TextAsset>("GeneratedData");
            string[] lines = test.text.Split('\n');

            ////Repeat for 500 customers - starting at index 1 because line 0 is the heading line
            for (int i = 1; i < lines.Length - 1; i++)
            {

                //Split current line of data in string array
                string[] line = lines[i].Split('\t'); //<---------Error is here!!!!!!!!!!!!!!!!!!!
                //double test = Convert.ToDouble(line[1]);
                //Debug.Log("|" + (test + 1) + "|");


                //For testing
                //Debug.Log(lines[i]);

                // //Extract individual data from each line and assign to respective variables
                int id = int.Parse(line[0]);
                float arrivalTime = float.Parse(line[1]);
                float serviceTime = float.Parse(line[2]);

                //Create new customer object and assign to custome array
                customersArray[i - 1] = new Customer(id, arrivalTime, serviceTime);

                //Print customer object
                Debug.Log(customersArray[i - 1].ToString());



            }

            StartCoroutine(Interval(customersArray, 0));
            StartCoroutine(ServiceCoroutine());

        }
        catch(IOException E){
            Debug.Log(E);
        }


    }

    public void OnSlider_Changed()
    {
        timeMultiplier = timeSlider.value;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Time.timeScale = timeMultiplier/60;
        time += Time.deltaTime;
        timeText.text = "Time (Minutes): " + time.ToString();
    }

    IEnumerator Interval(Customer[] customerArray, int index)
    {
        yield return new WaitForSeconds(customerArray[index].getArrivalTime());
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.position = new Vector3(GLOBAL_X, 0f, 0f);
        obj.name = customerArray[index].getID().ToString();
        obj.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.HSVToRGB((customerArray[index].getServiceTime()/10f) % 1f, 1f, 1f));
        customerQueue.Enqueue(customerArray[index]);
        interarrivalText.text = "Last Arrival Time: " + customerArray[index].getArrivalTime().ToString();
        GLOBAL_X++;
        StartCoroutine(Interval(customerArray, index+1));
    }

    IEnumerator ServiceCoroutine()
    {
        while (true)
        {
            if (customerQueue.Count > 0)
            {
                Customer c = customerQueue.Dequeue();
                yield return new WaitForSeconds(c.getServiceTime());
                Destroy(GameObject.Find(c.getID().ToString()));
                GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
                camera.transform.position = new Vector3(camera.transform.position.x + 1, camera.transform.position.y, camera.transform.position.z);
                numOfCustomers++;
                numOfCustomersText.text = "Customers Served: " + numOfCustomers;
                serviceTimeText.text = "Last Service Time: " + c.getServiceTime().ToString();
            } else
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
