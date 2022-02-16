using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class QueueSimulation : MonoBehaviour
{
    public string path = "Assets/Import/GeneratedData.txt";
    // Start is called before the first frame update
    void Start()
    {
        string [] lines;
        Customer [] customersArray = new Customer[500];
        try{
            //Read All lines of text file
            lines = System.IO.File.ReadAllLines(path);

            //Repeat for 500 customers - starting at index 1 because line 0 is the heading line
            for(int i = 1; i<lines.Length; i++){

                //Split current line of data in string array
                string [] line = lines[i].Split('\t'); //<---------Error is here!!!!!!!!!!!!!!!!!!!
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


        }
        catch(IOException E){
            Debug.Log(E);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
