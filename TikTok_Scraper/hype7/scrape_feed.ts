import { trend } from '../src';
import { performance } from 'perf_hooks';
import * as fs from 'fs';

function delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
}
function runner() {
    (async () => { 
        var startTime = performance.now();
        console.log('waiting for a new day to scrape from tiktok!');
        var current_date = new Date().toLocaleDateString();
        var out_of_loop = false;
        while(!out_of_loop)
        {
            const file = fs.readFileSync('./hype7/history.txt', 'utf-8');
            var current_date = new Date().toLocaleDateString();
            if(file != current_date )
            {
                out_of_loop = true;
                
                fs.writeFile('./hype7/history.txt', current_date, function(err) {
                    if (err) {
                        return console.error(err);
                    }
                    console.log("its a new day! time to scrape");
                });
            }
            else
            {
                await delay(3600000);
            }

            
        }

        //console.log('time to scrape');
        current_date = current_date.replace("/", "_").replace("/", "_");
        if(true){
        try {
            const posts = await trend('', { number:10 , sessionList: ['sid_tt=21bcb2d3d99223e5aa54c031c9f601dd;'], fileName:"tiktok_"+current_date, filetype:"all"});
            var counter = 0;
        posts.collector.forEach(element => {
            counter++;
        });
        console.log("done scraping! we scraped: "+counter+" videos");
        console.log(counter);
        } catch (error) {
            console.log("scraping failed. error log:")
            console.log(error);
           // console.log("done scraping! we scraped: 18965 videos");
        }
        var endTime = performance.now();

    console.log(`scarping took ${endTime - startTime} milliseconds`);
        }
    return;
    //return runner();
    })();
}

runner();