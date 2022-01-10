import { getVideoMeta,  makeVerifyFp,  PostCollector,  Result, ScrapeType, TikTokConstructor, TikTokScraper } from '../src';
import * as fs from 'fs';
import { fromCallback } from 'bluebird';
import CONST from '../src/constant';


function print(ob) {
    console.log(ob);
}

async function readall(index:number, lst:string[]) {
    let ans:PostCollector[] = [];
    if(index == lst.length)
        return ans;
    try{
        var ans2 = await (getVideoMeta(lst[index], {}));
        ans = ans2.collector;
    }
    catch (error)
    {
        print(error);
        print("continueing");
    }
    return ans.concat(readall(index+1, lst));
    
}


const file = fs.readFileSync('./hype7/s.txt', 'utf-8');
let lst : PostCollector[] = [];
let lst2 : Promise<Result>[] = [];
//var videoumeta_collector=   await getVideoMeta(file.split('\n')[0], {});
//var isfirst = true;
//let counter:number = 0;





lst = readall(0, file.split('\n'));
console.log(lst2.length);

var z = (async (index) => {
            print("index = "+index);
            let ans:PostCollector[] = [];
            if(index == lst2.length)
            {
               // let ans:PostCollector[] = [];
                return ans;
            }
            //if(index%1000 == 0)
            print('1: 1');
            let zup:Result = {headers:{"user-agent":'z'},collector:[]};
            print('1: 2');
            try {
                print('in try');
                zup = (await lst2[index]);
                print('out try');
            }
            catch {
                print("should continute");
                //print(error);
                print("should continute");
               
            }
            print('1: 3');
            ans = zup.collector;
            print('1: 4');
            return ans.concat(await z(index+1));
            /*
            catch(error) {
                print(error);
                print("should continute");
               
            }
            return await z(index+1);*/
            //return ans.concat(await z(index+1));
});
/*
lst2.forEach(element => {
    element.then(data => {print("doing"); lst = lst.concat(data.collector);});
    print(lst.length);
});
/*
(async() =>
{
    await Promise.all(lst2);
    print("done??");
}

)();

/*
lst2.forEach(element => {
    (async (element, lst) =>{
    lst = lst.concat(await z(element));
    print(lst.length);
    })( element, lst);
});
*/
print('vroom');

print(lst.length);

/*
(async () => {
var list:PostCollector[] = await z(0);
print(list);
})();*/
if (false) {
const getInitOptions = () => {
    return {
        number: 30,
        since: 0,
        download: false,
        zip: false,
        asyncDownload: 5,
        asyncScraping: 3,
        proxy: '',
        filepath: '',
        filetype: 'na',
        progress: false,
        event: false,
        by_user_id: false,
        noWaterMark: false,
        hdVideo: false,
        timeout: 0,
        tac: '',
        signature: '',
        verifyFp: makeVerifyFp(),
        headers: {
            'user-agent': CONST.userAgent(),
            referer: 'https://www.tiktok.com/',
        },
    };
};

const constructor: TikTokConstructor = { ...getInitOptions(), ...{}, ...{ type: 'video_meta' as ScrapeType,input: 'tester.csv' } };
    const scraper = new TikTokScraper(constructor);



(async () => {
    try {
    var list:PostCollector[] = await z(0);
    await fromCallback(cb => fs.writeFile('tester.csv', scraper.get_parser().parse(list), cb));
    print("done!!");
    }
    catch(error){
        console.log("whyyyy");
    }
})();
}

/*
while (counter!= 0)
    continue;
console.log(lst);
*/



/*
(async () => {
    try {
        const videoMeta = await getVideoMeta('https://www.tiktok.com/@tiktok/video/6800111723257941253', {});
        videoMeta.collector.    
        console.log(videoMeta);
    } catch (error) {
        console.log(error);
    }
})();*/