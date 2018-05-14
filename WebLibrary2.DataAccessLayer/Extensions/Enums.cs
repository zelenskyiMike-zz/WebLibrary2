using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Extensions
{
    public static class Enums   
    {
        [Flags]
        public enum ErrorList
        {
            [Description("Wrong filef for this publications type. Please, choose another file")]
            WrongFileFormat = 0
        }
    }
}
