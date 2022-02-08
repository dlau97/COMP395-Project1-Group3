using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer
{
    private int ID;
    private float arrivalTime = 0f;
    private float serviceTime = 0f;

    public Customer(int id, float aTime, float sTime){
        ID = id;
        arrivalTime = aTime;
        serviceTime = sTime;
    }

    public float getID(){
        return ID;
    }
    public float getArrivalTime(){
        return arrivalTime;
    }

    public float getServiceTime(){
        return serviceTime;
    }

    public override string ToString(){
        string output = "Customer ID: "+ID+"\tInterarrival Time (min): "+arrivalTime+"\tService Time: "+serviceTime;

        return output;
    }
}
