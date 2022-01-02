TikTok SCraper instruction:
input: none
output: will generate a csv and a json of the scraping data in the main folder under the name Tiktok_dd_mm_yyyy.csv
how to execute: run (double click) TheJob.bat in the main folder. it will infintly run untill canceld
notes: the only code we did is in folder Hype7. it includes the code for scraping and a history.txt file as support (do not change it)
plans for the future: scrape all meta data from a lsit of videos



Scrapper Manager instruction:
input: place csv files in input_folder to be formated. do not change history.txt.
output: all formated csv files will be placed in output_folder
how to execute:open the project using visual studio, and run program.cs
notes: in the main folder there is a 'settings.txt' file, which is used to describe how to format a given csv. syntax will be written in the end.
plans for the future: change output to be in DB form. make settings.txt able to contain multiple scrapers. some basic info aggragation. generic hashtag configuration and settings



Scraper Manager settings syntax:

<columnName_in_csv>;<desired_uniform_name>;;<different_column_name_in_csv>;<its_desired_name>;;.....<last_column_name>;<that_desired_name> {newline}
<the scraper social media>

any number of columns can be chosen. any column not in the settings will be ignored. the writing order will decide the order of the formated csv.