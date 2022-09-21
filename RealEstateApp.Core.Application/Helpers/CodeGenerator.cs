using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Helpers
{
    public static class CodeGenerator
    {
        public static int Generate()
        {
            Random x = new();
            int num = x.Next(0,999999);
            return num;
        }
    }
}
