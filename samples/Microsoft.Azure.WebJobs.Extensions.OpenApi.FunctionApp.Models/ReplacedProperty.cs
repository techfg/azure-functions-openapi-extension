using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.WebJobs.Extensions.OpenApi.FunctionApp.Models
{
    public class MyData
    {
        public string Prop1 { get; set; }
    }
    public class MyBase
    {
        public object Data { get; set; }
    }

    public class MyDerived : MyBase
    {
        new public MyData Data { get; set; }
    }

    public class MyDerived<T> : MyBase
    {
        new public T Data { get; set; }
    }

}
