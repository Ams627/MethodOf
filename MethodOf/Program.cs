using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;
using FluentAssertions;

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
        public void Print(string blob21)
        {
            throw new ArgumentNullException("hello");
        }
    }

    class X
    {

    }

    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var paramName = typeof(C1).GetMethods().Where(m => m.Name == "Print").SelectMany(x => x.GetParameters().Select(y => y.Name)).First();

                var c1 = new C1();

                Action act = () => c1.Print("hello");
                act.Should().ThrowExactly<ArgumentNullException>().WithMessage($"*Parameter name: {paramName}");
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
