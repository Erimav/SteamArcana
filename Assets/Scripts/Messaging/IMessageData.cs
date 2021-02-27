using MLAPI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IMessageData : IBitWritable
{
    MessageCode MessageCode { get; }
}

