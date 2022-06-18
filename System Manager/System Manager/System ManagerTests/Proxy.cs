using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_ManagerTests
{
    /// <summary>
    /// depracated
    /// </summary>
    class Proxy : iProxy
    {

        
        public iProxy realImp { get; set; } = null;
        public bool isReal { get { return realImp != null; } private set { } }
        public Proxy()
        {

        }

        public Proxy( iProxy realImplementation)
        {
            this.realImp = realImplementation;
        }

        public void scraper(string directory)
        {
            if (this.isReal)
                this.realImp.scraper(directory);
            else
                throw new NotImplementedException();
        }

        public void scraper_manager(string directory)
        {
            if (this.isReal)
                this.realImp.scraper_manager(directory);
            else
                throw new NotImplementedException();
        }

        public void metrics(string directory)
        {
            if (this.isReal)
                this.realImp.metrics(directory);
            else
                throw new NotImplementedException();
        }

        public void model(string directory)
        {
            if (this.isReal)
                this.realImp.model(directory);

            else
                throw new NotImplementedException();
        }

        public Process runGui(string[] directory)
        {
            if (this.isReal)
                return this.realImp.runGui(directory);

            else
                throw new NotImplementedException();
        }
    }
}
