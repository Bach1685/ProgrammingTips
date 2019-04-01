using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceExample
{
    public class WEBService : IWEBService
    {
        public Response SumPost(Request req)
        {
            Response res = new Response();

            res.Sum = req.X + req.Y;

            return res;
        }

        public Response SumGet(int x, int y)
        {
            Response res = new Response();

            res.Sum = x + y;

            return res;
        }
    }

    [DataContract]
    public class Response
    {
        [DataMember(Name = "SUM")]
        public int Sum { get; set; }
    }

    [DataContract]
    public class Request
    {
        [DataMember(Name = "X")]
        public int X { get; set; }

        [DataMember(Name = "Y")]
        public int Y { get; set; }
    }
}
