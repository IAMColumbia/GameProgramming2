using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Observer
{
    public interface IPacmanSubject : ISubject
    {
        List<IPacmanObserver> observers { get; set; }

        void Attach(IPacmanObserver o);

        void Detach(IPacmanObserver o);
    }
}
