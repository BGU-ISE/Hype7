import sqlite3
import pandas as pd # data processing, CSV file I/O (e.g. pd.read_csv)

class DBConnection():
    """description of class"""
    def __init__(self, db):
        self.db_file = db
        self.connection = None

    

    def create_connection(self):
        """ create a database connection to the SQLite database
            specified by the db_file
        :param db_file: database file
        :return: Connection object or None
        """
        try:
            self.connection = sqlite3.connect(self.db_file)
        except Error as e:
            print(e)


    def select_all_tasks(self):
        """
        Query all rows in the tasks table
        :param conn: the Connection object
        :return:
        """
        cur = self.connection.cursor()
        cur.execute("SELECT * FROM VideosInfoDay1")

        rows = cur.fetchall()

        for row in rows:
            print(row)
    
    def get_numeric_dataframe(self, num):
        #important to save the following fields: 'authorMeta.id' 'musicMeta.musicOriginal'
        tableName = 'VideosInfoDay' + str(num)
        query = 'SELECT id, text, createTime, authorMeta_verified, authorMeta_following, authorMeta_fans, authorMeta_heart, authorMeta_video, authorMeta_digg, musicMeta_musicId,  diggCount, shareCount, playCount, commentCount FROM ' + tableName
        dataframe = pd.read_sql(query, self.connection)
        return dataframe

    def get_videosInfoDay_dataframe(self):
        dataframe = pd.read_sql('SELECT id, text, createTime, authorMeta_name, authorMeta_nickName, authorMeta_verified, authorMeta_following, authorMeta_fans, authorMeta_heart, authorMeta_video, authorMeta_digg, musicMeta_musicId, musicMeta_musicName, musicMeta_duration, videoMeta_height, videoMeta_width, videoMeta_duration, diggCount, shareCount, playCount, commentCount, hashtags, PullDate, FROM VideosInfoDay1', self.connection)
        return dataframe