This Winforms app serves the sole purpose of keeping track of biggest-hits games I have (and have not) played over the years.

1. Starting the app will load the first 50 games from a SQL database sorted by Metacritic Rating
2. Each of the cells in the table contains the box art, name, platform and rating of a corresponding game.
3. If you have owned/played a game, you simply need to click on a cell and it will become Owned. The cell will also turn Green. The opposite is also true and clicking an already Owned game will turn it to Not Owned.
4. This is all saved on the database for some continuity once you close the app.
5. Scrolling down to the bottom of the app will load the next 50 games. There are 20k games in the database.

I used webscraping to get all of the information from the Metacritic site and save it all to a CSV file. The images for each game were downloaded in a separate folder. This was all done via a different app I created, which I can share if need be.

I use CSVHelper for the CSV file manipulation/reading and Dapper for all the SQL queries. You can view that in the two classes DatabaseHandler and CSVHandler. The CSV file is also included in the Files folder. All of the images are in the bin/Debug folder.
