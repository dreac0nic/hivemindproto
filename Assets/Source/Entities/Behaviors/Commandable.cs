using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tuple<T1, T2>
{
  public T1 First { get; private set; }
  public T2 Second { get; private set; }

  internal Tuple(T1 first, T2 second) {
    First = first;
    Second = second;
  }
}

public class Commandable : MonoBehaviour
{
  public bool Selected { get { return selected; }
                         set {
                           if(value && !selected) { addSelectMarker(); }
                           else if(selected) { removeSelectMarker(); }

                           selected = value; }}

  public GameObject SelectMarkerPrefab;

  protected GameObject selectMarker;
  protected bool selected;
  protected Queue< Tuple<string, string> > orders;

  void Start() {
    orders = new Queue< Tuple<string, string> >();
  }

  void Update() {
    if(orders.Count > 0) {
      Tuple<string, string> order = orders.Dequeue();
      Debug.Log("I AM DOING " + order.First + " AT " + order.Second);
    }
  }

  public void GiveOrder(string order, string data, bool clearOrders = true) {
    if(clearOrders)
      orders.Clear();

    orders.Enqueue(new Tuple<string, string>(order, data));
  }

  private void addSelectMarker()
  {
    selectMarker = (GameObject)Instantiate(SelectMarkerPrefab, Vector3.zero, Quaternion.identity);
    selectMarker.transform.SetParent(transform, false);

    selectMarker.transform.Rotate(new Vector3(90, 0, 0));
  }

  private void removeSelectMarker() { Destroy(selectMarker); }
}
