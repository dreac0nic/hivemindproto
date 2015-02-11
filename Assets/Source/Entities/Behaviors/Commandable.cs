using UnityEngine;
using System.Tuple;
using System.Collections;
using System.Collections.Generic;

public class Commandable : MonoBehaviour
{
    public Queue< Tuple<string, string> > orders;
    
    void Start() {
	orders = new Queue< Tuple<string, string> >;
    }
    
    void Update() {
	Debug.log("I AM DOING " + orders.peek().Item1 + " AT " + orders.peek().Item2);
    }

    public void order(string order, string data, bool clearOrders = true) {
	if(clearOrders)
	    orders.Clear();

	orders.Enqueue(new Tuple<string, string>(order, data));
    }
}
