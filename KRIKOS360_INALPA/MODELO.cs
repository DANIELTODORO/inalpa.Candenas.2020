using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRIKOS360_INALPA
{
    public partial class SBDABEJERMAN
    {
        private static SBDABEJERMAN instancia;
        public static SBDABEJERMAN obtenerinstancia()
        {
            if (instancia == null)
                instancia = new SBDABEJERMAN();

            return instancia;
        }
    }
}
