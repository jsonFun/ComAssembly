using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComAssembly
{
    class Program
    {
        static void Main(string[] args)
        {
            MyCom pay = new MyCom();
            string web = "https://cdn.pixabay.com/photo/2016/09/29/10/08/halloween-1702531_960_720.jpg";
            var ss = pay.SettingBz(web, "D:\\");
        }
    }
}
