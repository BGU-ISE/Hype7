import { getVideoMeta,  makeVerifyFp,  PostCollector, ScrapeType, TikTokConstructor, TikTokScraper } from '../src';
import * as fs from 'fs';
import { fromCallback } from 'bluebird';
import CONST from '../src/constant';
import { performance } from 'perf_hooks';

function print(ob) {
    console.log(ob);
}

async function readall(index:number, lst:string[]) {
    let ans:PostCollector[] = [];
    if(index == lst.length)
        return ans;

    if(index%10 == 0)
        print("index " +index);
    try{
        var ans0 = await (getVideoMeta(lst[index], {}));
        ans = ans.concat(ans0.collector);
    }
    catch (error)
    {
        print(error);
        print("continueing");
    }
    try{
        var ans1 = await (getVideoMeta(lst[index+1], {}));
        ans = ans.concat(ans1.collector);
    }
    catch (error)
    {
        print(error);
        print("continueing");
    }
    try{
        var ans2 = await (getVideoMeta(lst[index+2], {}));
        ans = ans.concat(ans2.collector);
    }
    catch (error)
    {
        print(error);
        print("continueing");
    }
    try{
        var ans3 = await (getVideoMeta(lst[index+3], {}));
        ans = ans.concat(ans3.collector);
    }
    catch (error)
    {
        print(error);
        print("continueing");
    }
    try{
        var ans4 = await (getVideoMeta(lst[index+4], {}));
        ans = ans.concat(ans4.collector);
    }
    catch (error)
    {
        print(error);
        print("continueing");
    }
    
    return ans.concat(await readall(index+5, lst));
    
}

var startTime = performance.now();
const file = fs.readFileSync('./hype7/s.txt', 'utf-8');
let lst : PostCollector[] = [];
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
print(file.split('\n').length);

if (true){

(async ()=>{
lst = await readall(0, file.split('\n'));
print(lst.length);
await fromCallback(cb => fs.writeFile('tester.csv', scraper.get_parser().parse(lst), cb));
print("done!!");
var ned = performance.now();
console.log(ned-startTime);
})();
}