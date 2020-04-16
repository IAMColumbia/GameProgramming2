using UnityEngine;
using System.Collections;

public interface ISubject {
    void Attach(IObserver o);

    void Detach(IObserver o);

    void Notify();
}
