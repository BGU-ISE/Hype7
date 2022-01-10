import * as fs from 'fs';

const z = fs.writeFile('fffile.txt', 'hello world', function(err) {
    if (err) {
        return console.error(err);
    }
    console.log("File created!");
});

//const file = fs.readFileSync('./sett.txt', 'utf-8');
//console.log(file);
console.log(z);