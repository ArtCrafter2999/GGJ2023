using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

public interface INode
{
    NodeSequence NodeSequence { get; set; }


    void Init();
    bool Invoke();
    void Break();
    void Stop();
    void Pause();
    bool Skip();
}

