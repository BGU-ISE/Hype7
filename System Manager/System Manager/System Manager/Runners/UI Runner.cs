using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Manager
{
    class UI_Runner : Runner
    {
        Process process;
        public override void kill()
        {
            if (process == null)
                return;
            if (process.HasExited)
                return;
            this.process.Kill();
        }
        public override bool run()
        {
            throw new NotImplementedException();
        }


    }
}
