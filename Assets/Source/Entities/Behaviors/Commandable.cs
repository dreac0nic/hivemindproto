using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuple<T1, T2>
{
	public T1 First { get; private set; }
	public T2 Second { get; private set; }
	internal Tuple(T1 first, T2 second)
	{
		First = first;
		Second = second;
	}
}

public class Commandable : MonoBehaviour
{
    public Queue< Tuple<string, string> > orders;
    
    void Start() {
		orders = new Queue< Tuple<string, string> >();
    }
    
    void Update() {
		Debug.Log("I AM DOING " + orders.Peek().First + " AT " + orders.Peek().Second);
    }

    public void order(string order, string data, bool clearOrders = true) {
		if(clearOrders)
		    orders.Clear();

		orders.Enqueue(new Tuple<string, string>(order, data));
    }
}
