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
                var m = typeof(C1).GetMethods().Select(x => new { x.Name, Params = x.GetParameters().Select(y => y.Name).ToList() }).ToDictionary(y=>y.Name, y=>y.Params);
                foreach (var m0 in m)
                {
                    if (m0.Value.Any())
                    {
                        Console.WriteLine($"{m0.Key} {m0.Value.First()}");
                    }
                }
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
