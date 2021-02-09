using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace MethodOf
{
    public class Methodof<T>
    {
        private MethodInfo method;

        public Methodof(T func)
        {
            Delegate del = (Delegate)(object)func;
            this.method = del.Method;
        }

        public static implicit operator Methodof<T>(T methodof)
        {
            return new Methodof<T>(methodof);
        }

        public static implicit operator MethodInfo(Methodof<T> methodof)
        {
            return methodof.method;
        }
    }

    interface I1
    {
        void Print(string str2);

    }

    class C1 : I1
    {
        public void Print(string str2)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var methodAndParams = typeof(C1).GetMethods().Where(m => m.Name == "Print").Select(x => new { x.Name, Params = x.GetParameters().Select(y => y.Name).ToList() }).First();
                Console.WriteLine($"{methodAndParams.Params[0]}");
            }
            catch (Exception ex)
            {
                var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                var progname = Path.GetFileNameWithoutExtension(fullname);
                Console.Error.WriteLine($"{progname} Error: {ex.Message}");
            }

        }
    }
}
