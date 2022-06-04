using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_ManagerTests
{

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

        public void scrapers(string exePath, string outputFolder)
        {
            if (this.isReal)
                this.realImp.scrapers(exePath, outputFolder);
            throw new NotImplementedException();
        }

        public void scraper_manager(string exePath, string inputPath, string DBPath)
        {
            if (this.isReal)
                this.realImp.scraper_manager(exePath, inputPath, DBPath);
            throw new NotImplementedException();
        }

        public void metrics(string exePath, string DBPath)
        {
            if (this.isReal)
                this.realImp.metrics(exePath, DBPath);

            throw new NotImplementedException();
        }

        public void model(string exePath, string DBPath)
        {
            if (this.isReal)
                this.realImp.model(exePath, DBPath);

            throw new NotImplementedException();
        }
    }
}
